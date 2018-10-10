using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Functions;
using System.Threading;
using WorldGeneralLib.TaskBase;
using OmronFins.Net;
using WorldGeneralLib.Alarm;

namespace WorldGeneralLib.Hardware.Omron.TypeNJ
{
    public class PlcOmronTypeNJ:HardwareBase, IInputAction,IOutputAction
    {
        public int times = 0;
        public OmronFinsAPI omronFinsAPI;
        public PlcOmronTypeNJData plcData;

        public PlcOmronTypeNJ(PlcOmronTypeNJData plcData)
        {
            omronFinsAPI = new OmronFinsAPI();
            this.plcData = plcData;
        }
        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = false;
            try
            {
                if(omronFinsAPI.ConnectToOmronPLC(plcData.IP, 9600,null))
                {
                    bInitOk = true;
                }
                else
                {
                }
            }
            catch //(Exception)
            {
                
            }
            System.Threading.Thread threadRefresh = new System.Threading.Thread(ThreadRefresh);
            threadRefresh.IsBackground = true;
            threadRefresh.Start();

            return bInitOk;
        }
        public override bool IsConnected()
        {
            return omronFinsAPI.bConnectOmronPLC;
        }
        private void ThreadRefreshHandler()
        {
            try
            {
                string strValue = string.Empty;
                object objTemp = new object();
                foreach (PlcScanItems item in plcData.listScanItems)
                {
                    if (!item.Refresh)
                    {
                        continue;
                    }

                    switch (item.DataType)
                    {
                        case DataType.BIT:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = (bool)objTemp ? "1" : "0";
                            break;
                        case DataType.INT16:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = ((Int16)objTemp).ToString();
                            break;
                        case DataType.UINT16:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = ((UInt16)objTemp).ToString();
                            break;
                        case DataType.INT32:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = ((Int32)objTemp).ToString();
                            break;
                        case DataType.UINT32:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = ((UInt32)objTemp).ToString();
                            break;
                        case DataType.REAL:
                            omronFinsAPI.ReadSingleElement(item, ref objTemp);
                            item.strValue = ((float)objTemp).ToString();
                            break;
                        case DataType.STRING:
                            if (omronFinsAPI.ReadString(item, 32, ref strValue))
                            {
                                item.strValue = strValue.Replace('\0', ' ').Trim();
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        private void ReConnectToPlc()
        {
            if (!omronFinsAPI.bConnectOmronPLC)
            {
                times++;
                if (times >= 10)
                {
                    System.Threading.Thread.Sleep(1000);
                    return;
                }

                omronFinsAPI.DisconnectToOmronPLC();
                Thread.Sleep(500);
                omronFinsAPI.ConnectToOmronPLC(plcData.IP, 9600,null);
                if (omronFinsAPI.bConnectOmronPLC)
                {
                    times = 0;
                }
                else
                {
                    Thread.Sleep(2000);
                    if (times >= 10 - 1)
                    {
                        return;
                    }
                }
            }
        }
        private void ThreadRefresh()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);

            while (!MainModule.formMain.bExit)
            {
                System.Threading.Thread.Sleep(10);
                if(!omronFinsAPI.bConnectOmronPLC)
                {
                    //MainModule.alarmManage.InsertAlarm(AlarmKeys.PLCOmron连接断开, "与PLC[" + plcData.Name + "]断开连接。");
                    ReConnectToPlc();
                    continue;       //Disconnect
                }

                try
                {
                    ThreadRefreshHandler();
                }
                catch// (Exception)
                {
                }
            }
            return;
        }
        public bool GetInputBit(int iBit)
        {
            foreach (PlcScanItems item in plcData.listScanItems)
            {
                if (iBit == item.Index)
                {
                    return (item.strValue == "1" ? true : false);
                }
            }
            return false;
        }
        public bool SetOutBit(int iBit, bool bOn)
        {
            if (iBit < 200)
                return false ;

            try
            {
                foreach (PlcScanItems item in plcData.listScanItems)
                {
                    if (iBit == item.Index)
                    {
                        omronFinsAPI.WriteSingleElement(item, bOn);
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool GetOutBit(int iBit)
        {
            foreach (PlcScanItems item in plcData.listScanItems)
            {
                if (iBit == item.Index)
                {
                    return (item.strValue == "1" ? true : false);
                }
            }
            return false;
        }

        public bool WriteData(string strItemName, object objValue)
        {
            if (!plcData.dicScanItems.ContainsKey(strItemName))
                return false;
            try
            {
                return omronFinsAPI.WriteSingleElement(plcData.dicScanItems[strItemName], objValue);
            }
            catch (Exception)
            {
            }
            return false;
        }
    }
}
