using System;
using System.Collections.Generic;
using System.Linq;


namespace KSYY
{
    public class DefaultCls
    {
        public readonly static DefaultCls Instance = new DefaultCls();
        private DefaultCls() { }
        #region 日志文件存放路径
        /// <summary>
        /// 日志文件存放路径
        /// </summary>
        public static string LogPath = System.Environment.CurrentDirectory + "\\Log\\";
        #endregion

        #region 创建DataTable
        /// <summary>
        /// 创建DataTable
        /// </summary>
        /// <param name="columnsCount">列数</param>
        /// <param name="columns">列名</param>
        /// <param name="tableName">DataTableName</param>
        /// <returns>DataTable对象</returns>
        internal System.Data.DataTable CreateDataTable(int columnsCount, string[] columns, string tableName)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            for (int i = 0; i < columnsCount; i++)
            {
                dt.Columns.Add(columns[i], typeof(System.String));
            }
            dt.TableName = tableName;
            return dt;
        }
        #endregion


        #region 写入日志
        #region 写入错误日志
        /// <summary>
        /// 写入错误日志
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="explain">异常说明</param>
        public void WriteErrorLog(Exception ex, string explain)
        {
            lock (this)
            {
                string fileName = System.DateTime.Now.ToString("yyyy-MM-dd");
                string str = "";
                str = "程序出现异常" + System.Environment.NewLine +
                    "异常说明：" + explain + "--" + System.Environment.NewLine +
                    System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + System.Environment.NewLine +
                    "★异常类型：" + ex.GetType().Name + System.Environment.NewLine +
                    "★异常消息：" + ex.Message + System.Environment.NewLine +
                    "★异常信息：" + ex.StackTrace + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine + System.Environment.NewLine;

                if (!System.IO.Directory.Exists(LogPath + @"ErrorLog\\"))
                {
                    System.IO.Directory.CreateDirectory(LogPath + @"ErrorLog\\");
                }
                using (System.IO.FileStream file = new System.IO.FileStream(LogPath + @"ErrorLog\\" + fileName + ".txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                {
                    System.Text.Encoding encoder = System.Text.Encoding.UTF8;
                    byte[] bytes = encoder.GetBytes(str);
                    file.Position = file.Length;
                    file.Write(bytes, 0, bytes.Length);
                    file.Close();
                }
            }
        }
        #endregion
        #region 写入运行日志
        /// <summary>
        /// 写入运行日志
        /// </summary>
        /// <param name="explain"></param>
        public void WriteRunLog(string explain)
        {
            lock (this)
            {
                string fileName = System.DateTime.Now.ToString("yyyy-MM-dd");
                string str = "";
                str = System.Environment.NewLine + System.Environment.NewLine + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + System.Environment.NewLine + explain + System.Environment.NewLine;

                if (!System.IO.Directory.Exists(LogPath + @"RunLog\\"))
                {
                    System.IO.Directory.CreateDirectory(LogPath + @"RunLog\\");
                }
                using (System.IO.FileStream file = new System.IO.FileStream(LogPath + @"RunLog\\" + fileName + ".txt", System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite))
                {
                    System.Text.Encoding encoder = System.Text.Encoding.UTF8;
                    byte[] bytes = encoder.GetBytes(str);
                    file.Position = file.Length;
                    file.Write(bytes, 0, bytes.Length);
                    file.Close();
                }
            }
        }
        #endregion
        #endregion
    }
}