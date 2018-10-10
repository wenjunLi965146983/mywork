using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Table;
using WorldGeneralLib.Alarm;
using WorldGeneralLib.Data;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.Yamaha.RCX340
{
    public class YamahaRobotRCX340 : HardwareBase, IMotionAction, IInputAction, IOutputAction
    {
        private long _iTimes;
        private YamahaRobotData _robotData;
        private static readonly object _objLock = new object();
        public YamahaRCX340API yamahaRCX340API;
                    
        public YamahaRobotRCX340(YamahaRobotData robotData)
        {
            _iTimes = 0;
            _robotData = robotData;
            yamahaRCX340API = new YamahaRCX340API();
        }

        public override bool Init(HardwareData hardeareData)
        {
            bInitOk = false;
            try
            {
                if (yamahaRCX340API.ConnectToYamahaRobot(_robotData.IP, (short)_robotData.Port, (short)_robotData.ReadTimeout))
                {
                    bInitOk = true;
                }
            }
            catch //(Exception)
            {

            }
            System.Threading.Thread threadRefresh = new System.Threading.Thread(ThreadScan);
            threadRefresh.IsBackground = true;
            threadRefresh.Start();

            return bInitOk;
        }
        public override bool Close()
        {
            try
            {
                StopMove(0);
                yamahaRCX340API.Close();
            }
            catch (Exception)
            {
            }

            return true;
        }
        public override bool IsConnected()
        {
            return yamahaRCX340API.Connected;
        }

        #region 掉线处理
        private void ReConnectToRobot()
        {
            string strMsg = "";
            
            try
            {
                if (!yamahaRCX340API.Connected)
                {
                    _iTimes++;
                    strMsg = string.Format(">{0} 断开连接，即将尝试第 {1} 次重连...", _robotData.Name, _iTimes.ToString());
                    MainModule.AddRunMessage(strMsg, OutputLevel.Warn);
                }
                yamahaRCX340API.DisconnectToYamahaRobot();
                System.Threading.Thread.Sleep(1000);
                yamahaRCX340API.ConnectToYamahaRobot(_robotData.IP, (short)_robotData.Port, (short)_robotData.ReadTimeout);
                if (yamahaRCX340API.Connected)
                {
                    _iTimes = 0;
                    MainModule.AddRunMessage(">成功建立连接。", OutputLevel.Trace);
                }
                else
                {
                    MainModule.AddRunMessage(">连接未成功。", OutputLevel.Warn);
                }
                System.Threading.Thread.Sleep(1000);
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region 机械手接口 内部调用
        private bool _bIsRobotAlarm = false;
        public bool IsRobotAlarm
        {
            get { return _bIsRobotAlarm; }
        }

        private float _fRobotErrCode = 0;
        public float RobotErrCode
        {
            get { return _fRobotErrCode; }
        }
        /// <summary>
        /// 获取Robot报警状态
        /// </summary>
        /// <returns></returns>
        private bool GetAlarmStatus()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    if (yamahaRCX340API.ReadIsAlarm(out float fAlarm))
                    {
                        _bIsRobotAlarm = fAlarm == 0 ? false : true;
                        _fRobotErrCode = fAlarm;
                        yamahaRCX340API.Connected = true;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private bool _bIsEStop = false;
        /// <summary>
        /// 获取Robot是否处于急停状态
        /// </summary>
        /// <returns></returns>
        private bool GetEStopStatus()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    bool bIsEStop = false;
                    if (yamahaRCX340API.ReadIsEMG(ref bIsEStop))
                    {
                        _bIsEStop = bIsEStop;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private bool _bIsServoOn = false;
        /// <summary>
        /// 获取Robot使能状态
        /// </summary>
        /// <returns></returns>
        private bool GetServoStatus()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    bool bIsServoOn = false;
                    if (yamahaRCX340API.ReadIsServoOn(ref bIsServoOn))
                    {
                        _bIsServoOn = bIsServoOn;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private float _fCurrentVel = 0.0f;
        /// <summary>
        /// 获取Robot当前速度
        /// </summary>
        private bool GetCurrentVel()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float fVel = 0.0f;
                    if (yamahaRCX340API.ReadCurrentSpeed(ref fVel))
                    {
                        _fCurrentVel = fVel;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private float _fDist = 1.0f;
        /// <summary>
        /// 获取当前微动距离
        /// </summary>
        /// <returns></returns>
        private bool GetCurrDist()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float fDist = 0.0f;
                    if (yamahaRCX340API.ReadCurrentDist(ref fDist))
                    {
                        _fDist = fDist / 1000;
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private double[] _dArrayCurrPos = new double[9];
        /// <summary>
        /// 获取Robot当前位置
        /// </summary>
        /// 
        private bool GetCurrentPos()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.ReadCurrentPos(ref fArrayPos, false))
                    {
                        for (int index = 0; index < 9; index++) _dArrayCurrPos[index] = fArrayPos[index];
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        private bool[] _bArrayInputSta = new bool[64];
        private bool[] _bArrayOutputSta = new bool[64];
        /// <summary>
        /// 获取Robot输入输出状态
        /// </summary>
        private bool GetInputStatus()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.ReadRobotInputOutput(true,ref _bArrayInputSta))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        private bool GetOutputStatus()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.ReadRobotInputOutput(false, ref _bArrayOutputSta);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        private void ThreadScan()
        {
            int iCount = 0;
            System.Threading.Thread.Sleep(1000);

            while (!MainModule.formMain.bExit)
            {
                System.Threading.Thread.Sleep(1000);
                try
                {
                    #region 掉线检测重连
                    if (!yamahaRCX340API.Connected)    //取消自动重连
                    {
                        if (DataManage.CheckItemExist("System", "RXC340掉线重连次数"))
                        {
                            int iLimitTimes = DataManage.IntValue("System", "RXC340掉线重连次数");
                            if (_iTimes == iLimitTimes)
                            {
                                MainModule.alarmManage.InsertAlarm(AlarmKeys.ConnectAlarm1, "与机械手 YamahaRXC340 连接断开，请检查网络后在手动页面中手动重连！");
                                MainModule.AddRunMessage(">连接未成功。", OutputLevel.Warn);
                                _iTimes++;
                                continue;
                            }
                            else if (_iTimes > iLimitTimes)
                                continue;
                        }
                        ReConnectToRobot();
                        continue;
                    }
                    #endregion

                    #region 机械手信息获取
                    if (iCount % 2 == 0)
                    {
                        //2S
                        GetAlarmStatus();
                        //GetServoStatus();
                        GetCurrentPos();
                    }
                    if (iCount > 3)
                    {
                        //3S
                        //GetCurrentVel();
                        //GetCurrDist();
                        //iCount = 0;
                    }

                    //1S
                    GetInputStatus();
                    GetOutputStatus();
                    iCount++;
                    #endregion
                }
                catch (Exception)
                {
                }

            }
        }
        #endregion

        #region 机械手接口 外部调用
        /// <summary>
        /// 机械手程序复位
        /// </summary>
        /// <returns></returns>
        public bool RobotProgramRst()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.WriteRobotProgramRstCommand())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 机械手程序停止
        /// </summary>
        /// <returns></returns>
        public bool RobotProgramStop()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.WriteRobotProgramStopCommand())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 机械手程序启动
        /// </summary>
        /// <returns></returns>
        public bool RobotProgramStart()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.WriteRobotProgramRunCommand())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 机械手报警清除
        /// </summary>
        /// <returns></returns>
        public bool RobotAlarmClr()
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    float[] fArrayPos = new float[9];
                    if (yamahaRCX340API.WriteRobotAlamrResetCommand())
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        /// <summary>
        /// RCX340 Move Abs
        /// </summary>
        /// <param name="index">机械手内部点Table中的索引号</param>
        /// <param name="dPosX">绝对位置X</param>
        /// <param name="dPosY">绝对位置Y</param>
        /// <param name="dPosZ">绝对位置Z</param>
        /// <param name="dPosU">绝对位置U</param>
        /// <param name="dVel">移动速度</param>
        /// <param name="bCheckPoint">设置点位后是否再次读取比对是否一致</param>
        /// <returns></returns>
        public bool RobotAbsPosMove(ushort index, double dPosX, double dPosY, double dPosZ, double dPosU, double dVel, bool bCheckPoint, ref int iErr)
        {
            lock(_objLock)
            {
                try
                {
                    iErr = 0;
                    #region Write point to robot
                    if (!yamahaRCX340API.WritePointPosCommand(index, dPosX, dPosY, dPosZ, dPosU))
                    {
                        iErr = 1;
                        return false;
                    }
                    #endregion
                    #region Read point from robot points table
                    if (bCheckPoint)
                    {
                        double dReadPosX = 0;
                        double dReadPosY = 0;
                        double dReadPosZ = 0;
                        double dReadPosU = 0;

                        if (!yamahaRCX340API.ReadRobotPointData(index, out dReadPosX, out dReadPosY, out dReadPosZ, out dReadPosU))
                        {
                            iErr = 2;
                            return false;
                        }
                        if (dPosX != dReadPosX || dPosY != dReadPosY || dPosZ != dReadPosZ || dPosU != dReadPosU)
                        {
                            iErr = 3;
                            return false;
                        }
                    }
                    #endregion
                    #region Set Speed
                    if (Math.Abs(dVel) != _fCurrentVel)
                    {
                        if (!SetVel(0, dVel))
                        {
                            iErr = 4;
                            return false;
                        }
                    }
                    #endregion

                    if (!yamahaRCX340API.WriteRobotMoveCommand(index))
                    {
                        iErr = 5;
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                }
                return false;
            }
        }

        /// <summary>
        /// 读取点位信息
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dPosX"></param>
        /// <param name="dPosY"></param>
        /// <param name="dPosZ"></param>
        /// <param name="dPosU"></param>
        /// <returns></returns>
        public bool ReadPointData(ushort index, out double dPosX, out double dPosY, out double dPosZ, out double dPosU)
        {
            dPosX = 0;
            dPosY = 0;
            dPosZ = 0;
            dPosU = 0;
            try
            {
                if (!yamahaRCX340API.Connected)
                    return false;
                lock(_objLock)
                {
                    return yamahaRCX340API.ReadRobotPointData(index, out dPosX, out dPosY, out dPosZ, out dPosU);
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 写坐标到robot内部point table
        /// </summary>
        /// <param name="index"></param>
        /// <param name="dPosX"></param>
        /// <param name="dPosY"></param>
        /// <param name="dPosZ"></param>
        /// <param name="dPosU"></param>
        /// <returns></returns>
        public bool WritePointData(ushort index, double dPosX, double dPosY, double dPosZ, double dPosU)
        {
            try
            {
                if (!yamahaRCX340API.Connected)
                    return false;
                lock (_objLock)
                {
                    return yamahaRCX340API.WritePointPosCommand(index, dPosX, dPosY, dPosZ, dPosU);
                }
            }
            catch (Exception)
            {
            }

            return false;
        }
        /// <summary>
        /// 设置寸动距离
        /// </summary>
        /// <param name="sAxis"></param>
        /// <param name="dDist"></param>
        /// <returns></returns>
        public bool SetDist(short sAxis, double dDist)
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteSetDistCommand((int)dDist * 1000);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool ReadASGIValue(ushort index,ref int iValue)
        {
            try
            {
                lock (_objLock)
                {
                    if (yamahaRCX340API.ReadASGIValue(index, ref iValue))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool WriteASGIValue(ushort index, int iValue)
        {
            try
            {
                lock (_objLock)
                {
                    if (yamahaRCX340API.WriteASGIValue(index, iValue))
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
            } 
            return false;
        }
        public bool RobotDebug(string strSend, ref string strRecv)
        {
            if (!yamahaRCX340API.Connected)
            {
                strRecv = "发送失败，机械手未连接！";
                return false;
            }

            try
            {
                lock (_objLock)
                {
                    if (yamahaRCX340API.WrteDebugMsg(strSend, ref strRecv))
                    {
                        strRecv = "Successful：" + strRecv;
                        return true;
                    }
                    strRecv = "Failed：" + strRecv;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion

        #region 外部通用接口
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
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    bool bIsHome = false;
                    if (yamahaRCX340API.ReadIsOrigin(ref bIsHome))
                    {
                        return bIsHome;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool GetEstop(short sAxis)
        {
            return _bIsEStop;
        }
        public bool GetServoOn(short sAxis)
        {
            return _bIsServoOn;
        }
        public bool GetAlarm(short sAxis)
        {
            return _bIsRobotAlarm;
        }
        //-------------------待实现
        public bool IsMoveDone(short sAxis)
        {
            return false;
        }
        public bool SetVel(short sAxis, double dVel)
        {
            if (!yamahaRCX340API.Connected)
                return false;
            if (dVel < 1)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteSetSpeedCommand((int)dVel);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public double GetVel(short sAxis)
        {
            return _fCurrentVel;
        }
        public bool JobMove(short sAxis, double dAcc, double dDec, double dVel)
        {
            return false;
        }
        public bool StopMove(short sAxis)
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteStopCommand();
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool ServoOn(short sAxis)
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteServoOnOffCommand(true);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool ServoOff(short sAxis)
        {
            if (!yamahaRCX340API.Connected)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteServoOnOffCommand(false);
                }
            }
            catch (Exception)
            {
            }
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
            return false;
        }
        public bool ReferPosMove(short sAxis, double dAcc, double dDec, double dSpeed, double pos)
        {
            try
            {
                int iVel = (int)dSpeed;
                if (0 == iVel)
                {
                    return false;
                }
                    
                #region Set Speed
                if (Math.Abs(iVel) != _fCurrentVel)
                {
                    if (!SetVel(sAxis, Math.Abs(iVel)))
                        return false;
                }
                #endregion
                #region Set Dist
                if(pos != _fDist)
                {
                    if (!SetDist(sAxis,pos))
                        return false;
                }
                #endregion
                bool bDirPos = iVel < 0 ? false : true;
                return yamahaRCX340API.WriteJogInchCommand(sAxis, bDirPos);
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool GetInputBit(int iBit)
        {
            if (iBit < 64 && iBit > -1)
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
            if (iBit >= 64 && iBit < 0)
                return false;
            try
            {
                lock (_objLock)
                {
                    return yamahaRCX340API.WriteOutputCommand(iBit, bOn);
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool GetOutBit(int iBit)
        {
            if (iBit < 64 && iBit > -1)
            {
                return _bArrayOutputSta[iBit];
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region Not Use
        public bool SetPulseMode(short axis, PulseMode psm)
        {
            return false;
        }
        public bool GetLimtCW(short sAxis)
        {
            return false;
        }
        public bool GetLimtCCW(short sAxis)
        {
            return false;
        }
        public bool IsMoving(short sAxis)
        {
            return false;
        }
        public bool HandleErrorMessage(short errorMessage)
        {
            return true;
        }
        public bool StartSearchLimit(short sAxis, double dAcc, double dDec, double dCatchSpeed)
        {
            return true;
        }
        public bool FinishSearchLimit(short sAxis)
        {
            return true;
        }
        public bool StartSearchHome(short sAxis, double dAcc, double dDec, double dHomeSpd)
        {
            return true;
        }
        public bool FinishSearchHome(short sAxis)
        {
            return true;
        }
        public bool SetAxisPos(short sAxis, double dPos)
        {
            return false;
        }
        public bool ZeroAxisPos(short sAxis)
        {
            return false;
        }
        public bool SetAlarmOn(short sAxis)
        {
            return false;
        }
        public bool SetAlarmOff(short sAxis)
        {
            return false;
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

        #endregion
    }
}
