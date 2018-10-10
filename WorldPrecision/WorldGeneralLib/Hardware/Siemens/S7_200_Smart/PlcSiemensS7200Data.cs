using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;
using System.Net;
using System.Windows.Forms;

namespace WorldGeneralLib.Hardware.Siemens.S7_200_Smart
{
    public class PlcSiemensS7200Data : HardwareData
    {
        private string _strIpAddress;
        public List<PlcScanItems> m_ScanDataList;
        [XmlIgnore]
        public Dictionary<string, PlcScanItems> m_scanDictionary;
        //[XmlIgnore]
        //public IPAddress ipAddress;
        [XmlIgnore]
        public int nplcindex;

        [CategoryAttribute("Communication")]
        [Browsable(true)]
        [Description("IP Address")]
        public string IP
        {
            get { return _strIpAddress; }
            set { _strIpAddress = value; }
        }

        public PlcSiemensS7200Data()
        {
            m_ScanDataList = new List<PlcScanItems>();
            m_scanDictionary = new Dictionary<string, PlcScanItems>();
            //ipAddress = IPAddress.Parse("192.168.250.1");
            _strIpAddress = "192.168.250.1";
            nplcindex = 0;
        }

        public override void DataInit()
        {
            try
            {
                if (null == m_ScanDataList)
                    return;
                if (m_ScanDataList.Count <= 0)
                    return;

                m_scanDictionary = m_ScanDataList.ToDictionary(p => p.strName);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
