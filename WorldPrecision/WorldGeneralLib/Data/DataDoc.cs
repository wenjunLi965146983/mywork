using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Forms.Design;
using System.Drawing.Design;
using System.Threading.Tasks;

using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WorldGeneralLib.Data
{
    public enum DataType
    {
        String = 0,
        Int,
        Short,
        Double,
        Bool
    }
    public class DataItem
    {
        public string strItemName;
        public string strItemRemark;
        public DataType dataType;
        public object objValue;
        public bool bVisible;
        
        public DataItem()
        {
            strItemName = string.Empty;
            strItemRemark = string.Empty;
            dataType = DataType.String;
            bVisible = true;
        }
    }
    public class DataGroup
    {
        public string strGroupName;
        public string strGroupRemark;

        public List<DataItem> listDataItem;
        [XmlIgnore]
        public Dictionary<string, DataItem> dicDataItem;

        public DataGroup()
        {
            strGroupName = string.Empty;
            strGroupRemark = string.Empty;
            listDataItem = new List<DataItem>();
            dicDataItem = new Dictionary<string, DataItem>();
        }
    }
    public class DataDoc
    {
        public List<DataGroup> listDataGroup;
        [XmlIgnore]
        public Dictionary<string, DataGroup> dicDataGroup;

        public DataDoc()
        {
            listDataGroup = new List<DataGroup>();
            dicDataGroup = new Dictionary<string, DataGroup>();
        }
        public static DataDoc LoadObj()
        {
            DataDoc pDoc = null;
            FileStream fs = null;

            try
            {
                fs = File.OpenRead(@".//Parameter/Data/SystemData.xml");
                XmlSerializer xml = new XmlSerializer(typeof(DataDoc));
                pDoc = (DataDoc)xml.Deserialize(fs);
                fs.Close();

                pDoc.dicDataGroup = pDoc.listDataGroup.ToDictionary(p => p.strGroupName);
                foreach (DataGroup item in pDoc.listDataGroup)
                {
                    item.dicDataItem = item.listDataItem.ToDictionary(p => p.strItemName);
                }
            }
            catch (Exception)
            {
                if(null != fs)
                {
                    fs.Close();
                }
                pDoc = new DataDoc();
            }

            return pDoc;
        }
        public bool SaveDataDoc()
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(@".//Parameter/Data/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Data/");
                }
                fs = new FileStream(@".//Parameter/Data/SystemData.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                XmlSerializer xml = new XmlSerializer(typeof(DataDoc));
                xml.Serialize(fs, this);
                fs.Close();

                return true;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }

                return false;
            }
        }
    }
}
