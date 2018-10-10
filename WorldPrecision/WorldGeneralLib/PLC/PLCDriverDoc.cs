using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.ComponentModel;
using System.Diagnostics;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace WorldGeneralLib.PLC
{
    [Serializable]
    public class PLCDriverDoc
    {
        public Dictionary<string, PLCDriverInfo> dicPlcInfo;
        internal PLCDriverDoc()
        {
            dicPlcInfo = new Dictionary<string, PLCDriverInfo>();
        }
        internal static PLCDriverDoc LoadObj()
        {
            PLCDriverDoc pDoc;
            BinaryFormatter fmt = new BinaryFormatter();
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/PLC/PlcDoc.dat");
                pDoc = (PLCDriverDoc)fmt.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                //MessageBox.Show(eMy.ToString());
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new PLCDriverDoc();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/PLC/"))
            {
                Directory.CreateDirectory(@".//Parameter/PLC/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/PLC/PlcDoc.dat", FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
