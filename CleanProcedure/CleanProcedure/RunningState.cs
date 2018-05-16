using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{
    public class RunningState : State
    {
        
        //public StepNode node;
        
        public RunningState()
        {
            StartTime = DateTime.Now;
        }
        public void Convert(string Ip)
        {
            ClientIP = Ip;
          
        }
        public bool IsFinish(int stepTime)
        {
            bool bFinish = false;
            if (!Config_func.GetInstance().StrictTime)
                bFinish = true;
            else
            {
                DateTime NewTime = DateTime.Now;
                TimeSpan ts = NewTime - StartTime;
                int dur = (int)Math.Round(ts.TotalMinutes, 0);
                if (dur >= stepTime)
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
