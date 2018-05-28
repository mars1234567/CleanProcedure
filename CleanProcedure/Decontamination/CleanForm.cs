using CleanProcedure;
using EXControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;

namespace Decontamination
{
    public partial class CleanForm : CCSkinMain
    {
        //洗消流程管理
        CleanManager cm = new CleanManager();


        public CleanForm()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            cm.InitForm(this.tabmoniter);//初始化界面
           cm.Start();
        }

        private void tabManager_SizeChanged(object sender, EventArgs e)
        {
            cm.sizeChange();
        }

        private void tabsearch_Click(object sender, EventArgs e)
        {

        }





    }
}
