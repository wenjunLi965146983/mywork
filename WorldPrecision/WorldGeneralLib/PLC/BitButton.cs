using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldGeneralLib.PLC
{
    public enum ButtonType
    {
        PUSH = 0,
        REVERSE,
        ON,
        OFF
    }
    public enum ButtonStyle
    {
        LAMP = 0,
        SWITCH,
    }
    public partial class BitButton : UserControl, IControlPLC
    {
        public object objlock = new object();
        public PLCResponse m_plcRes;
        public bool bCurrentStatus;
        private bool bPreStatus;
        private bool bTimeOut = false;
        private string m_strDriverName;
        private string m_strDriverAddrMonitor;
        private string m_strDriverAddrWrite;
        private Color colorOnForeColor;
        private Color colorOffForeColor;
        private Color colorOnBackColor;
        private Color colorOffBackColor;
        private String strOnText;
        private String strOffText;
        private ButtonType buttonTypeSet;
        private bool bAlwaysFresh = false;
        private bool bRefresh = false;
        public ButtonType ButtonTypeSet
        {
            get
            {
                return buttonTypeSet;
            }
            set
            {
                buttonTypeSet = value;
            }
        }
        private ButtonStyle buttonStyle;
        public ButtonStyle ButtonStyle
        {
            get
            {
                return buttonStyle;
            }
            set
            {
                buttonStyle = value;
            }
        }
        public string DriverName
        {
            get
            {
                return m_strDriverName;
            }
            set
            {
                m_strDriverName = value;
            }
        }
        public string DriverAddrMonitor
        {
            get
            {
                return m_strDriverAddrMonitor;
            }
            set
            {
                m_strDriverAddrMonitor = value;
            }
        }
        public string DriverAddrWrite
        {
            get
            {
                return m_strDriverAddrWrite;
            }
            set
            {
                m_strDriverAddrWrite = value;
            }
        }
        public Color ColorOnForeColor
        {
            get
            {
                return colorOnForeColor;
            }
            set
            {
                colorOnForeColor = value;
                buttonBit.ForeColor = value;
            }
        }
        public Color ColorOffForeColor
        {
            get
            {
                return colorOffForeColor;
            }
            set
            {
                colorOffForeColor = value;
                buttonBit.ForeColor = value;
            }
        }
        public Color ColorOnBackColor
        {
            get
            {
                return colorOnBackColor;
            }
            set
            {
                colorOnBackColor = value;
                buttonBit.BackColor = value;
            }
        }
        public Color ColorOffBackColor
        {
            get
            {
                return colorOffBackColor;
            }
            set
            {
                colorOffBackColor = value;
                buttonBit.BackColor = value;
            }
        }
        public String StrOnText
        {
            get
            {
                return strOnText;
            }
            set
            {
                strOnText = value;
                buttonBit.Text = value;
            }
        }
        public String StrOffText
        {
            get
            {
                return strOffText;
            }
            set
            {
                strOffText = value;
                buttonBit.Text = value;
            }
        }
        public bool AlwayFresh
        {
            get
            {
                return bAlwaysFresh;
            }
            set
            {
                bAlwaysFresh = value;
            }
        }
        public BitButton()
        {
            InitializeComponent();
            bCurrentStatus = false;
        }
        private void buttonBit_MouseDown(object sender, MouseEventArgs e)
        {
            lock (objlock)
            {
                if (buttonStyle == ButtonStyle.LAMP)
                {
                    return;
                }
                if (buttonTypeSet == ButtonType.PUSH)
                {
                    //SetDriverStatus(true);
                    //FreshDriverStatus();
                    PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, true);
                }
                if (buttonTypeSet == ButtonType.ON)
                {
                    //SetDriverStatus(true);
                    //FreshDriverStatus();
                    PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, true);
                }
                if (buttonTypeSet == ButtonType.OFF)
                {
                    //SetDriverStatus(false);
                    //FreshDriverStatus();
                    PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, true);
                }
                if (buttonTypeSet == ButtonType.REVERSE)
                {
                    if (bCurrentStatus)
                    {
                        //SetDriverStatus(false);
                        //FreshDriverStatus();
                        PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, false);
                    }
                    else
                    {
                        //SetDriverStatus(true);
                        //FreshDriverStatus();
                        PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, true);
                    }
                }
            }
        }
        public PLCResponse GetDriverStatus()
        {

            if (Visible == false || bTimeOut)
            {
                return PLCResponse.SUCCESS;
            }
            lock (objlock)
            {
                m_plcRes = PLC.GetBit(m_strDriverName, m_strDriverAddrMonitor, ref bCurrentStatus);
            }
            return m_plcRes;
        }
        public void SetDriverStatus(bool bOn)
        {
            if (m_plcRes == PLCResponse.SUCCESS)
            {
                bTimeOut = false;
                bRefresh = false;
            }
            bCurrentStatus = bOn;
        }
        public void FreshDriverStatus()
        {
            if (Visible == false)
            {
                return;
            }
            Action atc = () =>
            {

                if (m_plcRes == PLCResponse.SUCCESS)
                {
                    if (bPreStatus == bCurrentStatus && bRefresh == true)
                    {
                        return;
                    }
                    if (bCurrentStatus)
                    {
                        buttonBit.Text = strOnText;
                        buttonBit.ForeColor = colorOnForeColor;
                        buttonBit.BackColor = colorOnBackColor;
                    }
                    else
                    {
                        buttonBit.Text = strOffText;
                        buttonBit.ForeColor = colorOffForeColor;
                        buttonBit.BackColor = colorOffBackColor;
                    }
                }
                else
                {
                    bTimeOut = true;
                    buttonBit.Text = m_plcRes.ToString();
                    buttonBit.ForeColor = colorOffForeColor;
                    buttonBit.BackColor = colorOffBackColor;
                }
                bRefresh = true;
                bPreStatus = bCurrentStatus;
            };
            buttonBit.Invoke(atc);
        }
        private void buttonBit_MouseUp(object sender, MouseEventArgs e)
        {
            lock (objlock)
            {
                if (buttonStyle == ButtonStyle.LAMP)
                {
                    return;
                }
                if (buttonTypeSet == ButtonType.PUSH)
                {

                    PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, false);
                }
            }
        }
        private void BitButton_Load(object sender, EventArgs e)
        {
            try
            {
                PLCControlManageClass.PLCControlGroup[DriverName].PLCControls.Add(this);
            }
            catch
            {

            }
            if (buttonStyle == ButtonStyle.LAMP)
            {
                buttonBit.Enabled = false;
            }
            buttonBit.Font = Font;
        }
        public PLCResponse SetValue(bool bOn)
        {
            lock (objlock)
            {
                return PLC.SetBit(m_strDriverName, m_strDriverAddrWrite, bOn);
            }
        }

    }
}
