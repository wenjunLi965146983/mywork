using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace WorldGeneralLib.PLC
{
    [Serializable]
    public class AlarmDate
    {
        public List<AlarmItem> listItem;
        internal AlarmDate()
        {
            listItem = new List<AlarmItem>();
        }
        public static AlarmDate LoadObj()
        {
            AlarmDate pDoc;
            BinaryFormatter fmt = new BinaryFormatter();
            FileStream fsReader = null;
            try
            {
                fsReader = File.OpenRead(@".//Parameter/AlarmDate.dat");
                pDoc = (AlarmDate)fmt.Deserialize(fsReader);
                fsReader.Close();
            }
            catch //(Exception eMy)
            {
                if (fsReader != null)
                {
                    fsReader.Close();
                }
                pDoc = new AlarmDate();
            }
            return pDoc;
        }
        public bool SaveDoc()
        {
            if (!Directory.Exists(@".//Parameter/"))
            {
                Directory.CreateDirectory(@".//Parameter/");
            }
            FileStream fsWriter = new FileStream(@".//Parameter/AlarmDate.dat", FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryFormatter fmt = new BinaryFormatter();
            fmt.Serialize(fsWriter, this);
            fsWriter.Close();

            return true;
        }
    }
}
