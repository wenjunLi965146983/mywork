using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Forms.TipsForm
{
    public partial class FormTips : Form
    {
        private int _iCloseSecs = 0;

        public FormTips()
        {
            InitializeComponent();
        }

        public FormTips(int iCloseSecs, bool bCancelBtn)
        {
            InitializeComponent();

            if (!bCancelBtn)
            {
                btnCancel.Visible = false;
            }
            if(iCloseSecs > 0)
            {
                this._iCloseSecs = iCloseSecs;
                timer1.Start();
            }

        }

        public void SetTipsText(string strTips)
        {
            this.tbTipsText.Text = strTips;
            //this.tbTipsText.ForeColor = Color.Black;
        }

        private void btnSure_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// 默认按钮点击定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_iCloseSecs < 0)
            {
                timer1.Stop();
                this.DialogResult = DialogResult.Yes;
                this.Close();
            }
            btnSure.Text = string.Format("确定({0})", _iCloseSecs);
            _iCloseSecs--;
        }

        #region 实现无边框窗体拖动
        //鼠标移动位置变量
        private Point mouseOff;
        //是否是左键
        private bool leftFlag;


        private void TipsFrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y);   //得到变量的值
                leftFlag = true;
            }
        }

        private void TipsFrm_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;
            }
        }

        private void TipsFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;
            }
        }
        #endregion

        #region 绘边框
        private void TipsFrm_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            //Pen mPen = new Pen(System.Drawing.Color.FromArgb(120, 168, 168, 168), 1);
            Pen mPen = new Pen(System.Drawing.Color.Red, 1);
            Rectangle rect = new Rectangle(0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            g.DrawRectangle(mPen, rect);
        }
        #endregion

    }
}
