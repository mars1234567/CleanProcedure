using audiotest;
using CleanProcedure.UDP;
using Decontamination;
using NetFrame.Net.UDP.Sock.Asynchronous;
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
        AsyncSocketUDPServer server;//udp socket 服务
        Task task;//洗消流程
        PerformManager perform;//界面管理
        public Dictionary<string, string> Cardlist = new Dictionary<string, string>();
        public void Start()
        {
            //udp socket server 配置
            int sport = Convert.ToInt32(ConfigMgr.GetAppConfig("UdpServerPort"));
            int cport = Convert.ToInt32(ConfigMgr.GetAppConfig("UdpClientPort"));
            int maxClient =  Convert.ToInt32(ConfigMgr.GetAppConfig("UdpServermaxClient"));
            string ip = ConfigMgr.GetAppConfig("UdpServerIP");

            server = new AsyncSocketUDPServer(IPAddress.Parse(ip), sport,cport, maxClient);
            server.DataReceived += server_OnDataReceived;

            //开始udp SOCKET 服务
            server.Start();
        }
        //初始化界面，把洗消步骤显示到窗口
        public void InitForm(Control form)
        {
            //消息对列的回调初始化
            BusinessInfoHelper.Instance.m_proc = ProcessDelegate;
            BusinessInfoHelper.Instance.Start();

            DataInfo.GetCardListInfo(ref Cardlist);
            //洗消流程初始化
            task = new Task();
            perform = new PerformManager();

            task.InitProcedure();
            task.UpdateUIDelegate += perform.UpdataUIStatus;
            task.TaskCallBack += perform.Endupdate;
                       
            perform.SetCtrl(form,Cardlist);
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
        static void server_OnDataReceived(object sender, AsyncSocketUDPEventArgs e)
        {
            string card = ConfigMgr.byteToHexStr(e._state.buffer,e._state.buffer.Length);
            BusinessInfoHelper.Instance.AddQueue(card, (e._state.remote as IPEndPoint));  
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
