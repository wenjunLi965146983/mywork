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

namespace WorldGeneralLib.Hardware.LeadShine
{
    public partial class FormLeadShineMotionCard : Form
    {
        private LeadShineMotionCardData _mcData = null;
        public FormLeadShineMotionCard()
        {
            InitializeComponent();
        }
        public FormLeadShineMotionCard(LeadShineMotionCardData data):this()
        {
            _mcData = data;
            this.panelMain.Text = _mcData.Name;
            ViewInit();
        }

        private void ViewInit()
        {
            if(null != _mcData)
            {
                tbCardNum.Text = _mcData.Index.ToString();
            }
            else
            {
                tbCardNum.Text = " ";
            }
        }

        private void tbCardNum_Validated(object sender, EventArgs e)
        {
            try
            {
                if (!JudgeNumber.isPositiveInteger(tbCardNum.Text) && !tbCardNum.Text.Equals("0"))
                {
                    MessageBox.Show("The card number should be Uint16", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    tbCardNum.Text = _mcData.Index.ToString();
                    tbCardNum.Focus();
                    return;
                }
                _mcData.Index = Convert.ToUInt16(tbCardNum.Text.Trim());

            }
            catch (Exception)
            {
            }

        }

        private void tbCardNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{tab}");
            }
        }

        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(HardwareManage.docHardware.SaveDoc())
                {
                    MessageBox.Show("Save successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
            }
            catch (Exception)
            { 
                MessageBox.Show("Save failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
    }
}
