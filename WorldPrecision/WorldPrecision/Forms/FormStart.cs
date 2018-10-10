using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WorldPrecision.Task;
using WorldGeneralLib;
using WorldGeneralLib.Alarm;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using WorldGeneralLib.Hardware.CodeReader.Keyence.SR700;
using WorldGeneralLib.Forms.TipsForm;
using WorldPrecision.NameItems;

namespace WorldPrecision.Forms
{
    public partial class FormStart : Form
    {
        private object _objLock;
        private bool _bClrAllAlarm = false;
        public FormProductInfo formProductInfo;
        private PlcOmronTypeNJ _plc1 = null;
        private PlcOmronTypeNJ _plc2 = null;
        private KeyenceSR700 _codereader1 = null;
        private KeyenceSR700 _codereader2 = null;
        public bool bMesSwitch = true;

        //Tasks
        private TaskGroup _scanGroup;
        private ScanTask _scanTask;

        private TaskGroup _wsTask1Group;
        private WsTask1 _wsTask1;
        private TaskGroup _wsTask2Group;
        private WsTask2 _wsTask2;
        private TaskGroup _wsTask3Group;
        private WsTask3 _wsTask3;
        private TaskGroup _wsTask4Group;
        private WsTask4 _wsTask4;
        private TaskGroup _wsTask5Group;
        private WsTask5 _wsTask5;
        private TaskGroup _wsTask6Group;
        private WsTask6 _wsTask6;

        public FormStart()
        {
            InitializeComponent();
            _objLock = new object();
        }

