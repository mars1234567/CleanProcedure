using CA_sign;
using EXControls;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CCWin.SkinControl;
using CleanProcedure;

namespace Decontamination
{
    public class PerformManager
    {
        private delegate void del_do_update(SkinProgressBar pb);
        private delegate void del_do_changetxt(LinkLabel l, string text);

        private System.Windows.Forms.Control ParentForm;
        Dictionary<string, EXListView> TaskList = new Dictionary<string, EXListView>();
        private ConcurrentBag<SkinProgressBar> progressList = new ConcurrentBag<SkinProgressBar>();
        Dictionary<string, string> Cardlist = null;
        public void SetCtrl(System.Windows.Forms.Control form, Dictionary<string, string> list)
        {
            ParentForm =  form;
            Cardlist = list;

        }

        public void UpdataUIStatus(TaskInfo info)
        {
            // EXListView iTaskList = TaskList[info.Group];
           // ListViewItem foundItem = iTaskList.FindItemWithText(info.CleanWorker, true, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 

            if ((info.Step == 1)&&info.bStart )
            {
                AddTaskList(info);

            }
            else
            {
                StartTask(info);
            }
        }

        private void EndTask(TaskInfo info)
        {
            EXListView iTaskList = TaskList[info.Group];
            ListViewItem foundItem = iTaskList.FindItemWithText(info.Card, true, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 


        }

        public void Init(Dictionary<string, List<StepInfo>> infolist)
        {
            try
            {
                int height = ParentForm.Size.Height / infolist.Count;
                int icount = 0;
                foreach (var item in infolist)
                {
                    EXListView iTaskList = new EXListView();
                    ImageList image = new ImageList();
                    image.ImageSize = new Size(24, 24);//这边设置宽和高
                    iTaskList.SmallImageList = image;
                    iTaskList.MySortBrush = SystemBrushes.ControlLight;
                    iTaskList.MyHighlightBrush = Brushes.Goldenrod;
                    iTaskList.GridLines = true;
                    iTaskList.Location = new Point(0, icount * height);
                    iTaskList.Size = new Size(ParentForm.Size.Width, height - 3);
                    iTaskList.ControlPadding = 4;

                    int colwidth = (ParentForm.Size.Width) / (item.Value.Count + 4);
                    iTaskList.Columns.Add(new EXEditableColumnHeader("内镜编号", colwidth));
                    iTaskList.Columns.Add(new EXEditableColumnHeader("内镜名称", colwidth));
                    iTaskList.Columns.Add(new EXEditableColumnHeader("洗消人员", colwidth));
                    iTaskList.Columns.Add(new EXEditableColumnHeader("消息", colwidth));

                    foreach (StepInfo info in item.Value)
                        iTaskList.Columns.Add(new EXColumnHeader(info.name, colwidth));

                    TaskList.Add(item.Key, iTaskList);
                    ParentForm.Controls.Add(iTaskList);
                    icount++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }
        public delegate void UpdateTaskList(TaskInfo step);//声明一个更新主线程的委托
        //绑定工作人员卡和内镜卡
        public void AddTaskList(TaskInfo info)
        {
            try
            {
                EXListView iTaskList = TaskList[info.Group];
                if (iTaskList.InvokeRequired)
                {
                    UpdateTaskList updatedelegate = new UpdateTaskList(AddTaskList);
                    ParentForm.Invoke(updatedelegate, new object[] { info });
                }
                else
                {
                    iTaskList.BeginUpdate();

                    //movie
                    EXListViewItem item = new EXListViewItem(info.Card);
                    string cardname = "";
                    string workName = info.CleanWorker;
                    if (Cardlist.ContainsKey(info.Card))
                        cardname = Cardlist[info.Card];
                    if (Cardlist.ContainsKey(info.CleanWorker))
                        workName = Cardlist[info.CleanWorker];
                    item.SubItems.Add(cardname);
                    item.SubItems.Add(workName);
                    item.SubItems.Add(info.errorInfo);
                    for (int i = 4; i < iTaskList.Columns.Count; i++)
                    {
                        EXControlListViewSubItem cs = new EXControlListViewSubItem();
                        SkinProgressBar b = new SkinProgressBar();
                        b.TextFormat = SkinProgressBar.TxtFormat.None;
                        b.Tag = item;
                        b.Minimum = 0;
                        b.Maximum = 100;
                        b.Step = 1;
                        item.SubItems.Add(cs);
                        iTaskList.AddControlToSubItem(b, cs);
                    }

                    iTaskList.Items.Add(item);
                    
                    EXControlListViewSubItem subitem = item.SubItems[info.Step + 3] as EXControlListViewSubItem;
                    SkinProgressBar p = subitem.MyControl as SkinProgressBar;
                    p.Maximum = info.Time*10;
                    Thread th = new Thread(new ParameterizedThreadStart(UpdateProgressBarMethod));
                    th.IsBackground = true;
                    th.Start(p);

                    iTaskList.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }

        //开始清洗工作并开始计时
        public void StartTask(TaskInfo info)
        {
            try
            {
                EXListView iTaskList = TaskList[info.Group];
                if (iTaskList.InvokeRequired)
                {
                    UpdateTaskList updatedelegate = new UpdateTaskList(StartTask);
                    ParentForm.Invoke(updatedelegate, new object[] { info });
                }
                else
                {

                    ListViewItem foundItem = iTaskList.FindItemWithText(info.Card, true, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 
                    
                    {
                        foundItem.SubItems[3].Text = info.errorInfo;
                    }
                    if (info.bStart)
                    {
                        if (info.Step>1)
                        {
                            EXControlListViewSubItem Stopsubitem = foundItem.SubItems[info.Step + 2] as EXControlListViewSubItem;
                            SkinProgressBar Stopp = Stopsubitem.MyControl as SkinProgressBar;
                            progressList.Add(Stopp);
                        }

                        EXControlListViewSubItem subitem = foundItem.SubItems[info.Step +3] as EXControlListViewSubItem;
                        SkinProgressBar p = subitem.MyControl as SkinProgressBar;
                        p.Maximum = info.Time*10;
                        Thread th = new Thread(new ParameterizedThreadStart(UpdateProgressBarMethod));
                        th.IsBackground = true;
                        th.Start(p);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }

        private void UpdateProgressBarMethod(object pb)
        {
            try
            {
                SkinProgressBar pp = (SkinProgressBar)pb;
                //int totalCount = pp.Maximum * 10;
                //pp.Maximum = pp.Maximum * 10;
                del_do_update delupdate = new del_do_update(do_update);

                for (int i = pp.Value; i < pp.Maximum; i++)
                {
                    SkinProgressBar endprog = progressList.FirstOrDefault(p => p == pp);
                    if (endprog == pp)
                    {

                        return;
                    }
                        
                    pp.BeginInvoke(delupdate, new object[] { pp });
                    Thread.Sleep(6000);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }

        }
        private void do_update(SkinProgressBar p)
        {
            Graphics g = p.CreateGraphics();
            Font mf = new System.Drawing.Font("宋体", 10);
            Brush mb = System.Drawing.Brushes.Black;
            Point mp = new System.Drawing.Point(10, 0);
            
            p.PerformStep();
            int m = p.Value /10;
            int s = (p.Value * 6) % 60;
            string  str = string.Format("{0}分:{1}秒",m,s);
            g.DrawString(string.Format("已处理....{0}", str), mf, mb, mp);
        }

        public void Endupdate(TaskInfo info)
        {
            EXListView iTaskList = TaskList[info.Group];

            if (iTaskList.InvokeRequired)
            {
                UpdateTaskList updatedelegate = new UpdateTaskList(Endupdate);
                ParentForm.Invoke(updatedelegate, new object[] { info });
            }
            else
            {

                ListViewItem foundItem = iTaskList.FindItemWithText(info.Card, false, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 

                for (int i = 4; i < iTaskList.Columns.Count; i++)
                {
                    EXControlListViewSubItem subitem = foundItem.SubItems[i] as EXControlListViewSubItem;
                    iTaskList.RemoveControlFromSubItem(subitem);
                }

                iTaskList.Items.Remove(foundItem);
            }
        }

        public void SizeChange()
        {
            int icount = 0;
            int height = ParentForm.Size.Height / TaskList.Count;
            foreach(var   item in TaskList)
            {
                EXListView iTaskList = item.Value;
                iTaskList.Location = new Point(0, icount * height);
                iTaskList.Size = new Size(ParentForm.Size.Width, height - 3);
                int colwidth = (ParentForm.Size.Width  ) / iTaskList.Columns.Count -1;
                foreach (ColumnHeader ch in iTaskList.Columns)
                    ch.Width = colwidth;
                icount++;
            }
        }
    }
}
