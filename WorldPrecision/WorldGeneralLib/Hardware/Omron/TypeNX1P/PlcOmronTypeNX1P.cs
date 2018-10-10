using OmronFins.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using WorldGeneralLib.TaskBase;
using System.Threading;
using WorldGeneralLib.Alarm;

namespace WorldGeneralLib.Hardware.Omron.TypeNX1P
{
    public class PlcOmronTypeNX1P : HardwareBase
    {
        public int times = 0;
        public OmronFinsAPI omronFinsAPI;
        public PlcOmronTypeNX1PData plcData;

        public PlcOmronTypeNX1P(PlcOmronTypeNX1PData plcData)
        {
            omronFinsAPI = new OmronFinsAPI();
            this.plcData = plcData;
        }
        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = false;
            try
            {
                if (omronFinsAPI.ConnectToOmronPLC(plcData.RemoteIP, 9600, plcData.LocalIP))
                {
                    bInitOk = true;
                }
            }
            catch
            {

            }
            System.Threading.Thread threadRefresh = new System.Threading.Thread(ThreadRefresh);
            //threadRefresh.IsBackground = true;
            threadRefresh.Start();

            return bInitOk;
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
                    if(!omronFinsAPI.bConnectOmronPLC)
                    {
                        break;
                    }

                    switch (item.DataType)
                    {
                        case DataType.BIT :
                            if(omronFinsAPI.ReadSingleElement(item, ref objTemp))   
                            {
                                item.strValue = (bool)objTemp ? "1" : "0";
                            }
                            break;
                        case DataType.INT16:
                            if (omronFinsAPI.ReadSingleElement(item, ref objTemp))
                            {
                                item.strValue = ((Int16)objTemp).ToString();
                            }
                            break;
                        case DataType.UINT16:
                            if (omronFinsAPI.ReadSingleElement(item, ref objTemp))
                            {
                                item.strValue = ((UInt16)objTemp).ToString();
                            }
                            break;
                        case DataType.INT32:
                            if(omronFinsAPI.ReadSingleElement(item, ref objTemp))
                            {
                                item.strValue = ((Int32)objTemp).ToString();
                            }
                            break;
                        case DataType.UINT32:
                            if(omronFinsAPI.ReadSingleElement(item, ref objTemp))
                            {
                                item.strValue = ((UInt32)objTemp).ToString();
                            }
                            break;
                        case DataType.REAL:
                            if(omronFinsAPI.ReadSingleElement(item, ref objTemp))
                            {
                                item.strValue = ((float)objTemp).ToString();
                            }
                            break;
                        case DataType.STRING:
                            if(omronFinsAPI.ReadString(item, 32, ref strValue))
                            {
                                item.strValue = strValue.Replace('\0', ' ').Trim();
                            }
                            break;
                        default: break;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        private void ReConnectToPlc()
        {
            try
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
                    omronFinsAPI.ConnectToOmronPLC(plcData.RemoteIP, 9600,plcData.LocalIP);
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
            catch (Exception)
            {
            }
        }
        private void ThreadRefresh()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);

            while (!MainModule.formMain.bExit)
            {
                System.Threading.Thread.Sleep(10);
                if (!omronFinsAPI.bConnectOmronPLC)
                {
                    //MainModule.alarmManage.InsertAlarm(AlarmKeys.PLCOmron连接断开, "与PLC[" + plcData.Name + "]断开连接。");
                    //ReConnectToPlc();
                    //if (omronFinsAPI.bConnectOmronPLC)
                    //{
                    //    MainModule.alarmManage.RemoveAlarm(AlarmKeys.PLCOmron连接断开);
                    //}
                    continue;
                }

                try
                {
                    ThreadRefreshHandler();
                }
                catch
                {
                }
            }
            return;
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
