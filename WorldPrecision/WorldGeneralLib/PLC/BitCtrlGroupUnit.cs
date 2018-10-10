using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class BitCtrlGroupUnit
    {
        public string m_strPlcName = "";
        public List<BitCtrlGroupItem> bitGroupList;
        public BitCtrlGroupUnit()
        {
            bitGroupList = new List<BitCtrlGroupItem>();
        }
        public bool addItemToGroup(BitButton BitButtonItem)
        {
            bool bAddOk = false;
            foreach (BitCtrlGroupItem bitGroupTemp in bitGroupList)
            {
                if (bitGroupTemp.CheckCanAddTo(BitButtonItem) == 3)
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
                BitCtrlGroupItem bitGroup = new BitCtrlGroupItem();
                bitGroupList.Add(bitGroup);
                bitGroup.CheckCanAddTo(BitButtonItem);
            }
            return true;
        }
    }
}
