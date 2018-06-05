using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CleanProcedure.UDP
{
    public class ThreadPing
    {
        private string[] _target;
        private int _timeOutVal;

        public ThreadPing(string[] target, int timeOutVal)
        {
            _target = target;
            _timeOutVal = timeOutVal;
        }

        public void PingWorker()
        {
            for (; ; )
            {
                int retval = IcmpPacket.PingHost(_target, _timeOutVal);
                Console.WriteLine(_target[0] + " status:" + retval.ToString());
                Thread.Sleep(1000);
            }
        }
    }

    public class IcmpPacket
    {
        private Byte _type;
        private Byte _subCode;
        private UInt16 _checkSum;
        private UInt16 _identifier;
        private UInt16 _sequenceNumber;
        private Byte[] _data;

        private static UInt16 UInt16_Seq = 0;

        public IcmpPacket(Byte type, Byte subCode, UInt16 checkSum, UInt16 identifier,UInt16 sequenceNumber, int dataSize)
        {
            _type = type;
            _subCode = subCode;
            _checkSum = checkSum;
            _identifier = identifier;
            _sequenceNumber = sequenceNumber;
            _data = new Byte[dataSize];
            for (int i = 0; i < dataSize; i++)
            {
                _data[i] = (Byte)'#';
            }

        }


        public UInt16 CheckSum
        {
            get
            {
                return _checkSum;
            }
            set
            {
                _checkSum = value;
            }
        }

        public int ConverToByte(Byte[] buffer)
        {
            Byte[] b_type = new Byte[1] { _type };
            Byte[] b_subCode = new Byte[1] { _subCode };
            Byte[] b_cksum = BitConverter.GetBytes(_checkSum);
            Byte[] b_id = BitConverter.GetBytes(_identifier);
            Byte[] b_seq = BitConverter.GetBytes(_sequenceNumber);

            int i = 0;
            Array.Copy(b_type, 0, buffer, i, b_type.Length);
            i += b_type.Length;
            Array.Copy(b_subCode, 0, buffer, i, b_subCode.Length);
            i += b_subCode.Length;
            Array.Copy(b_cksum, 0, buffer, i, b_cksum.Length);
            i += b_cksum.Length;
            Array.Copy(b_id, 0, buffer, i, b_id.Length);
            i += b_id.Length;
            Array.Copy(b_seq, 0, buffer, i, b_seq.Length);
            i += b_seq.Length;
            Array.Copy(_data, 0, buffer, i, _data.Length);
            i += _data.Length;

            return i;
        }

        public UInt16 GetSequenceNum(Byte[] buffer)
        {
            UInt16 i_seq = 0;
            try
            {
                i_seq = BitConverter.ToUInt16(buffer, 26);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return i_seq;
        }


        public static UInt16 SumofCheck(UInt16[] buffer)
        {
            int sum = 0;
            for (int i = 0; i < buffer.Length; i++)
                sum += (int)buffer[i];
            sum = (sum >> 16) + (sum & 0xffff);
            sum += (sum >> 16);

            return (UInt16)(~sum);
        }

        public static int PingHost(string[] hostclient, int timeOutVal)
        {
            const int ICMP_ECHO = 8;
            const int PING_ERR = -1;
            const int TIME_OUT = 200;

            int retval = 0;

            Socket sock = new Socket(AddressFamily.InterNetwork,SocketType.Raw, ProtocolType.Icmp);

            sock.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.SendTimeout, TIME_OUT);
            sock.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.ReceiveTimeout, TIME_OUT);

            IPHostEntry hostInfo = null;
            try
            {
                IPAddress ipaddr = IPAddress.Parse(hostclient[0]);
                hostInfo = Dns.GetHostEntry(hostclient[0]);
            }
            catch (Exception)
            {
                retval = PING_ERR;
                goto exit;
            }

            EndPoint hostPoint = (EndPoint)new IPEndPoint(hostInfo.AddressList[0], 0);

            IPHostEntry clientInfo = null;
            try
            {
                if (hostclient.Length > 1)
                    clientInfo = Dns.GetHostByAddress(hostclient[1]);
                else
                    clientInfo = Dns.GetHostEntry(Dns.GetHostName());
            }
            catch (Exception)
            {
                retval = PING_ERR;
                goto exit;
            }

            EndPoint clientPoint = (EndPoint)new IPEndPoint(clientInfo.AddressList[0], 0);

            int dataSize = 32;
            int packetSize = dataSize + 8;

            IcmpPacket packet = new IcmpPacket(ICMP_ECHO, 0, 0, 45,UInt16_Seq++, dataSize);

            Byte[] buffer = new Byte[packetSize];
            int index = packet.ConverToByte(buffer);
            if (index != packetSize)
            {
                retval = PING_ERR;
                goto exit;
            }

            int count = (int)Math.Ceiling((Double)index / 2);
            UInt16[] buffer2 = new UInt16[count];

            index = 0;
            for (int i = 0; i < count; i++)
            {
                buffer2[i] = BitConverter.ToUInt16(buffer, index);
                index += 2;
            }

            packet.CheckSum = IcmpPacket.SumofCheck(buffer2);

            Byte[] sendData = new Byte[packetSize];
            index = packet.ConverToByte(sendData);

            if (index != packetSize)
            {
                retval = PING_ERR;
                goto exit;
            }

            for (int i = 0; i < 5; i++)
            {
                int nBytes = 0;
                int startTime = Environment.TickCount;

                try
                {
                    if ((nBytes = sock.SendTo(sendData, packetSize,SocketFlags.None, (EndPoint)hostPoint)) == -1)
                    {
                        retval = PING_ERR;
                        goto exit;
                    }
                    //                  Console.WriteLine(nBytes.ToString());
                }
                catch (Exception ex)
                {
                     
                    return -1;
                }

                Byte[] receiveData = new Byte[256];
                nBytes = 0;
                int timeout = 0;
                int timeConsume = 0;
                while (true)
                {
                    try
                    {

                        nBytes = sock.ReceiveFrom(receiveData, 256,SocketFlags.None, ref  clientPoint);

                        if (nBytes > 0)
                        {
                            if (packet._sequenceNumber == packet.GetSequenceNum(receiveData))
                            {
                                retval = 0;
                                goto exit;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return -1;
                    }
                    if (nBytes == -1)
                    {
                        retval = PING_ERR;
                        goto exit;
                    }
                    else if (nBytes > 0)
                    {
                        timeConsume = System.Environment.TickCount - startTime;
                        retval = timeConsume;

                    }

                    timeout = System.Environment.TickCount - startTime;
                    if (timeout > timeOutVal)
                    {
                        retval = PING_ERR;
                        goto exit;
                    }

                }
            }

        exit: sock.Close();
            return retval;

        }

    }
}
