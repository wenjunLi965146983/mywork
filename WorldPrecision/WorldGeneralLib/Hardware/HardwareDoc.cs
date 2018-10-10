using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace WorldGeneralLib.Hardware
{
    public class HardwareDoc
    {
        public List<HardwareData> listHardwareData;
        [XmlIgnore]
        public Dictionary<string, HardwareData> dicHardwareData;

        public HardwareDoc()
        {
            listHardwareData = new List<HardwareData>();
            dicHardwareData = new Dictionary<string, HardwareData>();
        }
        public static HardwareDoc LoadObj()
        {
            HardwareDoc pDoc;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(HardwareDoc));
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/Hardware/HardwareDoc" + ".xml");
                pDoc = (HardwareDoc)xmlSerializer.Deserialize(fsReader);
                fsReader.Close();
                pDoc.dicHardwareData = pDoc.listHardwareData.ToDictionary(p => p.Name);

                foreach (HardwareData item in pDoc.listHardwareData)
                {
                    item.DataInit();
                }
            }
            catch// (Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new HardwareDoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            try
            {
                if (!Directory.Exists(@".//Parameter/Hardware/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Hardware/");
                }
                FileStream fsWriter = new FileStream(@".//Parameter/Hardware/HardwareDoc" + ".xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(HardwareDoc));
                xmlSerializer.Serialize(fsWriter, this);
                fsWriter.Close();

                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
            return false;
        }
    }
}
