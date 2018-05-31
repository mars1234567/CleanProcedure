using audiotest;
using CleanProcedure.UDP;
using Decontamination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using udp_csharp;
using udp_csharp.include;

namespace CleanProcedure
{
    public class CleanManager
    {
        UdpServer server;//udp socket 服务
        Task task;//洗消流程
        PerformManager perform;//界面管理

        public void Start()
        {
            //udp socket server 配置
            int sport = Convert.ToInt32(ConfigMgr.GetAppConfig("UdpServerPort"));
            int cport = Convert.ToInt32(ConfigMgr.GetAppConfig("UdpClientPort"));
            int maxClient =  Convert.ToInt32(ConfigMgr.GetAppConfig("UdpServermaxClient"));
            string ip = ConfigMgr.GetAppConfig("UdpServerIP");

            server = new UdpServer(ip,sport, cport);
            server.OnReceivedData += new EventHandler<SocketAsyncEventArgs>(server_OnDataReceived);
            server.OnSentData += new EventHandler<SocketAsyncEventArgs>(server_OnDataSending);
           //开始udp SOCKET 服务
            server.Start();
        }
        //初始化界面，把洗消步骤显示到窗口
        public void InitForm(Control form)
        {
            //消息对列的回调初始化
            BusinessInfoHelper.Instance.m_proc = ProcessDelegate;
            BusinessInfoHelper.Instance.Start();
            //洗消流程初始化
            task = new Task();
            perform = new PerformManager();

            task.InitProcedure();
            task.UpdateUIDelegate += perform.UpdataUIStatus;
            task.TaskCallBack += perform.Endupdate;
                       
            perform.SetCtrl(form);
            Dictionary<string, List<StepInfo>> infolist = new Dictionary<string, List<StepInfo>>();
            task.GetStepInfo(ref infolist);
            //初始化步骤显示列表
            perform.Init(infolist);

        }

        //消息队列的处理函数，处理内镜卡信息
        public  void ProcessDelegate(QueueInfo info)
        {
           string sResult = task.Handle(info.m_ClientIp, info.m_RecBuffer);           
          
            //刷卡器的显示和语音提示

           byte[] text = CardConfigCtrl.TextOut(sResult);
           server.Send(text, info.ClientIpPort);
           byte[] voice = CardConfigCtrl.TextOut(sResult);
           server.Send(text, info.ClientIpPort);
        }

        //udp socket 回调函数，接收客户端数据，把数据放到消息队列等待处理
        static void server_OnDataReceived(object sender, SocketAsyncEventArgs e)
        {
            string card = ConfigMgr.byteToHexStr(e.Buffer,e.BytesTransferred);
            BusinessInfoHelper.Instance.AddQueue(card, (e.RemoteEndPoint as IPEndPoint));  
        }
        //udp socket 回调函数，发送数据到客户端之后被调用
        static void server_OnDataSending(object sender, EventArgs e)
        {


        }
        public void sizeChange()
        {
            perform.SizeChange();
        }

        



    }
}
