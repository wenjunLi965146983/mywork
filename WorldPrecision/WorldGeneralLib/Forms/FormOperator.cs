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
using WorldGeneralLib.Login;
using WorldGeneralLib.IO;
using WorldGeneralLib.Forms.TipsForm;

namespace WorldGeneralLib.Forms
{

    public partial class FormOperator : Form
    {
        private static Color _backColor = Color.FromArgb(217, 217, 217);
        private Color _color = _backColor;
        private bool _bClrBtnDown = false;

        public delegate void EventStartButtonPushed(object sender, EventArgs e);
        public delegate void EventResetButtonPushed(object sender, EventArgs e);
        public delegate void EventStopButtonPushed(object sender, EventArgs e);
        public delegate void EventClearButtonMouseDown(object sender, EventArgs e);
        public delegate void EventClearButtonMouseUp(object sender, EventArgs e);

        public static event EventStartButtonPushed eventStartButtonPushed;
        public static event EventStopButtonPushed eventStopButtonPushed;
        public static event EventResetButtonPushed eventResetButtonPushed;
        public static event EventClearButtonMouseDown eventClearButtonMouseDown;
        public static event EventClearButtonMouseUp eventClearButtonMouseUp;

        public FormOperator()
        {
            InitializeComponent();
        }

        private void FormOperator_Load(object sender, EventArgs e)
        {
            timer1.Start();

            eventStartButtonPushed += new EventStartButtonPushed(MainModule.formMain.EventStartButtonPushed);
            eventStopButtonPushed += new EventStopButtonPushed(MainModule.formMain.EventStopButtonPushed);
            eventResetButtonPushed += new EventResetButtonPushed(MainModule.formMain.EventResetButtonPushed);
            eventClearButtonMouseDown += new EventClearButtonMouseDown(MainModule.formMain.EventClearButtonPressed);
            eventClearButtonMouseUp += new EventClearButtonMouseUp(MainModule.formMain.EventClearButtonReleased);

            Thread threadColorControl = new Thread(ThreadColorControl);
            threadColorControl.IsBackground = true;
            threadColorControl.Start();

            Thread threadOutputControl = new Thread(ThreadOutputControl);
            threadOutputControl.IsBackground = true;
            threadOutputControl.Start();
        }
        private void btnMacOp_Click(object sender, EventArgs e)
        {
            #region Check user level
            if (LoginManage.iCurrUserLevel < 0)
            {
                FormTips tipsFrm = new FormTips(3, false);
                tipsFrm.SetTipsText("抱歉，您没有权限进行此操作，请先登陆。");
                tipsFrm.ShowDialog();
                return;
            }
            #endregion

            try
            {
                if (MainModule.formMain.bRunFlag || MainModule.formMain.macHomeSta == MacHomeSta.Reseting)
                {
                    eventStopButtonPushed?.Invoke(sender, e);
                }
                else if (MainModule.formMain.macHomeSta == MacHomeSta.WaittingReset)
                {
                    eventResetButtonPushed?.Invoke(sender, e);
                }
                else if(!MainModule.formMain.bRunFlag && MainModule.formMain.macHomeSta == MacHomeSta.Reseted)
                {
                    eventStartButtonPushed?.Invoke(sender, e);
                }
            }
            catch (Exception)
            {
            }

        }

