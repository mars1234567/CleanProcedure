using CA_sign;
using Decontamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CleanProcedure
{


    public class Task
    {
        public delegate void UpdateUI(TaskInfo step);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask(TaskInfo step);//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        //内镜当前步骤 key 为[CardNo] ，对应当前的状态
        Dictionary<string, RunningState> CardList = new Dictionary<string, RunningState>();
        //洗消步骤 KEY 为ClientIP,该步骤的设置
        public Dictionary<string, StepNode> IPlist = new Dictionary<string, StepNode>();
        //互斥器
        public static Mutex mutex = new Mutex();

        //刷卡器ip ，工作人员
        Dictionary<string, PendingState> PendingList = new Dictionary<string, PendingState>();

        //public delegate void UpdateTask(TaskInfo info);

        //public UpdateTask m_updateTaskProc = null;
       //工作人员列表
        List<string> Worker = new List<string>();
          public Task()
        {
            
        }


          //获取分组和每个分组的步骤
          public void GetStepInfo(ref  Dictionary<string, List<StepInfo>> infolist)
          {
              using (CleanProcedureEntities db = new CleanProcedureEntities())
              {
                  //刷卡器
                  var list = db.Clean_CardDevice.Select(p => p.CleanGroup).Distinct();
                  foreach (var item in list)
                  {
                      var machinlist = db.Clean_CardDevice.Where(p => p.CleanGroup == item && p.Stopped == false && p.DevType.Equals("消毒监控")).OrderBy(p => p.StepNumber);
                      if (machinlist.Count() == 0)
                          continue;
                      List<StepInfo> iList = new List<StepInfo>();
                      foreach (var i in machinlist)
                      {
                          StepInfo info = new StepInfo();
                          info.name = i.StepName;
                          iList.Add(info);
                      }
                      infolist.Add(item, iList);
                  }
              }
          }

        public void InitProcedure()
        {
            try
            {
                using (CleanProcedureEntities db = new CleanProcedureEntities())
                {
                    //刷卡器
                    var list = db.Clean_CardDevice.Select(p => p.CleanGroup).Distinct();
                    foreach (var item in list)
                    {
                        var machinlist = db.Clean_CardDevice.Where(p => p.CleanGroup == item && p.Stopped == false).OrderBy(p => p.StepNumber);
                        StepNode sn = null;
                        StepNode snLast = null;
                        foreach (var m in machinlist)
                        {
                            snLast = sn;
                            sn = new StepNode(m.ClientIP, m);
                            sn.SetLast(snLast);
                            IPlist.Add(m.ClientIP, sn);
                        }
                    }
                    //人员卡
                    var wlist = from a in db.Clean_Card where a.DevType == "人员" select a.CardNo;
                    Worker.AddRange(wlist.AsEnumerable());

                    //内镜卡
                    var nlist = from c in db.Clean_Card where c.DevType == "镜子" select c;
                    foreach (var item in nlist)
                    {
                        CardList.Add(item.CardNo, null);
                    }

                    //如果隔夜消毒的内镜不作废，查询昨天消毒未用的内镜卡
                    if (!Config_func.GetInstance().LastNight)
                    {
                        var reclist = db.Clean_RecordList.GroupBy(p => p.CleanCard);

                        foreach (var item in reclist)
                        {
                            string cardname = item.Key;
                            Clean_RecordList it = item.OrderBy(p => p.StartTime).LastOrDefault();
                            //2天内的消毒记录
                            if (it.StartTime.Date.Equals(DateTime.Today.AddDays(-1)) || it.StartTime.Date.Equals(DateTime.Today))
                            {
                                if (it.StepNum < it.MaxNum)//消毒未完成记录
                                {
                                    RunningState mState = new RunningState();
                                    mState.ClientIP = it.CleanIp;
                                    mState.CardNo = cardname;
                                    CardList[cardname] = mState;

                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }
        //工作人员卡
        public bool IsWorkCard(string card)
        {
            return Worker.Contains(card);
        }
        //内镜卡
        public bool IsMCard(string card)
        {
            return CardList.ContainsKey(card);
        }
        public void Pending(string ClientIP, string card)
        {
            if (PendingList.ContainsKey(ClientIP))
            {
                PendingList.Add(ClientIP, new PendingState(card));
            }
            else
            {
                //工作人员卡是新的覆盖旧的
                PendingList[ClientIP] = new PendingState(card);
            }
        }

        public void Running(string ClientIP, string card)
        {
            bool bConvert = false;
            mutex.WaitOne();

            if (CardList[card] == null)
            {
                RunningState mState = new RunningState();
                mState.ClientIP = ClientIP;
                mState.CardNo = card;
                mState.WorkCard = PendingList[ClientIP].WorkCard;
                CardList[card] = mState;
                bConvert = true;
            }

            mutex.ReleaseMutex();

            //获取当前刷卡器的步骤信息
            StepNode curr_node = IPlist[ClientIP];
            if (bConvert)
            {
                TaskInfo info = new TaskInfo(ClientIP, card, PendingList[ClientIP].WorkCard, curr_node.GetDur(), 1, curr_node.GetCleanGroup());
                UpdateUIDelegate(info);
            }
        }
        public string  Step(string ClientIP, string card)
        {
            string sRet= "";
            DateTime currenttime = DateTime.Now;

            //获取当前刷卡器的步骤信息
            StepNode curr_node = IPlist[ClientIP];

            StepNode last_node = curr_node.GetLast();
            //不能机洗跳手工
            if (last_node.IsMachineWash()&&!curr_node.IsMachineWash())
            {
                sRet = "不能机洗跳手工";
                return sRet;
            }
            bool bConvert = false;
            mutex.WaitOne();
            //获取内镜卡上次的刷卡信息
            RunningState mState = CardList[card];
            //当前步骤的上一个刷卡器地址是否相同，即洗消顺序相同
            if (last_node.StepIp().Equals(mState.ClientIP))
            {
                if (mState.IsFinish(last_node.GetDur()))
                {
                    mState.Convert(ClientIP);
                    bConvert = true;
                }
            }
            mutex.ReleaseMutex();

            if(bConvert)
            {
                TaskInfo info = new TaskInfo(ClientIP, card,mState.WorkCard, curr_node.GetDur(), curr_node.GetStepNum(), curr_node.GetCleanGroup());
                UpdateUIDelegate(info);
            }
            return sRet;
        }


        public string Handle(string ClientIP, string card)
        {
            //

            //获取当前刷卡器配置
            StepNode current_dev = IPlist[ClientIP];
            StepNode current_last = current_dev.GetLast();

            //刷卡器为第一个步骤
            if (current_last == null)
            {
                //工作人员刷卡
                if (IsWorkCard(card))
                {
                    Pending(ClientIP, card);
                }

                //内镜刷卡
                if (IsMCard(card))
                {
                    //工作人员卡已刷
                    if (PendingList.ContainsKey(ClientIP))
                    {
                        string workDev = PendingList[ClientIP].WorkCard;
                       
                        Running(ClientIP, card);
                        PendingList.Remove(ClientIP);
                    }
                    else
                    {

                    }
                }

            }
            else
            {

                //内镜刷卡
                if (CardList.ContainsKey(card))
                {

                    Step(ClientIP, card);
                    //TaskInfo info = new TaskInfo();
                    //m_updateTaskProc(info);
                }

            }
            return "OK";
        }

    }
}
