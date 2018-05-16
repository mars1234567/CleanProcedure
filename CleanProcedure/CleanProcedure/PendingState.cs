using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{

    public class PendingState : State
    {


        public PendingState(string cardno)
        {
            WorkCard = cardno;
            StartTime = DateTime.Now;
        }
    }
}
