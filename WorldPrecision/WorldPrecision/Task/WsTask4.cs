using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Forms.TipsForm;
using WorldPrecision.Logs;
using WorldGeneralLib;
using WorldGeneralLib.IO;
using WorldGeneralLib.Alarm;
using WorldGeneralLib.Data;
using WorldGeneralLib.Table;
using WorldGeneralLib.Hardware.Yamaha.RCX340;
using WorldGeneralLib.Hardware;
using WorldPrecision.NameItems;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using CATL;
using WorldGeneralLib.Company.Catl.MES.G08;

namespace WorldPrecision.Task
{
    /// <summary>
    /// 清洗工位4任务逻辑
    /// </summary>
    public class WsTask4 : TaskUnit
    {
        public float fSprayingPressure = 0.0f;  //清洗喷淋压力
        public float fDryingPressure = 0.0f;    //干燥压力
        public string strInsideCode = "";       //内线二维码
        public string strOutsideCode = "";      //外线二维码
        public string strErr = "Task alarm , machine stop.";
        public PlcOmronTypeNJ mainPLC = null;
        public PlcOmronTypeNJ remotePLC = null;
        private bool _bMESRet = false;
        public WsTask4(string name, TaskGroup taskGroup) : base(name, taskGroup)
        {
            if (HardwareManage.dicHardwareDriver.ContainsKey("PLC1"))
            {
                mainPLC = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver["PLC1"];
            }
            if (HardwareManage.dicHardwareDriver.ContainsKey("PLC2"))
            {
                remotePLC = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver["PLC2"];
            }
        }

