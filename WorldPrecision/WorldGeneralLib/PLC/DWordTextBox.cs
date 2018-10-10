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
    public enum TextBoxType
    {
        DISPLAY = 0,
        EDIT,
    }
    public partial class DWordTextBox : UserControl, IControlPLC
    {
        private bool bTimeOut = false;
        private bool bRefresh = false;
        private bool bMouseIn;
        private string strValue = "";
        private string strPreValue = "";
        public PLCResponse m_plcRes;
        private string m_strDriverName;
        private string m_strDriverAddr;
        private TextBoxType textBoxType;
        private PLCDataType plcDataType;
        public TextBoxType TextBoxType
        {
            get
            {
                return textBoxType;
            }
            set
            {
                textBoxType = value;
            }
        }
        public PLCDataType PlcDataType
        {
            get
            {
                return plcDataType;
            }
            set
            {
                plcDataType = value;
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
        public string DriverAddr
        {
            get
            {
                return m_strDriverAddr;
            }
            set
            {
                m_strDriverAddr = value;
            }
        }
        public DWordTextBox()
        {
            InitializeComponent();
        }

        private void DWordTextBox_Load(object sender, EventArgs e)
        {
            try
            {
                PLCControlManageClass.PLCControlGroup[DriverName].PLCControls.Add(this);
            }
            catch
            {

            }
            if (TextBoxType == TextBoxType.DISPLAY)
            {
                textBoxDisplay.ReadOnly = true;
            }
            textBoxDisplay.Font = Font;
            textBoxDisplay.BackColor = BackColor;
            textBoxDisplay.ForeColor = ForeColor;

        }
        public PLCResponse GetDriverStatus()
        {
            if (Visible == false || bMouseIn || bTimeOut)
            {
                return PLCResponse.SUCCESS;
            }

            m_plcRes = PLC.GetWord(m_strDriverName, m_strDriverAddr, plcDataType, ref strValue);
            return m_plcRes;
        }
        public void FreshDriverStatus()
        {
            if (Visible == false || bMouseIn)
            {
                return;
            }

            Action atc = () =>
            {

                if (m_plcRes == PLCResponse.SUCCESS)
                {
                    if (strPreValue == strValue && bRefresh == true)
                    {
                        return;
                    }
                    textBoxDisplay.Text = strValue;
                }
                else
                {
                    bTimeOut = true;
                    strValue = m_plcRes.ToString();
                    textBoxDisplay.Text = m_plcRes.ToString();
                }
                bRefresh = true;
                strPreValue = strValue;
            };
            textBoxDisplay.Invoke(atc);
        }

        private void textBoxDisplay_Enter(object sender, EventArgs e)
        {
            bMouseIn = true;
        }

        private void textBoxDisplay_Leave(object sender, EventArgs e)
        {
            if (textBoxDisplay.Text != strValue)
            {
                PLC.SetWord(m_strDriverName, m_strDriverAddr, plcDataType, textBoxDisplay.Text);
            }
            bMouseIn = false;
        }
        public void SetDriverStatus(string strValueTemp)
        {
            if (m_plcRes == PLCResponse.SUCCESS)
            {
                bTimeOut = false;
                bRefresh = false;
            }
            if (bMouseIn)
            {
                return;
            }
            strValue = ConvertMesToValue(strValueTemp, plcDataType);
        }

        protected string ConvertMesToValue(string strMessage, PLCDataType dataType)
        {
            string strReturn = "####";
            if (strMessage.Length != 8)
            {
                return "Error";
            }
            string strLowWord = strMessage.Substring(0, 4);
            string strDWord = strMessage.Substring(4, 4) + strLowWord;
            Int16 iLowWord = Convert.ToInt16(strLowWord, 16);
            Int32 iDWord = Convert.ToInt32(strDWord, 16);
            //double dValueResult = Convert.ToDouble(strDWord,new FOR;
            if (dataType == PLCDataType.BIT16)
            {
                strReturn = Convert.ToString(iLowWord, 2);
                strReturn = strReturn.PadLeft(16, '0');
            }
            if (dataType == PLCDataType.BIT32)
            {
                strReturn = Convert.ToString(iDWord, 2);
                strReturn = strReturn.PadLeft(32, '0');
            }
            if (dataType == PLCDataType.BIN16)
            {
                strReturn = iLowWord.ToString();
            }
            if (dataType == PLCDataType.BIN32)
            {
                strReturn = iDWord.ToString();
            }
            if (dataType == PLCDataType.HEX16)
            {
                strReturn = strLowWord;
            }
            if (dataType == PLCDataType.HEX32)
            {
                strReturn = strDWord;
            }
            if (dataType == PLCDataType.DOUBLE)
            {
                int num = int.Parse(strDWord, System.Globalization.NumberStyles.AllowHexSpecifier);
                byte[] floatValues = BitConverter.GetBytes(num);
                float f = BitConverter.ToSingle(floatValues, 0);
                strReturn = f.ToString();
            }
            return strReturn;
        }

        private void textBoxDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBoxDisplay.Text != strValue)
                {
                    PLC.SetWord(m_strDriverName, m_strDriverAddr, plcDataType, textBoxDisplay.Text);
                }
                //bMouseIn = false;
            }
        }

        private void DWordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void DWordTextBox_FontChanged(object sender, EventArgs e)
        {
            textBoxDisplay.Font = Font;
            textBoxDisplay.BackColor = BackColor;
            textBoxDisplay.ForeColor = ForeColor;
        }
    }
}
