using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{
    public abstract class State
    {
        public DateTime StartTime;
        public string ClientIP;
        public string CardNo;
        public int StepTime;
        //工作人员卡
        public string WorkCard;
        //public string CleanWorker;
    }
}
