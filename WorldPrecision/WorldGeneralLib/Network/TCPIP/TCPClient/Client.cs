using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorldGeneralLib.Network.TCPIP.TCPClient
{
    public delegate void EventDataReceived(string strData);
    public delegate void EventErrorReceived(Exception e);
    public partial class Client : Component
    {
        private byte[] _arrayByte;
        private bool _bIsConnecting;
        public string _strRemain;
        private TcpClient _tcpClient;
        private object _objLock;
        private NetworkStream _networkStream;
        public event EventDataReceived OnDataReceived = null;
        public event EventErrorReceived OnErrorReceived = null;

        public Client()
        {
            InitializeComponent();
            _bIsConnecting = false;
            _bIsConnected = false;
            _tcpClient = null;
            _strRemain = "";
            _strStartCode = "";
            _strEndCode = "";
            _objLock = new object();
        }

        public Client(IContainer container):this()
        {
            container.Add(this);
        }

        private string _strIP;
        public string IpAddress
        {
            get { return _strIP; }
            set { _strIP = value; }
        }

        private int _iPort;
        public int Port
        {
            get
            {
                return _iPort;
            }
            set
            {
                if (value < 0)
                {
                    return;
                }

                if (_iPort == value)
                {
                    return;
                }

                if (_bIsConnected)
                {
                    throw new Exception("Invalid attempt to change port while still open.\nPlease close port before changing.");
                }
                _iPort = value;
            }
        }

        private string _strStartCode;
        public string StartCode
        {
            get { return _strStartCode; }
            set { _strStartCode = value; }
        }

        private string _strEndCode;
        public string EndCode
        {
            get { return _strEndCode; }
            set { _strEndCode = value; }
        }

        private bool _bIsConnected;
        [Browsable(false)]
        public bool IsConnected
        {
            get
            {
                return _bIsConnected;
            }
        }

        public void StartConnect()
        {
            if (_bIsConnecting)
                return;
            try
            {
                if(null != _tcpClient)
                {
                    _tcpClient.Close();
                    _tcpClient = null;
                }
                _tcpClient = new TcpClient();
                _bIsConnecting = true;
                _tcpClient.BeginConnect(IPAddress.Parse(IpAddress),Port,new AsyncCallback(CallbackConnected),(object)_tcpClient);
            }
            catch
            {
                _bIsConnecting = false;
            }
        }

        public void Close()
        {
            if (null != _tcpClient)
            {
                _tcpClient.Close();
                _tcpClient = null;
            }
        }

        private void CallbackConnected(IAsyncResult Ar)
        {
            try
            {
                if(!_tcpClient.Connected)
                {
                    _bIsConnecting = false;
                    _bIsConnected = false;
                    StartConnect();
                    return;
                }

                _bIsConnecting = false;
                _bIsConnected = true;
                _tcpClient.EndConnect(Ar);
                _arrayByte = new byte[this._tcpClient.ReceiveBufferSize];
                _networkStream = _tcpClient.GetStream();
                _networkStream.BeginRead(_arrayByte,0,_arrayByte.Length,new AsyncCallback(CallbackReceived), (object)_networkStream);
            }
            catch (Exception)
            {
                _bIsConnecting = false;
                _bIsConnected = false;
                StartConnect();
            }
        }

        private void CallbackReceived(IAsyncResult Ar)
        {
            int count;
            try
            {
                _networkStream = (NetworkStream)Ar.AsyncState;
                count = this._networkStream.EndRead(Ar);
            }
            catch
            {
                count = 0;
            }
            if (count == 0)
            {
                this._bIsConnected = false;
                this.StartConnect();
            }
            else
            {
                try
                {
                    this.JudgeData(_strRemain + Encoding.ASCII.GetString(_arrayByte, 0, count));
                    this._networkStream.BeginRead(_arrayByte, 0, _arrayByte.Length, new AsyncCallback(this.CallbackReceived), (object)this._networkStream);
                }
                catch (Exception)
                {
                }
            }
        }

        private void CallbackDataWritten(IAsyncResult Ar)
        {
            ((TcpClient)Ar.AsyncState).GetStream().EndWrite(Ar);
        }

        private void JudgeData(string strData)
        {
            if(string.IsNullOrEmpty(StartCode) && string.IsNullOrEmpty(EndCode))
            {
                OnDataReceived?.Invoke(strData);
            }
            else if(string.IsNullOrEmpty(StartCode) && EndCode.Equals("CRLF"))
            {
                while (true)
                {
                    if (strData.IndexOf("\r\n") > -1)
                    {
                        try
                        {
                            string str = strData.Substring(0, strData.IndexOf("\r\n"));
                            OnDataReceived?.Invoke(strData);
                            _strRemain = strData.Substring(strData.IndexOf("\r\n") + 2);
                            strData = _strRemain;
                        }
                        catch
                        {
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                _strRemain = strData;
            }
            else if(string.IsNullOrEmpty(StartCode) && EndCode.Equals("CR"))
            {
                while (true)
                {
                    if (strData.IndexOf("\r") > -1)
                    {
                        try
                        {
                            string str = strData.Substring(0, strData.IndexOf("\r"));
                            OnDataReceived?.Invoke(strData);
                            _strRemain = strData.Substring(strData.IndexOf("\r") + 1);
                            strData = _strRemain;
                        }
                        catch
                        {
                            return;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                _strRemain = strData;
            }
            else if(string.IsNullOrEmpty(StartCode) && EndCode.Equals("ETX"))
            {
                while (true)
                {
                    char ch = '\x03';
                    if (strData.IndexOf(ch) > -1)
                    {
                        string str = strData.Substring(0, strData.IndexOf(ch));
                        OnDataReceived?.Invoke(str);
                        _strRemain = strData.Substring(strData.IndexOf(ch) + 1);
                        strData = _strRemain;
                    }
                    else
                    {
                        break;
                    }
                }
                _strRemain = strData;
            } 
            else if (StartCode.Equals("STX") && EndCode.Equals("ETX"))
            {
                while (true)
                {
                    char ch1 = '\x0002';
                    char ch2 = '\x0003';
                    if (strData.IndexOf(ch1) > -1 && strData.IndexOf(ch2) > -1)
                    {
                        string str = strData.Substring(strData.IndexOf(ch1), strData.IndexOf(ch2));
                        OnDataReceived?.Invoke(str);
                        _strRemain = strData.Substring(strData.IndexOf(ch2) + 1);
                        strData = _strRemain;
                    }
                    else
                        break;
                }
                _strRemain = strData;
            }
            else
            {
                while (true)
                {
                    if (strData.IndexOf(StartCode) > -1 && strData.IndexOf(EndCode) > -1)
                    {
                        string str = strData.Substring(strData.IndexOf(StartCode), strData.IndexOf(EndCode));
                        OnDataReceived?.Invoke(str);
                        _strRemain = strData.Substring(strData.IndexOf(EndCode) + EndCode.Length);
                        strData = _strRemain;
                    }
                    else
                    {
                        break;
                    }     
                }
                _strRemain = strData;
            }
        }

        public bool Send(string strSend)
        {
            if (!_bIsConnected)
                return false;
            try
            {
                if (!_bIsConnected || _tcpClient == null || !_tcpClient.Connected)
                    return false;

                byte[] bytes = Encoding.ASCII.GetBytes(strSend);
                if (bytes == null)
                {
                    return false;
                }
                    
                lock (_objLock)
                {
                    _tcpClient.GetStream().BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(CallbackDataWritten), (object)_tcpClient);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
