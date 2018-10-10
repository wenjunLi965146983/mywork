using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Network.TCPIP.TCPClient;

namespace WorldGeneralLib.Hardware.CodeReader.Keyence.SR700
{
    public class KeyenceSR700:HardwareBase,ICodeReader
    {
        public KeyenceSR700Data codeReaderData;
        private string _strRecvData;
        private object _objLock;
        private SerialPort _port;
        private Client _tcpClient;
        public KeyenceSR700(KeyenceSR700Data data)
        {
            codeReaderData = data;
            _strRecvData = "";
            _port = new SerialPort();
            _objLock = new object();
        }
        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = false;
            #region TCP/IP
            if(codeReaderData.protocol == Protocol.TcpIP)
            {
                if(_tcpClient != null)
                {
                    _tcpClient.Close();
                    _tcpClient = null;
                }
                _tcpClient = new Client();
                _tcpClient.IpAddress = codeReaderData.IP;
                _tcpClient.Port = codeReaderData.Port;
                _tcpClient.StartCode = codeReaderData.strStartCode;
                _tcpClient.EndCode = codeReaderData.strEndCode;
                _tcpClient.OnDataReceived += new EventDataReceived(tcpClient_OnDataReceived);

                _tcpClient.StartConnect();
                bInitOk = true;
            }
            #endregion
            #region RS232
            if(codeReaderData.protocol == Protocol.RS232)
            {
                if (_port.IsOpen)
                {
                    bInitOk = true;
                    return true;
                }
                try
                {
                    _port.PortName = codeReaderData.SerialPort;
                    _port.BaudRate = codeReaderData.Buad;
                    _port.DataBits = codeReaderData.DataBits;
                    _port.StopBits = codeReaderData.StopBits;
                    _port.Parity = codeReaderData.Parity;
                    _port.Open();
                    bInitOk = true;
                }
                catch (Exception)
                {
                    bInitOk = false;
                }
            }
            #endregion

            return bInitOk;
        }

        public override bool Close()
        {
            if (null == codeReaderData)
                return false;
            if(codeReaderData.protocol == Protocol.RS232)
            {
                if (_port.IsOpen)
                    _port.Close();
            }
            if(codeReaderData.protocol == Protocol.TcpIP)
            {
                if(null != _tcpClient)
                {
                    _tcpClient.Close();
                }
            }

            bInitOk = false;
            return bInitOk;
        }
        public override  bool IsConnected()
        {
            try
            {
                if (codeReaderData.protocol == Protocol.RS232)
                    return bInitOk;
                if (codeReaderData.protocol == Protocol.TcpIP)
                {
                    if (null != _tcpClient)
                        return _tcpClient.IsConnected;
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        
        /// <summary>
        /// 同步方式读取二维码
        /// </summary>
        /// <param name="strBarCode">读码结果</param>
        /// <param name="iTimeout">超时时间</param>
        /// <returns></returns>
        public CodeReaderRes Read(out string strBarCode)
        {
            strBarCode = string.Empty;
            try
            {
                if (null == codeReaderData)
                {
                    return CodeReaderRes.OTHERS;
                }
                codeReaderData.iTimeout = codeReaderData.iTimeout < 1 ? 500 : codeReaderData.iTimeout;
                codeReaderData.iTimeout = codeReaderData.iTimeout > 10 * 1000 ? 10 * 1000 : codeReaderData.iTimeout;
                return Read(out strBarCode, false, codeReaderData.iTimeout);
            }
            catch (Exception)
            {
                return CodeReaderRes.ERROR;
            }
        }

        /// <summary>
        /// 异步方式读取二维码
        /// </summary>
        /// <returns></returns>
        public CodeReaderRes Read()
        {
            string strBarCode = "";
            try
            {
                return Read(out strBarCode, true, 0);
            }
            catch (Exception)
            {
                return CodeReaderRes.ERROR;
            }
        }
        internal CodeReaderRes Read(out string strBarCode, bool bAsync, int iTimeout)
        {
            strBarCode = string.Empty;
            if (codeReaderData.protocol == Protocol.RS232)
            {
                return ReadByRS232(out strBarCode, codeReaderData.iTimeout);
            }
            if (codeReaderData.protocol == Protocol.TcpIP)
            {
                return ReadByTcpClient(out strBarCode, bAsync, iTimeout);
            }
            return CodeReaderRes.OTHERS;
        }
        public string RecvData
        {
            get { return _strRecvData; }
            set { _strRecvData = value; }
        }
        #region RS232
        internal CodeReaderRes ReadByRS232(out string strRecv, int iTimeout)
        {
            strRecv = string.Empty;
            if (!bInitOk)
            {
                return CodeReaderRes.INITFAIL;
            }
            HiPerfTimer hiPerfTimer = new HiPerfTimer();
            string strTemp = "";
            lock (_objLock)
            {
                try
                {
                    _port.ReadExisting();
                    System.Threading.Thread.Sleep(1);
                    _port.Write(codeReaderData.strTriggerCmd + "\r\n");
                    System.Threading.Thread.Sleep(1);
                    strTemp = _port.ReadExisting();
                    hiPerfTimer.Start();
                    while (true)
                    {
                        strTemp += _port.ReadExisting();
                        if (strRecv.Contains(codeReaderData.strEndCode))
                        {
                            strRecv = strTemp;
                            return CodeReaderRes.SUCCESS;
                        }
                        if (hiPerfTimer.TimeUp(iTimeout))
                        {
                            return CodeReaderRes.TIMEOUT;
                        }
                        System.Threading.Thread.Sleep(10);
                    }
                }
                catch (Exception)
                {
                    return CodeReaderRes.ERROR;
                }
            }
        }
        #endregion

        #region TCP/IP
        internal CodeReaderRes ReadByTcpClient(out string strRecv ,bool bAsync , int iTimeout)
        {
            strRecv = string.Empty;
            _strRecvData = string.Empty;
            try
            {
                if(null == codeReaderData || null == _tcpClient || !_tcpClient.IsConnected)
                {
                    return CodeReaderRes.INITFAIL;
                }
                if(!_tcpClient.Send(codeReaderData.strTriggerCmd + "\r\n"))
                {
                    return CodeReaderRes.ERROR;
                }
                if(bAsync)
                {
                    return CodeReaderRes.SUCCESS;
                }

                //sync
                HiPerfTimer hiPerfTimer = new HiPerfTimer();
                hiPerfTimer.Start();
                while(true)
                {
                    if(!string.IsNullOrEmpty(_strRecvData))
                    {
                        break;
                    }
                    if(hiPerfTimer.TimeUp((double)((double)iTimeout/1000.0)))
                    {
                        return CodeReaderRes.TIMEOUT;
                    }
                }
                strRecv = _strRecvData;
                return CodeReaderRes.SUCCESS;
            }
            catch (Exception)
            {
                return CodeReaderRes.ERROR;
            }
        }
        private void tcpClient_OnDataReceived(string strData)
        {
            _strRecvData = strData;
        }
        #endregion
    }
}
