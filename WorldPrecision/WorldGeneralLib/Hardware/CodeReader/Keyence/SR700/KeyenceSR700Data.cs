using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace WorldGeneralLib.Hardware.CodeReader.Keyence.SR700
{
    public class KeyenceSR700Data:HardwareData
    {
        private string _strIP;
        public string IP
        {
            get { return _strIP; }
            set { _strIP = value; }
        }

        private int _iPort;
        public int Port
        {
            get { return _iPort; }
            set { _iPort = value; }
        }
        private string _strSerialPort;
        public string SerialPort
        {
            get { return _strSerialPort; }
            set { _strSerialPort = value; }
        }

        private int _iBuad;
        public int Buad
        {
            get { return _iBuad; }
            set { _iBuad = value; }
        }

        private int _iDataBits;
        public int DataBits
        {
            get { return _iDataBits; }
            set { _iDataBits = value; }
        }

        private Parity _parity;
        public Parity Parity
        {
            get { return _parity; }
            set { _parity = value; }
        }

        private StopBits _stopBits;
        public StopBits StopBits
        {
            get { return _stopBits; }
            set { _stopBits = value; }
        }

        public string strStartCode;
        public string strEndCode;
        public string strTriggerCmd;
        public int iTimeout;
        public Protocol protocol;
        public KeyenceSR700Data()
        {
            _strIP = "192.168.0.1";
            _iPort = 9000;
            SerialPort = "";
            Buad = 9600;
            DataBits = 8;
            StopBits = StopBits.One;
            Parity = Parity.None;

            strStartCode = "";
            strEndCode = "";
            strTriggerCmd = "cmd";
            iTimeout = 500;
            protocol = Protocol.RS232;
        }
    }
}
