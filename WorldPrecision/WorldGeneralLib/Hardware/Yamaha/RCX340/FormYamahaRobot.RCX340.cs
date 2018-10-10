using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Functions;

namespace WorldGeneralLib.Hardware.Yamaha.RCX340
{
    public partial class FormYamahaRobotRCX340 : Form
    {
        private YamahaRobotData _robotData;
        public FormYamahaRobotRCX340()
        {
            InitializeComponent();
        }
        public FormYamahaRobotRCX340(YamahaRobotData robotData) : this()
        {
            _robotData = robotData;
            this.panelMain.Text = _robotData.Name;
        }

        private void ViewInit()
        {
            try
            {
                if (null == _robotData)
                    return;
                tbIndex.Text = _robotData.Index.ToString();
                tbPort.Text = _robotData.Port.ToString();
                ipAddressControl1.Text = _robotData.IP;
                tbTimeout.Text = _robotData.ReadTimeout.ToString();
            }
            catch (Exception)
            {
            }
        }

        private void FormYamahaRobot_Load(object sender, EventArgs e)
        {
            ViewInit();
            timer1.Start();
        }

        private void tbIndex_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!JudgeNumber.isPositiveInteger(tbIndex.Text) && !tbIndex.Text.Equals("0"))
                {
                    MessageBox.Show("The card number should be Uint16", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    tbIndex.Text = _robotData.Index.ToString();
                    tbIndex.Focus();
                    return;
                }
                _robotData.Index = Convert.ToUInt16(tbIndex.Text.Trim());

            }
            catch (Exception)
            {
            }
        }

        private void tbPort_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!JudgeNumber.isPositiveInteger(tbPort.Text) && !tbPort.Text.Equals("0"))
                {
                    MessageBox.Show("The card number should be Uint16", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    tbPort.Text = _robotData.Port.ToString();
                    tbPort.Focus();
                    return;
                }
                _robotData.Port = Convert.ToUInt16(tbPort.Text.Trim());

            }
            catch (Exception)
            {
            }
        }

        private void ipAddressControl1_Validated(object sender, EventArgs e)
        {
            try
            {
                if (_robotData == null)
                    return;
                _robotData.IP = ipAddressControl1.Text;
            }
            catch (Exception)
            {
            }
            ipAddressControl1.Text = _robotData.IP;
        }

        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (HardwareManage.docHardware.SaveDoc())
                {
                    MessageBox.Show("Save successful.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Save failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        private void tbTimeout_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!JudgeNumber.isPositiveInteger(tbTimeout.Text))
                {
                    MessageBox.Show("The card number should be Uint16", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    tbTimeout.Text = _robotData.ReadTimeout.ToString();
                    tbTimeout.Focus();
                    return;
                }
                UInt16 uTemp = Convert.ToUInt16(tbTimeout.Text.Trim());
                if (uTemp < 100)
                    uTemp = 100;
                _robotData.ReadTimeout = uTemp;

            }
            catch (Exception)
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(HardwareManage.dicHardwareDriver.ContainsKey(_robotData.Name))
            {
                if(((YamahaRobotRCX340)HardwareManage.dicHardwareDriver[_robotData.Name]).IsConnected())
                {
                    labConnSta.Text = "Successfuly connect to Robot.";
                    btnConn.Text = "Disconnect";
                    labConnSta.ForeColor = Color.Green;
                }
                else
                {
                    labConnSta.Text = "Failed to connect to robot .";
                    btnConn.Text = "Connect";
                }
            }
        }

        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!HardwareManage.dicHardwareDriver.ContainsKey(_robotData.Name))
                    throw new Exception();
                YamahaRobotRCX340 yamahaRobot = (YamahaRobotRCX340)HardwareManage.dicHardwareDriver[_robotData.Name];
                if (yamahaRobot.IsConnected())
                {
                    yamahaRobot.yamahaRCX340API.DisconnectToYamahaRobot();
                }
                else
                {
                    yamahaRobot.yamahaRCX340API.ConnectToYamahaRobot(_robotData.IP, (short)_robotData.Port, (short)_robotData.ReadTimeout);
                    if (!yamahaRobot.IsConnected())
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Failure to connected to Robot " + _robotData.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