        public void ButtonStartPushed()
        {
            if(this.InvokeRequired)
            {
                Action action = () =>
                  {
                      eventStartButtonPushed?.Invoke(null, null);
                  };
                this.Invoke(action);
            }
            else
            {
                eventStartButtonPushed?.Invoke(null, null);
            }
            
        }
        public void ButtonStopPushed()
        {
            if (this.InvokeRequired)
            {
                Action action = () =>
                {
                    eventStopButtonPushed?.Invoke(null, null);
                };
                this.Invoke(action);
            }
            else
            {
                eventStopButtonPushed?.Invoke(null, null);
            }
            
        }
        public void ButtonResetPushed()
        {
            if (this.InvokeRequired)
            {
                Action action = () =>
                {
                    eventResetButtonPushed?.Invoke(null, null);
                };
                this.Invoke(action);
            }
            else
            {
                eventResetButtonPushed?.Invoke(null, null);
            }
            
        }
        public void ButtonClearPressed()
        {
            if (this.InvokeRequired)
            {
                Action action = () =>
                 {
                     eventClearButtonMouseDown?.Invoke(null, null);
                 };
                this.Invoke(action);
            }
            else
            {
                eventClearButtonMouseDown?.Invoke(null,null);
            }
        }
        public void ButtonClearReleased()
        {
            if (this.InvokeRequired)
            {
                Action action = () =>
                {
                    eventClearButtonMouseUp?.Invoke(null, null);
                };
                this.Invoke(action);
            }
            else
            {
                eventClearButtonMouseUp?.Invoke(null, null);
            }
        }
        private void machineResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            eventResetButtonPushed?.Invoke(sender, e);
        }
        private void toolStripMenuItemAlarmClear_MouseDown(object sender, MouseEventArgs e)
        {
            _bClrBtnDown = true;
            eventClearButtonMouseDown?.Invoke(sender, e);
        }

        private void toolStripMenuItemAlarmClear_MouseUp(object sender, MouseEventArgs e)
        {
            if (_bClrBtnDown)
            {
                _bClrBtnDown = false;
                eventClearButtonMouseUp?.Invoke(sender, e);
            }
        }

        private void toolStripMenuItemAlarmClear_MouseLeave(object sender, EventArgs e)
        {
            if(_bClrBtnDown)
            {
                _bClrBtnDown = false;
                eventClearButtonMouseUp?.Invoke(sender, e);
            }
        }

