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

namespace Decontamination
{
    public partial class CleanForm : Form
    {
        //洗消流程管理
        CleanManager cm = new CleanManager();


        public CleanForm()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            cm.InitForm(this);//初始化界面
            cm.Start();
        }





    }
}
