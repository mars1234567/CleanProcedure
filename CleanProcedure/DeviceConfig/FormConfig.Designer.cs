namespace DeviceConfig
{
    partial class FormConfig
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConfig));
            this.TabCardControl = new CCWin.SkinControl.SkinTabControl();
            this.TabCardDev = new CCWin.SkinControl.SkinTabPage();
            this.skinPanel1 = new CCWin.SkinControl.SkinPanel();
            this.skinTextBox1 = new CCWin.SkinControl.SkinTextBox();
            this.skinComboBox1 = new CCWin.SkinControl.SkinComboBox();
            this.listConfig = new ListViewEx.ListViewEx();
            this.ListCardDev = new CCWin.SkinControl.SkinListView();
            this.TabCard = new CCWin.SkinControl.SkinTabPage();
            this.TabCardControl.SuspendLayout();
            this.TabCardDev.SuspendLayout();
            this.skinPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabCardControl
            // 
            this.TabCardControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.TabCardControl.AnimatorType = CCWin.SkinControl.AnimationType.HorizSlide;
            this.TabCardControl.BackColor = System.Drawing.Color.SkyBlue;
            this.TabCardControl.CloseRect = new System.Drawing.Rectangle(2, 2, 12, 12);
            this.TabCardControl.Controls.Add(this.TabCardDev);
            this.TabCardControl.Controls.Add(this.TabCard);
            this.TabCardControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCardControl.DrawType = CCWin.SkinControl.DrawStyle.Draw;
            this.TabCardControl.HeadBack = null;
            this.TabCardControl.ImgTxtOffset = new System.Drawing.Point(0, 0);
            this.TabCardControl.ItemSize = new System.Drawing.Size(100, 100);
            this.TabCardControl.Location = new System.Drawing.Point(4, 28);
            this.TabCardControl.Multiline = true;
            this.TabCardControl.Name = "TabCardControl";
            this.TabCardControl.PageArrowDown = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageArrowDown")));
            this.TabCardControl.PageArrowHover = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageArrowHover")));
            this.TabCardControl.PageCloseHover = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageCloseHover")));
            this.TabCardControl.PageCloseNormal = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageCloseNormal")));
            this.TabCardControl.PageDown = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageDown")));
            this.TabCardControl.PageHover = ((System.Drawing.Image)(resources.GetObject("TabCardControl.PageHover")));
            this.TabCardControl.PageImagePosition = CCWin.SkinControl.SkinTabControl.ePageImagePosition.Left;
            this.TabCardControl.PageNorml = null;
            this.TabCardControl.RightToLeftLayout = true;
            this.TabCardControl.SelectedIndex = 0;
            this.TabCardControl.Size = new System.Drawing.Size(858, 577);
            this.TabCardControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.TabCardControl.TabIndex = 1;
            // 
            // TabCardDev
            // 
            this.TabCardDev.BackColor = System.Drawing.Color.SkyBlue;
            this.TabCardDev.Controls.Add(this.skinPanel1);
            this.TabCardDev.Controls.Add(this.ListCardDev);
            this.TabCardDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCardDev.Font = new System.Drawing.Font("华文宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabCardDev.Location = new System.Drawing.Point(100, 0);
            this.TabCardDev.Name = "TabCardDev";
            this.TabCardDev.Size = new System.Drawing.Size(758, 577);
            this.TabCardDev.TabIndex = 0;
            this.TabCardDev.TabItemImage = null;
            this.TabCardDev.Text = "刷卡器";
            this.TabCardDev.Click += new System.EventHandler(this.TabCardDev_Click);
            // 
            // skinPanel1
            // 
            this.skinPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.skinPanel1.BackColor = System.Drawing.Color.Transparent;
            this.skinPanel1.Controls.Add(this.skinTextBox1);
            this.skinPanel1.Controls.Add(this.skinComboBox1);
            this.skinPanel1.Controls.Add(this.listConfig);
            this.skinPanel1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinPanel1.DownBack = null;
            this.skinPanel1.Location = new System.Drawing.Point(5, 267);
            this.skinPanel1.MouseBack = null;
            this.skinPanel1.Name = "skinPanel1";
            this.skinPanel1.NormlBack = null;
            this.skinPanel1.Size = new System.Drawing.Size(749, 266);
            this.skinPanel1.TabIndex = 1;
            // 
            // skinTextBox1
            // 
            this.skinTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.skinTextBox1.DownBack = null;
            this.skinTextBox1.Icon = null;
            this.skinTextBox1.IconIsButton = false;
            this.skinTextBox1.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.IsPasswordChat = '\0';
            this.skinTextBox1.IsSystemPasswordChar = false;
            this.skinTextBox1.Lines = new string[] {
        "skinTextBox1"};
            this.skinTextBox1.Location = new System.Drawing.Point(116, 121);
            this.skinTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.skinTextBox1.MaxLength = 32767;
            this.skinTextBox1.MinimumSize = new System.Drawing.Size(28, 28);
            this.skinTextBox1.MouseBack = null;
            this.skinTextBox1.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.skinTextBox1.Multiline = false;
            this.skinTextBox1.Name = "skinTextBox1";
            this.skinTextBox1.NormlBack = null;
            this.skinTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.skinTextBox1.ReadOnly = false;
            this.skinTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.skinTextBox1.Size = new System.Drawing.Size(147, 28);
            // 
            // 
            // 
            this.skinTextBox1.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.skinTextBox1.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skinTextBox1.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.skinTextBox1.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.skinTextBox1.SkinTxt.Name = "BaseText";
            this.skinTextBox1.SkinTxt.Size = new System.Drawing.Size(137, 18);
            this.skinTextBox1.SkinTxt.TabIndex = 0;
            this.skinTextBox1.SkinTxt.Text = "skinTextBox1";
            this.skinTextBox1.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox1.SkinTxt.WaterText = "";
            this.skinTextBox1.TabIndex = 3;
            this.skinTextBox1.Text = "skinTextBox1";
            this.skinTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.skinTextBox1.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.skinTextBox1.WaterText = "";
            this.skinTextBox1.WordWrap = true;
            // 
            // skinComboBox1
            // 
            this.skinComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.skinComboBox1.FormattingEnabled = true;
            this.skinComboBox1.Location = new System.Drawing.Point(116, 59);
            this.skinComboBox1.Name = "skinComboBox1";
            this.skinComboBox1.Size = new System.Drawing.Size(147, 35);
            this.skinComboBox1.TabIndex = 2;
            this.skinComboBox1.WaterText = "";
            // 
            // listConfig
            // 
            this.listConfig.AllowColumnReorder = true;
            this.listConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listConfig.DoubleClickActivation = false;
            this.listConfig.FullRowSelect = true;
            this.listConfig.Location = new System.Drawing.Point(3, 3);
            this.listConfig.Name = "listConfig";
            this.listConfig.Size = new System.Drawing.Size(288, 260);
            this.listConfig.TabIndex = 0;
            this.listConfig.UseCompatibleStateImageBehavior = false;
            this.listConfig.View = System.Windows.Forms.View.Details;
            // 
            // ListCardDev
            // 
            this.ListCardDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ListCardDev.Location = new System.Drawing.Point(4, 2);
            this.ListCardDev.Name = "ListCardDev";
            this.ListCardDev.OwnerDraw = true;
            this.ListCardDev.Size = new System.Drawing.Size(751, 258);
            this.ListCardDev.TabIndex = 0;
            this.ListCardDev.UseCompatibleStateImageBehavior = false;
            this.ListCardDev.View = System.Windows.Forms.View.Details;
            // 
            // TabCard
            // 
            this.TabCard.BackColor = System.Drawing.Color.SkyBlue;
            this.TabCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabCard.Font = new System.Drawing.Font("华文宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TabCard.Location = new System.Drawing.Point(100, 0);
            this.TabCard.Name = "TabCard";
            this.TabCard.Size = new System.Drawing.Size(758, 577);
            this.TabCard.TabIndex = 1;
            this.TabCard.TabItemImage = null;
            this.TabCard.Text = "内镜卡";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(866, 609);
            this.Controls.Add(this.TabCardControl);
            this.Name = "FormConfig";
            this.Text = "洗消配置";
            this.Load += new System.EventHandler(this.FormConfig_Load);
            this.TabCardControl.ResumeLayout(false);
            this.TabCardDev.ResumeLayout(false);
            this.skinPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTabControl TabCardControl;
        private CCWin.SkinControl.SkinTabPage TabCardDev;
        private CCWin.SkinControl.SkinTabPage TabCard;
        private CCWin.SkinControl.SkinListView ListCardDev;
        private CCWin.SkinControl.SkinPanel skinPanel1;
        private CCWin.SkinControl.SkinTextBox skinTextBox1;
        private CCWin.SkinControl.SkinComboBox skinComboBox1;
        private ListViewEx.ListViewEx listConfig;
    }
}

