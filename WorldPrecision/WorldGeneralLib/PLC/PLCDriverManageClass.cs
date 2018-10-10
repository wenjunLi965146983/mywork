using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public static class PLCDriverManageClass
    {
        public static Dictionary<string, PLCBaseClass> dicDrivers;
        public static PLCDriverDoc docPlc;

        public static void LoadData()
        {
            docPlc = PLCDriverDoc.LoadObj();
        }
        public static void InitPLCS()
        {
            docPlc = PLCDriverDoc.LoadObj();
            dicDrivers = new Dictionary<string, PLCBaseClass>();
            foreach (KeyValuePair<string, PLCDriverInfo> posInfoPair in docPlc.dicPlcInfo)
            {
                if (posInfoPair.Value.plcType == PLCType.Panasonic)
                {
                    dicDrivers.Add(posInfoPair.Key, new PLCPanasonicClass());
                    dicDrivers[posInfoPair.Key].Init(posInfoPair.Value.PortName, posInfoPair.Value.BaudRate,
                                                     posInfoPair.Value.Parity, posInfoPair.Value.DataBits, posInfoPair.Value.StopBits);
                }
                if (posInfoPair.Value.plcType == PLCType.Omron)
                {
                    dicDrivers.Add(posInfoPair.Key, new PLCOmronClass());
                    dicDrivers[posInfoPair.Key].Init(posInfoPair.Value.PortName, posInfoPair.Value.BaudRate,
                                                     posInfoPair.Value.Parity, posInfoPair.Value.DataBits, posInfoPair.Value.StopBits);
                }
            }
            PLCControlManageClass.StartScan();
        }
        public static void ShowDriverForm()
        {
            PLCDriverForm frmDriver = new PLCDriverForm();
            frmDriver.ShowDialog();
        }
        
        //public static PLCBaseClass PLC(string strPlcName)
        //{
            
        //        return plcDrivers[strPlcName];
            
        //}
    }
}
