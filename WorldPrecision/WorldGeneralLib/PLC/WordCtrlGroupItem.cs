using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class WordCtrlGroupItem
    {
        public string strPlcName="";
        public string strStation = "";
        public string strWordType="";
        public string strWordStartAddress = "";
        public string strWordEndAddress = "";
        public int iStartAddr=-1;
        public int iEndAdd=100000;
        public bool bInit = false;
        public Dictionary<string, DWordTextBox> wordCtrlItemDis;
        public WordCtrlGroupItem()
        {
            wordCtrlItemDis = new Dictionary<string, DWordTextBox>();
        }
        public int CheckCanAddTo(DWordTextBox WordTextItem)
        {
            string strTempStation = "";
            string strTempWordType="";
            string strTempWordAddress = "";
            PLCBaseClass plcDriver = PLC.Driver(WordTextItem.DriverName);
            if (plcDriver == null)
            {
                WordTextItem.m_plcRes = PLCResponse.ERROR;
                WordTextItem.FreshDriverStatus();
                return 1;
            }
            if (plcDriver.GetWordAddress(
                WordTextItem.DriverAddr, ref strTempStation, ref strTempWordType, ref strTempWordAddress) == false)
            {
                WordTextItem.m_plcRes = PLCResponse.ADDWRONG;
                WordTextItem.FreshDriverStatus();
                return 2;
            }
            int iTempWordAdd = int.Parse(strTempWordAddress);
            if (bInit == false)
            {
                strPlcName = WordTextItem.DriverName;
                strStation = strTempStation;
                iStartAddr = iTempWordAdd;
                strWordType = strTempWordType;
                iEndAdd = iTempWordAdd+1;
                bInit = true;
            }
            int iPHBeginWordAdd =iEndAdd - 15;
            int iPHEndWordAdd = iStartAddr +15;
            if (iTempWordAdd >= iPHBeginWordAdd && iTempWordAdd <= iPHEndWordAdd && strStation == strTempStation && strWordType == strTempWordType)
            {
                if (iTempWordAdd < iStartAddr)
                {
                    iStartAddr = iTempWordAdd;
                }
                if (iTempWordAdd >= iEndAdd)
                {
                    iEndAdd = iTempWordAdd+1;
                }
            }
            else
            {
                return 3;
            }
            string strItemFullName = strTempStation + strTempWordType + strTempWordAddress;
            if (wordCtrlItemDis.Keys.Contains(strItemFullName))
            {
                WordTextItem.m_plcRes = PLCResponse.OTHERS;
                WordTextItem.FreshDriverStatus();
                return 4;
            }
            wordCtrlItemDis.Add(strItemFullName, WordTextItem);
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
            int iIndex = 0;
            WorldGeneralLib.PLC.PLCResponse response = 0;
            response = PLC.GetWordSS(strPlcName, strWordStartAddress, strWordEndAddress, ref strValue);
            if (response == WorldGeneralLib.PLC.PLCResponse.SUCCESS)
            {

                //01 C 00000 01
                HiPerfTimer timeTemp = new HiPerfTimer();
                timeTemp.Start();
                foreach (KeyValuePair<string, DWordTextBox> keyValuePair in wordCtrlItemDis)
                {
                    iWordNo = int.Parse(keyValuePair.Key.Substring(3, 5));
                    iIndex = (iWordNo - iStartAddr)*4;
                    keyValuePair.Value.m_plcRes = response;
                    keyValuePair.Value.SetDriverStatus(strValue.Substring(iIndex,8));
                   
                }
                foreach (KeyValuePair<string, DWordTextBox> keyValuePair in wordCtrlItemDis)
                {
                    keyValuePair.Value.FreshDriverStatus();
                }
                double Time = timeTemp.Duration;
            }
            else
            {
                foreach (KeyValuePair<string, DWordTextBox> keyValuePair in wordCtrlItemDis)
                {

                    keyValuePair.Value.m_plcRes = response;
                   
                }
                foreach (KeyValuePair<string, DWordTextBox> keyValuePair in wordCtrlItemDis)
                {
                    keyValuePair.Value.FreshDriverStatus();
                }
            }
        }
    }
}
