using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;


namespace CleanProcedure
{
    public class CardListMoniter
    {
        Timer m_timer = new Timer(5000);
        public Dictionary<string, Clean_RecordList> Cardlist = new Dictionary<string, Clean_RecordList>();

        public CardListMoniter()
        {
            m_timer.Elapsed += new System.Timers.ElapsedEventHandler(Moniter);//到达时间的时候执行事件；
            m_timer.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            m_timer.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；
        }
        public void GetCardList()
        {
            Cardlist.Clear();
            using (CleanProcedureEntities db = new CleanProcedureEntities())
            {
                var reclist = db.Clean_RecordList.GroupBy(p => p.CleanCard);

                foreach (var item in reclist)
                {
                    string cardname = item.Key;
                    Clean_RecordList it = item.OrderBy(p => p.StartTime).LastOrDefault();

                    if (it.StartTime.Date.Equals(DateTime.Today))
                    {
                        if (it.StepNum < it.MaxNum)//消毒未完成记录
                        {
                            Cardlist[it.CleanCard] = it;



                        }
                    }
                }
            }
        }

        public void Moniter(object source, System.Timers.ElapsedEventArgs e)
        {
            GetCardList();
            DateTime now = DateTime.Now;
            foreach(var item in Cardlist)
            {
                TimeSpan ts = now - item.Value.StartTime;
                int dur = (int)Math.Round(ts.TotalMinutes, 0);
                if (ts.Minutes >= item.Value.StepTime)//时间到了
                {
                     
                }
            }
        }
 


    }
}
