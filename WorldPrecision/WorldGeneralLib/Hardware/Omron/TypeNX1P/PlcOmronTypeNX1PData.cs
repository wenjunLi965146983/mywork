using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Hardware.Omron.TypeNX1P
{
    public class PlcOmronTypeNX1PData:HardwareData
    {
        private string _strRemoteIpAddress;
        private string _strLocalIpAddredd;
        public List<PlcScanItems> listScanItems;
        [XmlIgnore]
        public Dictionary<string, PlcScanItems> dicScanItems;
        [XmlIgnore]
        public int nplcindex;

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        [Description("Remote IP Address")]
        public string RemoteIP
        {
            get { return _strRemoteIpAddress; }
            set { _strRemoteIpAddress = value; }
        }

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        [Description("Local IP Address")]
        public string LocalIP
        {
            get { return _strLocalIpAddredd; }
            set { _strLocalIpAddredd = value; }
        }

        public PlcOmronTypeNX1PData()
        {
            listScanItems = new List<PlcScanItems>();
            dicScanItems = new Dictionary<string, PlcScanItems>();
            _strRemoteIpAddress = "192.168.250.1";
            _strLocalIpAddredd = "192.168.250.2";
            nplcindex = 0;
        }

        public override void DataInit()
        {
            try
            {
                if (null == listScanItems)
                    return;
                if (listScanItems.Count <= 0)
                    return;

                dicScanItems = listScanItems.ToDictionary(p => p.strName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
