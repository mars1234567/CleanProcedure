using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using CA_sign;

namespace udp_csharp
{


    public class UdpReceiveSocket
    {
        /// <summary>
        /// 接收用Socket;
        /// </summary>
        private Socket receiveSocket;

        private SocketAsyncEventArgs receiveSocketArgs;

        private IPEndPoint localEndPoint;

        private byte[] receivebuffer;
        /// <summary>
        /// 接收到数据包时触发
        /// </summary>
        public event EventHandler<SocketAsyncEventArgs> OnDataReceived;

        /// <summary>
        /// 初始化Socket 和 地址。
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="socket"></param>
        public UdpReceiveSocket(string ip,int sport,int cport)
        {
            try
            {
                receiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                localEndPoint = new IPEndPoint(IPAddress.Parse(ip), sport);

                receiveSocket.Bind(localEndPoint);

                receivebuffer = new byte[200];
                receiveSocketArgs = new SocketAsyncEventArgs();
                receiveSocketArgs.RemoteEndPoint = new IPEndPoint(IPAddress.Any, cport);
                receiveSocketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(receiveSocketArgs_Completed);
                receiveSocketArgs.SetBuffer(receivebuffer, 0, receivebuffer.Length);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }



        /// <summary>
        /// 开始接收数据
        /// </summary>
        public void StartReceive()
        {
            try
            {
                if (!receiveSocket.ReceiveFromAsync(receiveSocketArgs))
                {
                    processReceived(receiveSocketArgs);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }

        }


        void receiveSocketArgs_Completed(object sender, SocketAsyncEventArgs e)
        {
            try
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.ReceiveFrom:
                        this.processReceived(e);
                        break;
                    default:
                        throw new ArgumentException("The last operation completed on the socket was not a receive");
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }

        /// <summary>
        /// 接收完成时处理请求。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void processReceived(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    if (OnDataReceived != null)
                    {
                        //不要进行耗时操作
                        OnDataReceived(receiveSocket, e);
                    }
                }
                StartReceive();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(Exception), ex);
            }
        }
    }
}
