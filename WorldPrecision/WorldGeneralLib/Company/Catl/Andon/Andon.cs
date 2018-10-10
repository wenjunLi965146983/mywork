using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using WorldGeneralLib;
using WorldGeneralLib.Alarm;

namespace WorldGeneralLib.Company.Catl.Andon
{
    public class Andon
    {
        public bool bUplodaMacAlarm = true;
        public bool bAndonConnectSta = false;

        //设备资源号，使用Andon需要配置以下项
        public string strResourceID = "AMTP1002";

        //Andon系统是否启用
        public bool bAndonSwitch = true;

        private bool _bAlarmChanged = false;

        //机器的操作权限（来自andon控制文件）
        public bool bMacOpAuthority = true; // True - 允许启动设备  false - 禁止启动设备

        //Path
        private string strMacStaFilePath = "D:\\AndonData\\AndonStatus\\";
        private string strMacAlarmFilePath = "D:\\AndonData\\AndonAlarm\\";
        private string strProInfoFilePath = "D:\\AndonData\\AndonOutput\\";
        private string strLedStaFilePath = "D:\\AndonData\\AndonLight\\";
        private string strMacHeartFilePath = "D:\\AndonData\\AndonHeart\\";
        private string strMacHeartFileBackupPath = "D:\\AndonData\\AndonHeart\\Backup\\";
        private string strMacCtrlFilePath = "D:\\AndonData\\AndonControl\\";
        private string strMacCtrlFileBackupPath = "D:\\AndonData\\AndonControl\\Backup\\";
        private string strTempFilePath = "D:\\temp\\";

        //Mac sta
        private string[] strMacSta = { "45", "100","45" };
        private string[] strLedSta = { "Y", "G", "R"};
        private MacSta preMacSta = MacSta.Alarm;

        //产量信息
        public int iProductInputCount = 0;
        public int iProductOutputCount = 0;
        public int iProductOkCount = 0;
        public int iProductNgCount = 0;

        public Andon()
        {
            MainModule.alarmManage.eventAlarmInsert += new AlarmManage.EventAlarmInsertHandler(EventAlarmInsertHandler);
            DeleteAllHeartFiles();
        }

        public bool StartAndonThread()
        {
            Thread threadUploadMacSta = new Thread(ThreadUploadMacSta);
            threadUploadMacSta.IsBackground = true;
            threadUploadMacSta.Start();

            Thread threadUploadMacAlarm = new Thread(ThreadUploadMacAlarm);
            threadUploadMacAlarm.IsBackground = true;
            threadUploadMacAlarm.Start();

            Thread threadUploadProInfo = new Thread(ThreadUploadMacProInfo);
            threadUploadProInfo.IsBackground = true;
            threadUploadProInfo.Start();

            Thread threadUploadMacHeart = new Thread(ThreadUploadMacHeart);
            threadUploadMacHeart.IsBackground = true;
            threadUploadMacHeart.Start();

            Thread threadMacCtrl = new Thread(ThreadMacCtrl);
            threadMacCtrl.IsBackground = true;
            threadMacCtrl.Start();

            //Thread threadGather = new Thread(ThreadGatherMacInfo);
            //threadGather.IsBackground = true;
            //threadGather.Start();

            return true;
        }

