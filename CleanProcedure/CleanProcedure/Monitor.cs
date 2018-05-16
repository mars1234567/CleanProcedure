using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace CleanProcedure
{
    public class Monitor
    {
        public Monitor()
        {
            System.Timers.Timer t25yi = new System.Timers.Timer(5000);//实例化Timer类，设置时间间隔为100毫秒
            t25yi.Elapsed += new System.Timers.ElapsedEventHandler(MethodTimer);//当到达时间的时候执行MethodTimer2事件 
            t25yi.AutoReset = true;//false是执行一次，true是一直执行
            t25yi.Enabled = true;//设置是否执行System.Timers.Timer.Elapsed事件 
        }
        void MethodTimer(object source, System.Timers.ElapsedEventArgs e) 
        {
           
        } 


    }
}
