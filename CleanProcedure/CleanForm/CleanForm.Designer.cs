
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CleanForm));
            this.tabManager = new CCWin.SkinControl.SkinTabControl();
            this.tabmoniter = new CCWin.SkinControl.SkinTabPage();
            this.tabsearch = new CCWin.SkinControl.SkinTabPage();
            this.ListCleanedCard = new CCWin.SkinControl.SkinListView();
            this.colSeq = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCardNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCardName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWorker = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabManager.SuspendLayout();
            this.tabsearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabManager
            // 
            this.tabManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabManager.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.tabManager.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.tabManager.Controls.Add(this.tabmoniter);
            this.tabManager.Controls.Add(this.tabsearch);
            this.tabManager.HeadBack = null;
            this.tabManager.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.tabManager.ItemSize = new System.Drawing.Size(70, 36);
            this.tabManager.Location = new System.Drawing.Point(7, 40);
            this.tabManager.Name = "tabManager";
            this.tabManager.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("tabManager.PageArrowDown")));
            this.tabManager.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("tabManager.PageArrowHover")));
            this.tabManager.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("tabManager.PageCloseHover")));
            this.tabManager.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("tabManager.PageCloseNormal")));
            this.tabManager.PageDown = ((System.Drawing.Image)(resources.GetObject("tabManager.PageDown")));
            this.tabManager.PageHover = ((System.Drawing.Image)(resources.GetObject("tabManager.PageHover")));
            this.tabManager.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.tabManager.PageNorml = null;
            this.tabManager.SelectedIndex = 1;
            this.tabManager.Size = new System.Drawing.Size(1187, 450);
            this.tabManager.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabManager.TabIndex = 0;
            this.tabManager.SelectedIndexChanged += new System.EventHandler(this.tabManager_SelectedIndexChanged);
            this.tabManager.SizeChanged += new System.EventHandler(this.tabManager_SizeChanged);
            // 
            // tabmoniter
            // 
            this.tabmoniter.BackColor = System.Drawing.Color.White;
            this.tabmoniter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabmoniter.Location = new System.Drawing.Point(0, 36);
            this.tabmoniter.Name = "tabmoniter";
            this.tabmoniter.Size = new System.Drawing.Size(1187, 414);
            this.tabmoniter.TabIndex = 0;
            this.tabmoniter.TabItemImage = null;
            this.tabmoniter.Text = "洗消监控";
            // 
            // tabsearch
            // 
            this.tabsearch.BackColor = System.Drawing.Color.White;
            this.tabsearch.Controls.Add(this.ListCleanedCard);
            this.tabsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabsearch.Location = new System.Drawing.Point(0, 36);
            this.tabsearch.Name = "tabsearch";
            this.tabsearch.Size = new System.Drawing.Size(1187, 414);
            this.tabsearch.TabIndex = 1;
            this.tabsearch.TabItemImage = null;
            this.tabsearch.Text = "查询";
            this.tabsearch.Click += new System.EventHandler(this.tabsearch_Click);
            // 
            // ListCleanedCard
            // 
            this.ListCleanedCard.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeq,
            this.colTime,
            this.colCardNo,
            this.colCardName,
            this.colWorker,
            this.colInfo});
            this.ListCleanedCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ListCleanedCard.Location = new System.Drawing.Point(0, 0);
            this.ListCleanedCard.Name = "ListCleanedCard";
            this.ListCleanedCard.OwnerDraw = true;
            this.ListCleanedCard.Size = new System.Drawing.Size(1187, 414);
            this.ListCleanedCard.TabIndex = 0;
            this.ListCleanedCard.UseCompatibleStateImageBehavior = false;
            this.ListCleanedCard.View = System.Windows.Forms.View.Details;
            this.ListCleanedCard.SelectedIndexChanged += new System.EventHandler(this.ListCleanedCard_SelectedIndexChanged);
            // 
            // colSeq
            // 
            this.colSeq.Text = "序号";
            this.colSeq.Width = 120;
            // 
            // colCardNo
            // 
            this.colCardNo.Text = "内镜号";
            this.colCardNo.Width = 200;
            // 
            // colCardName
            // 
            this.colCardName.Text = "内镜名称";
            this.colCardName.Width = 120;
            // 
            // colWorker
            // 
            this.colWorker.Text = "工作人员";
            this.colWorker.Width = 120;
            // 
            // colInfo
            // 
            this.colInfo.Text = "洗消信息";
            this.colInfo.Width = 400;
            // 
            // colTime
            // 
            this.colTime.Text = "洗消结束时间";
            this.colTime.Width = 200;
            // 
            // CleanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1201, 497);
            this.Controls.Add(this.tabManager);
            this.ForeColor = System.Drawing.SystemColors.Desktop;
            this.Name = "CleanForm";
            this.Text = "内镜洗消追溯系统";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabManager.ResumeLayout(false);
            this.tabsearch.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private CCWin.SkinControl.SkinTabControl tabManager;
        private CCWin.SkinControl.SkinTabPage tabmoniter;
        private CCWin.SkinControl.SkinTabPage tabsearch;
        private CCWin.SkinControl.SkinListView ListCleanedCard;
        private ColumnHeader colSeq;
        private ColumnHeader colCardNo;
        private ColumnHeader colCardName;
        private ColumnHeader colWorker;
        private ColumnHeader colInfo;
        private ColumnHeader colTime;

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

