using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public class AlarmBitGroup
    {
        public string strPlcName="";
        public string strStation = "";
        public string strWordType="";
        public string strWordStartAddress = "";
        public string strWordEndAddress = "";
        public int iStartAddr=-1;
        public int iEndAdd=100000;
        public bool bInit = false;
        public Dictionary<string, AlarmItem> alarmItemDis;
        public AlarmBitGroup()
        {
            alarmItemDis = new Dictionary<string, AlarmItem>();
        }
        public int CheckCanAddTo(AlarmItem alarmItem)
        {
            string strTempStation = "";
            string strTempWordType="";
            string strTempWordAddress = "";
            //string strTempWordEndAddress = "";
            string strTempBitAddress="";
            PLCBaseClass plcDriver = PLC.Driver(alarmItem.strPlcName);
            if (plcDriver == null)
            {
                return 1;
            }
            if (plcDriver.GetBitAddress(
                alarmItem.strAddress, ref strTempStation, ref strTempWordType, ref strTempWordAddress, ref strTempBitAddress) == false)
            {
                return 2;
            }
            int iTempWordAdd = int.Parse(strTempWordAddress);
            if (bInit == false)
            {
                strPlcName = alarmItem.strPlcName;
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
            string strItemFullName1 = strTempStation + strTempWordType + strTempWordAddress + strTempBitAddress;
            string strItemFullName = alarmItem.strMachine + strItemFullName1;
            if (alarmItemDis.Keys.Contains(strItemFullName))
            {
                return 4;
            }
            alarmItemDis.Add(strItemFullName, alarmItem);
            return 0;
        }
        public void MakeStartAndEndAddr()
        {
            strWordStartAddress = strStation + "#" + strWordType + "#" + iStartAddr.ToString("00000");
            strWordEndAddress = strStation + "#" + strWordType + "#" + iEndAdd.ToString("00000");
        }
        public void AlarmCheck()
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
                foreach (KeyValuePair<string, AlarmItem> keyValuePair in alarmItemDis)
                {
                    int iKeyLength = keyValuePair.Value.strMachine.Length;
                    iWordNo = int.Parse(keyValuePair.Key.Substring(iKeyLength + 3, 5));
                    iBitNo = int.Parse(keyValuePair.Key.Substring(iKeyLength + 8, 2));
                    //iWordNo = int.Parse(keyValuePair.Key.Substring(3, 5));
                    //iBitNo = int.Parse(keyValuePair.Key.Substring(8, 2));
                    iIndex = (iWordNo - iStartAddr) * 16 + iBitNo;
                    if (strValue[iIndex] == '1')
                        keyValuePair.Value.bCurrentStatus = true;
                    else
                        keyValuePair.Value.bCurrentStatus = false;
                }
                foreach (KeyValuePair<string, AlarmItem> keyValuePair in alarmItemDis)
                {

                    if (keyValuePair.Value.bCurrentStatus != keyValuePair.Value.bPreStatus)
                    {
                        if (keyValuePair.Value.bCurrentStatus)
                        {
                            if (AlarmManageMent.alarmForm != null)
                            {
                                AlarmManageMent.alarmForm.InsertAlarmPLC(keyValuePair.Value.strMachine, keyValuePair.Value.strAlarmMes);
                                //AlarmManageMent.alarmForm.InsertAlarmPLC(keyValuePair.Key, keyValuePair.Value.strAlarmMes);
                            }
                        }
                        else
                        {
                            if (AlarmManageMent.alarmForm != null)
                            {
                                AlarmManageMent.alarmForm.RemoveAlarmPLC(keyValuePair.Value.strMachine);
                            }
                        }
                    }
                    
                }
                foreach (KeyValuePair<string, AlarmItem> keyValuePair in alarmItemDis)
                {
                    keyValuePair.Value.bPreStatus = keyValuePair.Value.bCurrentStatus;
                }
            }
        }
    }
}
