using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Table;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.LeadShine
{
    public class LeadShineMotionCardDmc1000 : HardwareBase, IMotionAction, IInputAction, IOutputAction
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
        public LeadShineMotionCardDmc1000()
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
            lock (lockObj)
            {
                return Dmc1000.d1000_check_done(sAxis) == 0 ? false : true;
            }
        }

        public bool  SetCommandPos(short sAxis,double dPos)
        {
            lock (lockObj)
            {
                Dmc1000.d1000_set_command_pos(sAxis,dPos);
                return true;
            }
        }

        public bool SetVel(short sAxis, double dVel)
        {
            lock (lockObj)
            {
                if (Enum.IsDefined(typeof(DefaultAxis), sAxis))
                {
                    Dmc1000.d1000_change_speed((ushort)sAxis, (int)dVel);
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
                    return Dmc1000.d1000_get_speed((int)sAxis);
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
                    Dmc1000.d1000_start_tv_move((int)sAxis,0,(int)dVel,0);
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
                    //减速停止
                    Dmc1000.d1000_decel_stop((ushort)sAxis);
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
            return false;
        }
        public bool FinishSearchLimit(short sAxis)
        {
            return _bArrayCCWLLimitSta[sAxis] || _bArrayCWLLimitSta[sAxis];
        }
        public bool StartSearchHome(short sAxis, double dAcc, double dDec, double dHomeSpd)
        {
            lock (lockObj)
            {
                Dmc1000.d1000_home_move(sAxis,0,(int)dHomeSpd,0);
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
                    Dmc1000.d1000_set_command_pos(sAxis,dPos);
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
            return false;
        }
        public bool ServoOn(short sAxis)
        {
            return false;
        }
        public bool ServoOff(short sAxis)
        {
            return false;
        }
        public bool SetLimtOn(short sAxis)
        {
            return false;
        }
        public bool SetLimtOff(short sAxis)
        {
            return false;
        }
        public bool SetLimtDisable(short sAxis)
        {
            return false;
        }
        public bool SetHomeOn(short sAxis)
        {
            return false;
        }
        public bool SetHomeOff(short sAxis)
        {
            return false;
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
                    Dmc1000.d1000_start_ta_move(sAxis,(int)pos,0,(int)dSpeed,0);
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
                    if(dSpeed < 0)
                    {
                        pos = pos * -1;
                    }
                    dSpeed = Math.Abs(dSpeed);
                    Dmc1000.d1000_start_t_move(sAxis,(int)pos,0,(int)dSpeed,0);
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
                    int iValue = bOn ? (ushort)0 : (ushort)1;
                    Dmc1000.d1000_out_bit(iBit, iValue);
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
                iCardTotal = Dmc1000.d1000_board_init();
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
                for (ushort iAxis = 0; iAxis < 4; iAxis++)
                {
                    int iCurrent = 0;
                    try
                    {
                        _dArrayTargetPos[iAxis] = Dmc1000.d1000_get_command_pos(iAxis);
                        _dArrayCurrPos[iAxis] = Dmc1000.d1000_get_command_pos(iAxis);
                        _bArrayIsMoving[iAxis] = Dmc1000.d1000_check_done(iAxis) == 0 ? true : false;

                        iCurrent = Dmc1000.d1000_get_axis_status(iAxis);
                    }
                    catch
                    {

                    }
                    if ((iCurrent & 0x4) == 0)
                    {
                        _bArrayHomeSta[iAxis] = false;
                    }
                    else
                    {
                        _bArrayHomeSta[iAxis] = true;
                    }

                    //if ((uiCurrent & 0x1) == 0)
                    //{
                    //    _bArrayAlarmSta[iAxis] = false;
                    //}
                    //else
                    //{
                    //    _bArrayAlarmSta[iAxis] = true;
                    //}
                    //if ((uiCurrent & 0x2) == 0)
                    //{
                    //    _bArrayCWLLimitSta[iAxis] = false;
                    //}
                    //else
                    //{
                    //    _bArrayCWLLimitSta[iAxis] = true;
                    //}
                    //if ((uiCurrent & 0x4) == 0)
                    //{
                    //    _bArrayCCWLLimitSta[iAxis] = false;
                    //}
                    //else
                    //{
                    //    _bArrayCCWLLimitSta[iAxis] = true;
                    //}
                    //if ((uiCurrent & 0x10) == 0)
                    //{
                    //    _bArrayHomeSta[iAxis] = false;
                    //}
                    //else
                    //{
                    //    _bArrayHomeSta[iAxis] = true;
                    //}
                }
            }
        }
        public void GetAllIOStatus()
        {
            
            lock (lockObj)
            {
                try
                {
                    for(int index = 0; index < 48; index++)
                    {
                        //int iTemp = Dmc1000.d1000_in_bit(index);
                        _bArrayInputSta[index] = Dmc1000.d1000_in_bit(index) == 1 ? false : true;
                        _bArrayOutputSta[index] = Dmc1000.d1000_get_outbit(index) == 1 ? false : true;
                    }
                }
                catch
                {

                }
            }
        }
        public bool SetAlarmOn(short sAxis)
        {
            return true;
        }
        public bool SetAlarmOff(short sAxis)
        {
            return true;
        }
        public bool SetPulseMode(short sAxis, PulseMode plm)
        {
            lock (lockObj)
            {
                if (plm == PulseMode.PLDI)
                {
                    Dmc1000.d1000_set_pls_outmode((ushort)sAxis, 0);
                }
                else
                {
                    Dmc1000.d1000_set_pls_outmode((ushort)sAxis, 4);
                }
            }
            return true;
        }
        public override bool Close()
        {
            try
            {
                StopMove(0);
                StopMove(1);
                StopMove(2);
                StopMove(3);

                Dmc1000.d1000_board_close();
            }
            catch (Exception)
            {
            }

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
