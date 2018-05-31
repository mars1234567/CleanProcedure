using BK.Util;
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
        Dictionary<string,  State> CardList = new Dictionary<string,  State>();
        //洗消步骤 KEY 为ClientIP,该步骤的设置
        public MultimapBK<string, StepNode> IPlist = new MultimapBK<string, StepNode>();

        Object locker = new Object();
        //刷卡器ip ，工作人员
        Dictionary<string,  State> PendingList = new Dictionary<string, State>();

        //public delegate void UpdateTask(TaskInfo info);

        //public UpdateTask m_updateTaskProc = null;
       //工作人员列表
        List<string> Worker = new List<string>();

        //隔夜内镜列表
        List<string> YesterdayCard = new List<string>();
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
                            //昨天的消毒记录
                            if (it.StartTime.Date.Equals(DateTime.Today.AddDays(-1)) /*|| it.StartTime.Date.Equals(DateTime.Today)*/)
                            {
                                //if (it.StepNum < it.MaxNum)//消毒未完成记录
                                //{
                                //    State mState = new State(it.WorkCard,it.CleanIp,it.StepTime);
                                //    mState.Binding(cardname);
                                 

                                //    CardList[cardname] = mState;

                                //}
                                YesterdayCard.Add(it.CleanCard);
                            }
                        }
                    }
                }
                DBmanager.Instance.Start();
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
        public string  Pending(string ClientIP, string card)
        {
            if (PendingList.ContainsKey(ClientIP))
            {
                PendingList.Add(ClientIP, new State(card, ClientIP));
            }
            else
            {
                //工作人员卡是新的覆盖旧的
                PendingList[ClientIP] = new State(card, ClientIP);
            }
            return "请刷内镜卡";
        }

        public StepNode GetIpStep(string ClientIP,int lastStepNum)
        {
            StepNode current_node = IPlist.GetFirstItem(ClientIP);
             
            while (current_node != null)
            {
                int stepnum = current_node.GetStepNum();
                if ((stepnum == lastStepNum + 1) || (stepnum == lastStepNum ))
                    break;
                else
                    current_node = IPlist.Iterate(ClientIP);
            }
            return current_node;
        }
        public bool BindingCard(string ClientIP, string card, ref string sResult)
        {
            bool bConvert = false;
            
            //获取当前刷卡器的步骤信息
            StepNode curr_node = GetIpStep(ClientIP, 1);
            if (curr_node != null)
            {
                lock (locker)
                {
                    //绑卡过程,内镜卡不在洗消步骤中
                    if (CardList[card] == null)
                    {
                        State mstate = PendingList[ClientIP];
                        mstate.StepTime = curr_node.GetDur();
                        mstate.Binding(card);
                        mstate.UpdateMsgToDB(curr_node);
                        CardList[card] = mstate;
                        bConvert = true;
                        sResult = "绑卡成功，开始";
                    }
                    else
                    {
                        sResult = "内镜卡正在洗消";
                    }
                }
            }


            if (bConvert)
            {
               //更新界面
                TaskInfo info = new TaskInfo(ClientIP, card, PendingList[ClientIP].WorkCard, curr_node.GetDur(), 1, curr_node.GetCleanGroup(),"");
                UpdateUIDelegate(info);

            }
            return bConvert;
        }
        //洗消步骤切换
        public bool Step(string ClientIP, string card, ref string sRet)
        {
              sRet= "";
            bool bFinish = false;
            //bool bConvert = false;
            DateTime currenttime = DateTime.Now;
            //获取内镜卡上次的刷卡信息
            State mState = CardList[card];
            if (mState == null)
            {
                sRet = "";
                return false;
            }
            else
            {
                //获取当前刷卡器的步骤信息
                StepNode curr_node = GetIpStep(ClientIP, mState.StepNum);

                if (curr_node == null)
                    sRet = "刷卡步骤不对";
                else
                {
                    StepNode last_node = curr_node.GetLast();

                    //不能机洗跳手工
                    if (last_node.IsMachineWash() && !curr_node.IsMachineWash())
                    {
                        sRet = "不能机洗跳手工";

                    }
                    else
                    {

                        //int stepTime = last_node.GetDur();

                      //  lock (locker)
                        {
                            //当前步骤的上一个刷卡器地址是否相同，即洗消顺序相同
                            if (last_node.StepIp().Equals(mState.ClientIP))
                            {
                                if (mState.IsTimeFinish())
                                {
                                    mState.Convert(ClientIP);
                                    mState.StepTime = curr_node.GetDur();
                                    mState.UpdateMsgToDB(curr_node);
                                    sRet = last_node.GetStepName() + "结束";
                                }
                                else
                                {
                                    sRet = "洗消时间未到";

                                }
                            }
                            else if (curr_node.StepIp().Equals(mState.ClientIP))//结束当前步骤
                            {

                                mState.UpdateMsgToDB(curr_node, true);
                                if(curr_node.GetStepNum()==curr_node.GetTotalStepNum())
                                     bFinish = true;

                                sRet = "洗消步骤全部结束";
                            }
                            else
                                sRet = "洗消步骤不对";

                        }



                    }
                }

                TaskInfo info = new TaskInfo(ClientIP, card, mState.WorkCard, curr_node.GetDur(), curr_node.GetStepNum(), curr_node.GetCleanGroup(), sRet);
                if (bFinish)
                    TaskCallBack(info);
                else
                    UpdateUIDelegate(info);

                return bFinish;
            }
        }


        public string Handle(string ClientIP, string card)
        {
            //

            ////获取当前刷卡器配置
            StepNode current_dev = GetIpStep(ClientIP,1);
            //StepNode current_last = current_dev.GetLast();

            //刷卡器为第一个步骤
            //if (current_last == null)
            string sResult = "";
            lock (locker)
             {
                //工作人员刷卡
                 if ( IsWorkCard(card) && current_dev != null )
                {
                    sResult =  Pending(ClientIP, card);
                }

                //内镜刷卡
                if (IsMCard(card))
                {
                    //绑卡步骤
                    if (PendingList.ContainsKey(ClientIP))
                    {
                        //string workDev = PendingList[ClientIP].WorkCard;
                       
                        if(BindingCard(ClientIP, card,ref sResult))
                        //绑卡成功，从绑卡列表中删除
                            PendingList.Remove(ClientIP);
                    }
                    else if (CardList.ContainsKey(card))  //内镜洗消步骤
                    {

                        if (Step(ClientIP, card,ref sResult))
                        {                           
                         
                                CardList.Remove(card);
                           
                        }
                        //TaskInfo info = new TaskInfo();
                        //m_updateTaskProc(info);
                    }
                }

             }

            return sResult;
        }

    }
}
