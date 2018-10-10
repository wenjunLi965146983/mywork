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
    public partial class FormUserManage : Form
    {
        private string _strCurrSelUserName = "";

        private string _strCurrUserName = "";
        private int _iCurrUserLevel = 0;

        public FormUserManage()
        {
            InitializeComponent();
        }

        //用户列表刷新
        private void UserListRefresh()
        {
            try
            {
                listViewUser.Items.Clear();

                if (!LoginManage.loginDoc.dicLoginInfo.ContainsKey("Root"))
                {
                    ListViewItem li = new ListViewItem("Root");
                    li.SubItems.Add("3");
                    listViewUser.Items.Add(li);
                }

                foreach (KeyValuePair<string, LoginInfo> item in LoginManage.loginDoc.dicLoginInfo)
                {
                    ListViewItem li = new ListViewItem(item.Value.UserName);
                    li.SubItems.Add(item.Value.UserLevel.ToString());
                    listViewUser.Items.Add(li);
                }
            }
            catch (System.Exception)
            {
                MessageBox.Show("获取用户信息失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        //密码重置
        private void btnPasswordReset_Click(object sender, EventArgs e)
        {
            FormPasswordReset resetFrm = new FormPasswordReset();
            resetFrm.SetCurrUserName(_strCurrSelUserName);
            resetFrm.ShowDialog();
        }

        //确定按钮
        private void btnSure_Click(object sender, EventArgs e)
        {
            LoginManage.loginDoc.SaveDoc();
            MessageBox.Show("设置成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

            timer1.Stop();
            this.Close();
        }

        //用户添加
        private void btnUserAdd_Click(object sender, EventArgs e)
        {
            FormAddUser addUserFrm = new FormAddUser();
            addUserFrm.ShowDialog();

            UserListRefresh();
        }

        //用户删除
        private void btnUserDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginManage.loginDoc.dicLoginInfo.ContainsKey(_strCurrSelUserName))
                {
                    LoginManage.loginDoc.dicLoginInfo.Remove(_strCurrSelUserName);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("删除失败！\r\n" + ex.ToString(), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }

            UserListRefresh();
        }

        private void UserManageForm_Load(object sender, EventArgs e)
        {
            UserListRefresh();
            timer1.Start();

            try
            {
                _strCurrUserName = LoginManage.strCurrUserName;
                _iCurrUserLevel = LoginManage.iCurrUserLevel;

                _strCurrSelUserName = _strCurrUserName;

                groupBox1.Text = _strCurrSelUserName + " 的密码";
                labLoginTips2.Text = "要更改 " + _strCurrSelUserName + " 的密码，请单击 重置密码 。";
            }
            catch (System.Exception)
            {

            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_iCurrUserLevel < 2)
            {
                btnUserAdd.Enabled = false;
                btnUserDel.Enabled = false;

                if (_strCurrSelUserName == _strCurrUserName)
                {
                    btnPasswordReset.Enabled = true;
                    btnSure.Enabled = true;
                }
                else
                {
                    btnPasswordReset.Enabled = false;
                    btnSure.Enabled = false;
                }

                return;
            }
            else
            {
                if (_iCurrUserLevel == 2 && _strCurrSelUserName == "Root")
                {
                    btnPasswordReset.Enabled = false;
                }
                else
                {
                    btnPasswordReset.Enabled = true;
                }
            }

            if (string.IsNullOrEmpty(_strCurrSelUserName) || listViewUser.Items.Count <= 1 || 0 == string.Compare(_strCurrSelUserName, "Root"))
            {
                btnUserDel.Enabled = false;
            }
            else
            {
                btnUserDel.Enabled = true;
            }

        }

        private void listViewUser_Click(object sender, EventArgs e)
        {
            int selCount = listViewUser.SelectedItems.Count;

            if (selCount <= 0)
            {
                return;
            }

            _strCurrSelUserName = listViewUser.SelectedItems[0].SubItems[0].Text;

            groupBox1.Text = _strCurrSelUserName + " 的密码";
            labLoginTips2.Text = "要更改 " + _strCurrSelUserName + " 的密码，请单击 重置密码 。";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginManage.iCurrUserLevel = -1;
            LoginManage.strCurrUserName = "";
            LoginManage.strCurrPassword = "";

            this.Close();
        }

    }
}