        #region 1 - Machine status
        private void WriteMacStaToFile()
        {
            #region 写出机器停止原因
            if (MainModule.alarmManage.iStopReason >= 0)
            {
                try
                {
                    //首先生成到Temp目录
                    if (!Directory.Exists(strTempFilePath))
                    {
                        Directory.CreateDirectory(strTempFilePath);
                    }

                    //文件标头
                    string strStaFileHead = "ResourceID,Time,DeviceStatus_Reason\r\n";
                    string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                    string strData = strResourceID + "," +
                                                strTime + "," +
                                                MainModule.alarmManage.iStopReason.ToString() + "\r\n";
                    strData = strStaFileHead + strData;

                    string strFileName = "ResourceStatus_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";

                    StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                    sw.Write(strData);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                    if (!Directory.Exists(strMacStaFilePath))
                    {
                        Directory.CreateDirectory(strMacStaFilePath);
                    }

                    if (File.Exists(strMacStaFilePath + strFileName))
                    {
                        File.Delete(strMacStaFilePath + strFileName);
                    }
                    File.Move(strTempFilePath + strFileName, strMacStaFilePath + strFileName);
                }
                catch //(System.Exception ex)
                {
                   // throw ex;
                }
                MainModule.alarmManage.iStopReason = -1;
            }
            #endregion

            //机器状态发生改变时，写出csv文件
            if (preMacSta == MainModule.formMain.macSta)
            {
                return;
            }
            preMacSta = MainModule.formMain.macSta;

            #region 写出机器状态文件
            try
            {
                //首先生成到Temp目录
                if (!Directory.Exists(strTempFilePath))
                {
                    Directory.CreateDirectory(strTempFilePath);
                }

                //文件标头
                string strStaFileHead = "ResourceID,Time,DeviceStatus_Reason\r\n";
                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                string strData = strResourceID + "," +
                                            strTime + "," +
                                            strMacSta[(int)MainModule.formMain.macSta] + "\r\n";
                strData = strStaFileHead + strData;

                string strFileName = "ResourceStatus_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";

                StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                sw.Write(strData);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                if (!Directory.Exists(strMacStaFilePath))
                {
                    Directory.CreateDirectory(strMacStaFilePath);
                }

                if (File.Exists(strMacStaFilePath + strFileName))
                {
                    File.Delete(strMacStaFilePath + strFileName);
                }
                File.Move(strTempFilePath + strFileName, strMacStaFilePath + strFileName);
            }
            catch// (System.Exception ex)
            {
              //  throw ex;
            }
            #endregion
            #region 写出三色灯状态
            try
            {
                //文件标头
                string strLightStaFileHead = "ResourceID,Time,Light\r\n";
                string strLed = strLedSta[(int)MainModule.formMain.macSta];
                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                string strData = strResourceID + "," +
                                            strTime + "," +
                                            strLedSta[(int)MainModule.formMain.macSta] + "\r\n";
                strData = strLightStaFileHead + strData;

                string strFileName = "ResourceLight_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";

                StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                sw.Write(strData);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                if (!Directory.Exists(strLedStaFilePath))
                {
                    Directory.CreateDirectory(strLedStaFilePath);
                }

                if (File.Exists(strLedStaFilePath + strFileName))
                {
                    File.Delete(strLedStaFilePath + strFileName);
                }
                File.Move(strTempFilePath + strFileName, strLedStaFilePath + strFileName);
            }
            catch// (System.Exception ex)
            {
               // throw ex;
            }
            #endregion
            #region 写出产量信息文件
            try
            {
                //文件标头
                string strProInfoFileHead = "ResourceID,Time,QTY_INPUT,QTY_TOTAL,QTY_YIELD,QTY_SCRAP\r\n";
                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                string strData = strResourceID + "," +
                                            strTime + "," +
                                            iProductInputCount.ToString() + "," +
                                            iProductOutputCount.ToString() + "," +
                                            iProductOkCount.ToString() + "," +
                                            iProductNgCount.ToString() + "\r\n";

                string strFileName = "ResourceOutput" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";
                strData = strProInfoFileHead + strData;

                StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                sw.Write(strData);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                if (!Directory.Exists(strProInfoFilePath))
                {
                    Directory.CreateDirectory(strProInfoFilePath);
                }

                if (File.Exists(strProInfoFilePath + strFileName))
                {
                    File.Delete(strProInfoFilePath + strFileName);
                }
                File.Move(strTempFilePath + strFileName, strProInfoFilePath + strFileName);
            }
            catch (System.Exception)
            {
            }
            #endregion
        }
        public void ThreadUploadMacSta()
        {
            while (!MainModule.formMain.bExit)
            {
                if (bAndonSwitch)
                {
                    WriteMacStaToFile();
                }

                Thread.Sleep(100);
            }
        }
        #endregion

