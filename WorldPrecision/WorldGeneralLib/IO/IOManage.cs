using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WorldGeneralLib.Forms.TipsForm;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Table;
using WorldGeneralLib.Alarm;

namespace WorldGeneralLib.IO
{
    public class IOManage
    {
        public static IODoc docIO;

        public static InputDrivers inputDrivers;
        public static OutputDrivers outputDrivers;
        public static FormIOSetting formIOSetting;

        public static bool bEStop = false;
        public static void LoadObj()
        {
            docIO = IODoc.LoadObj();
        }

        public static void InitDrivers()
        {
            inputDrivers = new InputDrivers();
            foreach (KeyValuePair<string, IOData> item in docIO.dicInput)
            {
                InputDriver driver = new InputDriver();
                inputDrivers.dicDrivers.Add(item.Value.Name, driver);
            }
            foreach (KeyValuePair<string, InputDriver> item in inputDrivers.dicDrivers)
            {
                item.Value.Init(docIO.dicInput[item.Key]);
            }

            outputDrivers = new OutputDrivers();
            foreach (KeyValuePair<string, IOData> item in docIO.dicOutput)
            {
                OutputDriver driver = new OutputDriver();
                outputDrivers.dicDrivers.Add(item.Value.Name, driver);
            }
            foreach (KeyValuePair<string, OutputDriver> item in outputDrivers.dicDrivers)
            {
                item.Value.Init(docIO.dicOutput[item.Key]);
            }

            Thread threadScan = new Thread(ThreadScanIO);
            threadScan.IsBackground = true;
            threadScan.Start();
        }

        public static InputDriver INPUT(string strName)
        {
            try
            {
                return inputDrivers.dicDrivers[strName];
            }
            catch
            {
                //不存在名为 strName 的输入
                InputDriver driver = new InputDriver();
                inputDrivers.dicDrivers.Add(strName, driver);
                return inputDrivers.dicDrivers[strName];
            }
        }
        public static OutputDriver OUTPUT(string strName)
        {
            try
            {
                return outputDrivers.dicDrivers[strName];
            }
            catch
            {
                OutputDriver driver = new OutputDriver();
                outputDrivers.dicDrivers.Add(strName, driver);
                return outputDrivers.dicDrivers[strName];

            }
        }

