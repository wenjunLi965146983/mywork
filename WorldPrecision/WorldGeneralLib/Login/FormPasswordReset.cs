using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Login
{
    public partial class FormPasswordReset : Form
    {
        private string _strCurrSelUserName = "";

        public FormPasswordReset()
        {
            InitializeComponent();
        }

        public void SetCurrUserName(string strUserName)
        {
            this._strCurrSelUserName = strUserName;
            this.Text = strUserName + " 的密码重置";
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                if (!LoginManage.loginDoc.dicLoginInfo.ContainsKey(_strCurrSelUserName))
                {
                    MessageBox.Show("密码重置失败，此账户不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                if (string.IsNullOrEmpty(tbNewPasswrod.Text))
                {
                    MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                if (0 != string.Compare(tbNewPasswrod.Text, tbNewPasswrodConfirm.Text))
                {
                    MessageBox.Show("两次输入密码不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }

                LoginManage.loginDoc.dicLoginInfo[_strCurrSelUserName].Password = SecurityMd5.Encrypt(tbNewPasswrod.Text);
            }
            catch (System.Exception)
            {
                MessageBox.Show("两次输入密码不一致！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            
            MessageBox.Show("密码已重置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
