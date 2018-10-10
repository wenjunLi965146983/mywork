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
    public partial class FormAlarmCatl : Form
    {
        private Int32 _iTimes = -1;
        private int _iStopReason = -1;
        private bool _bShowFlag = false;
        public bool bNeedStopReason = false;
        public FormAlarmCatl()
        {
            InitializeComponent();
            timerRefresh.Start();
        }

        #region 报警信息显示
        public void ShowAlarmMsg()
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Action action = () =>
                     {
                         lvCurrAlarm.Items.Clear();
                         foreach (KeyValuePair<string, AlarmData> item in MainModule.alarmManage.DicCurrAlarmMsg)
                         {
                             ListViewItem listViewItem = lvCurrAlarm.Items.Insert(0, item.Key, item.Value.AlarmTime.ToString(), 0);
                             listViewItem.SubItems.Add(item.Value.AlarmKey + " - " + item.Value.AlarmMsg);
                         }

                         if (MainModule.alarmManage.IsAlarm && this.Visible == false)
                         {
                             _iTimes = 0;
                             _iStopReason = -1;
                             this.Show();
                         }
                         else if (!MainModule.alarmManage.IsAlarm)
                         {
                             labTips.Text = "";
                             this.Hide();
                             _iTimes = -1;
                             MainModule.alarmManage.iStopReason = _iStopReason;
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
                        listViewItem.SubItems.Add(item.Value.AlarmKey + " - " + item.Value.AlarmMsg);
                    }

                    _bShowFlag = true;
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
                        _iStopReason = -1;
                        this.Show();
                    }
                    else if (!MainModule.alarmManage.IsAlarm)
                    {
                        MainModule.alarmManage.iStopReason = _iStopReason;
                        labTips.Text = "";
                        _iTimes = -1;
                        this.Hide();
                    }
                }
                if (_iTimes >= 0)
                {
                    _iTimes++;
                    labTime.Text = (_iTimes / (600 * 60)).ToString("00") + ":" + (_iTimes / 600).ToString("00") + ":" + ((_iTimes / 10) % 60).ToString("00");
                }
                if (_iTimes > 600 && _iStopReason < 0)
                {
                    btnClear.Visible = false;
                    labTips.Text = "请先选择停机原因";
                    bNeedStopReason = true;
                    btnClear.Visible = false;
                }
                else
                {
                    bNeedStopReason = false;
                    btnClear.Visible = true;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #region 停机原因选择
        private void button1_Click(object sender, EventArgs e)
        {
            _iStopReason = 20;
            labTips.Text = "无生产计划";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            _iStopReason = 54;
            labTips.Text = "其它原因";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _iStopReason = 44;
            labTips.Text = "培训考核";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _iStopReason = 41;
            labTips.Text = "盘点";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            _iStopReason = 12;
            labTips.Text = "(PM)维护保养";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            _iStopReason = 21;
            labTips.Text = "换型/换模";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            _iStopReason = 42;
            labTips.Text = "交接班";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            _iStopReason = 46;
            labTips.Text = "吃饭时间";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            _iStopReason = 11;
            labTips.Text = "设备故障";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            _iStopReason = 40;
            labTips.Text = "AM(清洁点检)";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _iStopReason = 10;
            labTips.Text = "小报警处理";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            _iStopReason = 43;
            labTips.Text = "品质监控";
        }

        private void button15_Click(object sender, EventArgs e)
        {
            _iStopReason = 32;
            labTips.Text = "物料更换";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            _iStopReason = 56;
            labTips.Text = "等待物料";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            _iStopReason = 22;
            labTips.Text = "样品制作";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            _iStopReason = 13;
            labTips.Text = "品质异常";
        }

        private void button19_Click(object sender, EventArgs e)
        {
            _iStopReason = 31;
            labTips.Text = "来料异常";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            _iStopReason = 55;
            labTips.Text = "IT系统异常";
        }

        private void button17_Click(object sender, EventArgs e)
        {
            _iStopReason = 52;
            labTips.Text = "FE异常";
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            MainModule.alarmManage.RemoveAllAlarm();
            //ShowAlarmMsg();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            MainModule.alarmManage.RemoveAllAlarm();
        }

        private void FormAlarmCatl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            return;
        }
    }

 }
