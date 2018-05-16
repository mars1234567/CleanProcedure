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

namespace Decontamination
{
    public class PerformManager
    {
        private delegate void del_do_update(ProgressBar pb);
        private delegate void del_do_changetxt(LinkLabel l, string text);
       
        private Form ParentForm;
        Dictionary<string, EXListView> TaskList = new Dictionary<string, EXListView>();
        private ConcurrentBag<ProgressBar> progressList = new ConcurrentBag<ProgressBar>();
        public void SetCtrl(Form form)
        {
            ParentForm =  form;

        }

        public void UpdataUIStatus(TaskInfo info)
        {
            // EXListView iTaskList = TaskList[info.Group];
           // ListViewItem foundItem = iTaskList.FindItemWithText(info.CleanWorker, true, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 

            if (info.Step  == 1)
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
                    iTaskList.MySortBrush = SystemBrushes.ControlLight;
                    iTaskList.MyHighlightBrush = Brushes.Goldenrod;
                    iTaskList.GridLines = true;
                    iTaskList.Location = new Point(0, icount * height);
                    iTaskList.Size = new Size(ParentForm.Size.Width, height - 10);
                    iTaskList.ControlPadding = 4;

                    iTaskList.Columns.Add(new EXEditableColumnHeader("内镜", 120));
                    iTaskList.Columns.Add(new EXEditableColumnHeader("洗消人员", 120));
                    foreach (StepInfo info in item.Value)
                        iTaskList.Columns.Add(new EXColumnHeader(info.name, 120));

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
                    item.SubItems.Add(info.CleanWorker);

                    for (int i = 2; i < iTaskList.Columns.Count; i++)
                    {
                        EXControlListViewSubItem cs = new EXControlListViewSubItem();
                        ProgressBar b = new ProgressBar();
                        b.Tag = item;
                        b.Minimum = 0;
                        b.Maximum = 1000;
                        b.Step = 1;
                        item.SubItems.Add(cs);
                        iTaskList.AddControlToSubItem(b, cs);
                    }

                    iTaskList.Items.Add(item);

                    EXControlListViewSubItem subitem = item.SubItems[info.Step + 1] as EXControlListViewSubItem;
                    ProgressBar p = subitem.MyControl as ProgressBar;
                    p.Maximum = info.Time;
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

                    EXControlListViewSubItem subitem = foundItem.SubItems[info.Step + 1] as EXControlListViewSubItem;
                    ProgressBar p = subitem.MyControl as ProgressBar;
                    p.Maximum = info.Time;
                    Thread th = new Thread(new ParameterizedThreadStart(UpdateProgressBarMethod));
                    th.IsBackground = true;
                    th.Start(p);
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
                ProgressBar pp = (ProgressBar)pb;
                if (pp.Value == pp.Maximum) pp.Value = 0;
                del_do_update delupdate = new del_do_update(do_update);
                 
                for (int i = pp.Value; i < pp.Maximum*10; i++)
                {
                    ProgressBar endprog = progressList.FirstOrDefault(p => p == pp);
                    if (endprog == pp)
                        return;
                    pp.BeginInvoke(delupdate, new object[] { pp });
                    Thread.Sleep(6000);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }

        }
        private void do_update(ProgressBar p)
        {
            p.PerformStep();
        }

        public void Endupdate(TaskInfo info)
        {
            EXListView iTaskList = TaskList[info.Group];
            ListViewItem foundItem = iTaskList.FindItemWithText(info.Card, false, 0);    //参数1：要查找的文本；参数2：是否子项也要查找；参数3：开始查找位置 

            EXControlListViewSubItem subitem = foundItem.SubItems[info.Step + 1] as EXControlListViewSubItem;
            ProgressBar p = subitem.MyControl as ProgressBar;

            progressList.Add(p);
        }

    }
}
