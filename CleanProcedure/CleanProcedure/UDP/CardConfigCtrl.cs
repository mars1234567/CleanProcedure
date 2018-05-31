using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using udp_csharp.include;

namespace CleanProcedure.UDP
{
    public class CardConfigCtrl
    {
        //读卡器
        string LocalPort;
        string LocalIP;
        string LocalIPmask;
        string LocalIPgateway;
        string LocalMac;
        //服务器
        string RemoteIP;
        string RemotePort;
        bool bVoice;
       public void MacSet()
        {
           byte[] data = new byte[36];
           data[0] = 0xE0;
           data[1] = 0xE0;
           data[2] = 0xE0;
           data[3] = 0xE0;
           data[4] = 0x01;
           int index = 5;
           byte[] ip = new byte[4];

           ConfigMgr.IPtoHexStr(LocalIP,ref ip);
           Array.Copy(ip, 0, data, index, 4);

           index += 4;
           ConfigMgr.IPtoHexStr(LocalIPmask, ref ip);
           Array.Copy(ip, 0, data, index, 4);

           index += 4;
           ConfigMgr.IPtoHexStr(LocalIPgateway, ref ip);
           Array.Copy(ip, 0, data, index, 4);

           index += 4;
           ConfigMgr.IPtoHexStr(RemoteIP, ref ip);
           Array.Copy(ip, 0, data, index, 4);

           index += 4;
           byte[] port = new byte[2];
           port = ConfigMgr.strToToHexByte(LocalPort);
           Array.Copy(port, 0, data, index, 2);

           index += 2;
           port = ConfigMgr.strToToHexByte(RemotePort);
           Array.Copy(port, 0, data, index, 2);

           index += 2;
           byte[] MAC = new byte[6];
           ConfigMgr.MACtoHexStr(LocalMac,ref MAC);
           Array.Copy(MAC, 0, data, index, 6);

           index += 6;
           if(bVoice)
           {
               data[index] = 0xFF;
           }
           else
           {
               data[index] = 0x00;
           }

           index++;
           data[index++] = 0x0E;
           data[index++] = 0x0E;
           data[index++] = 0x0E;
           data[index] = 0x0E;

        }

       public void MacGet()
       {
           byte[] data = new byte[7];
           data[0] = 0xE6;
           data[1] = 0xE3;
           data[2] = 0xE9;
           data[3] = 0xE3;
           data[4] = 0xE9;
           data[5] = 0xE0;
           data[6] = 0xE8;
           data[7] = 0xE6;


       }

       public static byte[] TextOut(string text)
       {
           byte[] data = new byte[67];
           
           data[0] = 0xAA;
           data[1] = 0xFF;
           data[2] = 0x03;

           int index = 3;
           
           int len =text.Length;
           byte[] port = new byte[len];
           port = ConfigMgr.strToToHexByte(text);
           Array.Copy(port, 0, data, index, len);


           return data;

       }
       public static byte[] VoiceOut(string text)
        {
            int len = text.Length;
            byte[] data = new byte[len+4];

            data[0] = 0xAA;
            data[1] = 0xFF;
            data[2] = 0x05;
            data[3] = 0x04;

            byte[] port = new byte[len];
            port = ConfigMgr.strToToHexByte(text);
            Array.Copy(port, 0, data, 4, len);
           return data ;
        }

       public static byte[] Test()
        {        
            byte[] data = new byte[3];

            data[0] = 0xAA;
            data[1] = 0xFF;
            data[2] = 0x01;

            return data;
        }
    }
}
