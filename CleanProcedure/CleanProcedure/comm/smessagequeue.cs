using CA_sign;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using udp_csharp.include;

namespace audiotest
{

        public delegate void ProcessDelegate(QueueInfo info);
   
        //队列临时类
        public class QueueInfo
        {
            public string m_RecBuffer;
            public string m_ClientIp;
            public IPEndPoint ClientIpPort;
        }

        public class BusinessInfoHelper
        {
            #region 解决发布时含有优质媒体时，前台页面卡住的现象
            //原理：利用生产者消费者模式进行入列出列操作

            public readonly static BusinessInfoHelper Instance = new BusinessInfoHelper();
            private BusinessInfoHelper()
            { }

            private Queue<QueueInfo> ListQueue = new Queue<QueueInfo>();
            public ProcessDelegate m_proc = null;
            public void AddQueue(string RecBuffer, IPEndPoint ip) //入列
            {
                QueueInfo queueinfo = new QueueInfo();

                queueinfo.m_RecBuffer = RecBuffer;
                queueinfo.m_ClientIp = ip.Address.ToString();
                queueinfo.ClientIpPort = ip;
                ListQueue.Enqueue(queueinfo);
            }

            public void Start()//启动
            {
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }

            private void threadStart()
            {
                while (true)
                {
                    if (ListQueue.Count > 0)
                    {
                        try
                        {
                            ScanQueue();
                        }
                        catch (Exception e)
                        {
                            LogHelper.WriteLog(typeof(Exception), e);
                        }
                    }
                    else
                    {
                        //没有任务，休息3秒钟
                        Thread.Sleep(3000);
                    }
                }
            }

            //要执行的方法
            private void ScanQueue()
            {
                while (ListQueue.Count > 0)
                {
                    try
                    {
                        //从队列中取出
                        QueueInfo queueinfo = ListQueue.Dequeue();

                        //取出的queueinfo就可以用了，里面有你要的东西
                        //以下就是处理程序了

                        if (m_proc != null)
                        {
                            m_proc(queueinfo);
                            Writelog(queueinfo);
                        }

                    }
                    catch (Exception e)
                    {
                        LogHelper.WriteLog(typeof(Exception), e);
                        //throw;
                    }
                }
            }

            private void Writelog(QueueInfo info)
            {
                string msg="";
                msg += info.ClientIpPort.ToString();
                msg += " ";
                msg += info.m_RecBuffer;
             
                LogHelper.WriteLog(typeof(String), msg);
            }


            #endregion
        }
    




}
