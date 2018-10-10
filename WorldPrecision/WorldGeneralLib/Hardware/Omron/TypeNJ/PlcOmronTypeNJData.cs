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

namespace WorldGeneralLib.Hardware.Omron.TypeNJ
{
    public class PlcOmronTypeNJData : HardwareData
    {
        private string _strIpAddress;
        public List<PlcScanItems> listScanItems;
        [XmlIgnore]
        public Dictionary<string, PlcScanItems> dicScanItems;
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

        public PlcOmronTypeNJData()
        {
            listScanItems = new List<PlcScanItems>();
            dicScanItems = new Dictionary<string, PlcScanItems>();
            _strIpAddress = "192.168.250.1";
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
