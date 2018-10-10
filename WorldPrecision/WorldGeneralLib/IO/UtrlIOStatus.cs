using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.IO;

namespace WorldGeneralLib.IO
{
    public partial class UtrlIOStatus : UserControl
    {
        private bool _bSta = false;
        private string _driverName = "";
        private bool _bInput = false;
        public UtrlIOStatus()
        {
            InitializeComponent();
            btnSta.Enabled = false;
            timer1.Start();
        }

        public UtrlIOStatus(string strName, string strText, bool bInput, bool bEnable):this()
        {
            if(!string.IsNullOrEmpty(strText))
            {
                labText.Text = strText;
            }
            if (bInput)
            {
                bEnable = false;
            }

            _bInput = bInput;
            btnSta.Enabled = bEnable;
            _driverName = strName;
        }

        public void UpdateSta(bool bSta)
        {
            _bSta = bSta;
        }
        public void SetText(string strText)
        {
            if(string.IsNullOrEmpty(strText))
            {
                return;
            }
            labText.Text = strText;
        }
        public String IoText
        {
            get {return labText.Text; }
            set {labText.Text = value; }
        }
        public bool IoControl
        {
            get { return btnSta.Enabled; }
            set { btnSta.Enabled = value; }
        }
        public string IoDriverName
        {
            get { return _driverName; }
            set { _driverName = value; }
        }
        public bool InputType
        {
            get { return _bInput; }
            set { _bInput = value; }
        }

        private void btnSta_Click(object sender, EventArgs e)
        {
            try
            {
                SendKeys.Send("{tab}");
                if (_bInput)
                {
                    return;
                }

                if (!IOManage.outputDrivers.dicDrivers.ContainsKey(_driverName))
                {
                    return;
                }
                if(!IOManage.docIO.dicOutput.ContainsKey(_driverName))
                {
                    return;
                }
                if (_bSta)
                    IOManage.OUTPUT(_driverName).SetOutBit(false);
                else
                    IOManage.OUTPUT(_driverName).SetOutBit(true);
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_bInput)
                {
                    if (!IOManage.inputDrivers.dicDrivers.ContainsKey(_driverName))
                    {
                        _bSta = false;
                    }
                    else
                    {
                        _bSta = IOManage.INPUT(_driverName).GetOn();
                    }
                }
                else
                {
                    if (!IOManage.outputDrivers.dicDrivers.ContainsKey(_driverName))
                    {
                        _bSta = false;
                    }
                    else
                    {
                        _bSta = IOManage.OUTPUT(_driverName).GetOn();
                    }
                }
                btnSta.BackColor = _bSta ? Color.Lime : Color.White;
            }
            catch
            {
            }
        }
    }
}
