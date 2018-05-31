using CA_sign;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;

namespace CleanProcedure
{


    public class DBmanager 
    {
       
        private DBmanager( )  
        {  
          
        }

        public bool  AddEntity(Clean_RecordList entity)   
        {
            using (CleanProcedureEntities db = new CleanProcedureEntities())
            {
                db.Clean_RecordList.Add(entity);
                if (db.SaveChanges() > 0)
                    return true;
                else
                    return false; 
            }
        }

                    #region 解决发布时含有优质媒体时，前台页面卡住的现象
            //原理：利用生产者消费者模式进行入列出列操作

            public readonly static DBmanager Instance = new DBmanager();
            private Mutex m_listMutex = new Mutex();

            private Queue<Clean_RecordList> ListQueue = new Queue<Clean_RecordList>();
         
            public void AddQueue(Clean_RecordList record) //入列
            {
                m_listMutex.WaitOne();
                ListQueue.Enqueue(record);
                m_listMutex.ReleaseMutex();
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
                try
                {
                    using (CleanProcedureEntities db = new CleanProcedureEntities())
                    {
                        while (ListQueue.Count > 0)
                        {

                            //从队列中取出
                            Clean_RecordList queueinfo = ListQueue.Dequeue();

                            Clean_RecordList find = db.Clean_RecordList.FirstOrDefault(model => model.Sequence == queueinfo.Sequence && model.StepNum == queueinfo.StepNum );
                            if (find != null)
                            {
                                //if(find.MaxNum==find.StepNum)
                                  find.EndTime = queueinfo.EndTime;
                            }
                            else
                                db.Clean_RecordList.Add(queueinfo);
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.WriteLog(typeof(Exception), e);
                    //throw;
                }
            }




            #endregion
       
    }
}
