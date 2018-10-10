using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class AlarmPlcType
    {
        public string m_strPlcName = "";
        public Dictionary<string, AlarmPLCGroup> PlcGroupDic;
        public AlarmPlcType()
        {
            PlcGroupDic = new Dictionary<string, AlarmPLCGroup>();
        }
    }
}
