using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class BitCtrlGroupItem
    {
        public string strPlcName="";
        public string strStation = "";
        public string strWordType="";
        public string strWordStartAddress = "";
        public string strWordEndAddress = "";
        public int iStartAddr=-1;
        public int iEndAdd=100000;
        public bool bInit = false;
        public Dictionary<string, BitButton> bitCtrlItemDis;
        public BitCtrlGroupItem()
        {
            bitCtrlItemDis = new Dictionary<string, BitButton>();
        }
        public int CheckCanAddTo(BitButton BitButtonItem)
        {
            string strTempStation = "";
            string strTempWordType="";
            string strTempWordAddress = "";
            //string strTempWordEndAddress = "";
            string strTempBitAddress="";
            PLCBaseClass plcDriver = PLC.Driver(BitButtonItem.DriverName);
            if (plcDriver == null)
            {
                BitButtonItem.m_plcRes = PLCResponse.ERROR;
                BitButtonItem.FreshDriverStatus();
                return 1;
            }
            if (plcDriver.GetBitAddress(
                BitButtonItem.DriverAddrMonitor, ref strTempStation, ref strTempWordType, ref strTempWordAddress, ref strTempBitAddress) == false)
            {
                BitButtonItem.m_plcRes = PLCResponse.ADDWRONG;
                BitButtonItem.FreshDriverStatus();
                return 2;
            }
            int iTempWordAdd = int.Parse(strTempWordAddress);
            if (bInit == false)
            {
                strPlcName = BitButtonItem.DriverName;
                strStation = strTempStation;
                iStartAddr = iTempWordAdd;
                strWordType = strTempWordType;
                iEndAdd = iTempWordAdd;
                bInit = true;
            }
            int iPHBeginWordAdd =iEndAdd - 7;
            int iPHEndWordAdd = iStartAddr +7;
            if (iTempWordAdd >= iPHBeginWordAdd && iTempWordAdd <= iPHEndWordAdd && strStation == strTempStation && strWordType == strTempWordType)
            {
                if (iTempWordAdd < iStartAddr)
                {
                    iStartAddr = iTempWordAdd;
                }
                if (iTempWordAdd > iEndAdd)
                {
                    iEndAdd = iTempWordAdd;
                }
            }
            else
            {
                return 3;
            }
            string strItemFullName = strTempStation + strTempWordType + strTempWordAddress + strTempBitAddress;
            if (bitCtrlItemDis.Keys.Contains(strItemFullName))
            {
                BitButtonItem.m_plcRes = PLCResponse.OTHERS;
                BitButtonItem.FreshDriverStatus();
                return 4;
            }
            bitCtrlItemDis.Add(strItemFullName, BitButtonItem);
            return 0;
        }
        public void MakeStartAndEndAddr()
        {
            strWordStartAddress = strStation + "#" + strWordType + "#" + iStartAddr.ToString("00000");
            strWordEndAddress = strStation + "#" + strWordType + "#" + iEndAdd.ToString("00000");
        }
        public void UpdateGroupStatus()
        {
            string strValue="";
            int iWordNo = 0;
            int iBitNo = 0;
            int iIndex = 0;
            WorldGeneralLib.PLC.PLCResponse response = 0;
            response = PLC.GetWordS(strPlcName, strWordStartAddress, strWordEndAddress, ref strValue);
            if (response == WorldGeneralLib.PLC.PLCResponse.SUCCESS)
            {

                //01 C 00000 01
                HiPerfTimer timeTemp = new HiPerfTimer();
                timeTemp.Start();
                foreach (KeyValuePair<string, BitButton> keyValuePair in bitCtrlItemDis)
                {
                    iWordNo = int.Parse(keyValuePair.Key.Substring(3, 5));
                    iBitNo = int.Parse(keyValuePair.Key.Substring(8, 2));
                    iIndex = (iWordNo - iStartAddr) * 16 + iBitNo;
                    keyValuePair.Value.m_plcRes = response;
                    if (strValue[iIndex] == '1')
                        keyValuePair.Value.SetDriverStatus(true);
                    else
                        keyValuePair.Value.SetDriverStatus(false);
                }
                foreach (KeyValuePair<string, BitButton> keyValuePair in bitCtrlItemDis)
                {
                    keyValuePair.Value.FreshDriverStatus();
                }
                double Time = timeTemp.Duration;
            }
            else
            {
                foreach (KeyValuePair<string, BitButton> keyValuePair in bitCtrlItemDis)
                {

                    keyValuePair.Value.m_plcRes = response;
                   
                }
                foreach (KeyValuePair<string, BitButton> keyValuePair in bitCtrlItemDis)
                {
                    keyValuePair.Value.FreshDriverStatus();
                }
            }
        }
    }
}
