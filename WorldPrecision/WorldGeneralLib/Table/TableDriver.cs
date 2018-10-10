using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Table;
using WorldGeneralLib.Alarm;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Table
{
    public enum MoveMode
    {
        STOP,
        JOG,
        ABS,
        REL,
        LIMIT,
        HOME
    }
    public class AxisMoveSta
    {
        public MoveMode moveMode;
        public double Acc;
        public double Dec;
        public double dSpeed;
        public double dPos;
    }
    public class TableDriver
    {
        public string strDriverName;
        public string strPosName;
        public TablePreStatus tablePreStatus;
        public TableData tableData;

        private Dictionary<string, IMotionAction> _dicAxisAction;
        private Dictionary<string, short> _dicAxisIndex;
        private Dictionary<string, bool> _dicAxisReady;
        private Dictionary<string, bool> _dicAxisHoming;
        private Dictionary<string, AxisMoveSta> _dicAxisMoveSta;

        public TableDriver()
        {
            _dicAxisAction = new Dictionary<string, IMotionAction>();
            _dicAxisIndex = new Dictionary<string, short>();
            _dicAxisReady = new Dictionary<string, bool>();
            _dicAxisHoming = new Dictionary<string, bool>();
            _dicAxisMoveSta = new Dictionary<string, AxisMoveSta>();

            foreach (string item in Enum.GetNames(typeof(DefaultAxis)))
            {
                _dicAxisMoveSta.Add(item, new AxisMoveSta());
            }
        }

        public void Init(TableData data)
        {
            tableData = data;
            strDriverName = data.Name;
            tablePreStatus = new TablePreStatus();
            try
            {
                foreach (string item in Enum.GetNames(typeof(DefaultAxis)))
                {
                    _dicAxisReady.Add(item, false);
                    _dicAxisHoming.Add(item, false);
                }
                foreach (string item in Enum.GetNames(typeof(DefaultAxis)))
                {
                    if (!tableData.dicTableAxisItem.ContainsKey(item))
                    {
                        continue;
                    }
                    //if(!tableData.dicTableAxisItem[item].Active)
                    //{
                    //    //本平台已添加该轴，但轴未激活
                    //    continue;
                    //}

                    _dicAxisAction.Add(item, (IMotionAction)HardwareManage.dicHardwareDriver[tableData.MotionCardName]);
                    _dicAxisIndex.Add(item, tableData.dicTableAxisItem[item].Index);
                    if (HardwareManage.dicHardwareDriver[tableData.MotionCardName].bInitOk)
                    {
                        _dicAxisReady[item] = true;
                        if(HardwareManage.docHardware.dicHardwareData[tableData.MotionCardName].Type != HardwareType.Robot)
                        {
                            _dicAxisAction[item].ServoOn(_dicAxisIndex[item]);
                        }
                        
                        if (false == tableData.dicTableAxisItem[item].UseConfigFile)
                        {
                            #region Set Limit Logic.
                            if (SenserLogic.NC == tableData.dicTableAxisItem[item].LimitLogic)
                            {
                                _dicAxisAction[item].SetLimtOn(tableData.dicTableAxisItem[item].Index);
                            }
                            else if (SenserLogic.NO == tableData.dicTableAxisItem[item].LimitLogic)
                            {
                                _dicAxisAction[item].SetLimtOff(tableData.dicTableAxisItem[item].Index);
                            }
                            else if (SenserLogic.DISABLE == tableData.dicTableAxisItem[item].LimitLogic)
                            {
                                _dicAxisAction[item].SetLimtDisable(tableData.dicTableAxisItem[item].Index);
                            }
                            #endregion
                            #region Set Home Logic.
                            if (SenserLogic.NC == tableData.dicTableAxisItem[item].OrgLogic)
                            {
                                _dicAxisAction[item].SetHomeOff(tableData.dicTableAxisItem[item].Index);
                            }
                            else
                            {
                                _dicAxisAction[item].SetHomeOn(tableData.dicTableAxisItem[item].Index);
                            }
                            #endregion
                            #region Set Org Near Logic.
                            #endregion
                            #region Set Alarm Logic
                            if (SenserLogic.NC == tableData.dicTableAxisItem[item].AlarmLogic)
                            {
                                _dicAxisAction[item].SetAlarmOn(tableData.dicTableAxisItem[item].Index);
                            }
                            else
                            {
                                _dicAxisAction[item].SetAlarmOff(tableData.dicTableAxisItem[item].Index);
                            }
                            #endregion
                            #region Set Pulse Mode
                            if (PulseMode.PLDI == tableData.dicTableAxisItem[item].AxisPulseMode)
                            {
                                _dicAxisAction[item].SetPulseMode(tableData.dicTableAxisItem[item].Index, PulseMode.PLDI);
                            }
                            else
                            {
                                _dicAxisAction[item].SetPulseMode(tableData.dicTableAxisItem[item].Index, PulseMode.CWCCW);
                            }
                            #endregion
                        }
                    }
                }
            }
            catch //(xception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }
        public void StartPosMove(string strPosName)
        {
            #region Move X
            if (_dicAxisReady["X"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["X"].AbsPosMove(tableData.dicTableAxisItem["X"].Index,
                                                                              tableData.dicTableAxisItem["X"].Acc,
                                                                              tableData.dicTableAxisItem["X"].Dec,
                                                                              tableData.dicTableAxisItem["X"].RunSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["X"].PulseToMM);
                            _dicAxisMoveSta["X"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["X"].ReferPosMove(tableData.dicTableAxisItem["X"].Index,
                                                  tableData.dicTableAxisItem["X"].Acc,
                                                  tableData.dicTableAxisItem["X"].Dec,
                                                  tableData.dicTableAxisItem["X"].RunSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["X"].PulseToMM);
                            _dicAxisMoveSta["X"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["X"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["X"].dSpeed = tableData.dicTableAxisItem["X"].RunSpeed;
                    _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["X"].Acc;
                    _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["X"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move Y
            if (_dicAxisReady["Y"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["Y"].AbsPosMove(tableData.dicTableAxisItem["Y"].Index,
                                                                              tableData.dicTableAxisItem["Y"].Acc,
                                                                              tableData.dicTableAxisItem["Y"].Dec,
                                                                              tableData.dicTableAxisItem["Y"].RunSpeed / tableData.dicTableAxisItem["Y"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Y"].PulseToMM);
                            _dicAxisMoveSta["Y"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["Y"].ReferPosMove(tableData.dicTableAxisItem["Y"].Index,
                                                  tableData.dicTableAxisItem["Y"].Acc,
                                                  tableData.dicTableAxisItem["Y"].Dec,
                                                  tableData.dicTableAxisItem["Y"].RunSpeed / tableData.dicTableAxisItem["Y"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Y"].PulseToMM);
                            _dicAxisMoveSta["Y"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["Y"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["Y"].dSpeed = tableData.dicTableAxisItem["Y"].RunSpeed;
                    _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                    _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move Z
            if (_dicAxisReady["Z"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["Z"].AbsPosMove(tableData.dicTableAxisItem["Z"].Index,
                                                                              tableData.dicTableAxisItem["Z"].Acc,
                                                                              tableData.dicTableAxisItem["Z"].Dec,
                                                                              tableData.dicTableAxisItem["Z"].RunSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Z"].PulseToMM);
                            _dicAxisMoveSta["Z"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["Z"].ReferPosMove(tableData.dicTableAxisItem["Z"].Index,
                                                  tableData.dicTableAxisItem["Z"].Acc,
                                                  tableData.dicTableAxisItem["Z"].Dec,
                                                  tableData.dicTableAxisItem["Z"].RunSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Z"].PulseToMM);
                            _dicAxisMoveSta["Z"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["Z"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["Z"].dSpeed = tableData.dicTableAxisItem["Z"].RunSpeed;
                    _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                    _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move U
            if (_dicAxisReady["U"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["U"].AbsPosMove(tableData.dicTableAxisItem["U"].Index,
                                                                              tableData.dicTableAxisItem["U"].Acc,
                                                                              tableData.dicTableAxisItem["U"].Dec,
                                                                              tableData.dicTableAxisItem["U"].RunSpeed / tableData.dicTableAxisItem["U"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["U"].PulseToMM);
                            _dicAxisMoveSta["U"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["U"].ReferPosMove(tableData.dicTableAxisItem["U"].Index,
                                                  tableData.dicTableAxisItem["U"].Acc,
                                                  tableData.dicTableAxisItem["U"].Dec,
                                                  tableData.dicTableAxisItem["U"].RunSpeed / tableData.dicTableAxisItem["U"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["U"].PulseToMM);
                            _dicAxisMoveSta["U"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["U"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["U"].dSpeed = tableData.dicTableAxisItem["U"].RunSpeed;
                    _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                    _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move A
            if (_dicAxisReady["A"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["A"].AbsPosMove(tableData.dicTableAxisItem["A"].Index,
                                                                              tableData.dicTableAxisItem["A"].Acc,
                                                                              tableData.dicTableAxisItem["A"].Dec,
                                                                              tableData.dicTableAxisItem["A"].RunSpeed / tableData.dicTableAxisItem["A"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["A"].PulseToMM);
                            _dicAxisMoveSta["A"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["A"].ReferPosMove(tableData.dicTableAxisItem["A"].Index,
                                                  tableData.dicTableAxisItem["A"].Acc,
                                                  tableData.dicTableAxisItem["A"].Dec,
                                                  tableData.dicTableAxisItem["A"].RunSpeed / tableData.dicTableAxisItem["A"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["A"].PulseToMM);
                            _dicAxisMoveSta["A"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["A"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["A"].dSpeed = tableData.dicTableAxisItem["A"].RunSpeed;
                    _dicAxisMoveSta["A"].Acc = tableData.dicTableAxisItem["A"].Acc;
                    _dicAxisMoveSta["A"].Dec = tableData.dicTableAxisItem["A"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move B
            if (_dicAxisReady["B"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["B"].AbsPosMove(tableData.dicTableAxisItem["B"].Index,
                                                                              tableData.dicTableAxisItem["B"].Acc,
                                                                              tableData.dicTableAxisItem["B"].Dec,
                                                                              tableData.dicTableAxisItem["B"].RunSpeed / tableData.dicTableAxisItem["B"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["B"].PulseToMM);
                            _dicAxisMoveSta["B"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["B"].ReferPosMove(tableData.dicTableAxisItem["B"].Index,
                                                  tableData.dicTableAxisItem["B"].Acc,
                                                  tableData.dicTableAxisItem["B"].Dec,
                                                  tableData.dicTableAxisItem["B"].RunSpeed / tableData.dicTableAxisItem["B"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["B"].PulseToMM);
                            _dicAxisMoveSta["B"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["B"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["B"].dSpeed = tableData.dicTableAxisItem["B"].RunSpeed;
                    _dicAxisMoveSta["B"].Acc = tableData.dicTableAxisItem["B"].Acc;
                    _dicAxisMoveSta["B"].Dec = tableData.dicTableAxisItem["B"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move C
            if (_dicAxisReady["C"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["C"].AbsPosMove(tableData.dicTableAxisItem["C"].Index,
                                                                              tableData.dicTableAxisItem["C"].Acc,
                                                                              tableData.dicTableAxisItem["C"].Dec,
                                                                              tableData.dicTableAxisItem["C"].RunSpeed / tableData.dicTableAxisItem["C"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["C"].PulseToMM);
                            _dicAxisMoveSta["C"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["C"].ReferPosMove(tableData.dicTableAxisItem["C"].Index,
                                                  tableData.dicTableAxisItem["C"].Acc,
                                                  tableData.dicTableAxisItem["C"].Dec,
                                                  tableData.dicTableAxisItem["C"].RunSpeed / tableData.dicTableAxisItem["C"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["C"].PulseToMM);
                            _dicAxisMoveSta["C"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["C"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["C"].dSpeed = tableData.dicTableAxisItem["C"].RunSpeed;
                    _dicAxisMoveSta["C"].Acc = tableData.dicTableAxisItem["C"].Acc;
                    _dicAxisMoveSta["C"].Dec = tableData.dicTableAxisItem["C"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
            #region Move D
            if (_dicAxisReady["D"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        if (tableData.dicTablePosItem[strPosName].MoveRel == false)
                        {
                            //Move Abs
                            _dicAxisAction["D"].AbsPosMove(tableData.dicTableAxisItem["D"].Index,
                                                                              tableData.dicTableAxisItem["D"].Acc,
                                                                              tableData.dicTableAxisItem["D"].Dec,
                                                                              tableData.dicTableAxisItem["D"].RunSpeed / tableData.dicTableAxisItem["D"].PulseToMM,
                                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["D"].PulseToMM);
                            _dicAxisMoveSta["D"].moveMode = MoveMode.ABS;

                        }
                        else
                        {
                            //Move Rel
                            _dicAxisAction["D"].ReferPosMove(tableData.dicTableAxisItem["D"].Index,
                                                  tableData.dicTableAxisItem["D"].Acc,
                                                  tableData.dicTableAxisItem["D"].Dec,
                                                  tableData.dicTableAxisItem["D"].RunSpeed / tableData.dicTableAxisItem["D"].PulseToMM,
                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["D"].PulseToMM);
                            _dicAxisMoveSta["D"].moveMode = MoveMode.REL;
                        }
                    }
                    _dicAxisMoveSta["D"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                    _dicAxisMoveSta["D"].dSpeed = tableData.dicTableAxisItem["D"].RunSpeed;
                    _dicAxisMoveSta["D"].Acc = tableData.dicTableAxisItem["D"].Acc;
                    _dicAxisMoveSta["D"].Dec = tableData.dicTableAxisItem["D"].Dec;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            #endregion
        }
        public void StartPosMove(string strPosName, bool SafeZ)
        {
            bool bNeedSafeZ = false;
            this.strPosName = strPosName;

            if (SafeZ)
            {
                if (_dicAxisReady["X"] && tableData.dicTablePosItem[strPosName].ActiveX)
                {
                    if (CurrentX != tableData.dicTablePosItem[strPosName].PosX)
                    {
                        bNeedSafeZ = true;
                    }
                }
                if (_dicAxisReady["Y"] && tableData.dicTablePosItem[strPosName].ActiveY)
                {
                    if (CurrentY != tableData.dicTablePosItem[strPosName].PosY)
                    {
                        bNeedSafeZ = true;
                    }
                }
                if (bNeedSafeZ)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(ThreadBypassZFunction);
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            else
            {
                StartPosMove(strPosName);
            }
        }
        void ThreadBypassZFunction()
        {
            if (!IsOnXYPos(strPosName, true))
            {
                _dicAxisAction["Z"].AbsPosMove(tableData.dicTableAxisItem["Z"].Index,
                                                                  tableData.dicTableAxisItem["Z"].Acc,
                                                                  tableData.dicTableAxisItem["Z"].Dec,
                                                                  tableData.dicTableAxisItem["Z"].RunSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                                                  tableData.dicTablePosItem["安全位"].PosZ / tableData.dicTableAxisItem["Z"].PulseToMM);
                _dicAxisMoveSta["Z"].moveMode = MoveMode.ABS;
                _dicAxisMoveSta["Z"].dPos = tableData.dicTablePosItem["安全位"].PosZ;
                _dicAxisMoveSta["Z"].dSpeed = tableData.dicTableAxisItem["Z"].RunSpeed;
                _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                System.Threading.Thread.Sleep(50);
                while (!MoveDone(DefaultAxis.Z))
                {
                    //if (MainModule.formMain.formAlarm.GetAlarm())
                    if (MainModule.alarmManage.IsAlarm)
                        return;
                    System.Threading.Thread.Sleep(50);
                }

                _dicAxisAction["X"].AbsPosMove(tableData.dicTableAxisItem["X"].Index,
                                                                  tableData.dicTableAxisItem["X"].Acc,
                                                                  tableData.dicTableAxisItem["X"].Dec,
                                                                  tableData.dicTableAxisItem["X"].RunSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["X"].PulseToMM);
                _dicAxisMoveSta["X"].moveMode = MoveMode.ABS;
                _dicAxisMoveSta["X"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                _dicAxisMoveSta["X"].dSpeed = tableData.dicTableAxisItem["X"].RunSpeed;
                _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["X"].Acc;
                _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["X"].Dec;
                System.Threading.Thread.Sleep(80);

                _dicAxisAction["Y"].AbsPosMove(tableData.dicTableAxisItem["Y"].Index,
                                                                  tableData.dicTableAxisItem["Y"].Acc,
                                                                  tableData.dicTableAxisItem["Y"].Dec,
                                                                  tableData.dicTableAxisItem["Y"].RunSpeed / tableData.dicTableAxisItem["Y"].PulseToMM,
                                                                  tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Y"].PulseToMM);
                _dicAxisMoveSta["Y"].moveMode = MoveMode.ABS;
                _dicAxisMoveSta["Y"].dPos = tableData.dicTablePosItem[strPosName].PosX;
                _dicAxisMoveSta["Y"].dSpeed = tableData.dicTableAxisItem["Y"].RunSpeed;
                _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                System.Threading.Thread.Sleep(50);
                while (!MoveDone(DefaultAxis.X) || !MoveDone(DefaultAxis.Y))
                //while (!_dicAxisAction["X"].IsMoveDone(tableData.dicTableAxisItem["X"].Index) || !_dicAxisAction["Y"].IsMoveDone(tableData.dicTableAxisItem["Y"].Index) || !IsOnXYPos(posName))
                {
                    if (MainModule.alarmManage.IsAlarm)
                        return;
                    System.Threading.Thread.Sleep(50);
                }
            }

            _dicAxisAction["Z"].AbsPosMove(tableData.dicTableAxisItem["Z"].Index,
                                                              tableData.dicTableAxisItem["Z"].Acc,
                                                              tableData.dicTableAxisItem["Z"].Dec,
                                                              tableData.dicTableAxisItem["Z"].RunSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                                              tableData.dicTablePosItem[strPosName].PosX / tableData.dicTableAxisItem["Z"].PulseToMM);
            _dicAxisMoveSta["Z"].moveMode = MoveMode.ABS;
            _dicAxisMoveSta["Z"].dPos = tableData.dicTablePosItem[strPosName].PosX;
            _dicAxisMoveSta["Z"].dSpeed = tableData.dicTableAxisItem["Z"].RunSpeed;
            _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
            _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
            while (!MoveDone(DefaultAxis.Z))
            //while (!_dicAxisAction["Z"].IsMoveDone(tableData.dicTableAxisItem["Z"].Index))
            {
                if (MainModule.alarmManage.IsAlarm)
                    return;
                System.Threading.Thread.Sleep(50);
            }
        }

        public bool IsOnXYPos(string strPosName, bool bEnc)
        {
            if (IsOnPos(DefaultAxis.X, strPosName, bEnc) && IsOnPos(DefaultAxis.Y, strPosName, bEnc))
                return true;
            else
                return false;
        }

        public bool IsOnXYZPos(string strPosName, bool bEnc)
        {
            if (IsOnPos(DefaultAxis.X, strPosName, bEnc) && IsOnPos(DefaultAxis.Y, strPosName, bEnc) && IsOnPos(DefaultAxis.Z, strPosName, bEnc))
                return true;
            else
                return false;
        }

        public bool IsOnPos(DefaultAxis Axis, string strPosName, bool bEnc)
        {
            double dTargetPos = 0.0;
            double dCurrentPos = 0.0;
            bool bRec = true;
            if (Axis == DefaultAxis.X)
            {
                try
                {
                    if (_dicAxisReady["X"] && tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                try
                {
                    if (_dicAxisReady["X"] && tableData.dicTablePosItem[strPosName].ActiveY)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosY;
                        dCurrentPos = _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                try
                {
                    if (_dicAxisReady["Z"] && tableData.dicTablePosItem[strPosName].ActiveZ)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosZ;
                        dCurrentPos = _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Z"].Index) * tableData.dicTableAxisItem["Z"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.U)
            {
                try
                {
                    if (_dicAxisReady["U"] && tableData.dicTablePosItem[strPosName].ActiveZ)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosZ;
                        dCurrentPos = _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            return bRec;
        }

        public bool IsOnPos(DefaultAxis Axis, double dTargetPos, bool bEnc)
        {
            double dCurrentPos = 0.0;
            bool bRec = true;
            if (Axis == DefaultAxis.X)
            {
                try
                {
                    if (_dicAxisReady["X"])
                    {
                        dCurrentPos = _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                try
                {
                    if (_dicAxisReady["Y"])
                    {
                        dCurrentPos = _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                try
                {
                    if (_dicAxisReady["Z"])
                    {
                        dCurrentPos = _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Z"].Index) * tableData.dicTableAxisItem["Z"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            if (Axis == DefaultAxis.U)
            {
                try
                {
                    if (_dicAxisReady["U"])
                    {
                        dCurrentPos = _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        { }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        { }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {
                    bRec = false;
                }
            }
            return bRec;
        }

        public bool IsOnPos(string strPosName)
        {
            double dTargetPos = 0.0;
            double dCurrentPos = 0.0;
            bool bRec = true;
            if (_dicAxisReady["X"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                        if (dTargetPos == dCurrentPos)
                        {

                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Y"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                        if (dTargetPos == dCurrentPos)
                        {

                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Z"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                        if (dTargetPos == dCurrentPos)
                        {

                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["U"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM;
                        if (dTargetPos == dCurrentPos)
                        {

                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            return bRec;
        }

        public bool IsOnPos(string strPosName, bool bEnc)
        {
            double dTargetPos = 0.0;
            double dCurrentPos = 0.0;
            bool bRec = true;
            if (_dicAxisReady["X"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveX)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosX;
                        dCurrentPos = _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        {
                        }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Y"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveY)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosY;
                        dCurrentPos = _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        {
                        }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Z"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveZ)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosZ;
                        dCurrentPos = _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Z"].Index) * tableData.dicTableAxisItem["Z"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        {
                        }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            if (_dicAxisReady["U"])
            {
                try
                {
                    if (tableData.dicTablePosItem[strPosName].ActiveU)
                    {
                        dTargetPos = tableData.dicTablePosItem[strPosName].PosU;
                        dCurrentPos = _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM;
                        double dDiffValue = Math.Abs(dTargetPos - dCurrentPos);//
                        if (bEnc && dDiffValue < 0.005)
                        {
                        }
                        else if (!bEnc && dTargetPos == dCurrentPos)
                        {
                        }
                        else
                        {
                            bRec = false;
                        }
                    }
                }
                catch
                {

                }
            }
            return bRec;
        }

        public bool MoveDone()
        {
            bool bMoveDoneX = false;
            bool bMoveDoneY = false;
            bool bMoveDoneZ = false;
            bool bMoveDoneU = false;
            if (_dicAxisReady["X"])
            {
                bMoveDoneX = _dicAxisAction["X"].IsMoveDone(tableData.dicTableAxisItem["X"].Index);
                if (bMoveDoneX)
                {
                    _dicAxisMoveSta["X"].moveMode = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneX = true;
            }
            if (_dicAxisReady["Y"])
            {
                bMoveDoneY = _dicAxisAction["Y"].IsMoveDone(tableData.dicTableAxisItem["Y"].Index);
                if (bMoveDoneY)
                {
                    _dicAxisMoveSta["Y"].moveMode = MoveMode.STOP;

                }
            }
            else
            {
                bMoveDoneY = true;
            }
            if (_dicAxisReady["Z"])
            {
                bMoveDoneZ = _dicAxisAction["Z"].IsMoveDone(tableData.dicTableAxisItem["Z"].Index);
                if (bMoveDoneZ)
                {
                    _dicAxisMoveSta["Z"].moveMode = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneZ = true;
            }
            if (_dicAxisReady["U"])
            {
                bMoveDoneU = _dicAxisAction["U"].IsMoveDone(tableData.dicTableAxisItem["U"].Index);
                if (bMoveDoneU)
                {
                    _dicAxisMoveSta["U"].moveMode = MoveMode.STOP;
                }
            }
            else
            {
                bMoveDoneU = true;
            }
            return bMoveDoneX && bMoveDoneY && bMoveDoneZ && bMoveDoneU;
        }
        public bool MoveDone(DefaultAxis Axis)
        {
            bool bMoveDoneX = false;
            bool bMoveDoneY = false;
            bool bMoveDoneZ = false;
            bool bMoveDoneU = false;
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    bMoveDoneX = _dicAxisAction["X"].IsMoveDone(tableData.dicTableAxisItem["X"].Index);
                    if (bMoveDoneX)
                    {
                        _dicAxisMoveSta["X"].moveMode = MoveMode.STOP;
                    }
                }
                else
                {
                    bMoveDoneX = true;
                }
                return bMoveDoneX;
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    bMoveDoneY = _dicAxisAction["Y"].IsMoveDone(tableData.dicTableAxisItem["Y"].Index);
                    if (bMoveDoneY)
                    {
                        _dicAxisMoveSta["Y"].moveMode = MoveMode.STOP;
                    }
                }
                else
                {
                    bMoveDoneY = true;
                }
                return bMoveDoneY;
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    bMoveDoneZ = _dicAxisAction["Z"].IsMoveDone(tableData.dicTableAxisItem["Z"].Index);
                    if (bMoveDoneZ)
                    {
                        _dicAxisMoveSta["Z"].moveMode = MoveMode.STOP;
                    }
                }
                else
                {
                    bMoveDoneZ = true;
                }
                return bMoveDoneZ;
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    bMoveDoneU = _dicAxisAction["U"].IsMoveDone(tableData.dicTableAxisItem["U"].Index);
                    if (bMoveDoneU)
                    {
                        _dicAxisMoveSta["U"].moveMode = MoveMode.STOP;
                    }
                }
                else
                {
                    bMoveDoneU = true;
                }
                return bMoveDoneU;
            }
            return true;
        }
        public void AbsMove(DefaultAxis Axis, double Acc, double Dec, double dSpeed, double dPos)
        {
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {

                        _dicAxisAction["X"].AbsPosMove(tableData.dicTableAxisItem["X"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["X"].PulseToMM);
                        _dicAxisMoveSta["X"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["X"].dPos = dPos;
                        _dicAxisMoveSta["X"].dSpeed = dSpeed;
                        _dicAxisMoveSta["X"].Acc = Acc;
                        _dicAxisMoveSta["X"].Dec = Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {

                        _dicAxisAction["Y"].AbsPosMove(tableData.dicTableAxisItem["Y"].Index,
                                           Acc,
                                           Dec,
                                           dSpeed / tableData.dicTableAxisItem["Y"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["Y"].PulseToMM);
                        _dicAxisMoveSta["Y"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["Y"].dPos = dPos;
                        _dicAxisMoveSta["Y"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Y"].Acc = Acc;
                        _dicAxisMoveSta["Y"].Dec = Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {

                        _dicAxisAction["Z"].AbsPosMove(tableData.dicTableAxisItem["Z"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["Z"].PulseToMM);
                        _dicAxisMoveSta["Z"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["Z"].dPos = dPos;
                        _dicAxisMoveSta["Z"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Z"].Acc = Acc;
                        _dicAxisMoveSta["Z"].Dec = Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {

                        _dicAxisAction["U"].AbsPosMove(tableData.dicTableAxisItem["U"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["U"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["U"].PulseToMM);
                        _dicAxisMoveSta["U"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["U"].dPos = dPos;
                        _dicAxisMoveSta["U"].dSpeed = dSpeed;
                        _dicAxisMoveSta["U"].Acc = Acc;
                        _dicAxisMoveSta["U"].Dec = Dec;

                    }
                    catch
                    {

                    }
                }
            }
        }
        //AbsMove(WeidyFrame.DefaultAxis.Z, WeidyFrame.DataManage.DoubleValue("HorizontalLeftZHeight", "Middle"), WeidyFrame.DataManage.DoubleValue("HorizontalLeftZSpeed", "Middle"));//获取参数);
        public void AbsMove(DefaultAxis Axis, double dPos, double dSpeed)
        {
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {

                        _dicAxisAction["X"].AbsPosMove(tableData.dicTableAxisItem["X"].Index,
                                            tableData.dicTableAxisItem["X"].Acc,
                                            tableData.dicTableAxisItem["X"].Dec,
                                            dSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["X"].PulseToMM);
                        _dicAxisMoveSta["X"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["X"].dPos = dPos;

                        _dicAxisMoveSta["X"].dSpeed = dSpeed;
                        _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["X"].Acc;
                        _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["X"].Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {

                        _dicAxisAction["Y"].AbsPosMove(tableData.dicTableAxisItem["Y"].Index,
                                           tableData.dicTableAxisItem["Y"].Acc,
                                           tableData.dicTableAxisItem["Y"].Dec,
                                           dSpeed / tableData.dicTableAxisItem["Y"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["Y"].PulseToMM);
                        _dicAxisMoveSta["Y"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["Y"].dPos = dPos;
                        _dicAxisMoveSta["Y"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                        _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {

                        _dicAxisAction["Z"].AbsPosMove(tableData.dicTableAxisItem["Z"].Index,
                                            tableData.dicTableAxisItem["Z"].Acc,
                                            tableData.dicTableAxisItem["Z"].Dec,
                                            dSpeed / tableData.dicTableAxisItem["Z"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["Z"].PulseToMM);
                        _dicAxisMoveSta["Z"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["Z"].dPos = dPos;
                        _dicAxisMoveSta["Z"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                        _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {

                        _dicAxisAction["U"].AbsPosMove(tableData.dicTableAxisItem["U"].Index,
                                            tableData.dicTableAxisItem["U"].Acc,
                                            tableData.dicTableAxisItem["U"].Dec,
                                            dSpeed / tableData.dicTableAxisItem["U"].PulseToMM,
                                            dPos / tableData.dicTableAxisItem["U"].PulseToMM);
                        _dicAxisMoveSta["U"].moveMode = MoveMode.ABS;
                        _dicAxisMoveSta["U"].dPos = dPos;
                        _dicAxisMoveSta["U"].dSpeed = dSpeed;
                        _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                        _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void AbsMove(DefaultAxis Axis, double dPos)
        {
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return;
                short index = tableData.dicTableAxisItem[strAxis].Index;
                double dAcc = tableData.dicTableAxisItem[strAxis].Acc;
                double dDec = tableData.dicTableAxisItem[strAxis].Dec;
                double dSpeed = tableData.dicTableAxisItem[strAxis].RunSpeed / tableData.dicTableAxisItem[strAxis].PulseToMM;
                double dDist = dPos / tableData.dicTableAxisItem[strAxis].PulseToMM;

                _dicAxisAction[strAxis].AbsPosMove(index, dAcc, dDec, dSpeed, dDist);
                _dicAxisMoveSta[strAxis].moveMode = MoveMode.ABS;
                _dicAxisMoveSta[strAxis].dPos = dPos;
                _dicAxisMoveSta[strAxis].dSpeed = tableData.dicTableAxisItem[strAxis].RunSpeed;
                _dicAxisMoveSta[strAxis].Acc = tableData.dicTableAxisItem[strAxis].Acc;
                _dicAxisMoveSta[strAxis].Dec = tableData.dicTableAxisItem[strAxis].Dec;
            }
            catch (Exception)
            {
            }
        }
        public bool AbsMoveXYZU(double dPosX, double dPosY, double dPosZ, double dPosU,double dSpeed)
        {
            try
            {
                if (!_dicAxisReady["X"] || !_dicAxisReady["Y"] || !_dicAxisReady["Z"] || !_dicAxisReady["U"])
                    return false;
                if(HardwareManage.docHardware.dicHardwareData[tableData.MotionCardName].Type == HardwareType.Robot)
                {
                    int iErr = 0;
                    return _dicAxisAction["X"].RobotAbsPosMove(1000,dPosX,dPosY,dPosZ,dPosU,dSpeed,false,ref iErr);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public void RelMove(DefaultAxis Axis, double Acc, double Dec, double dSpeed, double dPos)
        {
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return;

                _dicAxisAction[strAxis].ReferPosMove(tableData.dicTableAxisItem[strAxis].Index, Acc, Dec, dSpeed/tableData.dicTableAxisItem[strAxis].PulseToMM,dPos/tableData.dicTableAxisItem[strAxis].PulseToMM);
                _dicAxisMoveSta[strAxis].moveMode = MoveMode.REL;
                _dicAxisMoveSta[strAxis].dPos = dPos;
                _dicAxisMoveSta[strAxis].dSpeed = dSpeed;
                _dicAxisMoveSta[strAxis].Acc = Acc;
                _dicAxisMoveSta[strAxis].Dec = Dec;
            }
            catch (Exception)
            {
            }
        }
        public void RelMove(DefaultAxis Axis, double dPos, bool bHighSpd)
        {
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return;
                short index = tableData.dicTableAxisItem[strAxis].Index;
                double dAcc = tableData.dicTableAxisItem[strAxis].Acc;
                double dDec = tableData.dicTableAxisItem[strAxis].Dec;
                double dSpeed = bHighSpd? tableData.dicTableAxisItem[strAxis].RunSpeed / tableData.dicTableAxisItem[strAxis].PulseToMM : tableData.dicTableAxisItem[strAxis].JogSpeed / tableData.dicTableAxisItem[strAxis].PulseToMM;
                double dDist = dPos / tableData.dicTableAxisItem[strAxis].PulseToMM;

                _dicAxisAction[strAxis].ReferPosMove(index, dAcc, dDec, dSpeed, dDist);
                _dicAxisMoveSta[strAxis].moveMode = MoveMode.REL;
                _dicAxisMoveSta[strAxis].dPos = dPos;
                _dicAxisMoveSta[strAxis].dSpeed = dSpeed;
                _dicAxisMoveSta[strAxis].Acc = dAcc;
                _dicAxisMoveSta[strAxis].Dec = dDec;
            }
            catch (Exception)
            {
            }
        }
        public void JobMove(DefaultAxis Axis, bool bCW, bool bHighSpd)
        {
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["X"].JobMove(tableData.dicTableAxisItem["X"].Index,
                                                    tableData.dicTableAxisItem["X"].Acc,
                                                    tableData.dicTableAxisItem["X"].Dec,
                                                    tableData.dicTableAxisItem["X"].JogSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
                                _dicAxisMoveSta["X"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["X"].dSpeed = tableData.dicTableAxisItem["X"].JogSpeed;
                                _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["X"].JobMove(tableData.dicTableAxisItem["X"].Index,
                                                   tableData.dicTableAxisItem["X"].Acc,
                                                   tableData.dicTableAxisItem["X"].Dec,
                                                   tableData.dicTableAxisItem["X"].JogSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
                                _dicAxisMoveSta["X"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["X"].dSpeed = tableData.dicTableAxisItem["X"].JogSpeed;
                                _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["X"].JobMove(tableData.dicTableAxisItem["X"].Index,
                                                    tableData.dicTableAxisItem["X"].Acc,
                                                    tableData.dicTableAxisItem["X"].Dec,
                                                    -tableData.dicTableAxisItem["X"].JogSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
                                _dicAxisMoveSta["X"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["X"].dSpeed = -tableData.dicTableAxisItem["X"].JogSpeed;
                                _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["X"].JobMove(tableData.dicTableAxisItem["X"].Index,
                                                   tableData.dicTableAxisItem["X"].Acc,
                                                   tableData.dicTableAxisItem["X"].Dec,
                                                   -tableData.dicTableAxisItem["X"].JogSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
                                _dicAxisMoveSta["X"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["X"].dSpeed = -tableData.dicTableAxisItem["X"].JogSpeed;
                                _dicAxisMoveSta["X"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["X"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["Y"].JobMove(tableData.dicTableAxisItem["Y"].Index,
                                                    tableData.dicTableAxisItem["Y"].Acc,
                                                    tableData.dicTableAxisItem["Y"].Dec,
                                                    tableData.dicTableAxisItem["Y"].JogSpeed / tableData.dicTableAxisItem["Y"].PulseToMM);
                                _dicAxisMoveSta["Y"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Y"].dSpeed = tableData.dicTableAxisItem["Y"].JogSpeed;
                                _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                                _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["Y"].JobMove(tableData.dicTableAxisItem["Y"].Index,
                                                   tableData.dicTableAxisItem["Y"].Acc,
                                                   tableData.dicTableAxisItem["Y"].Dec,
                                                   tableData.dicTableAxisItem["Y"].JogSpeed / tableData.dicTableAxisItem["Y"].PulseToMM);
                                _dicAxisMoveSta["Y"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Y"].dSpeed = tableData.dicTableAxisItem["Y"].JogSpeed;
                                _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                                _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["Y"].JobMove(tableData.dicTableAxisItem["Y"].Index,
                                                    tableData.dicTableAxisItem["Y"].Acc,
                                                    tableData.dicTableAxisItem["Y"].Dec,
                                                    -tableData.dicTableAxisItem["Y"].JogSpeed / tableData.dicTableAxisItem["Y"].PulseToMM);
                                _dicAxisMoveSta["Y"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Y"].dSpeed = -tableData.dicTableAxisItem["Y"].JogSpeed;
                                _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                                _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["Y"].JobMove(tableData.dicTableAxisItem["Y"].Index,
                                                   tableData.dicTableAxisItem["Y"].Acc,
                                                   tableData.dicTableAxisItem["Y"].Dec,
                                                   -tableData.dicTableAxisItem["Y"].JogSpeed / tableData.dicTableAxisItem["Y"].PulseToMM);
                                _dicAxisMoveSta["Y"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Y"].dSpeed = -tableData.dicTableAxisItem["Y"].JogSpeed;
                                _dicAxisMoveSta["Y"].Acc = tableData.dicTableAxisItem["Y"].Acc;
                                _dicAxisMoveSta["Y"].Dec = tableData.dicTableAxisItem["Y"].Dec;
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["Z"].JobMove(tableData.dicTableAxisItem["Z"].Index,
                                                    tableData.dicTableAxisItem["Z"].Acc,
                                                    tableData.dicTableAxisItem["Z"].Dec,
                                                    tableData.dicTableAxisItem["Z"].JogSpeed / tableData.dicTableAxisItem["Z"].PulseToMM);
                                _dicAxisMoveSta["Z"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Z"].dSpeed = tableData.dicTableAxisItem["Z"].JogSpeed;
                                _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["Z"].JobMove(tableData.dicTableAxisItem["Z"].Index,
                                                   tableData.dicTableAxisItem["Z"].Acc,
                                                   tableData.dicTableAxisItem["Z"].Dec,
                                                   tableData.dicTableAxisItem["Z"].JogSpeed / tableData.dicTableAxisItem["Z"].PulseToMM);
                                _dicAxisMoveSta["Z"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Z"].dSpeed = tableData.dicTableAxisItem["Z"].JogSpeed;
                                _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["Z"].JobMove(tableData.dicTableAxisItem["Z"].Index,
                                                    tableData.dicTableAxisItem["Z"].Acc,
                                                    tableData.dicTableAxisItem["Z"].Dec,
                                                    -tableData.dicTableAxisItem["Z"].JogSpeed / tableData.dicTableAxisItem["Z"].PulseToMM);
                                _dicAxisMoveSta["Z"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Z"].dSpeed = -tableData.dicTableAxisItem["Z"].JogSpeed;
                                _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["Z"].JobMove(tableData.dicTableAxisItem["Z"].Index,
                                                   tableData.dicTableAxisItem["Z"].Acc,
                                                   tableData.dicTableAxisItem["Z"].Dec,
                                                   -tableData.dicTableAxisItem["Z"].JogSpeed / tableData.dicTableAxisItem["Z"].PulseToMM);
                                _dicAxisMoveSta["Z"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["Z"].dSpeed = -tableData.dicTableAxisItem["Z"].JogSpeed;
                                _dicAxisMoveSta["Z"].Acc = tableData.dicTableAxisItem["Z"].Acc;
                                _dicAxisMoveSta["Z"].Dec = tableData.dicTableAxisItem["Z"].Dec;

                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {
                        if (bCW)
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["U"].JobMove(tableData.dicTableAxisItem["U"].Index,
                                                    tableData.dicTableAxisItem["U"].Acc,
                                                    tableData.dicTableAxisItem["U"].Dec,
                                                    tableData.dicTableAxisItem["U"].JogSpeed / tableData.dicTableAxisItem["U"].PulseToMM);
                                _dicAxisMoveSta["U"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["U"].dSpeed = tableData.dicTableAxisItem["U"].JogSpeed;
                                _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                                _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["U"].JobMove(tableData.dicTableAxisItem["U"].Index,
                                                   tableData.dicTableAxisItem["U"].Acc,
                                                   tableData.dicTableAxisItem["U"].Dec,
                                                   tableData.dicTableAxisItem["U"].JogSpeed / tableData.dicTableAxisItem["U"].PulseToMM);
                                _dicAxisMoveSta["U"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["U"].dSpeed = tableData.dicTableAxisItem["U"].JogSpeed;
                                _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                                _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                            }
                        }
                        else
                        {
                            if (bHighSpd)
                            {
                                _dicAxisAction["U"].JobMove(tableData.dicTableAxisItem["U"].Index,
                                                    tableData.dicTableAxisItem["U"].Acc,
                                                    tableData.dicTableAxisItem["U"].Dec,
                                                    -tableData.dicTableAxisItem["U"].JogSpeed / tableData.dicTableAxisItem["U"].PulseToMM);
                                _dicAxisMoveSta["U"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["U"].dSpeed = -tableData.dicTableAxisItem["U"].JogSpeed;
                                _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                                _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                            }
                            else
                            {
                                _dicAxisAction["U"].JobMove(tableData.dicTableAxisItem["U"].Index,
                                                   tableData.dicTableAxisItem["U"].Acc,
                                                   tableData.dicTableAxisItem["U"].Dec,
                                                   -tableData.dicTableAxisItem["U"].JogSpeed / tableData.dicTableAxisItem["U"].PulseToMM);
                                _dicAxisMoveSta["U"].moveMode = MoveMode.JOG;
                                _dicAxisMoveSta["U"].dSpeed = -tableData.dicTableAxisItem["U"].JogSpeed;
                                _dicAxisMoveSta["U"].Acc = tableData.dicTableAxisItem["U"].Acc;
                                _dicAxisMoveSta["U"].Dec = tableData.dicTableAxisItem["U"].Dec;
                            }
                        }

                    }
                    catch
                    {

                    }
                }
            }
        }
        public void JobMove(DefaultAxis Axis, double Acc, double Dec, double dSpeed)
        {
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {
                        _dicAxisAction["X"].JobMove(tableData.dicTableAxisItem["X"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
                        _dicAxisMoveSta["X"].moveMode = MoveMode.JOG;
                        _dicAxisMoveSta["X"].dSpeed = dSpeed;
                        _dicAxisMoveSta["X"].Acc = Acc;
                        _dicAxisMoveSta["X"].Dec = Dec;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {
                        _dicAxisAction["Y"].JobMove(tableData.dicTableAxisItem["Y"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["Y"].PulseToMM);
                        _dicAxisMoveSta["Y"].moveMode = MoveMode.JOG;
                        _dicAxisMoveSta["Y"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Y"].Acc = Acc;
                        _dicAxisMoveSta["Y"].Dec = Dec;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {
                        _dicAxisAction["Z"].JobMove(tableData.dicTableAxisItem["Z"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["Z"].PulseToMM);
                        _dicAxisMoveSta["Z"].moveMode = MoveMode.JOG;
                        _dicAxisMoveSta["Z"].dSpeed = dSpeed;
                        _dicAxisMoveSta["Z"].Acc = Acc;
                        _dicAxisMoveSta["Z"].Dec = Dec;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {
                        _dicAxisAction["U"].JobMove(tableData.dicTableAxisItem["U"].Index,
                                            Acc,
                                            Dec,
                                            dSpeed / tableData.dicTableAxisItem["U"].PulseToMM);
                        _dicAxisMoveSta["U"].moveMode = MoveMode.JOG;
                        _dicAxisMoveSta["U"].dSpeed = dSpeed;
                        _dicAxisMoveSta["U"].Acc = Acc;
                        _dicAxisMoveSta["U"].Dec = Dec;
                    }
                    catch
                    {

                    }
                }
            }
        }
        public void Stop(DefaultAxis Axis)
        {
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {
                        _dicAxisAction["X"].StopMove(tableData.dicTableAxisItem["X"].Index);
                        _dicAxisMoveSta["X"].moveMode = MoveMode.STOP;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {
                        _dicAxisAction["Y"].StopMove(tableData.dicTableAxisItem["Y"].Index);
                        _dicAxisMoveSta["Y"].moveMode = MoveMode.STOP;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {
                        _dicAxisAction["Z"].StopMove(tableData.dicTableAxisItem["Z"].Index);
                        _dicAxisMoveSta["Z"].moveMode = MoveMode.STOP;
                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {
                        _dicAxisAction["U"].StopMove(tableData.dicTableAxisItem["U"].Index);
                        _dicAxisMoveSta["U"].moveMode = MoveMode.STOP;
                    }
                    catch
                    {

                    }
                }
            }
        }

        #region Home
        public void StartLimit(DefaultAxis Axis)
        {
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return;
                _dicAxisHoming[strAxis] = true;
                short index = tableData.dicTableAxisItem[strAxis].Index;
                double dAcc = tableData.dicTableAxisItem[strAxis].Acc;
                double dDec = tableData.dicTableAxisItem[strAxis].Dec;
                double dSearchLimitSpeed = tableData.dicTableAxisItem[strAxis].SearchLimitSpeed / tableData.dicTableAxisItem[strAxis].PulseToMM;
                dSearchLimitSpeed = (tableData.dicTableAxisItem[strAxis].AxisHomeMode == 0) ? dSearchLimitSpeed * -1 : dSearchLimitSpeed;
                _dicAxisMoveSta[strAxis].moveMode = MoveMode.LIMIT;
                _dicAxisAction[strAxis].StartSearchLimit(index, dAcc, dDec, dSearchLimitSpeed);
                return;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool LimitDone(DefaultAxis Axis)
        {
            bool bDone = false;
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {

                    try
                    {
                        if (tableData.dicTableAxisItem["X"].AxisHomeMode == 0 || (int)tableData.dicTableAxisItem["X"].AxisHomeMode == 1)
                        {

                            bDone = _dicAxisAction["X"].FinishSearchLimit(tableData.dicTableAxisItem["X"].Index);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            _dicAxisMoveSta["X"].moveMode = MoveMode.STOP;
                        }
                    }
                    catch
                    {

                    }

                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {

                    try
                    {
                        if (tableData.dicTableAxisItem["Y"].AxisHomeMode == 0 || (int)tableData.dicTableAxisItem["Y"].AxisHomeMode == 1)
                        {
                            bDone = _dicAxisAction["Y"].FinishSearchLimit(tableData.dicTableAxisItem["Y"].Index);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            _dicAxisMoveSta["Y"].moveMode = MoveMode.STOP;
                        }
                    }
                    catch
                    {

                    }

                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {

                    try
                    {
                        if (tableData.dicTableAxisItem["Z"].AxisHomeMode == 0 || (int)tableData.dicTableAxisItem["Z"].AxisHomeMode == 1)
                        {
                            bDone = _dicAxisAction["Z"].FinishSearchLimit(tableData.dicTableAxisItem["Z"].Index);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            _dicAxisMoveSta["Z"].moveMode = MoveMode.STOP;
                        }
                    }
                    catch
                    {

                    }

                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {

                    try
                    {
                        if (tableData.dicTableAxisItem["U"].AxisHomeMode == 0 || (int)tableData.dicTableAxisItem["U"].AxisHomeMode == 1)
                        {
                            bDone = _dicAxisAction["U"].FinishSearchLimit(tableData.dicTableAxisItem["U"].Index);
                        }
                        else
                        {
                            bDone = true;
                        }
                        if (bDone)
                        {
                            _dicAxisMoveSta["U"].moveMode = MoveMode.STOP;
                        }
                    }
                    catch
                    {

                    }

                }
            }
            return bDone;
        }
        public bool Home(DefaultAxis Axis)
        {
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return false;
                if (_dicAxisHoming[strAxis])
                    return false;
                System.Threading.Thread thread = new System.Threading.Thread(ThreadHomeFunction);
                thread.IsBackground = true;
                thread.Start((short)Axis);
                _dicAxisHoming[strAxis] = true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        public void StartHome(DefaultAxis Axis)
        {
            string strAxis = Axis.ToString();
            try
            {
                short index = tableData.dicTableAxisItem[strAxis].Index;
                double dAcc = tableData.dicTableAxisItem["X"].Acc;
                double dDec = tableData.dicTableAxisItem["X"].Dec;
                double dHomeSpeed = tableData.dicTableAxisItem["X"].SearchHomeSpeed / tableData.dicTableAxisItem["X"].PulseToMM;
                dHomeSpeed = (tableData.dicTableAxisItem["X"].AxisHomeMode == 0 || (int)tableData.dicTableAxisItem["X"].AxisHomeMode == 2) ? dHomeSpeed : dHomeSpeed * -1;

                _dicAxisHoming[strAxis] = true;
                _dicAxisAction[strAxis].StartSearchHome((short)Axis, dAcc, dDec, dHomeSpeed);
                _dicAxisMoveSta[strAxis].moveMode = MoveMode.HOME;
            }
            catch (Exception)
            {
            }
            return;
        }
        public bool HomeDone(DefaultAxis Axis)
        {
            bool bDone = false;
            string strAxis = Axis.ToString();
            try
            {
                if (!_dicAxisReady[strAxis])
                    return false;
                bDone = _dicAxisAction[strAxis].FinishSearchHome(tableData.dicTableAxisItem[strAxis].Index);
                if (bDone)
                {
                    _dicAxisHoming[strAxis] = false;
                    _dicAxisMoveSta[strAxis].moveMode = MoveMode.STOP;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return bDone;
        }
        void ThreadHomeFunction(object objAxis)
        {
            try
            {
                short sAxis = (short)objAxis;
                HiPerfTimer hTimer = new HiPerfTimer();
                DefaultAxis axis = (DefaultAxis)Enum.ToObject(typeof(DefaultAxis), sAxis);
                StartHome(axis);
                hTimer.Start();
                while (true)
                {
                    if (hTimer.TimeUp(30))
                        break;
                    if (HomeDone(axis))
                        break;
                    System.Threading.Thread.Sleep(50);
                }
                System.Threading.Thread.Sleep(200);
                SetPosZero(axis);
                _dicAxisHoming[axis.ToString()] = false;
            }
            catch (Exception)
            {
            }
        }
        #endregion


        
        public void GetTableStatus(ref double PosX, ref bool bAlarmX, ref bool bCWLimitX, ref bool bOrgX, ref bool bCCWLimitX, ref bool bMovingX,
            ref double PosY, ref bool bAlarmY, ref bool bCWLimitY, ref bool bOrgY, ref bool bCCWLimitY, ref bool bMovingY,
            ref double PosZ, ref bool bAlarmZ, ref bool bCWLimitZ, ref bool bOrgZ, ref bool bCCWLimitZ, ref bool bMovingZ,
            ref double PosU, ref bool bAlarmU, ref bool bCWLimitU, ref bool bOrgU, ref bool bCCWLimitU, ref bool bMovingU)
        {
            PosX = 0.0;
            bAlarmX = false;
            bCWLimitX = false;
            bOrgX = false;
            bCCWLimitX = false;
            bMovingX = false;

            _dicAxisMoveSta["Y"].dPos = 0.0;
            bAlarmY = false;
            bCWLimitY = false;
            bOrgY = false;
            bCCWLimitY = false;
            bMovingY = false;

            _dicAxisMoveSta["Z"].dPos = 0.0;
            bAlarmZ = false;
            bCWLimitZ = false;
            bOrgZ = false;
            bCCWLimitZ = false;
            bMovingZ = false;

            _dicAxisMoveSta["U"].dPos = 0.0;
            bAlarmU = false;
            bCWLimitU = false;
            bOrgU = false;
            bCCWLimitU = false;
            bMovingU = false;
            if (_dicAxisReady["X"])
            {

                try
                {
                    PosX = _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                    bAlarmX = _dicAxisAction["X"].GetAlarm(tableData.dicTableAxisItem["X"].Index);
                    bCWLimitX = _dicAxisAction["X"].GetLimtCW(tableData.dicTableAxisItem["X"].Index);
                    bOrgX = _dicAxisAction["X"].GetHome(tableData.dicTableAxisItem["X"].Index);
                    bCCWLimitX = _dicAxisAction["X"].GetLimtCCW(tableData.dicTableAxisItem["X"].Index);
                    bMovingX = _dicAxisAction["X"].IsMoving(tableData.dicTableAxisItem["X"].Index);
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Y"])
            {

                try
                {
                    _dicAxisMoveSta["Y"].dPos = _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM; ;
                    bAlarmY = _dicAxisAction["Y"].GetAlarm(tableData.dicTableAxisItem["Y"].Index);
                    bCWLimitY = _dicAxisAction["Y"].GetLimtCW(tableData.dicTableAxisItem["Y"].Index);
                    bOrgY = _dicAxisAction["Y"].GetHome(tableData.dicTableAxisItem["Y"].Index);
                    bCCWLimitY = _dicAxisAction["Y"].GetLimtCCW(tableData.dicTableAxisItem["Y"].Index);
                    bMovingY = _dicAxisAction["Y"].IsMoving(tableData.dicTableAxisItem["Y"].Index);
                }
                catch
                {

                }
            }
            if (_dicAxisReady["Z"])
            {

                try
                {
                    _dicAxisMoveSta["Z"].dPos = _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Z"].Index) * tableData.dicTableAxisItem["Z"].PulseToMM; ;
                    bAlarmZ = _dicAxisAction["Z"].GetAlarm(tableData.dicTableAxisItem["Z"].Index);
                    bCWLimitZ = _dicAxisAction["Z"].GetLimtCW(tableData.dicTableAxisItem["Z"].Index);
                    bOrgZ = _dicAxisAction["Z"].GetHome(tableData.dicTableAxisItem["Z"].Index);
                    bCCWLimitZ = _dicAxisAction["Z"].GetLimtCCW(tableData.dicTableAxisItem["Z"].Index);
                    bMovingZ = _dicAxisAction["Z"].IsMoving(tableData.dicTableAxisItem["Z"].Index);
                }
                catch
                {

                }
            }
            if (_dicAxisReady["U"])
            {

                try
                {
                    _dicAxisMoveSta["U"].dPos = _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM; ;
                    bAlarmU = _dicAxisAction["U"].GetAlarm(tableData.dicTableAxisItem["U"].Index);
                    bCWLimitU = _dicAxisAction["U"].GetLimtCW(tableData.dicTableAxisItem["U"].Index);
                    bOrgU = _dicAxisAction["U"].GetHome(tableData.dicTableAxisItem["U"].Index);
                    bCCWLimitU = _dicAxisAction["U"].GetLimtCCW(tableData.dicTableAxisItem["U"].Index);
                    bMovingU = _dicAxisAction["U"].IsMoving(tableData.dicTableAxisItem["U"].Index);
                }
                catch
                {

                }
            }
        }
        
        void SetPosZero(DefaultAxis Axis)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            if (Axis == DefaultAxis.X)
            {
                if (_dicAxisReady["X"])
                {
                    try
                    {

                        _dicAxisAction["X"].SetAxisPos(tableData.dicTableAxisItem["X"].Index, 0.0);

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Y)
            {
                if (_dicAxisReady["Y"])
                {
                    try
                    {

                        _dicAxisAction["Y"].SetAxisPos(tableData.dicTableAxisItem["Y"].Index, 0.0);

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.Z)
            {
                if (_dicAxisReady["Z"])
                {
                    try
                    {

                        _dicAxisAction["Z"].SetAxisPos(tableData.dicTableAxisItem["Z"].Index, 0.0);

                    }
                    catch
                    {

                    }
                }
            }
            if (Axis == DefaultAxis.U)
            {
                if (_dicAxisReady["U"])
                {
                    try
                    {

                        _dicAxisAction["U"].SetAxisPos(tableData.dicTableAxisItem["U"].Index, 0.0);

                    }
                    catch
                    {

                    }
                }
            }
        }
        public void SuspendMove()
        {

            if (_dicAxisReady["X"])
            {
                try
                {
                    _dicAxisAction["X"].StopMove(tableData.dicTableAxisItem["X"].Index);
                }
                catch
                {

                }
            }

            if (_dicAxisReady["Y"])
            {
                try
                {
                    _dicAxisAction["Y"].StopMove(tableData.dicTableAxisItem["Y"].Index);

                }
                catch
                {

                }
            }

            if (_dicAxisReady["Z"])
            {
                try
                {
                    _dicAxisAction["Z"].StopMove(tableData.dicTableAxisItem["Z"].Index);
                }
                catch
                {

                }
            }


            if (_dicAxisReady["U"])
            {
                try
                {
                    _dicAxisAction["U"].StopMove(tableData.dicTableAxisItem["U"].Index);
                }
                catch
                {

                }
            }

        }
        public void ResumeMove()
        {

            if (_dicAxisReady["X"])
            {
                try
                {
                    if (_dicAxisMoveSta["X"].moveMode == MoveMode.ABS)
                    {
                        AbsMove(DefaultAxis.X, _dicAxisMoveSta["X"].dPos);
                    }
                    if (_dicAxisMoveSta["X"].moveMode == MoveMode.REL)
                    {
                        AbsMove(DefaultAxis.X, _dicAxisMoveSta["X"].dPos);
                    }
                    if (_dicAxisMoveSta["X"].moveMode == MoveMode.JOG)
                    {
                        if (_dicAxisMoveSta["X"].dSpeed > 0)
                        {
                            JobMove(DefaultAxis.X, true, false);
                        }
                        else
                        {
                            JobMove(DefaultAxis.X, false, false);
                        }
                    }
                    if (_dicAxisMoveSta["X"].moveMode == MoveMode.LIMIT)
                    {
                        StartLimit(DefaultAxis.X);
                    }
                    if (_dicAxisMoveSta["X"].moveMode == MoveMode.HOME)
                    {
                        StartHome(DefaultAxis.X);
                    }
                }
                catch
                {

                }
            }


            if (_dicAxisReady["Y"])
            {
                try
                {
                    if (_dicAxisMoveSta["Y"].moveMode == MoveMode.ABS)
                    {
                        AbsMove(DefaultAxis.Y, _dicAxisMoveSta["Y"].dPos);
                    }
                    if (_dicAxisMoveSta["Y"].moveMode == MoveMode.REL)
                    {
                        AbsMove(DefaultAxis.Y, _dicAxisMoveSta["Y"].dPos);
                    }
                    if (_dicAxisMoveSta["Y"].moveMode == MoveMode.JOG)
                    {
                        if (_dicAxisMoveSta["Y"].dSpeed > 0)
                        {
                            JobMove(DefaultAxis.Y, true, false);
                        }
                        else
                        {
                            JobMove(DefaultAxis.Y, false, false);
                        }
                    }
                    if (_dicAxisMoveSta["Y"].moveMode == MoveMode.LIMIT)
                    {
                        StartLimit(DefaultAxis.Y);
                    }
                    if (_dicAxisMoveSta["Y"].moveMode == MoveMode.HOME)
                    {
                        StartHome(DefaultAxis.Y);
                    }
                }
                catch
                {

                }
            }


            if (_dicAxisReady["Z"])
            {
                try
                {
                    if (_dicAxisMoveSta["Z"].moveMode == MoveMode.ABS)
                    {
                        AbsMove(DefaultAxis.Z, _dicAxisMoveSta["Z"].dPos);
                    }
                    if (_dicAxisMoveSta["Z"].moveMode == MoveMode.REL)
                    {
                        AbsMove(DefaultAxis.Z, _dicAxisMoveSta["Z"].dPos);
                    }
                    if (_dicAxisMoveSta["Z"].moveMode == MoveMode.JOG)
                    {
                        if (_dicAxisMoveSta["Z"].dSpeed > 0)
                        {
                            JobMove(DefaultAxis.Z, true, false);
                        }
                        else
                        {
                            JobMove(DefaultAxis.Z, false, false);
                        }
                    }
                    if (_dicAxisMoveSta["Z"].moveMode == MoveMode.LIMIT)
                    {
                        StartLimit(DefaultAxis.Z);
                    }
                    if (_dicAxisMoveSta["Z"].moveMode == MoveMode.HOME)
                    {
                        StartHome(DefaultAxis.Z);
                    }
                }
                catch
                {

                }
            }


            if (_dicAxisReady["U"])
            {
                try
                {
                    if (_dicAxisMoveSta["U"].moveMode == MoveMode.ABS)
                    {
                        AbsMove(DefaultAxis.U, _dicAxisMoveSta["U"].dPos);
                    }
                    if (_dicAxisMoveSta["U"].moveMode == MoveMode.REL)
                    {
                        AbsMove(DefaultAxis.U, _dicAxisMoveSta["U"].dPos);
                    }
                    if (_dicAxisMoveSta["U"].moveMode == MoveMode.JOG)
                    {
                        if (_dicAxisMoveSta["U"].dSpeed > 0)
                        {
                            JobMove(DefaultAxis.U, true, false);
                        }
                        else
                        {
                            JobMove(DefaultAxis.U, false, false);
                        }
                    }
                    if (_dicAxisMoveSta["U"].moveMode == MoveMode.LIMIT)
                    {
                        StartLimit(DefaultAxis.U);
                    }
                    if (_dicAxisMoveSta["U"].moveMode == MoveMode.HOME)
                    {
                        StartHome(DefaultAxis.U);
                    }
                }
                catch
                {

                }
            }

        }
        public double CurrentX
        {
            get
            {
                if (_dicAxisReady["X"])
                {

                    try
                    {
                        return _dicAxisAction["X"].GetCurrentPos(tableData.dicTableAxisItem["X"].Index) * tableData.dicTableAxisItem["X"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentY
        {
            get
            {
                if (_dicAxisReady["Y"])
                {

                    try
                    {
                        return _dicAxisAction["Y"].GetCurrentPos(tableData.dicTableAxisItem["Y"].Index) * tableData.dicTableAxisItem["Y"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentZ
        {
            get
            {
                if (_dicAxisReady["Z"])
                {

                    try
                    {
                        return _dicAxisAction["Z"].GetCurrentPos(tableData.dicTableAxisItem["Z"].Index) * tableData.dicTableAxisItem["Z"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentU
        {
            get
            {
                if (_dicAxisReady["U"])
                {

                    try
                    {
                        return _dicAxisAction["U"].GetCurrentPos(tableData.dicTableAxisItem["U"].Index) * tableData.dicTableAxisItem["U"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentA
        {
            get
            {
                if (_dicAxisReady["A"])
                {

                    try
                    {
                        return _dicAxisAction["A"].GetCurrentPos(tableData.dicTableAxisItem["A"].Index) * tableData.dicTableAxisItem["A"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentB
        {
            get
            {
                if (_dicAxisReady["B"])
                {

                    try
                    {
                        return _dicAxisAction["B"].GetCurrentPos(tableData.dicTableAxisItem["B"].Index) * tableData.dicTableAxisItem["B"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentC
        {
            get
            {
                if (_dicAxisReady["C"])
                {

                    try
                    {
                        return _dicAxisAction["C"].GetCurrentPos(tableData.dicTableAxisItem["C"].Index) * tableData.dicTableAxisItem["C"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }
        public double CurrentD
        {
            get
            {
                if (_dicAxisReady["D"])
                {

                    try
                    {
                        return _dicAxisAction["D"].GetCurrentPos(tableData.dicTableAxisItem["D"].Index) * tableData.dicTableAxisItem["D"].PulseToMM;
                    }
                    catch
                    {
                        return 0.0;
                    }
                }
                else
                {
                    return 0.0;
                }
            }
        }

        public bool LineXYZMove(double Acc, double Dec, double dSpeed, double posX, double posY, double posZ)
        {
            _dicAxisAction["X"].LineXYZMove((short)tableData.dicTableAxisItem["X"].CorNo, tableData.dicTableAxisItem["X"].Index, tableData.dicTableAxisItem["Y"].Index, tableData.dicTableAxisItem["Z"].Index, Acc, Dec, dSpeed / tableData.dicTableAxisItem["X"].PulseToMM, posX / tableData.dicTableAxisItem["X"].PulseToMM, posY / tableData.dicTableAxisItem["Y"].PulseToMM, posZ / tableData.dicTableAxisItem["Z"].PulseToMM);
            return true;
        }
        public bool ArcXYMove(double Acc, double Dec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            _dicAxisAction["X"].ArcXYMove((short)tableData.dicTableAxisItem["X"].CorNo, tableData.dicTableAxisItem["X"].Index, tableData.dicTableAxisItem["Y"].Index, tableData.dicTableAxisItem["Z"].Index, Acc, Dec, dSpeed / tableData.dicTableAxisItem["X"].PulseToMM, posX / tableData.dicTableAxisItem["X"].PulseToMM, posY / tableData.dicTableAxisItem["Y"].PulseToMM, dR / tableData.dicTableAxisItem["Z"].PulseToMM, iCCW);
            return true;
        }
        public bool ArcXYCZMove(double Acc, double Dec, double dSpeed, double dEndSpeed, double posX, double posY, double dposZ, double dxCenter, double dyCenter, short iCCW)
        {
            _dicAxisAction["X"].ArcXYCZMove((short)tableData.dicTableAxisItem["X"].CorNo, tableData.dicTableAxisItem["X"].Index,
                tableData.dicTableAxisItem["Y"].Index, tableData.dicTableAxisItem["Z"].Index, posX / tableData.dicTableAxisItem["X"].PulseToMM,
                posY / tableData.dicTableAxisItem["Y"].PulseToMM, dposZ / tableData.dicTableAxisItem["Z"].PulseToMM, dxCenter / tableData.dicTableAxisItem["X"].PulseToMM, dyCenter / tableData.dicTableAxisItem["Y"].PulseToMM, iCCW, dSpeed / tableData.dicTableAxisItem["X"].PulseToMM, Acc, 0);
            return true;
        }
        public void BuildCor()
        {
            _dicAxisAction["X"].BuildCor((short)tableData.dicTableAxisItem["X"].CorNo, tableData.dicTableAxisItem["X"].Index, tableData.dicTableAxisItem["Y"].Index, tableData.dicTableAxisItem["Z"].Index);


        }
        public void InsertLine(double PosX, double PosY, double PosZ, double dSpeed, double Acc, double dEndSpeed)
        {
            _dicAxisAction["X"].InsertLine((short)tableData.dicTableAxisItem["X"].CorNo, PosX / tableData.dicTableAxisItem["X"].PulseToMM, _dicAxisMoveSta["Y"].dPos / tableData.dicTableAxisItem["Y"].PulseToMM, _dicAxisMoveSta["Z"].dPos / tableData.dicTableAxisItem["Z"].PulseToMM, dSpeed / tableData.dicTableAxisItem["X"].PulseToMM, Acc, dEndSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
        }
        public void InsertArc(double PosX, double PosY, double dR, double dSpeed, short iCCW, double Acc, double dEndSpeed)
        {
            _dicAxisAction["X"].InsertArc((short)tableData.dicTableAxisItem["X"].CorNo,
                PosX / tableData.dicTableAxisItem["X"].PulseToMM,
                _dicAxisMoveSta["Y"].dPos / tableData.dicTableAxisItem["Y"].PulseToMM,
                dR / tableData.dicTableAxisItem["Z"].PulseToMM,
                dSpeed / tableData.dicTableAxisItem["X"].PulseToMM,
                iCCW,
                Acc,
                dEndSpeed / tableData.dicTableAxisItem["X"].PulseToMM);
        }
        public void StartCure()
        {
            _dicAxisAction["X"].StartCure((short)tableData.dicTableAxisItem["X"].CorNo);

        }
        public bool CureMoveDone(out int iStep)
        {
            return _dicAxisAction["X"].CureMoveDone((short)tableData.dicTableAxisItem["X"].CorNo, out iStep);
            //return true;
        }
    }
}
