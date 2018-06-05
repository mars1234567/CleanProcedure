using CleanProcedure;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CCWin;
using NetFrame.Net.UDP.Sock.Asynchronous;
using System.Net;

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
            UpdateCleanCardListview();
        }

        private void ListCleanedCard_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.tabManager.SelectedIndex)
            {

                case 1:
                    UpdateCleanCardListview();


                    break;
            } 
        }
        public delegate void UpdateTaskList( );//声明一个更新主线程的委托
        public void UpdateCleanCardListview()
        {
            if (ListCleanedCard.InvokeRequired)
            {
                UpdateTaskList updatedelegate = new UpdateTaskList(UpdateCleanCardListview);
                ParentForm.Invoke(updatedelegate, new object[] { });
            }
            else
            {
                ListCleanedCard.Items.Clear();
                List<CleanListView> ul= new List<CleanListView>();
                DataInfo.GetCleanListInfo(ref ul);
                
                foreach(var i in ul)
                {
                  ListViewItem col =  ListCleanedCard.Items.Add(i.Sequence.ToString());
                  col.SubItems.Add(i.time.ToString());
                  string cardname = "";
                  string workName = i.WorkCard;
                  if (cm.Cardlist.ContainsKey(i.CleanCard))
                      cardname = cm.Cardlist[i.CleanCard];
                  if (cm.Cardlist.ContainsKey(i.WorkCard))
                      workName = cm.Cardlist[i.WorkCard];

                  col.SubItems.Add(i.CleanCard);
                  col.SubItems.Add(cardname);
                  col.SubItems.Add(workName);
                  col.SubItems.Add(i.cleanprc);


                }
            }
        }




    }
}
