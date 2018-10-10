using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGeneralLib;

namespace WorldGeneralLib.PLC
{
    public static class PLC
    {
        public static void Init(FormAlarm alarmForm)
        {
            PLCDriverManageClass.InitPLCS();
            AlarmManageMent.Init();
            AlarmManageMent.alarmForm = alarmForm;
        }
        public static void InitPlc()
        {
            PLCDriverManageClass.InitPLCS();
         
        }
        public static void InitAlarm(FormAlarm alarmForm)
        {
            AlarmManageMent.Init();
            AlarmManageMent.alarmForm = alarmForm;
        }
        public static PLCBaseClass Driver(string strPlcDriver)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver];
            }
            catch
            {
                return null;
            }
           
        }
        public static PLCResponse GetBit(string strPlcDriver,string strAddress,ref bool bOn)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].GetBit(strAddress, ref bOn);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }

        }
        public static PLCResponse SetBit(string strPlcDriver, string strAddress, bool bOn)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].SetBit(strAddress,bOn);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }

        }
        public static PLCResponse GetWord(string strPlcDriver, string strAddress, PLCDataType dataType, ref string strValue)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].GetWord(strAddress, dataType,ref strValue);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }
        }
        public static PLCResponse SetWord(string strPlcDriver, string strAddress, PLCDataType dataType, string strValue)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].SetWord(strAddress, dataType, strValue);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }
        }
        public static PLCResponse GetWordS(string strPlcDriver,string strAddressStart, string strAddressEnd, ref string strValue)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].GetWordS(strAddressStart, strAddressEnd, ref strValue);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }
        }
        public static PLCResponse GetWordSS(string strPlcDriver, string strAddressStart, string strAddressEnd, ref string strValue)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].GetWordSS(strAddressStart, strAddressEnd, ref strValue);
            }
            catch
            {
                return PLCResponse.OTHERS;
            }
        }
        public static bool PLCOpened(string strPlcDriver)
        {
            try
            {
                return PLCDriverManageClass.dicDrivers[strPlcDriver].bInitOK;
            }
            catch
            {
                return false;
            }
        }
    }
}