        #region Add/Remove/Rename IO
        public static string GetNewIOName(bool bInput)
        {
            string strTemp;
            int index = 0;

            if (bInput)
            {
                strTemp = "Input_";
                while (true)
                {
                    if (!IOManage.docIO.dicInput.ContainsKey(strTemp + index.ToString()))
                    {
                        return strTemp + index.ToString();
                    }
                    index++;
                    continue;
                }
            }
            else
            {
                strTemp = "Output_";

                while (true)
                {
                    if (!IOManage.docIO.dicOutput.ContainsKey(strTemp + index.ToString()))
                    {
                        return strTemp + index.ToString();
                    }
                    index++;
                    continue;
                }
            }
        }
        public static string AddIO(bool bInput)
        {
            if (docIO == null)
            {
                return null;
            }
            try
            {
                string strNewIOName = null;
                #region Input
                if (bInput)
                {
                    strNewIOName = GetNewIOName(true);
                    if (IOManage.docIO.dicInput.ContainsKey(strNewIOName))
                    {
                        MessageBox.Show("添加失败，已存在相同的项！", "添加", MessageBoxButtons.OK, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
                        return null;
                    }
                    IOData input = new IOData();

                    input.Name = strNewIOName;
                    input.CardName = HardwareType.InputCard.ToString();
                    input.Text = strNewIOName;
                    docIO.dicInput.Add(strNewIOName, input);
                    docIO.listInput.Add(input);
                    return strNewIOName;
                }
                #endregion
                #region Output
                strNewIOName = GetNewIOName(false);
                if (IOManage.docIO.dicOutput.ContainsKey(strNewIOName))
                {
                    MessageBox.Show("添加失败，已存在相同的项！", "添加", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return null;
                }
                IOData output = new IOData();

                output.Name = strNewIOName;
                output.CardName = HardwareType.OutputCard.ToString();
                output.Text = strNewIOName;
                docIO.dicOutput.Add(strNewIOName, output);
                docIO.listOutput.Add(output);
                return strNewIOName;
                #endregion
            }
            catch
            {
                return null;
            }
        }
        public static bool RemoveIO(bool bInput, string strName)
        {
            try
            {
                #region Input
                if (bInput)
                {
                    FormTips formTips = new FormTips(-1, true);
                    formTips.SetTipsText("确定要移除通用输入点" + strName + "吗？\r\n此操作不可撤销。");
                    if (DialogResult.Yes != formTips.ShowDialog())
                    {
                        return false;
                    }

                    docIO.dicInput.Remove(strName);
                    docIO.listInput.Clear();
                    foreach (KeyValuePair<string, IOData> item in docIO.dicInput)
                    {
                        docIO.listInput.Add(item.Value);
                    }
                    return true;
                }
                #endregion
                #region Output
                FormTips formTips2 = new FormTips(-1, true);
                formTips2.SetTipsText("确定要移除通用输出点" + strName + "吗？\r\n此操作不可撤销。");
                if (DialogResult.Yes != formTips2.ShowDialog())
                {
                    return false;
                }

                docIO.dicOutput.Remove(strName);
                docIO.listOutput.Clear();
                foreach (KeyValuePair<string, IOData> item in docIO.dicOutput)
                {
                    docIO.listOutput.Add(item.Value);
                }
                return true;
                #endregion
            }
            catch
            {
                return false;
            }
        }
        public static bool IORename(string strOldName, string strNewName, bool bInput)
        {
            try
            {
                if (string.IsNullOrEmpty(strOldName) || string.IsNullOrEmpty(strNewName) || docIO == null)
                {
                    return false;
                }
                if(bInput)
                {
                    #region Input
                    if (!docIO.dicInput.ContainsKey(strOldName) || docIO.dicInput.ContainsKey(strNewName))
                    {
                        return false;
                    }
                    foreach (IOData item in docIO.listInput)
                    {
                        if (item.Name.Equals(strOldName))
                        {
                            item.Name = strNewName;
                            docIO.dicInput.Remove(strOldName);
                            docIO.dicInput.Add(strNewName, item);

                            if(inputDrivers.dicDrivers.ContainsKey(strOldName))
                            {
                                InputDriver temp = inputDrivers.dicDrivers[strOldName];
                                inputDrivers.dicDrivers.Remove(strOldName);
                                temp.strDriverName = strNewName;
                                inputDrivers.dicDrivers.Add(strNewName, temp);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Output
                    if (!docIO.dicOutput.ContainsKey(strOldName) || docIO.dicOutput.ContainsKey(strNewName))
                    {
                        return false;
                    }
                    foreach (IOData item in docIO.listOutput)
                    {
                        if (item.Name.Equals(strOldName))
                        {
                            item.Name = strNewName;
                            docIO.dicOutput.Remove(strOldName);
                            docIO.dicOutput.Add(strNewName, item);

                            if(outputDrivers.dicDrivers.ContainsKey(strOldName))
                            {
                                OutputDriver temp = outputDrivers.dicDrivers[strOldName];
                                outputDrivers.dicDrivers.Remove(strOldName);
                                temp.strDriverName = strNewName;
                                outputDrivers.dicDrivers.Add(strNewName, temp);
                            }
                        }
                    }
                    #endregion
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region Scan Thread
        public static void ThreadScanIO()
        {
            bool bStop = true;
            Thread.Sleep(2000);

            while (!MainModule.formMain.bExit)
            {
                try
                {
                    #region ESTOP
                    if (inputDrivers.dicDrivers.ContainsKey("ESTOP"))
                    {
                        if (!inputDrivers.dicDrivers["ESTOP"].GetOn())
                        {
                            //急停触发
                            MainModule.formMain.macHomeSta = MacHomeSta.WaittingReset;
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.ESTOP, "机器急停");
                            MainModule.formMain.SetEStopStatus(true);
                            inputDrivers.dicDrivers["ESTOP"].bPreStatus = false;
                            bStop = true;
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["ESTOP"].bPreStatus == false)
                            {
                                MainModule.alarmManage.RemoveAlarm(AlarmKeys.ESTOP);
                                MainModule.formMain.SetEStopStatus(false);
                                inputDrivers.dicDrivers["ESTOP"].bPreStatus = true;
                            }
                        }
                    }
                    #endregion
                    #region DOOR
                    if (inputDrivers.dicDrivers.ContainsKey("DOOR"))
                    {
                        if (!inputDrivers.dicDrivers["DOOR"].GetOn())
                        {
                            //门禁触发
                            MainModule.formMain.macHomeSta = MacHomeSta.WaittingReset;
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.DOOR, "门禁触发");
                            MainModule.formMain.SetDoorStatus(true);
                            inputDrivers.dicDrivers["DOOR"].bPreStatus = false;

                            bStop = true;
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["DOOR"].bPreStatus == false)
                            {
                                MainModule.alarmManage.RemoveAlarm(AlarmKeys.DOOR);
                                MainModule.formMain.SetDoorStatus(false);
                                inputDrivers.dicDrivers["DOOR"].bPreStatus = true;
                            }
                        }
                    }
                    #endregion
                    #region DOOR1
                    if (inputDrivers.dicDrivers.ContainsKey("DOOR1"))
                    {
                        if (!inputDrivers.dicDrivers["DOOR1"].GetOn())
                        {
                            //门禁触发
                            MainModule.formMain.macHomeSta = MacHomeSta.WaittingReset;
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.DOOR1, "门禁1触发");
                            MainModule.formMain.SetDoorStatus(true);
                            inputDrivers.dicDrivers["DOOR1"].bPreStatus = false;
                            bStop = true;
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["DOOR1"].bPreStatus == false)
                            {
                                MainModule.alarmManage.RemoveAlarm(AlarmKeys.DOOR1);
                                MainModule.formMain.SetDoorStatus(false);
                                inputDrivers.dicDrivers["DOOR1"].bPreStatus = true;
                            }
                        }
                    }
                    #endregion
                    #region DOOR2
                    if (inputDrivers.dicDrivers.ContainsKey("DOOR2"))
                    {
                        if (!inputDrivers.dicDrivers["DOOR2"].GetOn())
                        {
                            //门禁触发
                            MainModule.formMain.macHomeSta = MacHomeSta.WaittingReset;
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.DOOR2, "门禁2触发");
                            MainModule.formMain.SetDoorStatus(true);
                            inputDrivers.dicDrivers["DOOR2"].bPreStatus = false;

                            bStop = true;
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["DOOR2"].bPreStatus == false)
                            {
                                MainModule.alarmManage.RemoveAlarm(AlarmKeys.DOOR2);
                                MainModule.formMain.SetDoorStatus(false);
                                inputDrivers.dicDrivers["DOOR2"].bPreStatus = true;
                            }
                        }
                    }
                    #endregion
                    #region PRESSURE
                    if (inputDrivers.dicDrivers.ContainsKey("PRESSURE"))
                    {
                        if (!inputDrivers.dicDrivers["PRESSURE"].GetOn())
                        {
                            //门禁触发
                            MainModule.formMain.macHomeSta = MacHomeSta.WaittingReset;
                            MainModule.alarmManage.InsertAlarm(AlarmKeys.PRESSURE, "气压异常");
                            MainModule.formMain.SetPressureStatus(true);
                            inputDrivers.dicDrivers["PRESSURE"].bPreStatus = false;
                            bStop = true;
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["PRESSURE"].bPreStatus == false)
                            {
                                MainModule.alarmManage.RemoveAlarm(AlarmKeys.PRESSURE);
                                MainModule.formMain.SetPressureStatus(false);
                                inputDrivers.dicDrivers["PRESSURE"].bPreStatus = true;
                            }
                        }
                    }
                    #endregion

                    #region STOP
                    if (inputDrivers.dicDrivers.ContainsKey("STOP"))
                    {
                        if (inputDrivers.dicDrivers["STOP"].GetOn())
                        {
                            if (!inputDrivers.dicDrivers["STOP"].bPreStatus)
                                MainModule.formMain.formOperator.ButtonStopPushed();
                        }
                        inputDrivers.dicDrivers["STOP"].bPreStatus = inputDrivers.dicDrivers["STOP"].GetOn();
                    }
                    #endregion
                    #region START
                    if (inputDrivers.dicDrivers.ContainsKey("START"))
                    {
                        if (inputDrivers.dicDrivers["START"].GetOn())
                        {
                            if (!inputDrivers.dicDrivers["START"].bPreStatus)
                                MainModule.formMain.formOperator.ButtonStartPushed();
                        }
                        inputDrivers.dicDrivers["START"].bPreStatus = inputDrivers.dicDrivers["START"].GetOn();
                    }
                    #endregion
                    #region RESET
                    if (inputDrivers.dicDrivers.ContainsKey("RESET"))
                    {
                        if (inputDrivers.dicDrivers["RESET"].GetOn())
                        {
                            if (!inputDrivers.dicDrivers["RESET"].bPreStatus)
                                MainModule.formMain.formOperator.ButtonResetPushed();
                        }
                        inputDrivers.dicDrivers["RESET"].bPreStatus = inputDrivers.dicDrivers["RESET"].GetOn();
                    }
                    #endregion
                    #region CLEAR
                    if (inputDrivers.dicDrivers.ContainsKey("CLEAR"))
                    {
                        if (inputDrivers.dicDrivers["CLEAR"].GetOn())
                        {
                            if (!inputDrivers.dicDrivers["CLEAR"].bPreStatus)
                                MainModule.formMain.formOperator.ButtonClearPressed();
                        }
                        else
                        {
                            if (inputDrivers.dicDrivers["CLEAR"].bready)
                                MainModule.formMain.formOperator.ButtonClearReleased();
                        }
                        inputDrivers.dicDrivers["CLEAR"].bPreStatus = inputDrivers.dicDrivers["CLEAR"].GetOn();
                    }
                    #endregion

                    #region Stop All move
                    if (bStop)
                    {
                        foreach (KeyValuePair<string, TableDriver> item in TableManage.tableDrivers.dicDrivers)
                        {
                            item.Value.SuspendMove();
                        }
                    }
                    bStop = false;
                    #endregion
                }
                catch
                {

                }

                System.Threading.Thread.Sleep(100);
            }
        }
        #endregion
    }
}
