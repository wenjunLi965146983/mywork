using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;

namespace OmronFins.Net
{
    public class EthernetBase
    {
        //TCP
        public TcpClient Client;
        public NetworkStream Stream;

        //UDP
        public Socket udpSocket;
        public EndPoint Remote;
        public IPEndPoint remoteIP;

        public ProtocolType protocolType;
        internal byte pcNode, plcNode;

        //检查PLC链接状况
        public EthernetBase()
        {
            Client = new TcpClient();
        }

        internal bool PingCheck(string ip,int timeOut)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pr = ping.Send(ip, timeOut);
                if (pr.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch (Exception )
            {
                return false;
            }
        }

        //内部方法，发送数据
        internal short SendData(byte[] sd , int length)
        {
            try
            {
                if (protocolType == ProtocolType.Tcp)
                    Stream.Write(sd, 0, length);
                else if (protocolType == ProtocolType.Udp)
                    udpSocket.SendTo(sd,length,SocketFlags.None ,remoteIP);
                else
                    return - 1;

                return 0;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        //内部方法，接收数据
        internal  short ReceiveData(byte[] rd)
        {
            if (protocolType == ProtocolType.Tcp)
            {
                #region TCP
                int len = 0;
                int index = 0;
                do
                {
                    try
                    {
                        len = Stream.Read(rd, index, rd.Length - index);
                        if (len == 0)
                            return -1;
                        else
                            index += len;
                    }
                    catch (Exception)
                    {
                        return -1;
                    }
                } while (index < rd.Length);

                return 0;
                #endregion
            }
            else if (protocolType == ProtocolType.Udp)
            {
                #region UDP
                try
                {
                    int len = udpSocket.ReceiveFrom(rd, ref Remote);
                    return len > 0 ? (short)0 : (short)-1;
                }
                catch (Exception)
                {
                    return -1;
                }
                #endregion
            }
            return -1;
        }
    }
}
