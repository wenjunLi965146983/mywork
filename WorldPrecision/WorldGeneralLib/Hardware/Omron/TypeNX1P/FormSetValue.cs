using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Functions;
using OmronFins.Net;

namespace WorldGeneralLib.Hardware.Omron.TypeNX1P
{
    public partial class FormSetValue : Form
    {
        private PlcOmronTypeNX1P _plcDriver;
        private PlcOmronTypeNX1PData _plcData;
        private string _strItemName;
        public FormSetValue(PlcOmronTypeNX1P plcDriver , PlcOmronTypeNX1PData plcData , string strItemName)
        {
            InitializeComponent();

            if(plcDriver == null || plcData == null)
            {
                MessageBox.Show("Can not find the plc driver !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                this.Close();
            }
            if (!plcData.dicScanItems.ContainsKey(strItemName))
            {
                MessageBox.Show("Can not find the item !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                this.Close();
            }

            _plcDriver = plcDriver;
            _strItemName = strItemName;
            _plcData = plcData;
        }

        private bool SetValue()
        {
            try
            {
                bool bRet = false;
                string strValue = textBoxValue.Text;
                if (string.IsNullOrEmpty(strValue))
                {
                    MessageBox.Show("Input value first !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    return false;
                }
                strValue = strValue.Trim();
                DataType dataType = _plcData.dicScanItems[_strItemName].DataType;
                switch(dataType)
                {
                    case DataType.BIT:
                        #region BIT
                        if (!strValue.Equals("1") && !strValue.Equals("0"))
                        {
                            MessageBox.Show("The value should be '0' or '1' !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.SelectAll();
                            return false;
                        }
                        object objValue = strValue.Equals("1") ? true : false;
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], objValue);
                        #endregion
                        break;
                    case DataType.INT16:
                        #region INT16
                        if(!JudgeNumber.isWhloeNumber(strValue))
                        {
                            MessageBox.Show("The value type should be INT16 !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.Focus();
                            textBoxValue.SelectAll();
                            return false;
                        }
                        Int16 tempValue = Convert.ToInt16(strValue);
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], tempValue);
                        #endregion
                        break;
                    case DataType.INT32:
                        #region INT32
                        if (!JudgeNumber.isWhloeNumber(strValue))
                        {
                            MessageBox.Show("The value type should be INT32 !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.Focus();
                            textBoxValue.SelectAll();
                            return false;
                        }
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], Convert.ToInt32(strValue));
                        #endregion
                        break;
                    case DataType.REAL:
                        #region REAL
                        if (!JudgeNumber.isRealNumber(strValue))
                        {
                            MessageBox.Show("The value type should be  FLOAT !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.Focus();
                            textBoxValue.SelectAll();
                            return false;
                        }
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], Convert.ToSingle(strValue));
                        #endregion
                        break;
                    case DataType.UINT16:
                        #region UINT16
                        if (!JudgeNumber.isPositiveUINT1632(strValue))
                        {
                            MessageBox.Show("The value type should be  UINT16 !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.Focus();
                            textBoxValue.SelectAll();
                            return false;
                        }
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], Convert.ToUInt16(strValue));
                        #endregion
                        break;
                    case DataType.UINT32:
                        #region UINT32
                        if (!JudgeNumber.isPositiveUINT1632(strValue))
                        {
                            MessageBox.Show("The value type should be  UINT32 !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            textBoxValue.Focus();
                            textBoxValue.SelectAll();
                            return false;
                        }
                        bRet = _plcDriver.omronFinsAPI.WriteSingleElement(_plcData.dicScanItems[_strItemName], Convert.ToUInt32(strValue));
                        #endregion
                        break;
                    case DataType.STRING:
                        #region STRING
                        if(strValue.Length >= 64)
                        {
                            return false;
                        }
                        bRet = _plcDriver.omronFinsAPI.WriteString(_plcData.dicScanItems[_strItemName], 32, strValue);
                        #endregion
                        break;
                    default:
                        return false;
                }
                return bRet;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if(false == SetValue())
            {
                MessageBox.Show("Value setting failed !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            else
            {
                this.Close();
            }
        }
    }
}
