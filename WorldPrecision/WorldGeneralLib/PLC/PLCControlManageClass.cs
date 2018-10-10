using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorldGeneralLib.PLC
{
    public static class PLCControlManageClass
    {
        //public static List<IControlPLC> PLCControls;
        public static Dictionary<string, PLCControlGroup> PLCControlGroup;
        public static Dictionary<string, double> PLCScanTime;
        //private static string _strScanTime;
        public static string strScanTime 
        { 
            
            get 
            {
                string strTemp = "";
                try
                {
                    
                    foreach (KeyValuePair<string, double> keyValuePair in PLCScanTime)
                    {
                        strTemp = strTemp + "PLC:" + keyValuePair.Key.ToString() + "-->>\r\n" + "   Time:" + keyValuePair.Value.ToString("0.0000") + "\r\n";
                    }
                }
                catch 
                {
                }
                return strTemp;
            }
        }
        public static void StartScan()
        {

            PLCControlGroup = new Dictionary<string, WorldGeneralLib.PLC.PLCControlGroup>();
            foreach(KeyValuePair<string,PLCBaseClass> keyValuePair in PLCDriverManageClass.dicDrivers)
            {
                PLCControlGroup plcGroup=new PLCControlGroup();
                PLCScanTime = new Dictionary<string, double>();
                PLCScanTime.Add(keyValuePair.Key, 0.0);
                PLCControlGroup.Add(keyValuePair.Key, plcGroup);
                System.Threading.ParameterizedThreadStart startFunction = new System.Threading.ParameterizedThreadStart(ScanThread);
                System.Threading.Thread threadScan = new System.Threading.Thread(startFunction);
                threadScan.IsBackground = true;
                threadScan.Start(keyValuePair.Key);
            }
            //System.Threading.Thread threadScan = new System.Threading.Thread(ScanThread);
            //threadScan.IsBackground = true;
            //threadScan.Start();
        }
        public static void ScanThread(object objGroupName)
        {
            string strGroupName=(string)objGroupName;
            HiPerfTimer timer = new HiPerfTimer();
            while (true)
            {
                timer.Start();
                BitCtrlGroupUnit bitCtrlGroupUnit = new BitCtrlGroupUnit();
                WordCtrlGroupUnit wordCtrlGroupUnit = new WordCtrlGroupUnit();
                try
                {
                    foreach (IControlPLC plsControl in PLCControlGroup[strGroupName].PLCControls)
                    {
                        if (plsControl.GetType() == typeof(BitButton))
                        {
                            BitButton bitControl = (BitButton)plsControl;
                            if (bitControl.Visible || bitControl.AlwayFresh)
                                bitCtrlGroupUnit.addItemToGroup(bitControl);
                        }
                        if (plsControl.GetType() == typeof(DWordTextBox))
                        {
                            DWordTextBox wordControl = (DWordTextBox)plsControl;
                            if (wordControl.Visible)
                                wordCtrlGroupUnit.addItemToGroup(wordControl);
                        }

                    }
                    foreach (BitCtrlGroupItem bitCtrlItem in bitCtrlGroupUnit.bitGroupList)
                    {
                        bitCtrlItem.MakeStartAndEndAddr();
                        bitCtrlItem.UpdateGroupStatus();
                        //System.Threading.Thread.Sleep(10);
                    }
                    foreach (WordCtrlGroupItem wordCtrlItem in wordCtrlGroupUnit.wordGroupList)
                    {
                        wordCtrlItem.MakeStartAndEndAddr();
                        wordCtrlItem.UpdateGroupStatus();
                        //System.Threading.Thread.Sleep(10);
                    }
                }
                catch
                {
 
                }
                
                System.Threading.Thread.Sleep(1);
                PLCScanTime[strGroupName] = timer.Duration;
                //strScanTime = timer.Duration.ToString("0.0000");
            }
        }
    }
}
