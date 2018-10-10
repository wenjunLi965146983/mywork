using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Net;

namespace WorldGeneralLib.Hardware.Yamaha
{
    public class EtherNetBase
    {
        public TcpClient tcpClient;
        public NetworkStream networkStream;
        public int iTimeout = 1000;
        public EtherNetBase(int iTimeout)
        {
            tcpClient = new TcpClient();
            this.iTimeout = iTimeout;
        }

        internal bool PintCheck(string strIP, int iTimeOut)
        {
            try
            {
                Ping ping = new Ping();
                PingReply pr = ping.Send(strIP, iTimeOut);
                if (pr.Status == IPStatus.Success)
                    return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        internal short SendData(byte[] byteArraySendData, int iLength)
        {
            try
            {
                try
                {
                    byte[] byteArrayReceiveData = new byte[256];
                    networkStream.ReadTimeout = 1;
                    networkStream.Read(byteArrayReceiveData, 0, byteArrayReceiveData.Length);
                }
                catch (Exception)
                {
                }
                networkStream.ReadTimeout = iTimeout;
                networkStream.Write(byteArraySendData, 0, iLength);
                return (short)iLength;
            }
            catch (Exception)
            {
            }
            return -1;
        }

        internal short ReceiveData(ref byte[] byteArrayReceiveData)
        {
            try
            {
                int len = -1;
                string strTemp = "END";
                while(strTemp.Contains("END"))      //将收到的END信息丢弃
                {
                    len = networkStream.Read(byteArrayReceiveData, 0, byteArrayReceiveData.Length);
                    strTemp = Encoding.Default.GetString(byteArrayReceiveData);
                }
                return (short)len;
            }
            catch
            {
            }
            return -1;
        }

        //internal short ReceiveData(byte[] byteArrayReceiveData)
        //{
        //    int len = 0;
        //    int index = 0;

        //    do
        //    {
        //        try
        //        {
        //            len = networkStream.Read(byteArrayReceiveData, index, byteArrayReceiveData.Length - index);
        //            if (0 == len)
        //                return -1;
        //            else
        //                index += len;
        //        }
        //        catch (Exception)
        //        {
        //            return -1;
        //        }

        //    } while (index < byteArrayReceiveData.Length);

        //    return (short)len;
        //}
    }
}