        #region 2 - Machine alarm
        private void WriteMacAlarmToFile()
        {
            string strData = "";
            try
            {
                if (!Directory.Exists(strTempFilePath))
                {
                    return;
                }
                if (!Directory.Exists(strMacAlarmFilePath))
                {
                    Directory.CreateDirectory(strMacAlarmFilePath);
                }

                if (!_bAlarmChanged)
                {
                    return;
                }

                Thread.Sleep(2000);
                if (!_bAlarmChanged || !MainModule.alarmManage.IsAlarm)
                {
                    return;
                }

                _bAlarmChanged = false;
                string strAlarmFileHead = "ResourceID,Time,DeviceStatus_Reason,AlarmAddress1,AlarmAddress2,AlarmAddress3,AlarmAddress4,AlarmAddress5,AlarmAddress6,AlarmAddress7,AlarmAddress8,AlarmAddress9,AlarmAddress10,AlarmAddress11,AlarmAddress12,AlarmAddress13,AlarmAddress14,AlarmAddress15,AlarmAddress16,AlarmAddress17,AlarmAddress18,AlarmAddress19,AlarmAddress20\r\n";
                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                string strFileName = "ResourceAlarm" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";

                #region Alarm data
                int iAlarmCount = 0;
                strData = strResourceID + "," + strTime + "," + "45";

                foreach (KeyValuePair<string, AlarmData> item in MainModule.alarmManage.DicCurrAlarmMsg)
                {
                    if (iAlarmCount >= 20)
                    {
                        break;
                    }
                    iAlarmCount++;
                    strData = strData + "," + item.Key;
                }
                for (int i = iAlarmCount; i < 20; i++)
                {
                    strData += ",";
                }

                strData += "\r\n";
                #endregion

                strData = strAlarmFileHead + strData;

                //写出到csv文件
                StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                sw.Write(strData);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                //剪切到指定目录下
                if (File.Exists(strMacAlarmFilePath + strFileName))
                {
                    File.Delete(strMacAlarmFilePath + strFileName);
                }
                File.Move(strTempFilePath + strFileName, strMacAlarmFilePath + strFileName);

            }
            catch //(Exception)
            {
                //throw;
            }
        }

        public void ThreadUploadMacAlarm()
        {
            while (!MainModule.formMain.bExit)
            {
                if (bAndonSwitch)
                {
                    WriteMacAlarmToFile();
                }

                Thread.Sleep(100);
            }
        }
#endregion

        #region 3 - Product info
        private void WriteProInfoToFile()
        {
            #region 写出产量信息文件
            try
            {
                if (!Directory.Exists(strTempFilePath))
                {
                    return;
                }

                //文件标头
                string strProInfoFileHead = "ResourceID,Time,QTY_INPUT,QTY_TOTAL,QTY_YIELD,QTY_SCRAP\r\n";
                string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Replace('/', '-');
                string strData = strResourceID + "," +
                                            strTime + "," +
                                            iProductInputCount.ToString() + "," +
                                            iProductOutputCount.ToString() + "," +
                                            iProductOkCount.ToString() + "," +
                                            iProductNgCount.ToString() + "\r\n";

                string strFileName = "ResourceOutput" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";
                strData = strProInfoFileHead + strData;

                StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, Encoding.UTF8);
                sw.Write(strData);
                sw.Flush();
                sw.Close();
                sw.Dispose();

                if (!Directory.Exists(strProInfoFilePath))
                {
                    Directory.CreateDirectory(strProInfoFilePath);
                }

                if (File.Exists(strProInfoFilePath + strFileName))
                {
                    File.Delete(strProInfoFilePath + strFileName);
                }
                File.Move(strTempFilePath + strFileName, strProInfoFilePath + strFileName);
            }
            catch// (System.Exception ex)
            {
            }
#endregion
        }
        public void ThreadUploadMacProInfo()
        {
            while (!MainModule.formMain.bExit)
            {
                if (bAndonSwitch)
                {
                    WriteProInfoToFile();
                }


                Thread.Sleep(30 * 60 * 1000);   //半小时更新一次
            }
        }
#endregion

