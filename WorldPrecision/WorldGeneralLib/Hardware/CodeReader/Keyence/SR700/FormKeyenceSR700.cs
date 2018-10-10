using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Hardware.CodeReader.Keyence.SR700
{
    public partial class FormKeyenceSR700 : Form
    {
        public KeyenceSR700Data codeReaderData;
        public KeyenceSR700 driver;
        public FormKeyenceSR700(KeyenceSR700Data data)
        {
            InitializeComponent();
            codeReaderData = data;
            panelMain.Text = data.Name;
            driver = null;
        }

        #region Events
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(!SetValues())
                {
                    throw new Exception();
                }
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
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(btnConnect.Text.Equals("Connect"))
            {
                try
                {
                    if(null == driver)
                    {
                        throw new Exception();
                    }
                    HiPerfTimer hTimer = new HiPerfTimer();
                    driver.Init(codeReaderData);
                    hTimer.Start();
                    while(true)
                    {
                        if (driver.IsConnected())
                            break;
                        if (hTimer.TimeUp(2))
                            break;
                    }
                    if(!driver.IsConnected())
                    {
                        MessageBox.Show("Failure to connected to code reader " + codeReaderData.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Failure to connected to code reader " + codeReaderData.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            else
            {

            }
        }

        private void FormKeyenceSR700_Load(object sender, EventArgs e)
        {
            Init();
            ShowValues();
            timer1.Start();
        }
        #endregion

        #region Method
        private void Init()
        {
            cmbCommucationType.DataSource = Enum.GetNames(typeof(Protocol));
            cmbStopBits.DataSource = Enum.GetNames(typeof(System.IO.Ports.StopBits));
            cmbParity.DataSource = Enum.GetNames(typeof(System.IO.Ports.Parity));
            string[] ports = System.IO.Ports.SerialPort.GetPortNames();
            foreach (string port in ports)
            {
                cmbSerialPort.Items.Add(port);
            }
        }
        private void ShowValues()
        {
            if(null == codeReaderData)
            {
                MessageBox.Show("Load configuration failed!");
                return;
            }
            try
            {
                cmbCommucationType.SelectedItem = codeReaderData.protocol.ToString();
                ipAddress.Text = codeReaderData.IP;
                tbPort.Text = codeReaderData.Port.ToString();

                string strTemp = codeReaderData.SerialPort.ToString();
                int index = cmbSerialPort.FindStringExact(strTemp, -1);
                if (index != -1)
                {
                    cmbSerialPort.SelectedIndex = index;
                }
                cmbStopBits.SelectedItem = codeReaderData.StopBits.ToString();
                cmbParity.SelectedItem = codeReaderData.Parity.ToString();
                tbDataBits.Text = codeReaderData.DataBits.ToString();
                tbBaudRate.Text = codeReaderData.Buad.ToString();

                tbCmd.Text = codeReaderData.strTriggerCmd;
                tbTimeout.Text = codeReaderData.iTimeout.ToString();
                tbStartCode.Text = codeReaderData.strStartCode;
                tbEndCode.Text = codeReaderData.strEndCode;
            }
            catch (Exception)
            {
            }
        }
        private bool SetValues()
        {
            if(null == codeReaderData)
            {
                return false;
            }

            try
            {
                codeReaderData.protocol = (Protocol)Enum.Parse(typeof(Protocol), cmbCommucationType.SelectedItem.ToString());
                codeReaderData.IP = ipAddress.Text;
                codeReaderData.Port = Convert.ToInt32(tbPort.Text);

                if(null != cmbSerialPort.SelectedItem)
                {
                    codeReaderData.SerialPort = cmbSerialPort.SelectedItem.ToString();
                }
                codeReaderData.StopBits = (System.IO.Ports.StopBits)Enum.Parse(typeof(System.IO.Ports.StopBits), cmbStopBits.SelectedItem.ToString());
                codeReaderData.Parity = (System.IO.Ports.Parity)Enum.Parse(typeof(System.IO.Ports.Parity),cmbParity.SelectedItem.ToString());
                codeReaderData.DataBits = Convert.ToInt32(tbDataBits.Text);
                codeReaderData.Buad = Convert.ToInt32(tbBaudRate.Text);

                codeReaderData.strTriggerCmd = tbCmd.Text;
                codeReaderData.strStartCode = tbStartCode.Text;
                codeReaderData.strEndCode = tbEndCode.Text;
                codeReaderData.iTimeout = Convert.ToInt32(tbTimeout.Text);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if(null == codeReaderData)
                {
                    throw new Exception();
                }
                if (null == driver)
                {
                    if (!HardwareManage.dicHardwareDriver.ContainsKey(codeReaderData.Name))
                    {
                        throw new Exception();
                    }
                    driver = (KeyenceSR700)HardwareManage.dicHardwareDriver[codeReaderData.Name];
                }
                if(driver.IsConnected())
                {
                    labConnSta.Text = "Connect successfully.";
                    labConnSta.BackColor = Color.Green;
                    btnConnect.Text = "Disconnect";
                }
                else
                {
                    labConnSta.Text = "Connect failed.";
                    labConnSta.BackColor = Color.Red;
                    btnConnect.Text = "Connect";
                }
            }
            catch (Exception)
            {
                driver = null;
                labConnSta.Text = "Connect failed.";
                labConnSta.BackColor = Color.Red;
            } 
        }
        #endregion

    }
}
