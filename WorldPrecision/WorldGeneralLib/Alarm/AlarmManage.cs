using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WorldGeneralLib.Functions;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Hardware.Omron.TypeNJ;
using WorldGeneralLib.Hardware.Omron.TypeNX1P;
using WorldGeneralLib.Hardware.Panasonic;

namespace WorldGeneralLib.Alarm
{
    public enum AlarmFormStyle
    {
        Normal,
        CatlStyle
    }

    public class AlarmManage
    {
        private object _objLock;
        private Dictionary<string, AlarmData> _dicCurrAlarmMsg;

        private AlarmFormStyle _alarmFormStyle;
        private FormAlarmCatl _formAlarmCatl;
        private FormAlarm _formAlarm;
        public FormAlarmManage formAlarmManage;

        public AlarmDoc docAlarm;

        //停机原因 - For Catl
        public int iStopReason = -1;

        public delegate void EventAlarmInsertHandler(string strAlarmKey);
        public EventAlarmInsertHandler eventAlarmInsert;

        public AlarmManage(AlarmFormStyle style)
        {
            docAlarm = null;
            _objLock = new object();
            _dicCurrAlarmMsg = new Dictionary<string, AlarmData>();

            _alarmFormStyle = style;
            if (style == AlarmFormStyle.CatlStyle)
            {
                _formAlarmCatl = new FormAlarmCatl();
                _formAlarmCatl.TopLevel = true;
                _formAlarmCatl.TopMost = true;
            }
            else
            {
                _formAlarm = new FormAlarm();
                _formAlarm.TopLevel = true;
                _formAlarm.TopMost = true;
            }

            formAlarmManage = new FormAlarmManage();
            StartScan();
        }
        public void LoadDoc()
        {
            docAlarm = AlarmDoc.LoadDoc();
        }
        public bool IsAlarm
        {
            get
            {
                return _dicCurrAlarmMsg.Count > 0 ? true : false;
            }
            private set {; }
        }
        public Dictionary<string, AlarmData> DicCurrAlarmMsg
        {
            get { return _dicCurrAlarmMsg; }
            private set {; }
        }
        internal bool GetValidKey(ref string strKey)
        {
            Random ran = new Random();
            int iKey = 0;
            int iTimes = 0;

            try
            {
                if(null == _dicCurrAlarmMsg)
                {
                    _dicCurrAlarmMsg = new Dictionary<string, AlarmData>();
                }

                while(iTimes < 10000)
                {
                    iKey = ran.Next(1,100000);
                    if(!_dicCurrAlarmMsg.ContainsKey(iKey.ToString()))
                    {
                        strKey = iKey.ToString();
                        return true;
                    }
                    iTimes++;
                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public void InsertAlarm(string strKey)
        {
            try
            {
                if(docAlarm.dicAlarmItems.ContainsKey(strKey))
                {
                    InsertAlarm(strKey, docAlarm.dicAlarmItems[strKey].AlarmMsg);
                }
            }
            catch (Exception)
            {
            }
        }
        public void InsertAlarm(string strKey, string strAlarmMsg)
        {
            lock(_objLock)
            {
                try
                {
                    if (string.IsNullOrEmpty(strKey))
                        return;
                    if (_dicCurrAlarmMsg.ContainsKey(strKey))
                        return;

                    AlarmData alarmData = new AlarmData();
                    alarmData.AlarmMsg = strAlarmMsg;
                    alarmData.AlarmTime = DateTime.Now;
                    alarmData.AlarmKey = strKey;
                    _dicCurrAlarmMsg.Add(strKey, alarmData);

                    iStopReason = -1;
                    TextLogWrite.WriteAlarmLog(strAlarmMsg);
                    if (strKey != AlarmKeys.未选择停机原因 && null != eventAlarmInsert)
                    {
                        this.eventAlarmInsert(strKey);
                    }
                    if (_formAlarm != null)
                        _formAlarm.ShowAlarmMsg();
                    if (_formAlarmCatl != null)
                        _formAlarmCatl.ShowAlarmMsg();
                }
                catch (Exception)
                {
                }
            }
        }
        public void RemoveAlarm(string strKey)
        {
            lock(_objLock)
            {
                if (!_dicCurrAlarmMsg.ContainsKey(strKey))
                    return;

                _dicCurrAlarmMsg.Remove(strKey);
                if (_formAlarm != null)
                    _formAlarm.ShowAlarmMsg();
                if (_formAlarmCatl != null)
                {
                    if(_dicCurrAlarmMsg.Count <= 0 && _formAlarmCatl.bNeedStopReason)
                    {
                        MainModule.alarmManage.InsertAlarm(AlarmKeys.未选择停机原因);
                    }
                    else
                    {
                        _formAlarmCatl.ShowAlarmMsg();
                    }
                } 
            }
        }
        public void RemoveAllAlarm()
        {
            lock(_objLock)
            {
                try
                {
                    _dicCurrAlarmMsg.Clear();
                    if (_formAlarm != null)
                        _formAlarm.ShowAlarmMsg();
                    if (_formAlarmCatl != null)
                    {
                        if(_formAlarmCatl.bNeedStopReason)
                        {
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.未选择停机原因);
                        }
                        else
                        {
                            _formAlarmCatl.ShowAlarmMsg();
                        }
                    } 
                }
                catch (Exception)
                {
                }
            }
        }

        #region Auto scan thread
        private void StartScan()
        {
            Thread threadScan = new Thread(ThreadScan);
            threadScan.IsBackground = true;
            threadScan.Start();
        }
        private void ThreadScan()
        {
            while(true)
            {
                Thread.Sleep(100);
                if (null == docAlarm || HardwareManage.docHardware == null)
                    continue;
                foreach(AlarmData item in docAlarm.listAlarmItems)
                {
                    try
                    {
                        if(!HardwareManage.docHardware.dicHardwareData.ContainsKey(item.AlarmSrc))
                        {
                            continue;
                        }
                        HardwareData hardwareData = HardwareManage.docHardware.dicHardwareData[item.AlarmSrc];

                        #region Omron PLC NJ series
                        if (hardwareData.Type == HardwareType.PLC && hardwareData.Series == HardwareSeries.Omron_PLC_NJ)
                        {
                            PlcOmronTypeNJData plcData = (PlcOmronTypeNJData)hardwareData;
                            if(!plcData.dicScanItems.ContainsKey(item.AlarmName))
                            {
                                continue;
                            }
                            if (plcData.dicScanItems[item.AlarmName].strValue.Equals("1"))
                            {
                                if (!DicCurrAlarmMsg.ContainsKey(item.AlarmKey))
                                {
                                    InsertAlarm(item.AlarmKey);
                                }
                            }
                            else if (DicCurrAlarmMsg.ContainsKey(item.AlarmKey))
                            {
                                RemoveAlarm(item.AlarmKey);
                            }
                        }
                        #endregion
                        #region Omron PLC NX1P series
                        if (hardwareData.Type == HardwareType.PLC && hardwareData.Series == HardwareSeries.Omron_PLC_NX1P)
                        {
                            PlcOmronTypeNX1PData plcData = (PlcOmronTypeNX1PData)hardwareData;
                            if (!plcData.dicScanItems.ContainsKey(item.AlarmName))
                            {
                                continue;
                            }
                            if (plcData.dicScanItems[item.AlarmName].strValue.Equals("1"))
                            {
                                if (!DicCurrAlarmMsg.ContainsKey(item.AlarmKey))
                                {
                                    InsertAlarm(item.AlarmKey);
                                }
                            }
                            else if (DicCurrAlarmMsg.ContainsKey(item.AlarmKey))
                            {
                                RemoveAlarm(item.AlarmKey);
                            }
                        }
                        #endregion
                        #region Panasonic PLC

                        #endregion
                    }
                    catch (Exception)
                    {
                    }
                }

                if (null == MainModule.formMain)
                    continue;
                if (MainModule.formMain.bExit)
                    break;
            }
        }
        #endregion
    }
}
