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
    public partial class FormAddUser : Form
    {
        public FormAddUser()
        {
            InitializeComponent();
        }


        private void AddUserForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen mPen = new Pen(System.Drawing.Color.FromArgb(120, 168, 168, 168), 1);
            g.DrawLine(mPen, 0, 450, 550, 450);

        }

        private void AddUserForm_Load(object sender, EventArgs e)
        {
            cbUserLevel.SelectedIndex = 0;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            #region 检查用户名
            if (0 == string.Compare(tbUserName.Text, "Root") || LoginManage.loginDoc.dicLoginInfo.ContainsKey(tbUserName.Text))
            {
                MessageBox.Show("系统有重名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            #endregion

            #region 检查密码
            //不能为空
            if (string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            #endregion

            LoginInfo loginInfo = new LoginInfo();

            loginInfo.UserName = tbUserName.Text;
            loginInfo.Password = SecurityMd5.Encrypt(tbPassword.Text);
            loginInfo.UserLevel = cbUserLevel.SelectedIndex;

            LoginManage.loginDoc.dicLoginInfo.Add(loginInfo.UserName, loginInfo);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
