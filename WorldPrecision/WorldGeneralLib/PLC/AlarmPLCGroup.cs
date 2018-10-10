using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class AlarmPLCGroup
    {
        public string m_strPlcName = "";
        public List<AlarmBitGroup> bitGroupList;
        public AlarmPLCGroup()
        {
            bitGroupList = new List<AlarmBitGroup>();
        }
        public bool addItemToGroup(AlarmItem alarmItem)
        {
            bool bAddOk = false;
            foreach (AlarmBitGroup bitGroupTemp in bitGroupList)
            {
                if (bitGroupTemp.CheckCanAddTo(alarmItem) == 3)
                {
                   
                }
                else
                {
                    bAddOk = true;
                    break;
                }
            }
            if (!bAddOk)
            {
                AlarmBitGroup bitGroup = new AlarmBitGroup();
                bitGroupList.Add(bitGroup);
                bitGroup.CheckCanAddTo(alarmItem);
            }
            return true;
        }
    }
}
