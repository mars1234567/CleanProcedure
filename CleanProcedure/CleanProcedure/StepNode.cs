using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanProcedure
{
    public class StepNode
    {
        public StepNode(string IP, Clean_CardDevice CardDevice)
        {
            ClientIp = IP;
            Step = CardDevice;
        }
        //当前步骤
        private Clean_CardDevice Step;
        //前一个
        private StepNode Last;
        //IP地址
        private string ClientIp;
        //端口号
        private string ClientPort;
       
        public void SetLast(StepNode Node)
        {
            Last = Node;
        }

        public int GetStepNum()
        {
            return (int)Step.StepNumber;
        }
        public string GetCleanGroup()
        {
            return Step.CleanGroup;
        }
        public StepNode GetLast()
        {
            return Last;
        }
        public string StepIp()
        {
            return ClientIp;
        }
        public int GetDur()
        {
            return Convert.ToInt32(Step.StepTime);
        }
        public bool IsLastStep()
        {
            return Step.StepCount == Step.StepNumber;

        }
        public bool IsMachineWash()
        {
            return Step.StepType.Equals("机洗");


        }
    }
}
