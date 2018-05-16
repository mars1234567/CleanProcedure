using EXControls;
using System.Collections;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace Decontamination
{
    partial class CleanForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        //private delegate void del_do_update(ProgressBar pb);
        //private delegate void del_do_changetxt(LinkLabel l, string text);
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CleanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 559);
            this.Name = "CleanForm";
            this.Text = "内镜洗消追溯系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        //private void llbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        //{
        //    LinkLabel l = (LinkLabel)sender;
        //    if (l.Text == "Downloading") return;
        //    EXControlListViewSubItem subitem = l.Tag as EXControlListViewSubItem;
        //    ProgressBar p = subitem.MyControl as ProgressBar;
        //    Thread th = new Thread(new ParameterizedThreadStart(UpdateProgressBarMethod));
        //    th.IsBackground = true;
        //    th.Start(p);
        //    ((LinkLabel)sender).Text = "Downloading";
        //}
        //private void listView1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    ListViewHitTestInfo listView1info = listView1.HitTest(e.X, e.Y);
        //    ListViewItem.ListViewSubItem subitem = listView1info.SubItem;
        //    if (subitem == null) return;
        //    if (subitem is EXListViewSubItemAB)
        //    {
                 
        //    }
        //}
        //private void do_update(ProgressBar p)
        //{
        //    p.PerformStep();
        //}
        //private void UpdateProgressBarMethod(object pb)
        //{
        //    ProgressBar pp = (ProgressBar)pb;
        //    if (pp.Value == pp.Maximum) pp.Value = 0;
        //    del_do_update delupdate = new del_do_update(do_update);
        //    for (int i = pp.Value; i < pp.Maximum; i++)
        //    {
        //        pp.BeginInvoke(delupdate, new object[] { pp });
        //        Thread.Sleep(10);
        //    }
        //    ListViewItem item = (ListViewItem)pp.Tag;
        //    LinkLabel l = ((LinkLabel)((EXControlListViewSubItem)item.SubItems[4]).MyControl);
        //    del_do_changetxt delchangetxt = new del_do_changetxt(ChangeTextMethod);
        //    l.BeginInvoke(delchangetxt, new object[] { l, "OK" });
        //}

        //private void ChangeTextMethod(LinkLabel l, string text)
        //{
        //    l.Text = text;
        //}
        #endregion

        //    private EXListView listView1;
    //    private EXListView listView1;
    }
}

