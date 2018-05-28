using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace udp_csharp.include
{
    public class ConfigMgr
    {

        public static string GetAppConfig(string strKey)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string str = config.AppSettings.Settings[strKey].Value;
            return str;
        }
        public static void SerAppConfig(string strKey,string strValue)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[strKey].Value = strValue;
 
        }
        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string byteToHexStr(byte[] bytes,int len)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < len; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                    returnStr += " ";

                }
            }
            return returnStr.Trim();
        }
    }
}
