using CleanProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using udp_csharp;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            UdpServer server = new UdpServer(1, 1, 1);
            //    server.OnNewClient += new EventHandler<SocketAsyncEventArgs>(server_OnNewClientAccept);
            server.OnReceivedData += new EventHandler<SocketAsyncEventArgs>(server_OnDataReceived);
            //   server.OnSentData += new EventHandler<SocketAsyncEventArgs>(server_OnDataSending);

            server.Start();


            Console.ReadLine();
        }
        static void server_OnDataReceived(object sender, EventArgs e)
        {


        }
    }
}
