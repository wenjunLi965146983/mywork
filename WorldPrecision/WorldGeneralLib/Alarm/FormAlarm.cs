using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Alarm
{
    public partial class FormAlarm : Form
    {
        private int _iTimes = -1;
        private bool _bShowFlag = false;
        public FormAlarm()
        {
            InitializeComponent();
            timerRefresh.Start();
        }

        public void ShowAlarmMsg()
        {
            try
            {
                if(this.InvokeRequired)
                {
                    Action action = () =>
                     {
                         lvCurrAlarm.Items.Clear();
                         foreach (KeyValuePair<string, AlarmData> item in MainModule.alarmManage.DicCurrAlarmMsg)
                         {
                             ListViewItem listViewItem = lvCurrAlarm.Items.Insert(0, item.Key, item.Value.AlarmTime.ToString(), 0);
                             listViewItem.SubItems.Add(item.Value.AlarmMsg);
                         }

                         if (MainModule.alarmManage.IsAlarm && this.Visible == false)
                         {
                             _iTimes = 0;
                             //this.Show();
                             _bShowFlag = true;
                         }
                         else if (!MainModule.alarmManage.IsAlarm)
                         {
                             this.Hide();
                             _iTimes = -1;
                         }
                     };
                    this.Invoke(action);
                }
                else
                {
                    lvCurrAlarm.Items.Clear();
                    foreach (KeyValuePair<string, AlarmData> item in MainModule.alarmManage.DicCurrAlarmMsg)
                    {
                        ListViewItem listViewItem = lvCurrAlarm.Items.Insert(0, item.Key, item.Value.AlarmTime.ToString(), 0);
                        listViewItem.SubItems.Add(item.Value.AlarmMsg);
                    }

                    if (MainModule.alarmManage.IsAlarm && this.Visible == false)
                    {
                        _iTimes = 0;
                        _bShowFlag = true;
                        //this.Show();
                    }
                    else if (!MainModule.alarmManage.IsAlarm)
                    {
                        this.Hide();
                        _iTimes = -1;
                    }
                }

            }
            catch (Exception)
            {

                // throw;
            }
        }
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                if(_bShowFlag)
                {
                    _bShowFlag = false;
                    if (MainModule.alarmManage.IsAlarm && this.Visible == false)
                    {
                        _iTimes = 0;
                        this.Show();
                    }
                    else if (!MainModule.alarmManage.IsAlarm)
                    {
                        _iTimes = -1;
                        this.Hide();
                    }
                }

                if (_iTimes >= 0)
                {
                    _iTimes++;
                    labTime.Text =  (_iTimes / (600 * 60)).ToString("00") + ":" + (_iTimes / 600).ToString("00") + ":" + ((_iTimes / 10) % 60).ToString("00");
                    panel1.BackColor = ((_iTimes / 10) % 60) % 2 == 0 ? Color.LightCoral : Color.White;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            MainModule.alarmManage.RemoveAllAlarm();
        }

        private void FormAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            return;
        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {

        }
    }
}
