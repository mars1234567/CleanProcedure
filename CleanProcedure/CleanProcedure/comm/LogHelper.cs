using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KSYY;


namespace CA_sign
{
    public class LogHelper
    {
        /// <summary>
         /// 输出日志到Log4Net
         /// </summary>
         /// <param name="t"></param>
        /// <param name="ex"></param>
       #region static void WriteLog(Type t, Exception ex)

         public static void WriteLog(Type t, Exception ex)
         {
             DefaultCls.Instance.WriteErrorLog(ex,typeof(Type).ToString());
             
         }

         #endregion
         /// <summary>
        /// 输出日志到Log4Net
         /// </summary>
         /// <param name="t"></param>
         /// <param name="msg"></param>
         #region static void WriteLog(Type t, string msg)
 
         public static void WriteLog(Type t, string msg)
         {
             DefaultCls.Instance.WriteRunLog(msg);
         }

         #endregion

    }
}
