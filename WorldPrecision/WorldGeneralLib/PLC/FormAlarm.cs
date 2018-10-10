using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WorldGeneralLib.PLC
{

    public partial class FormAlarm : Form
    {
        public delegate void AlarmStatusChanged(object sender, bool bAlarm, bool bSilence);
        // public event AlarmStatusChanged alarmChangedEvent;
        private bool bMouseDown = false;
        private int iMouseDownX = 0;
        private int iMouseDownY = 0;
        private bool bEstop = false;
        private bool bDoorOpen = false;
        private bool[] bMotorAlarm;
        private bool[] bMotorCWLim;
        private bool[] bMotorCCWLim;
        private object objLock;
        List<AlarmItemData> listAlarmItem;
        string strPlcDriver = "Line_G38";
        string strPLC1 = "1号PLC";
        string strAddr1 = "01#R0555";
        string strAddr3 = "03#R0555";
        string strAddr4 = "04#R0555";
        string strAddr5 = "05#R0555";
        //string strAddr6 = "01#R0556";
        //string strAddr7 = "03#R0556";
        //string strAddr8 = "04#R0556";
        //string strAddr9 = "05#R0556";
        public FormAlarm()
        {
            listAlarmItem = new List<AlarmItemData>();
            bMotorAlarm = new bool[50];
            bMotorCWLim = new bool[50];
            bMotorCCWLim = new bool[50];
            for (int i = 0; i < 50; i++)
            {
                bMotorAlarm[i] = false;
                bMotorCWLim[i] = false;
                bMotorCCWLim[i] = false;
            }
            objLock = new object();
            InitializeComponent();

        }
        public FormAlarm(Control control)
        {
            listAlarmItem = new List<AlarmItemData>();
            bMotorAlarm = new bool[50];
            bMotorCWLim = new bool[50];
            bMotorCCWLim = new bool[50];
            for (int i = 0; i < 50; i++)
            {
                bMotorAlarm[i] = false;
                bMotorCWLim[i] = false;
                bMotorCCWLim[i] = false;
            }
            objLock = new object();
            InitializeComponent();

            TopLevel = false;
            control.Controls.Add(this);
            this.Left = control.Width / 2 - this.Width / 2;
            this.Top = control.Height / 2 - this.Height / 2;
            if (Left < 0 || Top < 0)
            {
                Left = 0;
                Top = 0;
            }
            this.Hide();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.Visible == false)
            {
                if (listViewAlarmCur.Items.Count > 0)
                {
                    //bool bSilence = checkBoxAlramOff.Checked;
                    //alarmChangedEvent(this, true, bSilence);
                }

            }
            else
            {
                if (listViewAlarmCur.Items.Count <= 0)
                {
                    //bool bSilence = checkBoxAlramOff.Checked;
                    //alarmChangedEvent(this, false, bSilence);
                }

            }
            if (listViewAlarmCur.Items.Count > 0)
            {
                if (this.Visible == false)
                {
                    this.Show();
                    this.BringToFront();
                }
                else
                {
                    this.BringToFront();
                }
            }
            else
            {
                if (this.Visible)
                {
                    this.Hide();
                    foreach (AlarmItemData item in listAlarmItem)
                    {
                        item.dateTimeEnd = DateTime.Now;
                    }
                    WriteAllAlarmItem();
                    listAlarmItem.Clear();
                }
            }

        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {
            listViewAlarmCur.Columns.Add("Machine");
            listViewAlarmCur.Columns.Add("Message");
            listViewAlarmCur.Columns.Add("Time");
            listViewAlarmCur.Columns[0].Width = 200;
            listViewAlarmCur.Columns[1].Width = 350;
            listViewAlarmHis.Columns.Add("Machine");
            listViewAlarmHis.Columns.Add("Message");
            listViewAlarmHis.Columns.Add("Time");
            listViewAlarmHis.Columns[0].Width = 200;
            listViewAlarmHis.Columns[1].Width = 350;

        }
        public void InsertAlarmMessage(string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, strMessage, 1);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, strMessage, 1);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                    AddAlarmToLog(strMessage);
                };
                this.Invoke(action);
            }
        }
        public void InsertAlarmMessageRobot(string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, strMessage, 1);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, strMessage, 1);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                    AddAlarmToLog(strMessage);
                };
                this.Invoke(action);
            }
        }
        public void InsertAlarmPLC(string MessageName, string strMessage)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    if (listViewAlarmCur.Items.ContainsKey(MessageName) == false)
                    {
                        ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MessageName, MessageName, 0);
                        listViewItem.SubItems.Add(strMessage);
                        listViewItem.SubItems.Add(DateTime.Now.ToString());
                        AddAlarmToLog(strMessage);
                    }

                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MessageName, MessageName, 0);
                    listViewItemHis.SubItems.Add(strMessage);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                };
                this.Invoke(action);
            }

        }
        public void RemoveAlarmPLC(string MessageName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey(MessageName);
                };
                this.Invoke(action);
            }
        }
        private void FormAlarm_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDown = true;

            iMouseDownX = e.X;
            iMouseDownY = e.Y;
        }

        private void FormAlarm_MouseUp(object sender, MouseEventArgs e)
        {

            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;

            }
            bMouseDown = false;
        }

        private void FormAlarm_MouseMove(object sender, MouseEventArgs e)
        {
            int iXoffset = 0;
            int iYoffset = 0;
            if (bMouseDown)
            {

                iXoffset = e.X - iMouseDownX;
                iYoffset = e.Y - iMouseDownY;
                this.Top = this.Top + iYoffset;
                this.Left = this.Left + iXoffset;

            }

        }
        private bool GetAlarm()
        {
            if (bEstop || bDoorOpen)
                return true;
            for (int i = 0; i < 50; i++)
            {
                if (bMotorAlarm[i])
                {
                    return true;
                }
                if (bMotorCWLim[i])
                {
                    return true;
                }
                if (bMotorCCWLim[i])
                {
                    return true;
                }
            }
            return false;
        }
        public void SetEstopAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, "ESTOP", "E-Stop", 0);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    RemoveHisItem();
                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, "ESTOP", "E-Stop", 0);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                    AddAlarmToLog("E-Stop");
                };
                this.Invoke(action);
            }
        }
        public void RstEstopAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey("ESTOP");
                };
                this.Invoke(action);
                //AddAlarmToLog("E-Stop");
            }
        }

        public void SetDoorOpenAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, "DoorOpen", "DoorOpen", 0);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    RemoveHisItem();
                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, "DoorOpen", "DoorOpen", 0);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());
                    AddAlarmToLog("DoorOpen");
                };
                this.Invoke(action);
            }
        }
        public void RstDoorOpenAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey("DoorOpen");
                };
                this.Invoke(action);
            }
        }
        public void SetMotorAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    if (listViewAlarmCur.Items.ContainsKey(MotorName + "马达报警") == false)
                    {
                        ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "Alarm", MotorName + "马达报警", 0);
                        listViewItem.SubItems.Add(DateTime.Now.ToString());
                        AddAlarmToLog(MotorName + "马达报警");
                    }


                    RemoveHisItem();
                    if (listViewAlarmHis.Items.ContainsKey(MotorName + "马达报警") == false)
                    {
                        ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "Alarm", MotorName + "马达报警", 1);
                        listViewItemHis.SubItems.Add(DateTime.Now.ToString());
                    }
                };
                this.Invoke(action);
            }
        }
        public void RstMotorAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey(MotorName + "Alarm");
                };
                this.Invoke(action);
            }
        }
        public void SetMotorCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "CWL", MotorName + "正极限报警", 0);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    RemoveHisItem();
                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "CWL", MotorName + "正极限报警", 0);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                    AddAlarmToLog(MotorName + "正极限报警");
                };
                this.Invoke(action);
            }
        }
        public void RstMotorCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey(MotorName + "CWL");
                };
                this.Invoke(action);
            }
        }
        public void SetMotorCCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    ListViewItem listViewItem = listViewAlarmCur.Items.Insert(0, MotorName + "CCWL", MotorName + "负极限报警", 0);
                    listViewItem.SubItems.Add(DateTime.Now.ToString());

                    RemoveHisItem();
                    ListViewItem listViewItemHis = listViewAlarmHis.Items.Insert(0, MotorName + "CCWL", MotorName + "负极限报警", 0);
                    listViewItemHis.SubItems.Add(DateTime.Now.ToString());

                    AddAlarmToLog(MotorName + "负极限报警");
                };
                this.Invoke(action);
            }
        }
        public void RstMotorCCWLAlarm(string MotorName)
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    listViewAlarmCur.Items.RemoveByKey(MotorName + "CCWL");
                };
                this.Invoke(action);
            }
        }
        public void RstOtherAlarm()
        {
            lock (objLock)
            {
                Action action = () =>
                {
                    for (int i = listViewAlarmCur.Items.Count - 1; i > -1; i--)
                    {
                        if (listViewAlarmCur.Items[i].ImageIndex != 0)
                        {
                            listViewAlarmCur.Items.RemoveAt(i);
                        }
                    }
                };
                this.Invoke(action);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            lock (objLock)
            {
                RstOtherAlarm();
            }
        }
        public void RemoveHisItem()
        {

            if (listViewAlarmHis.Items.Count > 200)
            {
                for (int i = listViewAlarmHis.Items.Count - 1; i > 100; i--)
                {
                    listViewAlarmHis.Items.RemoveAt(i);
                }
            }

        }
        private void AddAlarmToLog(string strMessage)
        {
            TextLogWrite.AppendLog(strMessage);
            AlarmItemData alarmItem = new AlarmItemData();
            alarmItem.strAlarmMessage = strMessage;
            alarmItem.dateTimeStart = DateTime.Now;
            listAlarmItem.Add(alarmItem);
        }
        private void WriteAllAlarmItem()
        {
            if (!Directory.Exists(@".//AlarmLog/"))
            {
                Directory.CreateDirectory(@".//AlarmLog/");
            }
            string strFileName = Application.StartupPath + "//AlarmLog//" + DateTime.Now.ToString("yyyy-MM-dd") + ".CSV";
            string strWriteConsent = "";
            foreach (AlarmItemData item in listAlarmItem)
            {
                strWriteConsent = strWriteConsent + item.dateTimeStart.ToString("HH:mm:ss") + "," + item.strAlarmMessage + "," + item.dateTimeEnd.ToString("HH:mm:ss") + "\r\n";
            }
            //File.AppendAllText(strFileName,strWriteConsent);
            StreamWriter fileWriter = new StreamWriter(strFileName, true, Encoding.Default);
            fileWriter.Write(strWriteConsent);
            fileWriter.Flush();
            fileWriter.Close();
        }

        private void checkBoxAlramOff_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxAlramOff.Checked)
                {
                    PLC.SetBit(strPlcDriver, strAddr3, true);
                    PLC.SetBit(strPlcDriver, strAddr4, true);
                    PLC.SetBit(strPlcDriver, strAddr5, true);
                    PLC.SetBit(strPLC1, strAddr1, true);
                }
                else
                {
                    PLC.SetBit(strPlcDriver, strAddr3, false);
                    PLC.SetBit(strPlcDriver, strAddr4, false);
                    PLC.SetBit(strPlcDriver, strAddr5, false);
                    PLC.SetBit(strPLC1, strAddr1, false);
                }
            }
            catch
            {

            }
        }
    }
}
