using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Alarm
{
    [Serializable]
    public class AlarmData
    {
        private string _strKey;
        public string AlarmKey
        {
            get { return _strKey; }
            set { _strKey = value; }
        }

        private string _strName;
        public string AlarmName
        {
            get { return _strName; }
            set { _strName = value; }
        }

        private string _strAlarmSrc;
        public string AlarmSrc
        {
            get { return _strAlarmSrc; }
            set { _strAlarmSrc = value; }
        }

        private string _strMsg;
        public string AlarmMsg
        {
            get { return _strMsg; }
            set { _strMsg = value; }
        }

        private string _strRemark;
        public string AlarmRemark
        {
            get { return _strRemark; }
            set { _strRemark = value; }
        }

        private DateTime _time;
        public DateTime AlarmTime
        {
            get { return _time; }
            set { _time = value; }
        }

        public AlarmData()
        {
            _strKey = "000";
            _strName = " ";
            _strMsg = " ";
            _strRemark = " ";
        }
    }
}