        public override void Process()
        {
            if (taskInfo.bTaskAlarm || MainModule.alarmManage.IsAlarm || IOManage.bEStop || !MainModule.formMain.bRunFlag)
            {
                taskInfo.iTaskStep = MainModule.formMain.macHomeSta != MacHomeSta.Reseted ? 0 : taskInfo.iTaskStep;
                taskInfo.bTaskAlarm = MainModule.formMain.bClrFlag ? false : taskInfo.bTaskAlarm;
                taskInfo.bTaskFinish = false;
                taskInfo.bTaskOnGoing = false;
                MainModule.formMain.bRunFlag = false;

                return;
            }

            taskInfo.bTaskAlarm = false;
            taskInfo.bTaskOnGoing = true;
            switch (taskInfo.iTaskStep)
            {
                case 0:
                    if (MainModule.formMain.bRunFlag)
                    {
                        taskInfo.bTaskFinish = false;
                        taskInfo.bTaskAlarm = false;
                        taskInfo.bTaskOnGoing = true;
                        taskInfo.htTimer.Start();

                        _bMESRet = false;
                        taskHiperTimer.Start();
                        taskInfo.iTaskStep = 11;
                    }
                    break;
                case 11:
                    taskGroup.AddRunMessage("等待清洗开始或者清洗完成...", OutputLevel.Trace);
                    taskInfo.iTaskStep = 12;
                    break;
                case 12:
                    int iSta = 0;
                    Int32.TryParse(mainPLC.plcData.dicScanItems[PLC1.工位4清洗状态].strValue, out iSta);

                    if (100 == iSta || 101 == iSta)
                    {
                        if (100 == iSta)
                        {
                            //通知PLC PC已经收到清洗开始信号
                            mainPLC.WriteData(PLC1.工位4清洗状态, 101);
                        }
                        //电芯清洗中开始采集数据
                        float fTemp1 = 0;
                        float fTemp2 = 0;
                        float.TryParse(remotePLC.plcData.dicScanItems[PLC2.工位4喷淋压力].strValue, out fTemp1);
                        float.TryParse(remotePLC.plcData.dicScanItems[PLC2.工位4干燥压力].strValue, out fTemp2);

                        fSprayingPressure = fTemp1 > fSprayingPressure ? fTemp1 : fSprayingPressure;
                        fDryingPressure = fTemp2 > fDryingPressure ? fTemp2 : fDryingPressure;

                        break;
                    }
                    else if (110 == iSta)
                    {
                        //电芯清洗完成
                        taskInfo.iTaskStep = 21;
                    }
                    else
                    {
                        break;
                    }
                    mainPLC.WriteData(PLC1.工位4清洗状态, 0);
                    taskGroup.AddRunMessage("电芯清洗完成，从PLC获取二维码信息...", OutputLevel.Trace);
                    break;
                case 21:
                    //获取二维码
                    strInsideCode = "";
                    strOutsideCode = "";
                    mainPLC.omronFinsAPI.ReadString(mainPLC.plcData.dicScanItems[PLC1.工位4二维码1], 32, ref strInsideCode);
                    mainPLC.omronFinsAPI.ReadString(mainPLC.plcData.dicScanItems[PLC1.工位4二维码2], 32, ref strOutsideCode);

                    strInsideCode = strInsideCode.Replace('\0', ' ').Trim();
                    strOutsideCode = strOutsideCode.Replace('\0', ' ').Trim();
                    if (strInsideCode.Length < 1 || strOutsideCode.Length < 1)
                    {
                        mainPLC.WriteData(PLC1.工位4清洗状态, 301);
                        strErr = "从PLC获取二维码失败！";
                        taskInfo.iTaskStep = 501;
                        break;
                    }

                    taskGroup.AddRunMessage("获取二维码成功：", OutputLevel.Trace);
                    taskGroup.AddRunMessage("内线二维码：" + strInsideCode, OutputLevel.Trace);
                    taskGroup.AddRunMessage("外线二维码：" + strOutsideCode, OutputLevel.Trace);

                    if (!Program.formStart.bMesSwitch)
                    {
                        //MES屏蔽
                        mainPLC.WriteData(PLC1.工位4清洗状态, 200);

                        taskInfo.iTaskStep = 41;
                        break;
                    }

                    taskGroup.AddRunMessage("内线电芯MES数据上载开始...", OutputLevel.Trace);
                    taskInfo.iTaskStep = 31;
                    break;
                case 31:
                    //上载内线电芯清洗数据
                    MESRes res;
                    SprayingInfo info = new SprayingInfo();
                    info.strSprayingPressure = fSprayingPressure.ToString("0.0");
                    info.strSprayingTime = remotePLC.plcData.dicScanItems[PLC2.喷淋时间].strValue;
                    info.strDryingPressure = fDryingPressure.ToString("0.0");
                    info.strBlowingResidualFluidTime = remotePLC.plcData.dicScanItems[PLC2.吹残液时间].strValue;
                    info.strDryingTime = remotePLC.plcData.dicScanItems[PLC2.干燥时间].strValue;
                    info.strOilTemp = remotePLC.plcData.dicScanItems[PLC2.油温].strValue;

                    res = MainModule.formMain.mes.DataCollectForSfcEx(strInsideCode, info);
                    if (!res.bSuccess || null == res.miCheckResponse.@return || res.miCheckResponse.@return.code != 0)
                    {
                        //MES NG
                        mainPLC.WriteData(PLC1.工位4清洗状态, 302);
                        strErr = "内线电芯MES数据上载失败";
                        taskGroup.AddRunMessage("内线电芯MES数据上载失败！", OutputLevel.Warn);
                        taskInfo.iTaskStep = 41;

                        break;
                    }

                    taskGroup.AddRunMessage("内线电芯清洗数据上载成功，开始上载外线电芯清洗数据...", OutputLevel.Trace);
                    res = MainModule.formMain.mes.DataCollectForSfcEx(strOutsideCode, info);
                    if (!res.bSuccess || null == res.miCheckResponse.@return || res.miCheckResponse.@return.code != 0)
                    {
                        //MES NG
                        mainPLC.WriteData(PLC1.工位4清洗状态, 302);
                        strErr = "外线电芯清洗数据上载失败";
                        taskGroup.AddRunMessage("外线电芯清洗数据上载失败！", OutputLevel.Warn);
                        taskInfo.iTaskStep = 41;

                        break;
                    }

                    _bMESRet = true;
                    mainPLC.WriteData(PLC1.工位4清洗状态, 200);
                    taskGroup.AddRunMessage("上载完成，正常终了。", OutputLevel.Trace);
                    taskInfo.iTaskStep = 41;
                    break;
                case 41:
                    taskGroup.AddRunMessage("写出csv文件...");

                    MESLocalData data = new MESLocalData();

                    #region Set MES local data1
                    data.strBarcode = strInsideCode;
                    data.strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                    data.strResourceID = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.resource);
                    if (!Program.formStart.bMesSwitch)
                    {
                        data.strMESResult = "Disable";
                    }
                    else
                    {
                        data.strMESResult = _bMESRet ? "OK" : "NG";
                    }
                    data.strCleanResult = remotePLC.plcData.dicScanItems[PLC2.清洗结果].strValue.Equals("1") ? "OK" : "NG";
                    data.strSprPre = fSprayingPressure.ToString("0.0");
                    data.strSprPreMaxSettingVal = remotePLC.plcData.dicScanItems[PLC2.清洗喷淋压力设定上限].strValue;
                    data.strSprPreMinSettingVal = remotePLC.plcData.dicScanItems[PLC2.清洗喷淋压力设定下限].strValue;
                    data.strSprTime = remotePLC.plcData.dicScanItems[PLC2.喷淋时间].strValue;
                    data.strSprTimeSettingVal = data.strSprTime;
                    data.strDryPre = fDryingPressure.ToString("0.0");
                    data.strDryPreMaxSettingVal = remotePLC.plcData.dicScanItems[PLC2.干燥压力设定上限].strValue;
                    data.strDryPreMinSettingVal = remotePLC.plcData.dicScanItems[PLC2.干燥压力设定下限].strValue;
                    data.strDryTime = remotePLC.plcData.dicScanItems[PLC2.干燥时间].strValue;
                    data.strDryTimeSettingVal = data.strDryTime;
                    data.strBlowingTime = remotePLC.plcData.dicScanItems[PLC2.吹残液时间].strValue;
                    data.strBlowingTimeSettingVal = data.strBlowingTime;
                    data.strOilTemp = remotePLC.plcData.dicScanItems[PLC2.油温].strValue;
                    data.strOilTempSettingVal = data.strOilTemp;
                    #endregion

                    WriteMesFile.WriteToCsvFile(data);
                    data.strBarcode = strOutsideCode;
                    WriteMesFile.WriteToCsvFile(data);

                    Program.formStart.ShowCleanResult(strInsideCode, "Station 1", fSprayingPressure.ToString("0.0"), remotePLC.plcData.dicScanItems[PLC2.喷淋时间].strValue,
                                                      fDryingPressure.ToString("0.0"), remotePLC.plcData.dicScanItems[PLC2.吹残液时间].strValue, remotePLC.plcData.dicScanItems[PLC2.干燥时间].strValue, remotePLC.plcData.dicScanItems[PLC2.油温].strValue, _bMESRet);
                    Program.formStart.ShowCleanResult(strOutsideCode, "Station 1", fSprayingPressure.ToString("0.0"), remotePLC.plcData.dicScanItems[PLC2.喷淋时间].strValue,
                                                      fDryingPressure.ToString("0.0"), remotePLC.plcData.dicScanItems[PLC2.吹残液时间].strValue, remotePLC.plcData.dicScanItems[PLC2.干燥时间].strValue, remotePLC.plcData.dicScanItems[PLC2.油温].strValue, _bMESRet);

                    if (_bMESRet || !Program.formStart.bMesSwitch)
                    {
                        taskInfo.iTaskStep = 601;
                    }
                    else
                    {
                        taskInfo.iTaskStep = 501;
                    }
                    break;
                #region Error 501-600
                case 501: //致命异常，需重新开始
                    taskInfo.bTaskOnGoing = false;
                    taskInfo.bTaskFinish = false;
                    taskInfo.bTaskAlarm = true;
                    taskInfo.iTaskStep = 0;

                    fSprayingPressure = 0;
                    fDryingPressure = 0;

                    mainPLC.plcData.dicScanItems[PLC1.工位4清洗状态].strValue = "0";
                    taskGroup.AddRunMessage(strErr, OutputLevel.Warn);
                    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.扫码异常, strErr);
                    break;
                case 502:   //一般异常，解除后按下开始可以继续
                    taskInfo.bTaskOnGoing = false;
                    taskInfo.bTaskFinish = false;
                    taskInfo.bTaskAlarm = true;
                    taskInfo.strTaskMes = "异常报警解除后，可以继续执行。";
                    taskInfo.iTaskStep = taskInfo.iTaskAlarmStep;

                    fSprayingPressure = 0;
                    fDryingPressure = 0;

                    mainPLC.plcData.dicScanItems[PLC1.工位4清洗状态].strValue = "0";
                    taskGroup.AddRunMessage(strErr, OutputLevel.Warn);
                    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.扫码异常, strErr);
                    break;
                #endregion
                #region Success 601 - 999
                case 601:
                    taskInfo.bTaskOnGoing = false;
                    taskInfo.bTaskFinish = true;
                    taskInfo.bTaskAlarm = false;
                    taskInfo.iTaskStep = 0;
                    taskInfo.iTaskAlarmStep = 0;

                    fSprayingPressure = 0;
                    fDryingPressure = 0;

                    mainPLC.plcData.dicScanItems[PLC1.工位4清洗状态].strValue = "0";
                    taskGroup.AddRunMessage("正常终了。", OutputLevel.Trace);
                    break;
                #endregion
                default:
                    taskInfo.iTaskStep = 0;
                    taskInfo.bTaskAlarm = true;
                    break;
            }

        }
    }
}
