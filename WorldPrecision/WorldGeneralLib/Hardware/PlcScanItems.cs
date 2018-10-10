using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmronFins.Net;
using System.Xml.Serialization;

namespace WorldGeneralLib.Hardware
{
    public class PlcScanItems
    {
        public string strName { get; set; }
        public PlcMemory AddressType { get; set; }
        public string Address { get; set; }
        public DataType DataType { get; set; }
        [XmlIgnore]

        public string strValue { get; set; }

        //主从站ID
        private int _id;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _index;
        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        private bool _bRefresh;
        public bool Refresh
        {
            get { return _bRefresh; }
            set { _bRefresh = value; }
        }

        private byte _byteLen;
        public byte Length
        {
            get { return _byteLen; }
            set { _byteLen = value; }
        }

        public PlcScanItems()
        {
            strName = string.Empty;
            AddressType = PlcMemory.WR;
            Address = "0";
            DataType = DataType.BIT;
            strValue = "0";
        }
    }
}
