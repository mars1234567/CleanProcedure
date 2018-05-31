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
       public static void IPtoHexStr(string ip,ref byte[] bytes,char P = '.' )
        {
            char[] separator = new char[] { P };
            string[] items = ip.Split(separator);
            int i = 0;
            foreach(var item in items)
            {
                bytes[i] = byte.Parse(item);
                i++;
            }
        }

       public static void MACtoHexStr(string MAC, ref byte[] bytes,char P = '-')
       {
           char[] separator = new char[] { P };
           string[] items = MAC.Split(separator);
           int i = 0;
           foreach (var item in items)
           {
               bytes[i] = byte.Parse(item);
               i++;
           }
       }

       /// <summary>
       /// 字符串转16进制字节数组
       /// </summary>
       /// <param name="hexString"></param>
       /// <returns></returns>
       public static byte[] strToToHexByte(string num)
       {
          return System.Text.Encoding.Default.GetBytes(num);
       }

       
/// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <param name="fenge">是否每字符用逗号分隔</param>
       /// <returns></returns>
       public static byte[] ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                 s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
             }
             System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            return bytes;
         }

    }
}
