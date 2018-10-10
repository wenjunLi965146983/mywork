using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Hardware.Yamaha.RCX340
{
    public class YamahaRCX340API
    {
        public EtherNetBase etherNetBase;

        private bool _bConnectSta = false;
        public bool Connected
        {
            get { return _bConnectSta; }
            set { _bConnectSta = value; }
        }
        public bool ConnectToYamahaRobot(string ipaddress, short sPort, short sTimeout)
        {
            short bconnect = Link(ipaddress, sPort, sTimeout);
            if (bconnect == 0)
            {
                _bConnectSta = true;
            }
            else
            {
                _bConnectSta = false;
            }
            return _bConnectSta;
        }
        public bool DisconnectToYamahaRobot()
        {
            try
            {
                short bconnect = Close();
                if (bconnect == 0)
                {
                    _bConnectSta = false;
                    return true;
                }
                else
                {
                    //MessageBox.Show("断开出错！");
                    _bConnectSta = false;
                    return false;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        #region Socket
        /// <summary>
        /// 与YAMAHA RCX340 建立TCP连接
        /// </summary>
        /// <param name="strIP">Robot 的IP地址</param>
        /// <param name="sPort">端口号</param>
        /// <param name="sReadTimeout">读超时</param>
        /// <returns></returns>
        public short Link(string strIP, short sPort, short sReadTimeout = 3000)
        {
            try
            {
                etherNetBase = new EtherNetBase(sReadTimeout);
                if (!etherNetBase.PintCheck(strIP, sReadTimeout))
                    return -1;
                etherNetBase.tcpClient.ReceiveTimeout = sReadTimeout;
                etherNetBase.tcpClient.Connect(strIP, (int)sPort);
                etherNetBase.tcpClient.Client.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 100, 10), null);
                etherNetBase.networkStream = etherNetBase.tcpClient.GetStream();
                return 0;
            }
            catch (Exception)
            {
            }
            return -1;
        }

        /// <summary>
        /// 处理死链接
        /// </summary>
        /// <param name="onOff"></param>
        /// <param name="keepAliveTime"></param>
        /// <param name="keepAliveInterval"></param>
        /// <returns></returns>
        private byte[] KeepAlive(int onOff, int keepAliveTime, int keepAliveInterval)
        {
            byte[] buffer = new byte[12];
            BitConverter.GetBytes(onOff).CopyTo(buffer, 0);
            BitConverter.GetBytes(keepAliveTime).CopyTo(buffer, 4);
            BitConverter.GetBytes(keepAliveInterval).CopyTo(buffer, 8);
            return buffer;
        }

        /// <summary>
        /// 关闭Robot操作对象的TCP连接
        /// </summary>
        /// <returns></returns>
        public short Close()
        {
            try
            {
                etherNetBase.networkStream.Close();
                etherNetBase.tcpClient.Close();

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        ////////////////////////////////////////////////////////////

        #region Read
        public bool ReadCurrentPosAsPulse(ref float[] dArrayCurrPos)
        {
            return ReadCurrentPos(ref dArrayCurrPos, true);
        }
        public bool ReadCurrentPosAsMM(ref float[] dArrayCurrPos)
        {
            return ReadCurrentPos(ref dArrayCurrPos, false);
        }
        /// <summary>
        /// 读取robot当前位置
        /// </summary>
        /// <param name="dArrayCurrPos">位置返回</param>
        /// <param name="bAsPulse">以脉冲方式返回或者MM返回</param>
        /// <returns>成功返回True,否则返回false</returns>
        public bool ReadCurrentPos(ref float[] dArrayCurrPos, bool bAsPulse)
        {
            if (dArrayCurrPos.Length < 9)
                return false;
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = bAsPulse ? "@ ?WHERE \r" : "@ ?WHRXY \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    string[] strArrayPos = strArrayRecv[0].Split(' ');
                    if (strArrayPos.Length != 9)
                        return false;
                    for (int index = 0; index < 9; index++)
                    {
                        dArrayCurrPos[index] = Convert.ToSingle(strArrayPos[index]);
                    }
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool ReadCurrentSpeed(ref float fValue)
        {
            return ReadMotionInfo("@ ?MSPEED \r", ref fValue);
        }
        public bool ReadCurrentDist(ref float fValue)
        {
            return ReadMotionInfo("@ ?IDIST \r", ref fValue);
        }
        public bool ReadCurrentAccel(ref float fValue)
        {
            return ReadMotionInfo("@ ?ACCEL(1) \r", ref fValue);
        }
        public bool ReadCurrentDecel(ref float fValue)
        {
            return ReadMotionInfo("@ ?DECEL(1) \r", ref fValue);
        }
        /// <summary>
        /// Read motion info(curr speed, accel, decel)
        /// </summary>
        /// <param name="strCmd">指令内容</param>
        /// <param name="fValue">返回值</param>
        /// <returns></returns>
        internal bool ReadMotionInfo(string strCmd, ref float fValue)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                //if (strRecvData.Contains("OK"))   //有时没收到OK，只有数值
                if(strArrayRecv.Length > 0 )
                {
                    //解析返回内容
                    //TODO
                    float fTemp = 0;
                    float.TryParse(strArrayRecv[0], out fTemp);

                    fValue = fTemp;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool ReadASGIValue(ushort index, ref int iValue)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ ?ASGI" + index.ToString() + " \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                //if (strRecvData.Contains("OK"))   //有时没收到OK，只有数值
                if (strArrayRecv.Length > 0)
                {
                    //解析返回内容
                    //TODO
                    int iTemp = int.Parse(strArrayRecv[0], NumberStyles.Any);
                    //if (!int.TryParse(strArrayRecv[0], out iTemp))
                    //{
                    //    return false;
                    //}
                    iValue = iTemp;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }


        /// <summary>
        /// 获取Robot是否使能
        /// </summary>
        /// <param name="bIsServoOn">返回值</param>
        /// <returns></returns>
        public bool ReadIsServoOn(ref bool bIsServoOn)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ ?MOTOR \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                //string[] strArrayRecv = strRecvData.Split('\r');
                if (strRecvData.Contains("OK") && strRecvData.Contains("2"))
                {
                    //解析返回内容
                    //TODO
                    bIsServoOn = true;
                }
                else if (strRecvData.Contains("OK"))
                {
                    bIsServoOn = false;
                }
                else
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 是否已经回原点
        /// </summary>
        /// <param name="bIsOrigin"></param>
        /// <returns></returns>
        public bool ReadIsOrigin(ref bool bIsOrigin)
        {
            byte[] byteArrayRecv = new byte[256];
            try
            {
                string strCmd = "@？ORIGIN";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 是否处于急停状态
        /// </summary>
        /// <param name="bIsEMG"></param>
        /// <returns></returns>
        public bool ReadIsEMG(ref bool bIsEMG)
        {
            byte[] byteArrayRecv = new byte[256];
            try
            {
                string strCmd = "@ ?EMG \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO
                    bIsEMG = false;
                }
                else
                {
                    bIsEMG = true;
                }
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot是否处于报警状态
        /// </summary>
        /// <param name="fErr"></param>
        /// <returns></returns>
        public bool ReadIsAlarm(out float fErr)
        {
            fErr = 0;
            byte[] byteArrayRecv = new byte[256];
            try
            {

                string strCmd = "@ ?ALM \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO
                    fErr = Convert.ToSingle(strArrayRecv[0]);
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Read input/output status
        /// </summary>
        /// <param name="bReadInput"></param>
        /// <returns></returns>
        public bool ReadRobotInputOutput(bool bReadInput , ref bool[] bArraySta)
        {
            byte[] byteArrayRecv = new byte[256];
            try
            {
                string strCmd = bReadInput ? "@ ?DI2() \r" : "@ ?DO2() \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv.Contains("OK"))
                {
                    //解析返回内容
                    //TODO
                    byte byteTemp = 0;
                    byte.TryParse(strArrayRecv[0], out byteTemp);
                    for(int index=0; index < 8; index++)
                    {
                        bArraySta[index + 20] = (byteTemp & (1 << index)) > 0 ? true : false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 获取Robot单个IO输入状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bSta"></param>
        /// <returns></returns>
        public bool ReadSingleInputStatus(int index, out bool bSta)
        {
            bSta = false;
            byte[] byteArrayRecv = new byte[256];
            try
            {
                string strCmd = string.Format("@ ?DI{0}({1}) \r", index / 10, index % 10);
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO
                    bSta = strArrayRecv[0].Equals("0") ? false : true;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 获取Robot单个IO输出状态
        /// </summary>
        /// <param name="index"></param>
        /// <param name="bSta"></param>
        /// <returns></returns>
        public bool ReadSingleOutputStatus(int index, out bool bSta)
        {
            bSta = false;
            byte[] byteArrayRecv = new byte[256];
            try
            {
                string strCmd = string.Format("@ ?DO{0}({1}) \r", index / 10, index % 10);
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO
                    bSta = strArrayRecv[0].Equals("0") ? false : true;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 获取Robot点动距离
        /// </summary>
        /// <param name="fStep"></param>
        /// <returns></returns>
        public bool ReadRobotJogStep(out float fStep)
        {
            fStep = 0.0f;
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ ?IDIST \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO
                    fStep = Convert.ToSingle(strArrayRecv[0]) / 1000;
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        /// <summary>
        /// 读取robot内部点table中的坐标
        /// </summary>
        /// <param name="index">table index</param>
        /// <param name="dPosX">pos x</param>
        /// <param name="dPosY">pos y</param>
        /// <param name="dPosZ">pos z</param>
        /// <param name="dPosU">pos u</param>
        /// <returns></returns>
        public bool ReadRobotPointData(ushort index, out double dPosX, out double dPosY, out double dPosZ, out double dPosU)
        {
            dPosX = 0;
            dPosY = 0;
            dPosZ = 0;
            dPosU = 0;
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strIndex = index.ToString() + " \r";
                string strCmd = "@ ?P" + strIndex;
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO
                    string[] strArrayPos = strArrayRecv[0].Split(' ');
                    if (strArrayPos.Length < 9)
                        return false;
                    dPosX = Convert.ToDouble(strArrayPos[0]);
                    dPosY = Convert.ToDouble(strArrayPos[1]);
                    dPosZ = Convert.ToDouble(strArrayPos[2]);
                    dPosU = Convert.ToDouble(strArrayPos[3]);
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion

        #region Write
        /// <summary>
        /// 命令Robot立即停止运动
        /// </summary>
        /// <returns></returns>
        public bool WriteStopCommand()
        {
            byte[] byteArrayRecv = new byte[256];
            try
            {
                byte[] byteArrayCmd = new byte[5];
                byteArrayCmd[0] = (byte)'@';
                byteArrayCmd[1] = (byte)' ';
                byteArrayCmd[2] = 0x03;
                byteArrayCmd[3] = (byte)' ';
                byteArrayCmd[4] = (byte)'\r';
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;

                }
                
                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public bool WriteJogContinueCommand(short axis, bool bDirPos)
        {
            try
            {
                string strDir = bDirPos ? "+ \r" : "- \r";
                string strCmd = "";
                byte[] byteArrayRecv = new byte[256];
                switch (axis)
                {
                    case 0: strCmd = "@ JOG 1" + strDir; break;
                    case 1: strCmd = "@ JOG 2" + strDir; break;
                    case 2: strCmd = "@ JOG 3" + strDir; break;
                    case 3: strCmd = "@ JOG 4" + strDir; break;
                    default: return false;
                }

                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("RUN"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Robot寸动
        /// </summary>
        /// <param name="axis">轴选择</param>
        /// <param name="bDirPos">方向</param>
        /// <returns></returns>
        public bool WriteJogInchCommand(short axis, bool bDirPos)
        {
            try
            {
                string strDir = bDirPos ? "+ \r" : "- \r";
                string strCmd = "";
                byte[] byteArrayRecv = new byte[128];
                switch (axis)
                {
                    case 0: strCmd = "@ INCHXY 1" + strDir; break;
                    case 1: strCmd = "@ INCHXY 2" + strDir; break;
                    case 2: strCmd = "@ INCHXY 3" + strDir; break;
                    case 3: strCmd = "@ INCHXY 4" + strDir; break;
                    default: return false;
                }

                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("RUN"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 命令Robot回原点
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotHomeCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ABSRST \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("END"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// 设置Robot运行速度
        /// </summary>
        /// <param name="iSpeed"></param>
        /// <returns></returns>
        public bool WriteSetSpeedCommand(int iSpeed)
        {
            try
            {
                string strCmd = "@ MSPEED " +iSpeed .ToString() + " \r";
                byte[] byteArrayRecv = new byte[128];
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        public bool WriteSetDistCommand(int iDist)
        {
            try
            {
                string strCmd = "@ IDIST " + iDist.ToString() + " \r";
                byte[] byteArrayRecv = new byte[128];
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 设置Robot加速度
        /// </summary>
        /// <param name="iAcc"></param>
        /// <returns></returns>
        public bool WriteSetAccCommand(int iAcc)
        {
            try
            {
                string strCmd = "@ACCEL " + iAcc.ToString();
                byte[] byteArrayRecv = new byte[128];
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 设置Robot减速度
        /// </summary>
        /// <param name="iDec"></param>
        /// <returns></returns>
        public bool WriteSetDecCommand(int iDec)
        {
            try
            {
                string strCmd = "@DECEL " + iDec.ToString();
                byte[] byteArrayRecv = new byte[128];
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// 设定Robot左手或右手系统
        /// </summary>
        /// <param name="bRight"></param>
        /// <returns></returns>
        public bool WriteArmSel(bool bRight)
        {
            try
            {
                string strCmd = "";
                strCmd = bRight ? "@ARMSEL 1" : "@ARMSEL 2";
                byte[] byteArrayRecv = new byte[128];
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    return true;
                }
            }
            catch (Exception)
            {
            }

            return false;
        }

        /// <summary>
        /// Robot使能控制
        /// </summary>
        /// <param name="bServoOn"></param>
        /// <returns></returns>
        public bool WriteServoOnOffCommand(bool bServoOn)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = bServoOn ? "@MOTOR ON \r" : "@MOTOR OFF \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                //string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                //string[] strArrayRecv = strRecvData.Split('\r');
                //if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("END"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot输出控制
        /// </summary>
        /// <param name="iSet"></param>
        /// <returns></returns>
        public bool WriteOutputCommand(int iBit, bool bOn)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strOp = bOn ? "1 \r" : "0 \r";
                string strCmd = String.Format("@ DO{0}({1})={2}", iBit / 10, iBit % 10, strOp);
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序复位
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotSofResetCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ RESET \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序停止
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotSofStopCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ STOP \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序运行
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotSofRunCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ RUN \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public bool WriteASGIValue(ushort index, int iValue)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = string.Format("@ ASGI{0}={1} \r",index, iValue);
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length > 0 && strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot报警清除
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotAlamrResetCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ ALMRST \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot做点位移动 Abs
        /// </summary>
        /// <param name="iPointIndex">Robot points table index</param>
        /// <returns></returns>
        public bool WriteRobotMoveCommand(ushort iPointIndex)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = string.Format("@ MOVE P,P{0} \r", iPointIndex);
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                //string[] strArrayRecv = strRecvData.Split('\r');
                    if (strRecvData.Contains("RUN") || strRecvData.Contains("END"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot点位示教
        /// </summary>
        /// <param name="iPointIndex"></param>
        /// <param name="fPosX"></param>
        /// <param name="fPosY"></param>
        /// <param name="fPosZ"></param>
        /// <param name="fPosR"></param>
        /// <returns></returns>
        public bool WritePointPosCommand(ushort iPointIndex, double fPosX, double fPosY, double fPosZ, double fPosR)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = string.Format("@ P{0}={1} {2} {3} {4} {5} {6} {7} {8} {9} \r", iPointIndex.ToString(), fPosX.ToString("#0.000"), fPosY.ToString("#0.000"), fPosZ.ToString("#0.000"),
                                                fPosR.ToString("#0.000"), "0.000", "0.000", "2", "0", "0");
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot点动距离设定
        /// </summary>
        /// <param name="fStep">Jog step</param>
        /// <returns></returns>
        public bool WriteRobotJogStepCommand(float fStep)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ IDIST " + (fStep * 1000).ToString("#0") + "\r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                string[] strArrayRecv = strRecvData.Split('\r');
                if (strArrayRecv.Length >= 2 || strArrayRecv[1].Equals("END"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序复位命令
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotProgramRstCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ RESET \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序停止命令
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotProgramStopCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ STOP \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Robot程序启动命令
        /// </summary>
        /// <returns></returns>
        public bool WriteRobotProgramRunCommand()
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                string strCmd = "@ RUN \r";
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                if (strRecvData.Contains("OK"))
                {
                    //解析返回内容
                    //TODO

                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        /// <summary>
        /// Rotbot通讯调试接口
        /// </summary>
        /// <param name="strMsg">要发送到Rotbot的内容</param>
        /// <param name="strRet">Rotbot返回</param>
        /// <returns>timeout</returns>
        public bool WrteDebugMsg(string strMsg, ref string strRet)
        {
            byte[] byteArrayRecv = new byte[128];
            try
            {
                if (!strMsg.Contains("\r"))
                    strMsg = strMsg + " \r";
                string strCmd = strMsg;
                byte[] byteArrayCmd = Encoding.Default.GetBytes(strCmd);
                if (etherNetBase.SendData(byteArrayCmd, byteArrayCmd.Length) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }
                if (etherNetBase.ReceiveData(ref byteArrayRecv) < 0)
                {
                    _bConnectSta = false;
                    return false;
                }

                string strRecvData = Encoding.Default.GetString(byteArrayRecv);
                strRet = strRecvData;
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion



    }
}
