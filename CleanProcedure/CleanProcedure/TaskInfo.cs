﻿using System;
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
       public   TaskInfo(string dev, string card,string cleanwork, int time, int step,string group)
        {
            DevIP = dev;
            Card = card;
            Time = time;
            Step = step;
            Group = group;
            CleanWorker = cleanwork;
        }
    }

    public class StepInfo
    {
        public string name;

    }
}
