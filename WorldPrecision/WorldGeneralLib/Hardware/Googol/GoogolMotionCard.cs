using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.TaskBase;
using WorldGeneralLib.Table;

namespace WorldGeneralLib.Hardware.Googol
{
    public class GoogolMotionCard : HardwareBase, IMotionAction
    {

        //static bool bBoardClose = false;
        private bool[] bBitInputStatus = new bool[64];
        private bool[] bBitOutputStatus = new bool[64];

        private bool[] bAxisServo = new bool[8];
        private bool[] bAxisMoving = new bool[8];
        private bool[] bHome = new bool[8];
        private bool[] bAlarm = new bool[8];
        private bool[] bCWL = new bool[8];
        private bool[] bCCWL = new bool[8];

        private double[] dCurrentPos = new double[8];
        private double[] dTargetPos = new double[8];
        private double[] dCurrentVel = new double[8];

        private double[] dCommandTargetPos = new double[8];
        private int[,] iColAixsNo = new int[16, 3];


        public short usCardNo = 0;
        private int[] iMoveMode = new int[8];

        private bool[] bHomeDone = new bool[8];
        private bool[] bHomeing = new bool[8];
        private bool[] bHomeStop = new bool[8];
        private bool[] bHomeLast = new bool[8];
        public double GetCurrentPos(short Axis)
        {
            if (Axis < 8 && Axis > -1)
            {
                return dCurrentPos[Axis];
            }
            else
            {
                return 0.0;
            }
        }
        public bool GetHome(short Axis)
        {
            return bHome[Axis];
        }
        public bool GetAlarm(short Axis)
        {
            return bAlarm[Axis];
        }
        public bool GetLimtCW(short Axis)
        {
            return bCWL[Axis];
        }
        public bool GetLimtCCW(short Axis)
        {
            return bCCWL[Axis];
        }
        public bool GetEstop(short Axis)
        {
            return false;
        }
        public bool GetServoOn(short Axis)
        {
            if (Axis < 8 && Axis > -1)
            {
                return bAxisServo[Axis];
            }
            else
            {
                return false;
            }
        }
        public bool IsMoving(short Axis)
        {
            if (Axis < 8 && Axis > -1)
            {
                return bAxisMoving[Axis];
            }
            else
            {
                return false;
            }
        }
        public bool IsMoveDone(short Axis)
        {

            uint uIntClock;

            int lCurrentPos = 0;

            double dValue = 0.0;
            short sRtn;

            bool bResult;
            if (bAxisMoving[Axis])
            {
                bResult = false;
            }
            else
            {
                lock (lockObj)
                {
                    //sRtn = gts.mc.GT_GetPos((short)usCardNo, (short)(Axis+1), out lTargetPos);
                    sRtn = gts.mc.GT_GetAxisEncPos((short)usCardNo, (short)(Axis + 1), out dValue, 1, out uIntClock);
                }
                lCurrentPos = (int)dValue;

                double dDiffValue = Math.Abs(dCommandTargetPos[Axis] - lCurrentPos);//
                if (dDiffValue < 100)
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
        public bool SetVel(short Axis, double dVel)
        {
            short sRtn;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, (short)(Axis + 1), dVel);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (Axis));//更新轴运动
                }
            }
            else
            {
                return false;
            }
            return true;
        }
        public double GetVel(short Axis)
        {
            short sRtn;
            double dVel;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_GetVel((short)usCardNo, (short)(Axis + 1), out dVel);//设置目标速度
                    return dVel;
                }
            }
            else
            {
                return 0.0;
            }

        }
        public bool JobMove(short Axis, double dAcc, double dDec, double dVel)
        {
            short sRtn;
            if (Axis < 8 && Axis > -1)
            {
                lock (lockObj)
                {
                    Axis++;
                    gts.mc.TJogPrm jog = new gts.mc.TJogPrm();
                    jog.acc = dAcc;
                    jog.dec = dDec;

                    sRtn = gts.mc.GT_ClrSts((short)usCardNo, Axis, 8);//清除轴报警和限位
                    sRtn = gts.mc.GT_PrfJog((short)usCardNo, Axis);//设置为jog模式
                    sRtn = gts.mc.GT_SetJogPrm((short)usCardNo, Axis, ref jog);//设置jog运动参数
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, Axis, dVel / 1000.0);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (Axis - 1));//更新轴运动

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool StopMove(short Axis)
        {
            try
            {
                bHomeStop[Axis] = true;
                if (Axis < 8 && Axis > -1)
                {
                    lock (lockObj)
                    {
                        //if (bHomeLast[Axis] == false)
                        //{
                        gts.mc.GT_Stop((short)usCardNo, 1 << (Axis), 0);
                        // }
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return true;
        }
        public bool HandleErrorMessage(short errorMessage)
        {
            if (errorMessage != 0)
            {
                return false;
            }
            return true;
        }
        public bool StartSearchLimit(short axis, double dAcc, double dDec, double dCatchSpeed)
        {
            short sRtn;
            bHomeStop[axis] = false;
            bHomeDone[axis] = false;
            bHomeLast[axis] = false;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    axis++;
                    gts.mc.TJogPrm jog = new gts.mc.TJogPrm();
                    jog.acc = dAcc;
                    jog.dec = dDec;

                    sRtn = gts.mc.GT_ClrSts((short)usCardNo, axis, 1);//清除轴报警和限位
                    sRtn = gts.mc.GT_PrfJog((short)usCardNo, axis);//设置为jog模式
                    sRtn = gts.mc.GT_SetJogPrm((short)usCardNo, axis, ref jog);//设置jog运动参数
                    sRtn = gts.mc.GT_SetVel((short)usCardNo, axis, dCatchSpeed / 1000.0);//设置目标速度
                    sRtn = gts.mc.GT_Update((short)usCardNo, 1 << (axis - 1));//更新轴运动

                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool FinishSearchLimit(short axis)
        {
            return bCCWL[axis] || bCWL[axis];
        }
        public bool StartSearchHome(short axis, double dAcc, double dDec, double dHomeSpd)
        {
            short sRtn;
            if (bHomeing[axis] == false)
            {
                gts.mc.TTrapPrm trapPrm;
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_SetCaptureMode(usCardNo, (short)(axis + 1), gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = 0.25;
                    trapPrm.dec = 0.25;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), Math.Abs(dHomeSpd / 1000.0));
                    // 设置点位模式目标位置，即原点搜索距离
                    int dSearchDis = 0;
                    if (dHomeSpd > 0)
                        dSearchDis = 999999999;
                    else
                        dSearchDis = -999999999;
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), dSearchDis);
                    // 启动运动
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
                }
                bHomeStop[axis] = false;
                bHomeDone[axis] = false;
                bHomeing[axis] = true;
                bHomeLast[axis] = false;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(CheckHomeReturn));
                thread.IsBackground = true;
                thread.Start(axis);


            }
            return true;

        }
        public bool FinishSearchHome(short axis)
        {
            return !bAxisMoving[axis] && bHomeDone[axis];
        }
        public bool SetAxisPos(short axis, double dPos)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    gts.mc.GT_SetEncPos((short)usCardNo, (short)(axis + 1), (int)dPos);
                    gts.mc.GT_SetPos((short)usCardNo, (short)(axis + 1), (int)dPos);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ZeroAxisPos(short axis)
        {
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    short sRtn;
                    double dPos = 0;
                    uint iCH = 0;
                    System.Threading.Thread.Sleep(20);
                    sRtn = gts.mc.GT_ZeroPos((short)usCardNo, (short)(axis + 1), 1);
                    sRtn = gts.mc.GT_SetEncPos((short)usCardNo, (short)(axis + 1), 0);
                    gts.mc.GT_SetPos((short)usCardNo, (short)(axis + 1), (int)dPos);
                    sRtn = gts.mc.GT_GetEncPos((short)usCardNo, (short)(axis + 1), out dPos, 1, out iCH);
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ServoOn(short axis)
        {
            short sRtn;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_AxisOn((short)usCardNo, (short)(axis + 1));
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        public bool ServoOff(short axis)
        {
            short sRtn;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    sRtn = gts.mc.GT_AxisOff((short)usCardNo, (short)(axis + 1));
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool SetLimtOn(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_LmtsOn((short)usCardNo, (short)(axis + 1), -1);
                return true;
            }
        }
        public bool SetLimtOff(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_LmtsOff((short)usCardNo, (short)(axis + 1), -1);
                return true;
            }
        }
        public bool SetLimtDisable(short axis)
        {
            lock (lockObj)
            {
                //LTDMC.dmc_set_el_mode(usCardNo, (ushort)axis, 0, 1, 0);
                return true;
            }
        }
        public bool SetHomeOn(short axis)
        {
            lock (lockObj)
            {
                //gts.mc.gt_s;
                return true;
            }
        }
        public bool SetHomeOff(short axis)
        {
            lock (lockObj)
            {
                //LTDMC.dmc_set_home_pin_logic(usCardNo, (ushort)axis, 1, 0);
                return true;
            }
        }
        public bool SetNearHomeOn(short axis)
        {
            return true;
        }
        public bool SetNearHomeOff(short axis)
        {
            return true;
        }
        public bool AbsPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            short sRtn;
            gts.mc.TTrapPrm trapPrm;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    //sRtn = gts.mc.GT_SetCaptureMode(m_iCardNo, axis, gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = dAcc;
                    trapPrm.dec = dDec;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), dSpeed / 1000.0);
                    // 设置点位模式目标位置，即原点搜索距离
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), (int)(pos));
                    // 启动运动
                    dCommandTargetPos[axis] = pos;
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ReferPosMove(short axis, double dAcc, double dDec, double dSpeed, double pos)
        {
            short sRtn;
            gts.mc.TTrapPrm trapPrm;
            if (axis < 8 && axis > -1)
            {
                lock (lockObj)
                {
                    //sRtn = gts.mc.GT_SetCaptureMode(m_iCardNo, axis, gts.mc.CAPTURE_HOME);
                    // 切换到点位运动模式
                    sRtn = gts.mc.GT_PrfTrap(usCardNo, (short)(axis + 1));
                    // 读取点位模式运动参数
                    sRtn = gts.mc.GT_GetTrapPrm(usCardNo, (short)(axis + 1), out trapPrm);
                    trapPrm.acc = dAcc;
                    trapPrm.dec = dDec;
                    // 设置点位模式运动参数
                    sRtn = gts.mc.GT_SetTrapPrm(usCardNo, (short)(axis + 1), ref trapPrm);
                    // 设置点位模式目标速度，即回原点速度
                    sRtn = gts.mc.GT_SetVel(usCardNo, (short)(axis + 1), dSpeed / 1000.0);
                    // 设置点位模式目标位置，即原点搜索距离

                    uint uIntClock;
                    int lCurrentPos = 0;
                    double dValue = 0.0;

                    sRtn = gts.mc.GT_GetAxisEncPos((short)usCardNo, (short)(axis + 1), out dValue, 1, out uIntClock);
                    lCurrentPos = (int)dValue;

                    int iTargetPos = (int)(lCurrentPos + pos);
                    sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), iTargetPos);
                    // 启动运动
                    dCommandTargetPos[axis] = iTargetPos;
                    sRtn = gts.mc.GT_Update(usCardNo, 1 << (axis));
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
                return bBitInputStatus[iBit];
            }
            else
            {
                return false;
            }
        }
        public bool SetOutBit(int iBit, bool bOn)
        {
            short sRtn;
            lock (lockObj)
            {

            }
            if (iBit < 128 && iBit > -1)
            {
                iBit++;
                lock (lockObj)
                {
                    short uValue = bOn ? (short)0 : (short)1;
                    sRtn = gts.mc.GT_SetDoBit(usCardNo, gts.mc.MC_GPO, (short)iBit, uValue);
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
                return bBitOutputStatus[iBit];
            }
            else
            {
                return false;
            }

        }
        override public bool Init(HardwareData hardwareData)
        {
            try
            {
                bInitOk = false;
                GoogolMotionCardData data = (GoogolMotionCardData)hardwareData;
                usCardNo = (short)data.Index;
                short sRtn = 0;
                sRtn = gts.mc.GT_Open((short)usCardNo, 0, 0);
                if (!HandleErrorMessage(sRtn))
                {
                    return false;
                }
                //sRtn = gts.mc.GT_Stop(0xFF, 0);

                sRtn = gts.mc.GT_Reset((short)usCardNo);
                sRtn = gts.mc.GT_LoadConfig((short)usCardNo, data.Path);
                sRtn = gts.mc.GT_ClrSts((short)usCardNo, 1, 8);//清除轴报警和限位
                bInitOk = true;

                System.Threading.Thread threadScan = new System.Threading.Thread(ScanThreadFunction);
                threadScan.IsBackground = true;
                threadScan.Start();
                return true;
            }
            catch //(Exception)
            {
                return false;
            }

        }
        private void ScanThreadFunction()
        {
            HiPerfTimer timer = new HiPerfTimer();
            System.Threading.Thread.Sleep(1000);
            while (true)
            {
                System.Threading.Thread.Sleep(10);
                if (MainModule.formMain.bExit)
                    break;
                GetAllMotionStatus();
                GetAllIOStatus();


            }
        }
        public void GetAllMotionStatus()
        {
            double dValue = 0.0;
            int lAxisStatus = 0;
            uint uIntClock;
            short sRtn;
            lock (lockObj)
            {
                for (ushort iAxis = 0; iAxis < 8; iAxis++)
                {
                    //uint uiCurrent=0;
                    try
                    {
                        gts.mc.GT_GetAxisEncPos(usCardNo, (short)(iAxis + 1), out dValue, 1, out uIntClock);
                        dCurrentPos[iAxis] = dValue;
                        sRtn = gts.mc.GT_GetSts(usCardNo, (short)(iAxis + 1), out lAxisStatus, 1, out uIntClock);
                        if ((lAxisStatus & 0x1F2) != 0)
                        {
                            sRtn = gts.mc.GT_ClrSts(usCardNo, (short)(iAxis + 1), 1);
                        }

                    }
                    catch
                    {

                    }
                    if ((lAxisStatus & 0x400) != 0)
                    {
                        bAxisMoving[iAxis] = true;
                    }
                    else
                    {
                        bAxisMoving[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x2) != 0)
                    {
                        bAlarm[iAxis] = true;
                    }
                    else
                    {
                        bAlarm[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x20) != 0)
                    {
                        bCWL[iAxis] = true;
                    }
                    else
                    {
                        bCWL[iAxis] = false;
                    }
                    if ((lAxisStatus & 0x40) != 0)
                    {
                        bCCWL[iAxis] = true;
                    }
                    else
                    {
                        bCCWL[iAxis] = false;
                    }
                }
                int lGpiValueHome;
                sRtn = gts.mc.GT_GetDi(usCardNo, gts.mc.MC_HOME, out lGpiValueHome);
                if ((lGpiValueHome & 0x10) == 0)
                {
                    bHome[0] = false;
                }
                else
                {
                    bHome[0] = true;
                }
                if ((lGpiValueHome & 0x2) != 0)
                {
                    bHome[1] = false;
                }
                else
                {
                    bHome[1] = true;
                }
                if ((lGpiValueHome & 0x4) != 0)
                {
                    bHome[2] = false;
                }
                else
                {
                    bHome[2] = true;
                }
                if ((lGpiValueHome & 0x8) != 0)
                {
                    bHome[3] = false;
                }
                else
                {
                    bHome[3] = true;
                }
                if ((lGpiValueHome & 0x10) != 0)
                {
                    bHome[4] = false;
                }
                else
                {
                    bHome[4] = true;
                }
                if ((lGpiValueHome & 0x20) != 0)
                {
                    bHome[5] = false;
                }
                else
                {
                    bHome[5] = true;
                }
                if ((lGpiValueHome & 0x40) != 0)
                {
                    bHome[6] = false;
                }
                else
                {
                    bHome[6] = true;
                }
                if ((lGpiValueHome & 0x80) != 0)
                {
                    bHome[7] = false;
                }
                else
                {
                    bHome[7] = true;
                }
            }
        }
        public void GetAllIOStatus()
        {
            lock (lockObj)
            {
                int uiInput = 0;
                int uiOutput = 0;
                short sRtn;
                try
                {
                    sRtn = gts.mc.GT_GetDi(usCardNo, gts.mc.MC_GPI, out uiInput);
                    sRtn = gts.mc.GT_GetDo(usCardNo, gts.mc.MC_GPO, out uiOutput);
                }
                catch
                {

                }
                bBitInputStatus[0] = ((uiInput & 0x1) == 0) ? true : false;
                bBitInputStatus[1] = ((uiInput & 0x2) == 0) ? true : false;
                bBitInputStatus[2] = ((uiInput & 0x4) == 0) ? true : false;
                bBitInputStatus[3] = ((uiInput & 0x8) == 0) ? true : false;
                bBitInputStatus[4] = ((uiInput & 0x10) == 0) ? true : false;
                bBitInputStatus[5] = ((uiInput & 0x20) == 0) ? true : false;
                bBitInputStatus[6] = ((uiInput & 0x40) == 0) ? true : false;
                bBitInputStatus[7] = ((uiInput & 0x80) == 0) ? true : false;
                bBitInputStatus[8] = ((uiInput & 0x100) == 0) ? true : false;
                bBitInputStatus[9] = ((uiInput & 0x200) == 0) ? true : false;
                bBitInputStatus[10] = ((uiInput & 0x400) == 0) ? true : false;
                bBitInputStatus[11] = ((uiInput & 0x800) == 0) ? true : false;
                bBitInputStatus[12] = ((uiInput & 0x1000) == 0) ? true : false;
                bBitInputStatus[13] = ((uiInput & 0x2000) == 0) ? true : false;
                bBitInputStatus[14] = ((uiInput & 0x4000) == 0) ? true : false;
                bBitInputStatus[15] = ((uiInput & 0x8000) == 0) ? true : false;

                bBitOutputStatus[0] = ((uiOutput & 0x1) == 0) ? true : false;
                bBitOutputStatus[1] = ((uiOutput & 0x2) == 0) ? true : false;
                bBitOutputStatus[2] = ((uiOutput & 0x4) == 0) ? true : false;
                bBitOutputStatus[3] = ((uiOutput & 0x8) == 0) ? true : false;
                bBitOutputStatus[4] = ((uiOutput & 0x10) == 0) ? true : false;
                bBitOutputStatus[5] = ((uiOutput & 0x20) == 0) ? true : false;
                bBitOutputStatus[6] = ((uiOutput & 0x40) == 0) ? true : false;
                bBitOutputStatus[7] = ((uiOutput & 0x80) == 0) ? true : false;
                bBitOutputStatus[8] = ((uiOutput & 0x100) == 0) ? true : false;
                bBitOutputStatus[9] = ((uiOutput & 0x200) == 0) ? true : false;
                bBitOutputStatus[10] = ((uiOutput & 0x400) == 0) ? true : false;
                bBitOutputStatus[11] = ((uiOutput & 0x800) == 0) ? true : false;
                bBitOutputStatus[12] = ((uiOutput & 0x1000) == 0) ? true : false;
                bBitOutputStatus[13] = ((uiOutput & 0x2000) == 0) ? true : false;
                bBitOutputStatus[14] = ((uiOutput & 0x4000) == 0) ? true : false;
                bBitOutputStatus[15] = ((uiOutput & 0x8000) == 0) ? true : false;

            }
        }
        public bool SetAlarmOn(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_AlarmOn(usCardNo, (short)(axis + 1));
            }
            return true;
        }
        public bool SetAlarmOff(short axis)
        {
            lock (lockObj)
            {
                gts.mc.GT_AlarmOff(usCardNo, (short)(axis + 1));
            }
            return true;
        }
        public bool SetPulseMode(short axis, PulseMode plm)
        {
            lock (lockObj)
            {
                if (plm == PulseMode.PLDI)
                {
                    gts.mc.GT_StepDir(usCardNo, (short)(axis + 1));
                }
                else
                {
                    gts.mc.GT_StepPulse(usCardNo, (short)(axis + 1));
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
        private bool CheckHomeCatch(short axis, out int pos)
        {
            short sRtn;
            short capture;
            uint clk;
            lock (lockObj)
            {
                // 读取捕获状态
                sRtn = gts.mc.GT_GetCaptureStatus(usCardNo, (short)(axis + 1), out capture, out pos, 1, out clk);
                if (capture == 1)
                {
                    gts.mc.GT_Stop(usCardNo, 1 << (axis), 0);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void CheckHomeReturn(Object obj)
        {
            short axis = (short)obj;
            int iPos;

            while (true)
            {
                if (bHomeStop[axis])
                {
                    bHomeing[axis] = false;
                    bHomeStop[axis] = false;
                    return;
                }
                if (CheckHomeCatch(axis, out iPos))
                {
                    break;
                }
                System.Threading.Thread.Sleep(1);
            }
            bHomeLast[axis] = true;
            while (IsMoving(axis))
            {
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(100);

            ReturnToHomePos(axis, iPos);
            System.Threading.Thread.Sleep(50);
            //判断运动完成
            while (IsMoving(axis))
            {
                System.Threading.Thread.Sleep(1);
            }
            System.Threading.Thread.Sleep(200);
            ZeroAxisPos(axis);
            bHomeDone[axis] = true;
            bHomeing[axis] = false;
        }
        private void ReturnToHomePos(short axis, int pos)
        {
            short sRtn;

            lock (lockObj)
            {
                //运动到"捕获位置+偏移量"
                sRtn = gts.mc.GT_SetPos(usCardNo, (short)(axis + 1), pos);
                HandleErrorMessage(sRtn);
                // 在运动状态下更新目标位置
                sRtn = gts.mc.GT_Update(usCardNo, 1 << axis);
                HandleErrorMessage(sRtn);
            }

        }

        public bool LineXYZMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double posZ)
        {
            BuildCor(num, AxisX, AxisY, AxisZ);
            InsertLine(num, posX, posY, posZ, dSpeed, dAcc, 0);
            StartCure(num);
            return true;
        }
        public bool ArcXYMove(short num, short AxisX, short AxisY, short AxisZ, double dAcc, double dDec, double dSpeed, double posX, double posY, double dR, short iCCW)
        {
            BuildCor(num, AxisX, AxisY, AxisZ);
            InsertArc(num, posX, posY, dR, dSpeed, iCCW, dAcc, 0.0);
            StartCure(num);
            return true;
        }

        public bool ArcXYCZMove(short num, short AxisX, short AxisY, short AxisZ, double posX, double posY, double posZ, double xCenter, double yCenter, short iCCW, double dSpeed, double dAcc, double velEnd, short fifo = 0)
        {
            BuildCor(num, AxisX, AxisY, AxisZ);
            InserrtXYCZArc(num, posX, posY, posZ, xCenter, yCenter, iCCW, dSpeed, dAcc, velEnd, 0);
            StartCure(num);
            return true;
        }
        public void InserrtXYCZArc(short num, double dPosX, double dPosY, double dPosZ, double xCenter, double yCenter, short iCCW, double dSpeed, double dAcc, double velEnd, short fifo = 0)
        {

            short sRtn;

            lock (lockObj)
            {

                gts.mc.TCrdPrm crdPrm;
                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
                int iTargetPosX = (int)(dPosX);
                int iTargetPosY = (int)(dPosY);
                int iTargetPosZ = (int)dPosZ;

                dCommandTargetPos[iColAixsNo[num - 1, 0]] = iTargetPosX;
                dCommandTargetPos[iColAixsNo[num - 1, 1]] = iTargetPosY;
                dCommandTargetPos[iColAixsNo[num - 1, 2]] = iTargetPosZ;

                gts.mc.GT_HelixXYCZ(usCardNo, num, iTargetPosX, iTargetPosY, iTargetPosZ, xCenter, yCenter, iCCW, dSpeed / 1000, dAcc, velEnd, 0);
            }
            ;
        }
        //请只能使用1，2
        public void BuildCor(short num, short AxisX, short AxisY, short AxisZ)
        {
            short sRtn;
            iColAixsNo[num - 1, 0] = AxisX;
            iColAixsNo[num - 1, 1] = AxisY;
            iColAixsNo[num - 1, 2] = AxisZ;
            gts.mc.TCrdPrm crdPrm;

            lock (lockObj)
            {
                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, num, out crdPrm);
                crdPrm.dimension = 3; // 坐标系为三维坐标系
                crdPrm.synVelMax = 500; // 最大合成速度：500pulse/ms
                crdPrm.synAccMax = 1; // 最大加速度：1pulse/ms^2
                crdPrm.evenTime = 50; // 最小匀速时间：50ms
                switch (AxisX)
                {
                    case 0:
                        crdPrm.profile1 = 1;
                        break;
                    case 1:
                        crdPrm.profile2 = 1;
                        break;
                    case 2:
                        crdPrm.profile3 = 1;
                        break;
                    case 3:
                        crdPrm.profile4 = 1;
                        break;
                    case 4:
                        crdPrm.profile5 = 1;
                        break;
                    case 5:
                        crdPrm.profile6 = 1;
                        break;
                    case 6:
                        crdPrm.profile7 = 1;
                        break;
                    case 7:
                        crdPrm.profile8 = 1;
                        break;
                    default:
                        break;
                }
                switch (AxisY)
                {
                    case 0:
                        crdPrm.profile1 = 2;
                        break;
                    case 1:
                        crdPrm.profile2 = 2;
                        break;
                    case 2:
                        crdPrm.profile3 = 2;
                        break;
                    case 3:
                        crdPrm.profile4 = 2;
                        break;
                    case 4:
                        crdPrm.profile5 = 2;
                        break;
                    case 5:
                        crdPrm.profile6 = 2;
                        break;
                    case 6:
                        crdPrm.profile7 = 2;
                        break;
                    case 7:
                        crdPrm.profile8 = 2;
                        break;
                    default:
                        break;
                }
                switch (AxisZ)
                {
                    case 0:
                        crdPrm.profile1 = 3;
                        break;
                    case 1:
                        crdPrm.profile2 = 3;
                        break;
                    case 2:
                        crdPrm.profile3 = 3;
                        break;
                    case 3:
                        crdPrm.profile4 = 3;
                        break;
                    case 4:
                        crdPrm.profile5 = 3;
                        break;
                    case 5:
                        crdPrm.profile6 = 3;
                        break;
                    case 6:
                        crdPrm.profile7 = 3;
                        break;
                    case 7:
                        crdPrm.profile8 = 3;
                        break;
                    default:
                        break;
                }
                crdPrm.setOriginFlag = 1; // 表示需要指定坐标系的原点坐标的规划位置
                crdPrm.originPos1 = 0; // 坐标系的原点坐标的规划位置为（100, 100）
                crdPrm.originPos2 = 0;
                crdPrm.originPos3 = 0;

                // 建立1号坐标系，设置坐标系参数
                sRtn = gts.mc.GT_SetCrdPrm(usCardNo, num, ref crdPrm);
                sRtn = gts.mc.GT_CrdClear(0, num, 0);
                sRtn = gts.mc.GT_CrdClear(0, num, 1);
            }

        }
        public void InsertLine(short num, double dPosX, double dPosY, double dPosZ, double dSpeed, double dAcc, double dEndSpeed)
        {
            short sRtn;

            lock (lockObj)
            {

                gts.mc.TCrdPrm crdPrm;

                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, num, out crdPrm);
                int iTargetPosX = (int)(dPosX);
                int iTargetPosY = (int)(dPosY);
                int iTargetPosZ = (int)(dPosZ);
                dCommandTargetPos[iColAixsNo[num - 1, 0]] = iTargetPosX;
                dCommandTargetPos[iColAixsNo[num - 1, 1]] = iTargetPosY;
                dCommandTargetPos[iColAixsNo[num - 1, 2]] = iTargetPosZ;
                sRtn = gts.mc.GT_LnXYZ(usCardNo,
                        num, // 该插补段的坐标系是坐标系1
                        iTargetPosX, iTargetPosY, iTargetPosZ, // 该插补段的终点坐标(200000, 0)
                        dSpeed / 1000.0, // 该插补段的目标速度：100pulse/ms
                        dAcc, // 插补段的加速度：0.1pulse/ms^2
                        dEndSpeed / 1000.0, // 终点速度为0
                        0); // 向坐标系1的FIFO0缓存区传递该直线插补数据
            }

        }
        public void InsertArc(short num, double dPosX, double dPosY, double dR, double dSpeed, short iCCW, double dAcc, double dEndSpeed)
        {
            short sRtn;

            lock (lockObj)
            {

                gts.mc.TCrdPrm crdPrm;
                sRtn = gts.mc.GT_GetCrdPrm(usCardNo, 1, out crdPrm);
                int iTargetPosX = (int)(dPosX);
                int iTargetPosY = (int)(dPosY);
                int iR = (int)(dR);
                dCommandTargetPos[iColAixsNo[num - 1, 0]] = iTargetPosX;
                dCommandTargetPos[iColAixsNo[num - 1, 1]] = iTargetPosY;
                sRtn = gts.mc.GT_ArcXYR(usCardNo,
                        num,
                        iTargetPosX, iTargetPosY,
                        iR,
                        iCCW,
                        dSpeed / 1000.0,
                        dAcc,
                        dEndSpeed,
                        0);
            }

        }
        public void StartCure(short num)
        {
            short sRtn;
            lock (lockObj)
            {
                sRtn = gts.mc.GT_CrdStart(usCardNo, num, 0);
            }

        }
        public bool CureMoveDone(short num, out int iStep)
        {
            short sRtn;
            short run;
            int segment;

            lock (lockObj)
            {
                sRtn = gts.mc.GT_CrdStatus(usCardNo, num, out run, out segment, 0);
                iStep = segment;
            }
            if (run == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        //不用
        public bool RobotAbsPosMove(ushort index, double dPosX, double dPosY, double dPosZ, double dPosU, double dVel, bool bCheckPoint, ref int iErr)
        {
            return false;
        }
    }
}
