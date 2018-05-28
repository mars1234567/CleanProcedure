using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using udp_csharp.include;

namespace CleanProcedure
{
    public class Config_func
    {
        //是否隔夜作废
        bool bLastNight;
        //是否严格时间判断
        bool bStrictTime;

        public bool LastNight
        {
            get { return bLastNight; }

            set { bLastNight = value; }
        }

        public bool StrictTime
        {
            get { return bStrictTime; }

            set { bStrictTime = value; }
        }
        private static readonly Config_func instance = new Config_func();
       
        public static Config_func GetInstance()
        {       
            return instance;
        }
        private Config_func()
        {
            bLastNight =  Convert.ToBoolean(ConfigMgr.GetAppConfig("LastNight"));
            bStrictTime = Convert.ToBoolean(ConfigMgr.GetAppConfig("StrictTime"));
        }
          ~Config_func()
        {
            ConfigMgr.SerAppConfig("LastNight", bLastNight.ToString());
            ConfigMgr.SerAppConfig("StrictTime", bLastNight.ToString());

        }
        public static string judgement()
        {
             Type    t = typeof(Config_func);//括号中的为所要使用的函数所在的类的类名。
            MethodInfo    mt = t.GetMethod("TimeJudge",BindingFlags.Static);
            if    (mt == null)
            {
                return "没有获取到相应的函数！！";
            }
            else
            {
                string    str = (string)mt.Invoke(null,new object[] { "1234567890123"});
                 
            }
            return "OK";
        }

        private static string TimeJudge(string abc)
        {
            return abc;
        }
    }
}
