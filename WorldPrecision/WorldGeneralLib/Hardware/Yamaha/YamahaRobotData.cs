using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware.Yamaha
{
    public class YamahaRobotData : HardwareData
    {
        public YamahaRobotData()
        {
            _strIpAddress = "192.168.0.250";
            Index = 0;
            Port = 23;
            ReadTimeout = 1000;
        }

        private string _strIpAddress;
        public string IP
        {
            get { return _strIpAddress; }
            set { _strIpAddress = value; }
        }

        private int _iPort;
        public int Port
        {
            get { return _iPort; }
            set { _iPort = value; }
        }

        private int _iReadTimeout;
        public int ReadTimeout
        {
            get { return _iReadTimeout; }
            set { _iReadTimeout = value; }
        }
    }
}
