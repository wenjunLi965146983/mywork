using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorldGeneralLib;

namespace WorldGeneralLib.PLC
{
    public static class AlarmManageMent
    {
        public static AlarmDate alarmData;
        public static AlarmPlcType alarmPlcType;
        public static FormAlarm alarmForm;
        public static bool Init()
        {
            alarmData = AlarmDate.LoadObj();
            string strPlcType = "";
            alarmPlcType = new AlarmPlcType();
            for (int i = 0; i < alarmData.listItem.Count; i++)
            {
                strPlcType=alarmData.listItem[i].strPlcName;
                if (alarmPlcType.PlcGroupDic.Keys.Contains(strPlcType))
                {
                    alarmPlcType.PlcGroupDic[strPlcType].addItemToGroup(alarmData.listItem[i]);
                }
                else
                {
                    AlarmPLCGroup PLCGroup = new AlarmPLCGroup();
                    PLCGroup.m_strPlcName = strPlcType;
                    alarmPlcType.PlcGroupDic.Add(strPlcType, PLCGroup);
                    alarmPlcType.PlcGroupDic[strPlcType].addItemToGroup(alarmData.listItem[i]);
                }
            }
            StartScan();
            return true;
        }
        public static void StartScan()
        {
            foreach (KeyValuePair<string, AlarmPLCGroup> keyValuePair in alarmPlcType.PlcGroupDic)
            {
                System.Threading.ParameterizedThreadStart startFunction = new System.Threading.ParameterizedThreadStart(ScanThread);
                System.Threading.Thread threadScan = new System.Threading.Thread(startFunction);
                threadScan.IsBackground = true;
                threadScan.Start(keyValuePair.Key);
            }
            
        }
        public static void ScanThread(object objGroupName)
        {
            string strGroupName = (string)objGroupName;
            HiPerfTimer timer = new HiPerfTimer();
            foreach (AlarmBitGroup alarmBitGroup in alarmPlcType.PlcGroupDic[strGroupName].bitGroupList)
            {
                alarmBitGroup.MakeStartAndEndAddr();
            }
            while (true)
            {
                timer.Start();
                try
                {
                    foreach (AlarmBitGroup alarmBitGroup in alarmPlcType.PlcGroupDic[strGroupName].bitGroupList)
                    {

                        alarmBitGroup.AlarmCheck();
                        System.Threading.Thread.Sleep(200);
                    }
                }
                catch
                {

                }

                System.Threading.Thread.Sleep(200);
                //PLCScanTime[strGroupName] = timer.Duration;
                //strScanTime = timer.Duration.ToString("0.0000");
            }
        }
        public static void showSettingForm()
        {
            //AlarmManagerForm alarmManagerForm = new AlarmManagerForm(alarmData);
            //alarmManagerForm.ShowDialog();
        }

    }
}
