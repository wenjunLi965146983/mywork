using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Table;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.LeadShine
{
    public class LeadShineMotionCard : HardwareBase, IMotionAction, IInputAction, IOutputAction
    {
        static int iCardTotal;
        //IO status
        private bool[] _bArrayInputSta;
        private bool[] _bArrayOutputSta;

        //Pos
        private double[] _dArrayCurrPos;
        private double[] _dArrayTargetPos;
        private double[] _dCurrVel;

        //Home status
        private bool[] _bArrayHomeSta;

        //Axis alarm
        private bool[] _bArrayAlarmSta;

        //Limit status
        private bool[] _bArrayCWLLimitSta;
        private bool[] _bArrayCCWLLimitSta;

        //Servo status
        private bool[] _bArrayServoSta;

        //Axis is moving
        private bool[] _bArrayIsMoving;

        public ushort iCardIndex;
        public LeadShineMotionCard()
        {
            _bArrayInputSta = new bool[64];
            _bArrayOutputSta = new bool[64];

            _dArrayCurrPos = new double[8];
            _dArrayTargetPos = new double[8];
            _dCurrVel = new double[8];

            _bArrayHomeSta = new bool[8];
            _bArrayAlarmSta = new bool[8];

            _bArrayCWLLimitSta = new bool[8];
            _bArrayCCWLLimitSta = new bool[8];

            _bArrayServoSta = new bool[8];
            _bArrayIsMoving = new bool[8];

            iCardIndex = 0;
        }
        public double GetCurrentPos(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _dArrayCurrPos[sAxis];
            }
            return 0.0;
        }

        public bool GetHome(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayHomeSta[sAxis];
            }
            return false;
        }

        public bool GetAlarm(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayAlarmSta[sAxis];
            }
            return true;
        }

        public bool GetLimtCW(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayCWLLimitSta[sAxis];
            }
            return false;
        }

        public bool GetLimtCCW(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayCCWLLimitSta[sAxis];
            }
            return false;
        }

        public bool GetEstop(short sAxis)
        {
            return false;
        }

        public bool GetServoOn(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayServoSta[sAxis];
            }
            return false;
        }

        public bool IsMoving(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                return _bArrayIsMoving[sAxis];
            }
            return false;
        }

        public bool IsMoveDone(short sAxis)
        {
            bool bResult;
            if (_bArrayIsMoving[sAxis])
            {
                bResult = false;
            }
            else
            {
                lock (lockObj)
                {
                    _dArrayTargetPos[sAxis] = LTDMC.dmc_get_target_position(iCardIndex, (ushort)sAxis);
                }
                if (_dArrayCurrPos[sAxis] == _dArrayTargetPos[sAxis] && (!_bArrayAlarmSta[sAxis]) && (!_bArrayIsMoving[sAxis]))
                {
                    bResult = true;
                }
                else
                {
                    bResult = false;
                }
            }
            return bResult;
        }

        public bool SetVel(short sAxis, double dVel)
        {
            lock (lockObj)
            {
                if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
                {
                    LTDMC.dmc_change_speed(iCardIndex, (ushort)sAxis, dVel, 0.0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public double GetVel(short sAxis)
        {

            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    return LTDMC.dmc_read_current_speed(iCardIndex, (ushort)sAxis);
                }
            }
            else
            {
                return 0.0;
            }

        }
        public bool JobMove(short sAxis, double dAcc, double dDec, double dVel)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    ushort iDir = 0;

                    LTDMC.dmc_set_profile(iCardIndex, (ushort)sAxis, 0.0, Math.Abs(dVel), dAcc, dDec, 0);
                    if (dVel < 0)
                    {
                        iDir = 0;
                    }
                    else
                    {
                        iDir = 1;
                    }
                    LTDMC.dmc_vmove(iCardIndex, (ushort)sAxis, iDir);

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool StopMove(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_stop(iCardIndex, (ushort)sAxis, 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool HandleErrorMessage(short errorMessage)
        {
            return true;
        }
        public bool StartSearchLimit(short sAxis, double dAcc, double dDec, double dCatchSpeed)
        {
            lock (lockObj)
            {
                ushort iDir = 0;
                double dVel = Math.Abs(dCatchSpeed);
                LTDMC.dmc_set_profile(iCardIndex, (ushort)sAxis, 0.0, dVel, dAcc, dDec, 0);
                if (dCatchSpeed < 0)
                {
                    iDir = 0;
                }
                else
                {
                    iDir = 1;
                }
                LTDMC.dmc_vmove(iCardIndex, (ushort)sAxis, iDir);
            }
            return true;
        }
        public bool FinishSearchLimit(short sAxis)
        {
            return _bArrayCCWLLimitSta[sAxis] || _bArrayCWLLimitSta[sAxis];
        }
        public bool StartSearchHome(short sAxis, double dAcc, double dDec, double dHomeSpd)
        {
            lock (lockObj)
            {
                ushort iDir = 0;
                double dVel = Math.Abs(dHomeSpd);
                LTDMC.dmc_set_profile(iCardIndex, (ushort)sAxis, 0.0, dVel, dAcc, dDec, 0);
                if (dHomeSpd < 0)
                {
                    iDir = 0;
                }
                else
                {
                    iDir = 1;
                }
                LTDMC.dmc_set_homemode(iCardIndex, (ushort)sAxis, iDir, dVel, 2, 0);
                LTDMC.dmc_home_move(iCardIndex, (ushort)sAxis);
            }
            return true;
        }
        public bool FinishSearchHome(short sAxis)
        {
            return !_bArrayIsMoving[sAxis] && _bArrayHomeSta[sAxis];
        }
        public bool SetAxisPos(short sAxis, double dPos)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_position(iCardIndex, (ushort)sAxis, (int)dPos);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ZeroAxisPos(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_position(iCardIndex, (ushort)sAxis, 0);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ServoOn(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_write_sevon_pin(iCardIndex, (ushort)sAxis, 0);
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        public bool ServoOff(short sAxis)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_write_sevon_pin(iCardIndex, (ushort)sAxis, 1);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool SetLimtOn(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(iCardIndex, (ushort)sAxis, 1, 0, 0);
                return true;
            }
        }
        public bool SetLimtOff(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(iCardIndex, (ushort)sAxis, 1, 1, 0);
                return true;
            }
        }
        public bool SetLimtDisable(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_el_mode(iCardIndex, (ushort)sAxis, 0, 1, 0);
                return true;
            }
        }
        public bool SetHomeOn(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_home_pin_logic(iCardIndex, (ushort)sAxis, 0, 0);
                return true;
            }
        }
        public bool SetHomeOff(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_home_pin_logic(iCardIndex, (ushort)sAxis, 1, 0);
                return true;
            }
        }
        public bool SetNearHomeOn(short sAxis)
        {
            return true;
        }
        public bool SetNearHomeOff(short sAxis)
        {
            return true;
        }
        public bool AbsPosMove(short sAxis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_profile(iCardIndex, (ushort)sAxis, 0.0, dSpeed, dAcc, dDec, 0);
                    LTDMC.dmc_pmove(iCardIndex, (ushort)sAxis, (int)pos, 1);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ReferPosMove(short sAxis, double dAcc, double dDec, double dSpeed, double pos)
        {
            if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
            {
                lock (lockObj)
                {
                    LTDMC.dmc_set_profile(iCardIndex, (ushort)sAxis, 0.0, dSpeed, dAcc, dDec, 0);
                    LTDMC.dmc_pmove(iCardIndex, (ushort)sAxis, (int)pos, 0);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetInputBit(int iBit)
        {
            if (iBit < 128 && iBit > -1)
            {
                return _bArrayInputSta[iBit];
            }
            else
            {
                return false;
            }
        }
        public bool SetOutBit(int iBit, bool bOn)
        {
            if (iBit < 128 && iBit > -1)
            {
                lock (lockObj)
                {
                    ushort uValue = bOn ? (ushort)0 : (ushort)1;
                    short sValue = LTDMC.dmc_write_outbit(iCardIndex, (ushort)iBit, uValue);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool GetOutBit(int iBit)
        {
            if (iBit < 128 && iBit > -1)
            {
                return _bArrayOutputSta[iBit];
            }
            else
            {
                return false;
            }

        }
        override public bool Init(HardwareData hardwareData)
        {
            LeadShineMotionCardData tempInfo = (LeadShineMotionCardData)hardwareData;
            if (iCardTotal > 0)
            {
                if (iCardTotal >= tempInfo.Index)
                {
                    bInitOk = true;
                }
                else
                {
                    bInitOk = false;
                }
            }
            else
            {
                iCardTotal = LTDMC.dmc_board_init();
                if (iCardTotal <= 0)//控制卡初始化
                {
                    bInitOk = false;
                    return false;
                }
                if (iCardTotal >= tempInfo.Index)
                {
                    bInitOk = true;
                }
                else
                {
                    bInitOk = false;
                }
            }
            iCardIndex = (ushort)tempInfo.Index;
            System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
            threadScan.IsBackground = true;
            threadScan.Start();
            return true;
        }
        private void ScanThreadFunction()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            while (!MainModule.formMain.bExit)
            {
                System.Threading.Thread.Sleep(10);
                GetAllMotionStatus();
                GetAllIOStatus();
            }
        }
        public void GetAllMotionStatus()
        {
            lock (lockObj)
            {
                for (ushort iAxis = 0; iAxis < 8; iAxis++)
                {
                    uint uiCurrent = 0;
                    try
                    {
                        _dArrayTargetPos[iAxis] = LTDMC.dmc_get_target_position(iCardIndex, iAxis);
                        _dArrayCurrPos[iAxis] = LTDMC.dmc_get_position(iCardIndex, iAxis);

                        if (LTDMC.dmc_check_done(iCardIndex, iAxis) == 1)
                        {
                            _bArrayIsMoving[iAxis] = false;
                        }
                        else
                        {
                            _bArrayIsMoving[iAxis] = true;
                        }
                        uiCurrent = LTDMC.dmc_axis_io_status(iCardIndex, iAxis);
                    }
                    catch
                    {

                    }

                    if ((uiCurrent & 0x1) == 0)
                    {
                        _bArrayAlarmSta[iAxis] = false;
                    }
                    else
                    {
                        _bArrayAlarmSta[iAxis] = true;
                    }
                    if ((uiCurrent & 0x2) == 0)
                    {
                        _bArrayCWLLimitSta[iAxis] = false;
                    }
                    else
                    {
                        _bArrayCWLLimitSta[iAxis] = true;
                    }
                    if ((uiCurrent & 0x4) == 0)
                    {
                        _bArrayCCWLLimitSta[iAxis] = false;
                    }
                    else
                    {
                        _bArrayCCWLLimitSta[iAxis] = true;
                    }
                    if ((uiCurrent & 0x10) == 0)
                    {
                        _bArrayHomeSta[iAxis] = false;
                    }
                    else
                    {
                        _bArrayHomeSta[iAxis] = true;
                    }
                }
            }
        }
        public void GetAllIOStatus()
        {
            lock (lockObj)
            {
                uint uiInput = 0;
                uint uiOutput = 0;
                try
                {
                    uiInput = LTDMC.dmc_read_inport(iCardIndex, 0);
                    uiOutput = LTDMC.dmc_read_outport(iCardIndex, 0);
                }
                catch
                {

                }
                _bArrayInputSta[0] = ((uiInput & 0x1) == 0) ? true : false;
                _bArrayInputSta[1] = ((uiInput & 0x2) == 0) ? true : false;
                _bArrayInputSta[2] = ((uiInput & 0x4) == 0) ? true : false;
                _bArrayInputSta[3] = ((uiInput & 0x8) == 0) ? true : false;
                _bArrayInputSta[4] = ((uiInput & 0x10) == 0) ? true : false;
                _bArrayInputSta[5] = ((uiInput & 0x20) == 0) ? true : false;
                _bArrayInputSta[6] = ((uiInput & 0x40) == 0) ? true : false;
                _bArrayInputSta[7] = ((uiInput & 0x80) == 0) ? true : false;
                _bArrayInputSta[8] = ((uiInput & 0x100) == 0) ? true : false;
                _bArrayInputSta[9] = ((uiInput & 0x200) == 0) ? true : false;
                _bArrayInputSta[10] = ((uiInput & 0x400) == 0) ? true : false;
                _bArrayInputSta[11] = ((uiInput & 0x800) == 0) ? true : false;
                _bArrayInputSta[12] = ((uiInput & 0x1000) == 0) ? true : false;
                _bArrayInputSta[13] = ((uiInput & 0x2000) == 0) ? true : false;
                _bArrayInputSta[14] = ((uiInput & 0x4000) == 0) ? true : false;
                _bArrayInputSta[15] = ((uiInput & 0x8000) == 0) ? true : false;

                _bArrayOutputSta[0] = ((uiOutput & 0x1) == 0) ? true : false;
                _bArrayOutputSta[1] = ((uiOutput & 0x2) == 0) ? true : false;
                _bArrayOutputSta[2] = ((uiOutput & 0x4) == 0) ? true : false;
                _bArrayOutputSta[3] = ((uiOutput & 0x8) == 0) ? true : false;
                _bArrayOutputSta[4] = ((uiOutput & 0x10) == 0) ? true : false;
                _bArrayOutputSta[5] = ((uiOutput & 0x20) == 0) ? true : false;
                _bArrayOutputSta[6] = ((uiOutput & 0x40) == 0) ? true : false;
                _bArrayOutputSta[7] = ((uiOutput & 0x80) == 0) ? true : false;
                _bArrayOutputSta[8] = ((uiOutput & 0x100) == 0) ? true : false;
                _bArrayOutputSta[9] = ((uiOutput & 0x200) == 0) ? true : false;
                _bArrayOutputSta[10] = ((uiOutput & 0x400) == 0) ? true : false;
                _bArrayOutputSta[11] = ((uiOutput & 0x800) == 0) ? true : false;
                _bArrayOutputSta[12] = ((uiOutput & 0x1000) == 0) ? true : false;
                _bArrayOutputSta[13] = ((uiOutput & 0x2000) == 0) ? true : false;
                _bArrayOutputSta[14] = ((uiOutput & 0x4000) == 0) ? true : false;
                _bArrayOutputSta[15] = ((uiOutput & 0x8000) == 0) ? true : false;

            }
        }
        public bool SetAlarmOn(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_alm_mode(iCardIndex, (ushort)sAxis, 1, (ushort)0, (ushort)0);
            }
            return true;
        }
        public bool SetAlarmOff(short sAxis)
        {
            lock (lockObj)
            {
                LTDMC.dmc_set_alm_mode(iCardIndex, (ushort)sAxis, 1, (ushort)1, (ushort)0);
            }
            return true;
        }
        public bool SetPulseMode(short sAxis, PulseMode plm)
        {
            lock (lockObj)
            {
                if (plm == PulseMode.PLDI)
                {
                    LTDMC.dmc_set_pulse_outmode(iCardIndex, (ushort)sAxis, 0);
                }
                else
                {
                    LTDMC.dmc_set_pulse_outmode(iCardIndex, (ushort)sAxis, 4);
                }
            }
            return true;
        }
        override public bool Close()
        {
            //if (bBoardClose == false)
            //{
            //    LTDMC.dmc_board_close();
            //    bBoardClose = true;
            //}
            StopMove(0);
            StopMove(1);
            StopMove(2);
            StopMove(3);
            StopMove(4);
            StopMove(5);
            StopMove(6);
            StopMove(7);
            return true;
        }
        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            return true;
        }
        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            BuildCor(num, AxisX, AxisY, AxisZ);
            InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
            StartCure(num);
            return true;
        }
        public void BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {


        }
        public void InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {


        }
        public void InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {


        }
        public void StartCure(short num)
        {

        }
        public bool CureMoveDone(short num, out int iStep)
        {
            iStep = 0;
            return true;
        }
        public bool ArcXYCZMove(short num, short AxisX, short AxisY, short AxisZ, double posX, double posY, double posZ, double xCenter, double yCenter, short iCCW, double dSpeed, double dAcc, double velEnd, short fifo = 0)
        {
            return false;
        }

        //不用
        public bool RobotAbsPosMove(ushort index, double dPosX, double dPosY, double dPosZ, double dPosU, double dVel, bool bCheckPoint, ref int iErr)
        {
            return false;
        }
    }
}
