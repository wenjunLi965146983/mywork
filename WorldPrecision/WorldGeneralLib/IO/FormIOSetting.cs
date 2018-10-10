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

namespace WorldGeneralLib.IO
{
    public partial class FormIOSetting : Form
    {
        private string _strIOName;
        private bool _bInput = true;
        public FormIOSetting()
        {
            InitializeComponent();
        }
        public FormIOSetting(string strName) : this()
        {
            if (string.IsNullOrEmpty(strName))
                strName = " ";
            _strIOName = strName;
        }

        private void FormIOSetting_Load(object sender, EventArgs e)
        {

        }

        public void ShowIOSetting(string strName, bool bInput)
        {
            _strIOName = strName;
            _bInput = bInput;
            try
            {
                IOData io;
                if (bInput)
                    io = IOManage.docIO.dicInput[strName];
                else
                    io = IOManage.docIO.dicOutput[strName];

                panelMain.Text = "IO Setting [ " + _strIOName + " ]";
                cmbCard.Items.Clear();
                foreach (HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    if (bInput && (item.Type == HardwareType.InputCard || item.Type == HardwareType.InputOutputCard || item.Type == HardwareType.MotionCard || HardwareType.Robot == item.Type))
                    {
                        cmbCard.Items.Add(item.Name);
                    }
                    else if (!bInput && (item.Type == HardwareType.OutputCard || item.Type == HardwareType.InputOutputCard || item.Type == HardwareType.MotionCard || HardwareType.Robot == item.Type))
                    {
                        cmbCard.Items.Add(item.Name);
                    }
                }
                if (cmbCard.Items.Contains(io.CardName))
                {
                    cmbCard.SelectedItem = io.CardName;
                }
                else
                {
                    cmbCard.SelectedIndex = -1;
                }

                tbIndex.Text = io.Index.ToString();
                tbText.Text = io.Text;
                tbRemark.Text = io.Remark;

                checkedListBox1.SetItemChecked(0, io.Ignore);
                checkedListBox1.SetItemChecked(1, io.Inversion);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                IOManage.docIO.SaveDoc();
                MessageBox.Show("Saved successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            catch (Exception)
            {
                MessageBox.Show("Saved failed !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        #region Set Value
        private void cmbCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCard.Focused)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private void cmbCard_Validated(object sender, EventArgs e)
        {
            try
            {
                if (_bInput)
                {
                    IOManage.docIO.dicInput[_strIOName].CardName = cmbCard.SelectedItem.ToString();
                }
                else
                {
                    IOManage.docIO.dicOutput[_strIOName].CardName = cmbCard.SelectedItem.ToString();
                }
            }
            catch (Exception)
            {
                ShowIOSetting(_strIOName, _bInput);
            }
        }
        private void tbIndex_Validated(object sender, EventArgs e)
        {
            try
            {
                short sVlue = -1;
                if (!Int16.TryParse(tbIndex.Text, out  sVlue) || sVlue < 0)
                {
                    MessageBox.Show("The io index value should be a positive integer number !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    throw new Exception();
                }
                if (_bInput)
                {
                    IOManage.docIO.dicInput[_strIOName].Index = sVlue;
                }
                else
                {
                    IOManage.docIO.dicOutput[_strIOName].Index = sVlue;
                }
            }
            catch (Exception)
            {
                ShowIOSetting(_strIOName, _bInput);
            }
        }

        private void tbText_Validated(object sender, EventArgs e)
        {
            try
            {
                if (_bInput)
                {
                    IOManage.docIO.dicInput[_strIOName].Text = tbText.Text;
                }
                else
                {
                    IOManage.docIO.dicOutput[_strIOName].Text = tbText.Text;
                }
            }
            catch (Exception)
            {
                ShowIOSetting(_strIOName, _bInput);
            }
        }

        private void tbRemark_Validated(object sender, EventArgs e)
        {
            try
            {
                if (_bInput)
                {
                    IOManage.docIO.dicInput[_strIOName].Remark = tbRemark.Text;
                }
                else
                {
                    IOManage.docIO.dicOutput[_strIOName].Remark = tbRemark.Text;
                }
            }
            catch (Exception)
            {
                ShowIOSetting(_strIOName, _bInput);
            }
        }
        private void checkedListBox1_Click(object sender, EventArgs e)
        {
            this.checkedListBox1_SelectedValueChanged(sender, e);
        }
        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            bool bIgnore = checkedListBox1.GetItemChecked(0);
            bool bInversion = checkedListBox1.GetItemChecked(1);

            try
            {
                if (_bInput)
                {
                    IOManage.docIO.dicInput[_strIOName].Ignore = bIgnore;
                    IOManage.docIO.dicInput[_strIOName].Inversion = bInversion;
                }
                else
                {
                    IOManage.docIO.dicOutput[_strIOName].Ignore = bIgnore;
                    IOManage.docIO.dicOutput[_strIOName].Inversion = bInversion;
                }
            }
            catch (Exception)
            {
                ShowIOSetting(_strIOName, _bInput);
            }
        }

        #endregion


    }
}
