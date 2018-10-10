using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

using System.Threading.Tasks;

namespace WorldGeneralLib.IO
{
    [XmlInclude(typeof(IOData)), XmlInclude(typeof(IOData))]
    public class IODoc
    {
        public List<IOData> listInput;
        [XmlIgnore]
        public Dictionary<string, IOData> dicInput;

        public List<IOData> listOutput;
        [XmlIgnore]
        public Dictionary<string, IOData> dicOutput;

        public IODoc()
        {
            listInput = new List<IOData>();
            dicInput = new Dictionary<string, IOData>();

            listOutput = new List<IOData>();
            dicOutput = new Dictionary<string, IOData>();
        }
        public static IODoc LoadObj()
        {
            IODoc pDoc = null;
            XmlSerializer xml = new XmlSerializer(typeof(IODoc));
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@".//Parameter/IO/IODoc.xml");
                pDoc = (IODoc)xml.Deserialize(fs);
                fs.Close();

                pDoc.dicInput = pDoc.listInput.ToDictionary(p=>p.Name );
                pDoc.dicOutput = pDoc.listOutput.ToDictionary(p=>p.Name );
            }
            catch //(Exception ex)
            {
                if(null != fs)
                {
                    fs.Close();
                }
                pDoc = new IODoc();
            }
            return pDoc;
        }

        public void SaveDoc()
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(@".//Parameter/IO/"))
                {
                    Directory.CreateDirectory(@".//Parameter/IO/");
                }
                fs = new FileStream(@".//Parameter/IO/IODoc.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                XmlSerializer xml = new XmlSerializer(typeof(IODoc));
                xml.Serialize(fs, this);
                fs.Close();
            }
            catch //(Exception)
            {
                if(null != fs)
                {
                    fs.Close();
                }
            }
        }
    }
}
