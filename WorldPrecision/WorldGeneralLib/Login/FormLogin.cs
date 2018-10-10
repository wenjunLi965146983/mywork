using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib;


namespace WorldGeneralLib.Login
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.Close();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            this.btnExit.ForeColor = Color.Red;
        }

        private void btnExit_MouseLeave(object sender, EventArgs e)
        {
            this.btnExit.ForeColor = Color.Black;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            timer1.Start();

            LoginManage.LoadLoginDoc();
        }

        //画边框
        private void LoginForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen mPen = new Pen(System.Drawing.Color.FromArgb(120, 168, 168, 168), 1);
            Rectangle rect = new Rectangle(0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            //g.DrawRectangle(mPen, rect);
            g.DrawRectangle(Pens.Black, rect);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tbUserName.Focused)
            {
                if (0 == string.Compare(tbUserName.Text, "请输入账号"))
                {
                    tbUserName.Text = "";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(tbUserName.Text))
                {
                    tbUserName.Text = "请输入账号";
                }
            }

            if (tbPassword.Focused)
            {
                if (0 == string.Compare(tbPassword.Text, "请输入密码"))
                {
                    tbPassword.PasswordChar = '*';
                    tbPassword.Text = "";
                }

            }
            else
            {
                if (string.IsNullOrEmpty(tbPassword.Text))
                {
                    tbPassword.PasswordChar = Convert.ToChar(0);
                    tbPassword.Text = "请输入密码";
                }
            }
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string userName = "";
            string strPassword = "";
            int userLevel = 0;
            bool bLoginOk = false;

            labLoginTips.Visible = false;

            if (!LoginManage.loginDoc.dicLoginInfo.ContainsKey("Root"))
            {
                LoginInfo loginInfo = new LoginInfo();
                loginInfo.UserName = "Root";
                loginInfo.Password = SecurityMd5.Encrypt("306247003062477053420213942");
                loginInfo.UserLevel = 3;

                LoginManage.loginDoc.dicLoginInfo.Add(loginInfo.UserName, loginInfo);
            }

            if (LoginManage.loginDoc.dicLoginInfo.ContainsKey(tbUserName.Text))
            {
                try
                {
                    if (0 == string.Compare(SecurityMd5.Encrypt(tbPassword.Text), LoginManage.loginDoc.dicLoginInfo[tbUserName.Text].Password))
                    {
                        bLoginOk = true;
                        userName = LoginManage.loginDoc.dicLoginInfo[tbUserName.Text].UserName;
                        userLevel = LoginManage.loginDoc.dicLoginInfo[tbUserName.Text].UserLevel;
                        strPassword = LoginManage.loginDoc.dicLoginInfo[tbUserName.Text].Password;
                    }
                }
                catch (System.Exception)
                {

                }

            }


            if (!bLoginOk)
            {
                System.Threading.Thread.Sleep(50);
                labLoginTips.Visible = true;
                return;
            }

            LoginManage.strCurrUserName = userName;
            LoginManage.iCurrUserLevel = userLevel;
            LoginManage.strCurrPassword = strPassword;
            LoginManage.eventUserChanged?.Invoke();

            timer1.Stop();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #region 实现无边框拖动
        //鼠标移动位置变量
        private Point mouseOff;
        //是否是左键
        private bool leftFlag;

        private void LoginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);   //得到变量的值
                leftFlag = true;
            }
        }

        private void LoginForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void LoginForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        #endregion

    }
}
