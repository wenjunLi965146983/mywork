using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Alarm
{
    [Serializable]
    public class AlarmDoc
    {
        public List<AlarmData> listAlarmItems;
        [XmlIgnore]
        public Dictionary<string, AlarmData> dicAlarmItems;

        public AlarmDoc()
        {
            listAlarmItems = new List<AlarmData>();
            dicAlarmItems = new Dictionary<string, AlarmData>();
        }

        public static AlarmDoc LoadDoc()
        {
            AlarmDoc alarmDoc;
            XmlSerializer xml = new XmlSerializer(typeof(AlarmDoc));
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@".//Parameter/Alarm Manage/AlarmDoc" + ".xml");
                alarmDoc = (AlarmDoc)xml.Deserialize(fs);
                fs.Close();
                alarmDoc.dicAlarmItems = alarmDoc.listAlarmItems.ToDictionary(p=>p.AlarmKey);
            }
            catch (Exception)
            {
                if(null != fs)
                {
                    fs.Close();
                }
                alarmDoc = new AlarmDoc();
            }

            return alarmDoc;
        }

        public bool SaveDoc()
        {
            try
            {
                if (!Directory.Exists(@".//Parameter/Alarm Manage/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Alarm Manage/");
                }
                FileStream fs = new FileStream(@".//Parameter/Alarm Manage/AlarmDoc.xml", FileMode.Create, FileAccess.Write, FileShare.Read);
                XmlSerializer xml = new XmlSerializer(typeof(AlarmDoc));
                xml.Serialize(fs, this);
                fs.Close();

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