        #region Scan Thread
        private void ThreadColorControl()
        {
            while(!MainModule.formMain.bExit)
            {
                Thread.Sleep(50);
                try
                {
                    if(MainModule.alarmManage.IsAlarm)
                    {
                        _color = Color.LightCoral;
                    }
                    else if (MainModule.formMain.bRunFlag)
                    {
                        _color = Color.MediumSeaGreen;
                    }
                    else if(MainModule.formMain.macHomeSta == MacHomeSta.Reseting)
                    {
                        _color = Color.MediumSeaGreen;
                        Thread.Sleep(700);
                        _color = Color.Yellow;
                        Thread.Sleep(700);
                    }
                    else
                    {
                        if(MainModule.formMain.macHomeSta == MacHomeSta.WaittingReset)
                        {
                            _color = Color.Yellow;
                            Thread.Sleep(700);
                            _color = _backColor;
                            Thread.Sleep(700);
                        }
                        else if(MainModule.formMain.macHomeSta == MacHomeSta.Reseted)
                        {
                            _color = Color.MediumSeaGreen;
                            Thread.Sleep(700);
                            _color = _backColor;
                            Thread.Sleep(700);
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void ThreadOutputControl()
        {
            while (!MainModule.formMain.bExit)
            {
                Thread.Sleep(100);
                try
                {
                    if (MainModule.alarmManage.IsAlarm)
                    {
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampGreen")) IOManage.outputDrivers.dicDrivers["LampGreen"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampYellow")) IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampRed")) IOManage.outputDrivers.dicDrivers["LampRed"].SetOutBit(true);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("Buzzer")) IOManage.outputDrivers.dicDrivers["Buzzer"].SetOutBit(true);

                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("StartButtonLamp")) IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp")) IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(true);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ClearButtonLamp"))
                        {
                            IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(true);
                            Thread.Sleep(700);
                            IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(false);
                            Thread.Sleep(700);
                        }
                    }
                    else if (MainModule.formMain.bRunFlag)
                    {
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampGreen")) IOManage.outputDrivers.dicDrivers["LampGreen"].SetOutBit(true);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampYellow")) IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampRed")) IOManage.outputDrivers.dicDrivers["LampRed"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("Buzzer")) IOManage.outputDrivers.dicDrivers["Buzzer"].SetOutBit(false);

                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("StartButtonLamp")) IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(true); 
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ClearButtonLamp")) IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp"))
                        {
                            IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(true);
                            Thread.Sleep(700);
                            IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(false);
                            Thread.Sleep(700);
                        }
                    }
                    else if (MainModule.formMain.macHomeSta == MacHomeSta.Reseting)
                    {
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampGreen")) IOManage.outputDrivers.dicDrivers["LampGreen"].SetOutBit(true);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampRed")) IOManage.outputDrivers.dicDrivers["LampRed"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("Buzzer")) IOManage.outputDrivers.dicDrivers["Buzzer"].SetOutBit(false);

                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("StartButtonLamp")) IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(true);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("ClearButtonLamp")) IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(false);
                        if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampYellow"))
                        {
                            IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(true);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp")) IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(true);
                            Thread.Sleep(700);
                            IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp")) IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(false);
                            Thread.Sleep(700);
                        }
                    }
                    else
                    {
                        if (MainModule.formMain.macHomeSta == MacHomeSta.WaittingReset)
                        {
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampGreen")) IOManage.outputDrivers.dicDrivers["LampGreen"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampRed")) IOManage.outputDrivers.dicDrivers["LampRed"].SetOutBit(true);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("Buzzer")) IOManage.outputDrivers.dicDrivers["Buzzer"].SetOutBit(false);

                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StartButtonLamp")) IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp")) IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(true);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("ClearButtonLamp")) IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampYellow"))
                            {
                                IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(true);
                                if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(true);
                                Thread.Sleep(700);
                                IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(false);
                                if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(false);
                                Thread.Sleep(700);
                            }
                        }
                        else if (MainModule.formMain.macHomeSta == MacHomeSta.Reseted)
                        {
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampGreen")) IOManage.outputDrivers.dicDrivers["LampGreen"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampRed")) IOManage.outputDrivers.dicDrivers["LampRed"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("LampYellow")) IOManage.outputDrivers.dicDrivers["LampYellow"].SetOutBit(true);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("Buzzer")) IOManage.outputDrivers.dicDrivers["Buzzer"].SetOutBit(false);

                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("ClearButtonLamp")) IOManage.outputDrivers.dicDrivers["ClearButtonLamp"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StopButtonLamp")) IOManage.outputDrivers.dicDrivers["StopButtonLamp"].SetOutBit(true);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("ResetButtonLamp")) IOManage.outputDrivers.dicDrivers["ResetButtonLamp"].SetOutBit(false);
                            if (IOManage.outputDrivers.dicDrivers.ContainsKey("StartButtonLamp"))
                            {
                                IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(true);
                                Thread.Sleep(700);
                                IOManage.outputDrivers.dicDrivers["StartButtonLamp"].SetOutBit(false);
                                Thread.Sleep(700);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        #endregion

        #region Timer
        //private int _iTimes = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (MainModule.formMain.bRunFlag || MainModule.formMain.macHomeSta == MacHomeSta.Reseting)
                {
                    btnMacOp.Text = "STOP";
                    //btnMacOp.Image = imageList1.Images[1];
                    //image
                }
                else if (MainModule.formMain.macHomeSta == MacHomeSta.WaittingReset)
                {
                    btnMacOp.Text = "RESET";
                    //btnMacOp.Image = imageList1.Images[2];
                }
                else if (!MainModule.formMain.bRunFlag && MainModule.formMain.macHomeSta == MacHomeSta.Reseted)
                {
                    btnMacOp.Text = "RUN";
                   // btnMacOp.Image = imageList1.Images[0];
                }

                if (this.BackColor != _color)
                    this.BackColor = _color;
            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}
