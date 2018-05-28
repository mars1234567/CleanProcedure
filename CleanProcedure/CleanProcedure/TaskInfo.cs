using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decontamination
{
    public class TaskInfo
    {
       public string DevIP;
       public string CleanWorker;
       public string Card;
       public string Group;
       public int Time;
       public int Step;

       public string errorInfo;
       public   TaskInfo(string dev, string card,string cleanwork, int time, int step,string group,string error)
        {
            DevIP = dev;
            Card = card;
            Time = time;
            Step = step;
            Group = group;
            CleanWorker = cleanwork;
            errorInfo = error;
        }
    }

    public class ResultInfo
    {
        public string DevIP;
        public string CleanWorker;
        public string Card;
        public string Group;
        public int Time;

        public string errorInfo;
    }

    public class StepInfo
    {
        public string name;

    }
}
