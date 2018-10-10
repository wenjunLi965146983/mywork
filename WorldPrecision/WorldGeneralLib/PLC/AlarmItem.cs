using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    [Serializable]
    public class AlarmItem
    {
        public string strPlcName = "";
        public string strAddress = "";
        public string strMachine = "";
        public string strAlarmMes = "";
        [NonSerialized]
        public bool bCurrentStatus = false;
        [NonSerialized]
        public bool bPreStatus = false;
        //public string strWordGroup = "";
    }
}