        #region Method
        private void InitAllTasks()
        {
            //ScanTask
            _scanGroup = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Scan task", Logs.Log.ScanTaskLog, true));
            _scanTask = new ScanTask("ScanTask", _scanGroup);
            _scanGroup.AddTaskUnit(_scanTask);
            _scanGroup.StartThreadAlwayScan();

            //WsTask1
            _wsTask1Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task1", Logs.Log.WsTask1Log, true));
            _wsTask1 = new WsTask1("WorkstationTask1", _wsTask1Group);
            _wsTask1Group.AddTaskUnit(_wsTask1);
            _wsTask1Group.StartThreadAlwayScan();

            //WsTask2
            _wsTask2Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task2", Logs.Log.WsTask2Log, true));
            _wsTask2 = new WsTask2("WorkstationTask2", _wsTask2Group);
            _wsTask2Group.AddTaskUnit(_wsTask2);
            _wsTask2Group.StartThreadAlwayScan();

            //WsTask3
            _wsTask3Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task3", Logs.Log.WsTask3Log, true));
            _wsTask3 = new WsTask3("WorkstationTask3", _wsTask3Group);
            _wsTask3Group.AddTaskUnit(_wsTask3);
            _wsTask3Group.StartThreadAlwayScan();

            //WsTask4
            _wsTask4Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task4", Logs.Log.WsTask4Log, true));
            _wsTask4 = new WsTask4("WorkstationTask4", _wsTask4Group);
            _wsTask4Group.AddTaskUnit(_wsTask4);
            _wsTask4Group.StartThreadAlwayScan();

            //WsTask5
            _wsTask5Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task5", Logs.Log.WsTask5Log, true));
            _wsTask5 = new WsTask5("WorkstationTask5", _wsTask5Group);
            _wsTask5Group.AddTaskUnit(_wsTask5);
            _wsTask5Group.StartThreadAlwayScan();

            //WsTask6
            _wsTask6Group = new TaskGroup(MainModule.formMain.CreateNewOutputWindow("Workstation Task6", Logs.Log.WsTask6Log, true));
            _wsTask6 = new WsTask6("WorkstationTask6", _wsTask6Group);
            _wsTask6Group.AddTaskUnit(_wsTask6);
            _wsTask6Group.StartThreadAlwayScan();
        }
        public void SetUserInfoToPLC()
        {
            try
            {
                PlcOmronTypeNJ plc = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver[HardwareName.PLC1];
                if(!plc.IsConnected())
                {
                    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.用户信息传输失败, "用户信息发送至PLC失败，PLC连接异常！");
                    return;
                }
                plc.WriteData(PLC1.用户账户,WorldGeneralLib.Login.LoginManage.strCurrUserName);
                plc.WriteData(PLC1.用户等级,WorldGeneralLib.Login.LoginManage.iCurrUserLevel);
            }
            catch
            {
                MainModule.alarmManage.InsertAlarm(AppAlarmKeys.用户信息传输失败, "用户信息发送至PLC失败！");
            }
        }
        public void ShowScanResult(bool bScanResult, float fTime,string strInsideCode, string strOutsideCode,string strErrMsg)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Action action = () =>
                    {
                        labResult.Text = bScanResult ? "OK" : "NG";
                        labResult.ForeColor = bScanResult ? Color.LightGreen : Color.Red;
                        labInsideCode.Text = string.IsNullOrEmpty(strInsideCode) ? "<null>" : strInsideCode;
                        labOutsideCode.Text = string.IsNullOrEmpty(strOutsideCode) ? "<null>" : strOutsideCode;
                        labErrMsg.Text = strErrMsg;
                        labCycleTime.Text = ((int)fTime).ToString() + " ms";

                        labInsideCode.ForeColor = labInsideCode.Text.Contains("null") ? Color.Red : Color.White;
                        labOutsideCode.ForeColor = labOutsideCode.Text.Contains("null") ? Color.Red : Color.White;
                    };
                    this.Invoke(action);
                }
                else
                {
                    labResult.Text = bScanResult ? "OK" : "NG";
                    labResult.ForeColor = bScanResult ? Color.LightGreen : Color.Red;
                    labInsideCode.Text = string.IsNullOrEmpty(strInsideCode) ? "<null>" : strInsideCode;
                    labOutsideCode.Text = string.IsNullOrEmpty(strOutsideCode) ? "<null>" : strOutsideCode;
                    labErrMsg.Text = strErrMsg;
                    labCycleTime.Text = ((int)fTime).ToString() + " ms";

                    labInsideCode.ForeColor = labInsideCode.Text.Contains("null") ? Color.Red : Color.White;
                    labOutsideCode.ForeColor = labOutsideCode.Text.Contains("null") ? Color.Red : Color.White;
                }
            }
            catch (Exception)
            {
            }
        }

        public void FuncTest()
        {
            for(int index=0;index<20;index++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();

                cell1.Value = "12345678900" + index.ToString();
                cell2.Value = DateTime.Now.ToString();
                cell3.Value = "0.03 Mpa";
                cell4.Value = "3.5 s";
                cell5.Value = "0.05 Mpa";
                cell6.Value = "30 s";
                cell7.Value = "10 s";
                cell8.Value = "60 ℃";
                cell9.Value = "OK";

                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);
                row.Cells.Add(cell7);
                row.Cells.Add(cell8);
                row.Cells.Add(cell9);

                dataGridView1.Rows.Add(row);
            }
        }

        public void ShowCleanResult(string strCode, string strWS,string strSprPress, string strSprTime, string strDryPress,string strBlowTime, string strDryTime, string strOilTemp, bool bMESRet)
        {
            Action action = () =>
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell6 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell7 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell8 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell9 = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell cell10 = new DataGridViewTextBoxCell();

                cell1.Value = strCode.Replace('\0',' ').Trim();
                cell2.Value = strWS;
                cell3.Value = DateTime.Now.ToString();
                cell4.Value = strSprPress + " kPa";
                cell5.Value = strSprTime + " s";
                cell6.Value = strDryPress + " mPa";
                cell7.Value = strBlowTime + " s";
                cell8.Value = strDryTime + " s";
                cell9.Value = strOilTemp + " ℃";
                cell10.Value = bMESRet? "OK" :"NG";

                row.Cells.Add(cell1);
                row.Cells.Add(cell2);
                row.Cells.Add(cell3);
                row.Cells.Add(cell4);
                row.Cells.Add(cell5);
                row.Cells.Add(cell6);
                row.Cells.Add(cell7);
                row.Cells.Add(cell8);
                row.Cells.Add(cell9);
                row.Cells.Add(cell10);

                dataGridView1.Rows.Add(row);
            };
            this.Invoke(action);
        }

        #endregion

        #region Events
        #region Load . Exit
        private void FormStart_Load(object sender, EventArgs e)
        {
            MainModule.formMain.eventStartButtonPushedHandler += new WorldGeneralLib.Forms.FormMain.EventStartButtonPushedHandler(this.EventStartButtongPushedHandler);
            MainModule.formMain.eventStopButtonPushedHandler += new WorldGeneralLib.Forms.FormMain.EventStopButtonPushedHandler(this.EventStopButtongPushedHandler);
            MainModule.formMain.eventResetButtonPushedHandler += new WorldGeneralLib.Forms.FormMain.EventResetButtonPushedHandler(this.EventResetButtongPushedHandler);
            MainModule.formMain.eventClearButtonPressedHandler += new WorldGeneralLib.Forms.FormMain.EventClearButtonPressedHandler(this.EventClearButtonPressedHandler);
            MainModule.formMain.eventClearButtonReleasedHandler += new WorldGeneralLib.Forms.FormMain.EventClearButtonReleasedHandler(this.EventClearButtonReleaseHandler);
            WorldGeneralLib.Login.LoginManage.eventUserChanged += new WorldGeneralLib.Login.LoginManage.EventUserChanged(this.EventUserChangedHandler);
            formProductInfo = new FormProductInfo();
            formProductInfo.TopLevel = false;
            panelProductInfo.Controls.Add(formProductInfo);
            formProductInfo.Dock = DockStyle.Fill;
            formProductInfo.Show();

            //FuncTest();
            InitAllTasks();
            timer1.Start();
            timerBatterySta.Start();
            timerStationSta.Start();
            //SetUserInfoToPLC();
        }
