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
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using WorldPrecision.NameItems;
using WorldGeneralLib.Hardware.CodeReader.Keyence.SR700;
using WorldGeneralLib.Company.Catl.MES.G08;

namespace WorldPrecision.Task
{
    /// <summary>
    /// 扫码任务逻辑
    /// </summary>
    public class ScanTask:TaskUnit
    {
        private string strErr = "Task alarm , machine stop.";
        private PlcOmronTypeNJ _plcDriver = null;
        public KeyenceSR700 insideCodeReader = null;
        public KeyenceSR700 outsideCodeReader = null;
        public ScanTask(string name, TaskGroup taskGroup):base(name, taskGroup)
        {
            if(HardwareManage.dicHardwareDriver.ContainsKey("PLC1"))
            {
                _plcDriver = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver["PLC1"];
            }
            if(HardwareManage.dicHardwareDriver.ContainsKey(HardwareName.内线读码器))
            {
                insideCodeReader = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.内线读码器];
            }
            if (HardwareManage.dicHardwareDriver.ContainsKey(HardwareName.外线读码器))
            {
                outsideCodeReader = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.外线读码器];
            }
        }

        public override void Process()
        {
            if (taskInfo.bTaskAlarm || MainModule.alarmManage.IsAlarm || IOManage.bEStop || !MainModule.formMain.bRunFlag || null == _plcDriver)
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

                        taskInfo.iTaskStep = 1;
                    }
                    break;
                case 1:
                    taskGroup.AddRunMessage("等待扫码开始...", OutputLevel.Trace);
                    taskInfo.iTaskStep = 10;
                    break;
                case 10:
                    if(!_plcDriver.plcData.dicScanItems[PLC1.内线扫码触发].strValue.Equals("100") || !_plcDriver.plcData.dicScanItems[PLC1.外线扫码触发].strValue.Equals("100"))
                    {
                        break;
                    }
                    taskGroup.AddRunMessage("收到扫码触发信号，开始扫码...",OutputLevel.Trace);
                    taskInfo.iTaskStep = 21;
                    break;
                case 21:
                    //开始读码
                    if(null == insideCodeReader || null == outsideCodeReader)
                    {
                        _plcDriver.WriteData(PLC1.内线扫码触发, 300);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 300);

                        strErr = "未发现读码器";
                        taskInfo.iTaskStep = 501;
                        break;
                    }
                    
                    insideCodeReader.RecvData = string.Empty;
                    outsideCodeReader.RecvData = string.Empty;
                    CodeReaderRes res1 = insideCodeReader.Read(); //以异步方式读取内线二维码
                    CodeReaderRes res2 =  outsideCodeReader.Read(); //以异步方式读取外线二维码
                    if(res1 != CodeReaderRes.SUCCESS || res2 != CodeReaderRes.SUCCESS)
                    {
                        //读码命令发送失败
                        _plcDriver.WriteData(PLC1.内线扫码触发, 300);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 300);

                        strErr = GetErrMsg(res1,res2);
                        taskInfo.iTaskStep = 501;
                        Program.formStart.formProductInfo.iScanNgCount++;
                        Program.formStart.ShowScanResult(false, 0, insideCodeReader.RecvData, outsideCodeReader.RecvData, strErr);

                        break;
                    }
                    taskGroup.AddRunMessage("读码命令发送成功，等待读码完成...",OutputLevel.Trace);
                    taskHiperTimer.Start();
                    taskInfo.iTaskStep = 22;
                    break;
                case 22:
                    //等待读码完成
                    int iTimeout = 500;
                    if(DataManage.docData.dicDataGroup.ContainsKey(DataGroups.DataGroupSystem) && 
                       DataManage.docData.dicDataGroup[DataGroups.DataGroupSystem].dicDataItem.ContainsKey(DataItemSystem.等待读码完成时间))
                    {
                        iTimeout = DataManage.IntValue(DataGroups.DataGroupSystem, DataItemSystem.等待读码完成时间);
                    }
                    if(!string.IsNullOrEmpty(insideCodeReader.RecvData) && !string.IsNullOrEmpty(outsideCodeReader.RecvData))
                    {
                        if(insideCodeReader.RecvData.Contains("ERROR") || outsideCodeReader.RecvData.Contains("ERROR"))
                        {
                            //读码失败
                            _plcDriver.WriteData(PLC1.内线扫码触发, 300);
                            _plcDriver.WriteData(PLC1.外线扫码触发, 300);

                            strErr = "解码失败！";
                            taskInfo.iTaskStep = 501;
                            Program.formStart.formProductInfo.iScanNgCount++;
                            Program.formStart.ShowScanResult(false, (float)taskHiperTimer.Duration * 1000, insideCodeReader.RecvData, outsideCodeReader.RecvData, strErr);
                            break;
                        }
                        insideCodeReader.RecvData.Replace('\r',' ');
                        insideCodeReader.RecvData.Replace('\n', ' ');
                        insideCodeReader.RecvData.Trim();
                        outsideCodeReader.RecvData.Replace('\r', ' ');
                        outsideCodeReader.RecvData.Replace('\n', ' ');
                        outsideCodeReader.RecvData.Trim();
                        taskInfo.iTaskStep = 31;
                    }
                    else if(taskHiperTimer.TimeUp((double)((double)iTimeout/1000.0)))
                    {
                        //读码超时
                        _plcDriver.WriteData(PLC1.内线扫码触发, 300);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 300);

                        strErr = "读码超时！";
                        taskInfo.iTaskStep = 501;
                        Program.formStart.formProductInfo.iScanNgCount++;
                        Program.formStart.ShowScanResult(false, (float)taskHiperTimer.Duration * 1000, insideCodeReader.RecvData, outsideCodeReader.RecvData, strErr);
                    }
                    break;
                case 31:
                    //读码成功
                    taskGroup.AddRunMessage("读码成功: " + "<" + insideCodeReader.RecvData + ">  <" + outsideCodeReader.RecvData + ">", OutputLevel.Trace);
                    _plcDriver.WriteData(PLC1.内线二维码,insideCodeReader.RecvData);
                    _plcDriver.WriteData(PLC1.外线二维码, outsideCodeReader.RecvData);

                    Program.formStart.formProductInfo.iScanOkCount++;
                    Program.formStart.ShowScanResult(true, (float)taskHiperTimer.Duration * 1000, insideCodeReader.RecvData, outsideCodeReader.RecvData, "");
                    if (!Program.formStart.bMesSwitch)
                    {
                        //MES屏蔽
                        _plcDriver.WriteData(PLC1.内线扫码触发, 200);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 200);

                        taskInfo.iTaskStep = 601;
                        break;
                    }

                    taskGroup.AddRunMessage("开始进行电芯进站校验...",OutputLevel.Trace);
               
                    taskInfo.iTaskStep = 41;
                    break;
                case 41:
                    //调用MES 进行电芯进站校验
                    MESRes mesRes1 = MainModule.formMain.mes.CheckSfcStatus(insideCodeReader.RecvData);
                    MESRes mesRes2 = MainModule.formMain.mes.CheckSfcStatus(outsideCodeReader.RecvData);
                    if(!mesRes1.bSuccess || !mesRes2.bSuccess)
                    {
                        //MES 调用失败
                        _plcDriver.WriteData(PLC1.内线扫码触发, 400);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 400);

                        strErr = "MES 调用失败，请检查网络与MES设置！";
                        taskInfo.iTaskStep = 501;
                        break;
                    }
                    if(mesRes1.miCheckResponse.@return.code != 0 || mesRes2.miCheckResponse.@return.code != 0)
                    {
                        //电芯进站校验未通过
                        if(mesRes1.miCheckResponse.@return.code != 0 && mesRes2.miCheckResponse.@return.code != 0)
                        {
                            strErr = "内线与外线电芯进站校验均未通过！";
                        }
                        else if(mesRes1.miCheckResponse.@return.code != 0)
                        {
                            strErr = "内线电芯进站校验未通过！";
                        }
                        else
                        {
                            strErr = "外线电芯进站校验未通过！";
                        }
                        _plcDriver.WriteData(PLC1.内线扫码触发, 400);
                        _plcDriver.WriteData(PLC1.外线扫码触发, 400);

                        taskInfo.iTaskStep = 501;
                        break;
                    }

                    //MES 调用成功，电芯进站校验通过
                    _plcDriver.WriteData(PLC1.内线扫码触发, 200);
                    _plcDriver.WriteData(PLC1.外线扫码触发, 200);

                    taskGroup.AddRunMessage("电芯进站校验通过！",OutputLevel.Trace);
                    taskInfo.iTaskStep = 51;
                    taskHiperTimer.Start();
                    break;
                case 51:
                    if (!taskHiperTimer.TimeUp(0.5))
                        break;
                    taskInfo.iTaskStep = 601;
                    break;
                #region Error 501-600
                case 501: //致命异常，需重新开始
                    taskInfo.bTaskOnGoing = false;
                    taskInfo.bTaskFinish = false;
                    taskInfo.bTaskAlarm = true;
                    taskInfo.iTaskStep = 0;

                    _plcDriver.plcData.dicScanItems[PLC1.内线扫码触发].strValue = "0";
                    _plcDriver.plcData.dicScanItems[PLC1.外线扫码触发].strValue = "0";
                    taskGroup.AddRunMessage(strErr, OutputLevel.Warn);
                    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.扫码异常, strErr);
                    break;
                case 502:   //一般异常，解除后按下开始可以继续
                    taskInfo.bTaskOnGoing = false;
                    taskInfo.bTaskFinish = false;
                    taskInfo.bTaskAlarm = true;
                    taskInfo.strTaskMes = "异常报警解除后，可以继续执行。";
                    taskInfo.iTaskStep = taskInfo.iTaskAlarmStep;

                    _plcDriver.plcData.dicScanItems[PLC1.内线扫码触发].strValue = "0";
                    _plcDriver.plcData.dicScanItems[PLC1.外线扫码触发].strValue = "0";
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

                    _plcDriver.plcData.dicScanItems[PLC1.内线扫码触发].strValue = "0";
                    _plcDriver.plcData.dicScanItems[PLC1.外线扫码触发].strValue = "0";
                    taskGroup.AddRunMessage("正常终了。", OutputLevel.Trace);
                    break;
                #endregion
                default:
                    taskInfo.iTaskStep = 0;
                    taskInfo.bTaskAlarm = true;
                    break;
            }

        }

        private string GetErrMsg(CodeReaderRes insideCodeReaderRes, CodeReaderRes outsideCodeReaderRes)
        {
            string strErrMsg1 = "";
            string strErrMsg2 = "";

            if (insideCodeReaderRes == CodeReaderRes.ERROR) strErrMsg1 = "内线读码器发送读码命令失败！";
            else if (insideCodeReaderRes == CodeReaderRes.INITFAIL) strErrMsg1 = "内线读码器连接失败！";
            else if (insideCodeReaderRes == CodeReaderRes.TIMEOUT) strErrMsg1 = "内线读码器读码超时！";
            else  strErrMsg1 = "内线读码器读码异常！";

            if (outsideCodeReaderRes == CodeReaderRes.ERROR) strErrMsg2 = "外线读码器发送读码命令失败！";
            else if (outsideCodeReaderRes == CodeReaderRes.INITFAIL) strErrMsg2 = "外线读码器连接失败！";
            else if (outsideCodeReaderRes == CodeReaderRes.TIMEOUT) strErrMsg2 = "外线读码器读码超时！";
            else strErrMsg2 = "外线读码器读码异常！";

            return strErrMsg1 + strErrMsg2;
        }
    }
}
