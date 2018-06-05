using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{
    public   class State
    {
        //工作人员卡
        public string WorkCard;
        //刷卡器
        public string ClientIP;
        //开始时间
        public DateTime StartTime;
        //内镜卡
        public string CardNo;
        //洗消时间
        public int StepTime;
        //洗消步骤
        public int StepNum;
        public static int id = 0;

        //传入工作人员卡
        public State(string cardno,string clientIp,int time=0)
        {
            WorkCard = cardno;
            StartTime = DateTime.Now;
            ClientIP = clientIp;
            StepTime = time;
            id++;
        }
       //传入内镜卡,绑卡
        public void Binding(string cardno)
        {
            CardNo = cardno;
            StepNum = 1;


        }

        public void UpdateMsgToDB(StepNode curr_node,bool bEnd =false)
        {
            //保存数据库
            Clean_RecordList record = new Clean_RecordList();
            record.CleanCard = CardNo;
            record.CleanIp = ClientIP;
            record.CleanPort = curr_node.GetPort();
            record.MaxNum = curr_node.GetTotalStepNum();
            record.StartTime = StartTime;
            record.StepTime = StepTime;
            record.StepNum = curr_node.GetStepNum();
            record.WorkCard = WorkCard;
            record.StepName = curr_node.GetStepName();
            record.EndTime = StartTime;
            record.Sequence = id;
            if(bEnd)
            {
                record.EndTime = DateTime.Now;
            }
            DBmanager.Instance.AddQueue(record);
        }

 
        public void Convert(string Ip)
        {
            ClientIP = Ip;
            StartTime = DateTime.Now;
            StepNum++;
        }

        //判断洗消时间是否完成
        public bool IsTimeFinish()
        {
            bool bFinish = false;
            if (!Config_func.GetInstance().StrictTime)
                bFinish = true;
            else
            {
                DateTime NewTime = DateTime.Now;
                TimeSpan ts = NewTime - StartTime;
                int dur = (int)Math.Round(ts.TotalMinutes, 0);
                if (dur >= StepTime)
                {
                    StartTime = NewTime;
                    bFinish = true;
                }
                else
                    bFinish = false;
            }
            return bFinish;
        }
    }
}