#endregion

        #region Machine operator button pushed
        public void EventStartButtongPushedHandler()
        {
            if (MainModule.alarmManage.IsAlarm)
            {
                MainModule.alarmManage.InsertAlarm(AppAlarmKeys.异常处理提醒, "请先处理异常报警！");
                return;
            }
            if(MainModule.formMain.macHomeSta == MacHomeSta.Reseted)
            {
                MainModule.formMain.bRunFlag = true;
            }
        }
        public void EventStopButtongPushedHandler()
        {
            MainModule.formMain.bRunFlag = false;
        }
        public void EventResetButtongPushedHandler()
        {
            try
            {
                if(MainModule.alarmManage.IsAlarm)
                {
                    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.异常处理提醒, "请先处理异常报警！");
                    return;
                }
                if(!MainModule.formMain.bRunFlag)
                {
                    MainModule.formMain.macHomeSta = MacHomeSta.Reseting;
                    MainModule.formMain.macHomeSta = MacHomeSta.Reseted;
                }
            }
            catch (Exception)
            {
            }
        }
        public void EventClearButtonPressedHandler()
        {
            try
            {
                MainModule.formMain.bClrFlag = true;
            }
            catch (Exception)
            {
            }
        }
        public void EventClearButtonReleaseHandler()
        {
            _bClrAllAlarm = true;
            MainModule.formMain.bClrFlag = false;
        }
        #endregion

        #region Login events
        public void EventUserChangedHandler()
        {
            SetUserInfoToPLC();
        }
        #endregion
        private void panel2_SizeChanged(object sender, EventArgs e)
        {
            int iWidth = dataGridView1.Width;
            dataGridView1.Columns[0].Width = (int)(iWidth * 1) / 10;
            dataGridView1.Columns[1].Width = (int)(iWidth * 0.8) / 10;
            dataGridView1.Columns[2].Width = (int)(iWidth * 1) / 10;
            dataGridView1.Columns[3].Width = (int)(iWidth * 1.1) / 10;
            dataGridView1.Columns[4].Width = iWidth * 1 / 10;
            dataGridView1.Columns[5].Width = (int)(iWidth * 1.1) / 10;
            dataGridView1.Columns[6].Width = iWidth * 1 / 10;
            dataGridView1.Columns[7].Width = iWidth * 1 / 10;
            dataGridView1.Columns[8].Width = iWidth * 1 / 10;
            dataGridView1.Columns[9].Width = (int)(iWidth * 1) / 10 - 10;

            labTitle.Location = new Point((panel2.Width-labTitle.Width)/2,labTitle.Location.Y);
            labPlanView.Location = new Point((panel2.Width - labPlanView.Width) / 2, labPlanView.Location.Y);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 1);
            e.Graphics.DrawRectangle(pen, 0, 0, this.panel3.Width - 1, this.panel3.Height - 1);
        }

        private void panel3_SizeChanged(object sender, EventArgs e)
        {
            panel3.Refresh();
        }
        #endregion

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            #region Clr button pressed handle
            if (_bClrAllAlarm)
            {
                if (MainModule.alarmManage.IsAlarm && _bClrAllAlarm)
                {
                    MainModule.alarmManage.RemoveAllAlarm();
                }
                _bClrAllAlarm = false;
            }
            #endregion
            #region Set toolStripStatus
            if(null == _codereader1 && HardwareManage.dicHardwareDriver.ContainsKey(HardwareName.内线读码器))
            {
                _codereader1 = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.内线读码器];
            }
            if(null == _codereader2 && HardwareManage.dicHardwareDriver.ContainsKey(HardwareName.外线读码器))
            {
                _codereader2 = (KeyenceSR700)HardwareManage.dicHardwareDriver[HardwareName.外线读码器];
            }
            if(null != _plc1 && null != _plc2)
            {
                MainModule.formMain.SetPLCStatus(1, _plc1.IsConnected());
                MainModule.formMain.SetPLCStatus(2, _plc2.IsConnected());

                //if (!_plc1.IsConnected())
                //    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.与主控PLC连接断开, "与主控PLC连接断开,请检查PLC是否已上电以及网络连接是否正常。");
                //else
                //    MainModule.alarmManage.RemoveAlarm(AppAlarmKeys.与主控PLC连接断开);
                //if (!_plc2.IsConnected())
                //    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.与清洗机PLC连接断开, "与清洗机PLC连接断开,请检查PLC是否已上电以及网络连接是否正常。");
                //else
                //    MainModule.alarmManage.RemoveAlarm(AppAlarmKeys.与清洗机PLC连接断开);

            }
            if(null != _codereader1 && null != _codereader2)
            {
                MainModule.formMain.SetCodeReaderStatus(1, _codereader2.IsConnected());
                MainModule.formMain.SetCodeReaderStatus(2, _codereader2.IsConnected());

                //if (!_codereader1.IsConnected())
                //    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.与内线读码器连接断开, "与内线读码器连接断开。");
                //else
                //    MainModule.alarmManage.RemoveAlarm(AppAlarmKeys.与内线读码器连接断开);

                //if (!_codereader2.IsConnected())
                //    MainModule.alarmManage.InsertAlarm(AppAlarmKeys.与外线读码器连接断开, "与外线读码器连接断开。");
                //else
                //    MainModule.alarmManage.RemoveAlarm(AppAlarmKeys.与外线读码器连接断开);
            }
            #endregion
            #region Write PC status to PLC
            try
            {
                if(null != _plc1 && _plc1.IsConnected())
                {
                    int iPCAlarmSta = MainModule.alarmManage.IsAlarm ? 1 : 0;
                    if(!iPCAlarmSta.ToString().Equals(_plc1.plcData.dicScanItems[PLC1.上位机状态].strValue))
                    {
                        _plc1.WriteData(PLC1.上位机状态,iPCAlarmSta);
                    }
                }
            }
            catch (Exception)
            {
            }
            #endregion
        }
        private void timerBatterySta_Tick(object sender, EventArgs e)
        {
            try
            {
                if (null == _plc1)
                {
                    _plc1 = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver[HardwareName.PLC1];
                }
                if (null == _plc2)
                {
                    _plc2 = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver[HardwareName.PLC2];
                }
                Color colorBelt = Color.FromArgb(217, 253, 217);
                #region NG槽
                labBatteryStaNGA1.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码NG槽1].strValue == "1" ? Color.Red : Color.White;
                labBatteryStaNGA2.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码NG槽2].strValue == "1" ? Color.Red : Color.White;
                labBatteryStaNGB1.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码NG槽3].strValue == "1" ? Color.Red : Color.White;
                labBatteryStaNGB2.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码NG槽4].strValue == "1" ? Color.Red : Color.White;
                #endregion
                #region 扫码位
                labScanScanA.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码工位].strValue == "1" ? Color.Red : Color.White;
                labScanScanB.BackColor = _plc1.plcData.dicScanItems[PLC1.扫码工位].strValue == "1" ? Color.Red : Color.White;
                #endregion

                #region 上料皮带
                labInBeltStaA1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带1].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaA2.BackColor = labInBeltStaA1.BackColor;
                labInBeltStaB1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带2].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaB2.BackColor = labInBeltStaB1.BackColor;
                labInBeltStaC1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带3].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaC2.BackColor = labInBeltStaC1.BackColor;
                labInBeltStaD1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带4].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaD2.BackColor = labInBeltStaD1.BackColor;
                labInBeltStaE1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带5].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaE2.BackColor = labInBeltStaE1.BackColor;
                labInBeltStaF1.BackColor = _plc1.plcData.dicScanItems[PLC1.上料皮带6].strValue == "1" ? Color.DarkGray : colorBelt;
                labInBeltStaF2.BackColor = labInBeltStaF1.BackColor;
                #endregion
                #region 下料皮带
                labOutBeltStaA1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带1].strValue == "0" ? Color.Green : colorBelt;
                labOutBeltStaB1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带2].strValue == "0" ? Color.Green : colorBelt;
                labOutBeltStaC1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带3].strValue == "0" ? Color.Green : colorBelt;
                labOutBeltStaD1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带4].strValue == "0" ? Color.Green : colorBelt;
                labOutBeltStaE1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带5].strValue == "0" ? Color.Green : colorBelt;
                labOutBeltStaF1.BackColor = _plc1.plcData.dicScanItems[PLC1.下料皮带6].strValue == "0" ? Color.Green : colorBelt;

                labOutBeltStaA2.BackColor = labOutBeltStaA1.BackColor;
                labOutBeltStaB2.BackColor = labOutBeltStaB1.BackColor;
                labOutBeltStaC2.BackColor = labOutBeltStaC1.BackColor;
                labOutBeltStaD2.BackColor = labOutBeltStaD1.BackColor;
                labOutBeltStaE2.BackColor = labOutBeltStaE1.BackColor;
                labOutBeltStaF2.BackColor = labOutBeltStaF1.BackColor;
                #endregion
                #region 清洗腔
                labCleaningBoxStaA1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位1清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位1清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaA2.BackColor = labCleaningBoxStaA1.BackColor;
                labCleaningBoxStaB1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位2清洗状态].strValue == "100"|| _plc1.plcData.dicScanItems[PLC1.工位2清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaB2.BackColor = labCleaningBoxStaB1.BackColor;
                labCleaningBoxStaC1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位3清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位3清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaC2.BackColor = labCleaningBoxStaC1.BackColor;
                labCleaningBoxStaD1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位4清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位4清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaD2.BackColor = labCleaningBoxStaD1.BackColor;
                labCleaningBoxStaE1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位5清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位5清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaE2.BackColor = labCleaningBoxStaE1.BackColor;
                labCleaningBoxStaF1.BackColor = (_plc1.plcData.dicScanItems[PLC1.工位6清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位6清洗状态].strValue == "101") ? Color.DarkGray : Color.White;
                labCleaningBoxStaF2.BackColor = labCleaningBoxStaF1.BackColor;
                #endregion
            }
            catch (Exception)
            {
            }
        }
        private void timerStationSta_Tick(object sender, EventArgs e)
        {
            try
            {
                #region 清洗工位1
                if (_plc1.plcData.dicScanItems[PLC1.工位1清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位1清洗状态].strValue == "101")
                {
                    labCleaningBoxA.BackColor = labCleaningBoxA.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText1.Text = "清洗中";
                }
                else if (labCleaningBoxA.BackColor != Color.White)
                {
                    labCleaningBoxA.BackColor = Color.White;
                    labCleanText1.Text = "";
                }
                #endregion
                #region 清洗工位2
                if (_plc1.plcData.dicScanItems[PLC1.工位2清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位2清洗状态].strValue == "101")
                {
                    labCleaningBoxB.BackColor = labCleaningBoxB.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText2.Text = "清洗中";
                }
                else if (labCleaningBoxB.BackColor != Color.White)
                {
                    labCleaningBoxB.BackColor = Color.White;
                    labCleanText2.Text = "";
                }
                #endregion
                #region 清洗工位3
                if (_plc1.plcData.dicScanItems[PLC1.工位3清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位3清洗状态].strValue == "101")
                {
                    labCleaningBoxC.BackColor = labCleaningBoxC.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText3.Text = "清洗中";
                }
                else if (labCleaningBoxC.BackColor != Color.White)
                {
                    labCleaningBoxC.BackColor = Color.White;
                    labCleanText3.Text = "";
                }
                #endregion
                #region 清洗工位4
                if (_plc1.plcData.dicScanItems[PLC1.工位4清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位4清洗状态].strValue == "101")
                {
                    labCleaningBoxD.BackColor = labCleaningBoxD.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText4.Text = "清洗中";
                }
                else if (labCleaningBoxD.BackColor != Color.White)
                {
                    labCleaningBoxD.BackColor = Color.White;
                    labCleanText4.Text = "";
                }
                #endregion
                #region 清洗工位5
                if (_plc1.plcData.dicScanItems[PLC1.工位5清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位5清洗状态].strValue == "101")
                {
                    labCleaningBoxE.BackColor = labCleaningBoxE.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText5.Text = "清洗中";
                }
                else if (labCleaningBoxE.BackColor != Color.White)
                {
                    labCleaningBoxE.BackColor = Color.White;
                    labCleanText5.Text = "";
                }
                #endregion
                #region 清洗工位6
                if (_plc1.plcData.dicScanItems[PLC1.工位6清洗状态].strValue == "100" || _plc1.plcData.dicScanItems[PLC1.工位6清洗状态].strValue == "101")
                {
                    labCleaningBoxF.BackColor = labCleaningBoxF.BackColor == Color.Green ? Color.Gainsboro : Color.Green;
                    labCleanText6.Text = "清洗中";
                }
                else if (labCleaningBoxF.BackColor != Color.White)
                {
                    labCleaningBoxF.BackColor = Color.White;
                    labCleanText6.Text = "";
                }
                #endregion
            }
            catch (Exception)
            {
            }

        }
        #endregion
    }
}