        #region 4 - Machine heart
        private void DeleteAllHeartFiles()
        {
            try
            {
                DirectoryInfo dirInfo1 = null;
                DirectoryInfo dirInfo2 = null;

                if (!Directory.Exists(strMacHeartFilePath) || !Directory.Exists(strMacHeartFileBackupPath))
                {
                    return;
                }

                if (null == dirInfo1 || null == dirInfo2)
                {
                    dirInfo1 = new DirectoryInfo(strMacHeartFilePath);
                    dirInfo2 = new DirectoryInfo(strMacHeartFileBackupPath);
                }

                //删除目录下文件
                if (dirInfo1.GetFiles().Length > 0)
                {
                    for (int i = 0; i < dirInfo1.GetFiles().Length; i++)
                    {
                        File.Delete(strMacHeartFilePath + Path.GetFileName(dirInfo1.GetFiles()[i].FullName));
                    }
                }

                if (dirInfo2.GetFiles().Length > 0)
                {
                    for (int i = 0; i < dirInfo2.GetFiles().Length; i++)
                    {
                        File.Delete(strMacHeartFileBackupPath + Path.GetFileName(dirInfo2.GetFiles()[i].FullName));
                    }
                }
            }
            catch //(System.Exception ex)
            {
                //throw ex;
            }
        }
        public void ThreadUploadMacHeart()
        {
            int times = 120;
            DirectoryInfo dirInfo = null;

            while (!MainModule.formMain.bExit)
            {
                try
                {
                    if (!Directory.Exists(strMacHeartFilePath))
                    {
                        Directory.CreateDirectory(strMacHeartFilePath);
                    }

                    if (null == dirInfo)
                    {
                        dirInfo = new DirectoryInfo(strMacHeartFilePath);
                    }

                    //目录下无文件
                    if (dirInfo.GetFiles().Length <= 0)
                    {
                        bAndonConnectSta = false;
                    }
                    else
                    {
                        bAndonConnectSta = true;
                        Thread.Sleep(300);

                        //剪切到Backup目录下
                        if (!Directory.Exists(strMacHeartFileBackupPath))
                        {
                            Directory.CreateDirectory(strMacHeartFileBackupPath);
                        }
                        for (int i = 0; i < dirInfo.GetFiles().Length; i++)
                        {
                            if (File.Exists(strMacHeartFileBackupPath + Path.GetFileName(dirInfo.GetFiles()[i].FullName)))
                            {
                                File.Delete(strMacHeartFileBackupPath + Path.GetFileName(dirInfo.GetFiles()[i].FullName));
                            }

                            File.Move(dirInfo.GetFiles()[i].FullName, strMacHeartFileBackupPath + Path.GetFileName(dirInfo.GetFiles()[i].FullName));
                        }
                    }
                }
                catch// (System.Exception ex)
                {
                   // throw ex;
                }

                do
                {
                    Thread.Sleep(1000);
                    times--;

                    if (times < 1)
                    {
                        break;
                    }
                    if (null == dirInfo)
                    {
                        continue;
                    }
                    if (dirInfo.GetFiles().Length > 0)
                    {
                        break;
                    }
                } while (times > 0);
                times = 120;
            }
        }
#endregion

        #region 5 - Machine control
        private void ControlFileHandler(string strFullName)
        {
            try
            {
                if (!File.Exists(strFullName))
                {
                    return;
                }

                string strCmd = "";
                StreamReader sr = new StreamReader(strFullName, true);

                strCmd = sr.ReadLine();
                if (!string.IsNullOrEmpty(strCmd))
                {
                    if (strCmd == "1" && bAndonSwitch == true)
                    {
                        //禁止启动设备
                        bMacOpAuthority = false;
                    }
                    else
                    {
                        //允许启动设备
                        bMacOpAuthority = true;
                    }
                }

                sr.Close();

                if (File.Exists(strMacCtrlFileBackupPath + Path.GetFileName(strFullName)))
                {
                    File.Delete(strMacCtrlFileBackupPath + Path.GetFileName(strFullName));
                }
                File.Move(strFullName, strMacCtrlFileBackupPath + Path.GetFileName(strFullName));
            }
            catch// (System.Exception ex)
            {
               // throw ex;
            }

        }

        public void ThreadMacCtrl()
        {
            DirectoryInfo dirInfo = null;

            while (!MainModule.formMain.bExit)
            {
                try
                {
                    if (bMacOpAuthority)
                    {
                        //Set bit
                        //禁止机器启动            
                    }
                    else
                    {
                        //Set bit
                        //允许机器启动
                    }

                    if (!Directory.Exists(strMacCtrlFilePath))
                    {
                        Directory.CreateDirectory(strMacCtrlFilePath);
                    }
                    if (!Directory.Exists(strMacCtrlFileBackupPath))
                    {
                        Directory.CreateDirectory(strMacCtrlFileBackupPath);
                    }
                    if (null == dirInfo)
                    {
                        dirInfo = new DirectoryInfo(strMacCtrlFilePath);
                    }

                    if (bAndonSwitch == false)
                    {
                        //允许启动设备
                        bMacOpAuthority = true;
                    }

                    if (dirInfo.GetFiles().Length <= 0)
                    {
                        Thread.Sleep(500);
                        continue;
                    }

                    ControlFileHandler(dirInfo.GetFiles()[0].FullName);
                    if (bAndonSwitch == false)
                    {
                        //允许启动设备
                        bMacOpAuthority = true;
                    }
                }
                catch //(System.Exception ex)
                {
                   // throw ex;
                }

                Thread.Sleep(200);
            }
        }
#endregion

        #region Gather mac info
        private void SetBit(int bit, ref int value)
        {
            if (bit < 0 || bit > 15)
            {
                return;
            }

            value |= (1 << bit);
        }

        private bool ReadBit(int bit, int value)
        {
            value &= (1 << bit);

            return value > 0 ? true : false;
        }
        #endregion

        public void EventAlarmInsertHandler(string strKey)
        {
            _bAlarmChanged = true;
        }
    }
}
