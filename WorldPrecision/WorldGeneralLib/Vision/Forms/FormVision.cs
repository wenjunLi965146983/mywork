using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Vision.Actions;

namespace WorldGeneralLib.Vision.Forms
{
    public partial class FormVision : Form
    {
        private int _iCurrProcessIndex = -1;
        public delegate void formRefresh();
        public static event formRefresh eventRun;
        public FormVision()
        {
            InitializeComponent();
        }

        #region Method
        private void ProcessActionsListRefresh()
        {
            try
            {
                if (VisionManage.iCurrSceneIndex < 0)
                    return;
                panelActions.Controls.Clear();

                List<ActionBase> list = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction;
                for (int index = list.Count - 1; index >= 0; index--)
                {
                    RibBtnVA.RibBtnVA btn = new RibBtnVA.RibBtnVA();
                    #region Set button style
                    btn.Arrow = RibBtnVA.RibBtnVA.e_arrow.None;
                    btn.ColorBase = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
                    btn.BackColor = btn.ColorBase;
                    btn.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
                    btn.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    btn.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    btn.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    btn.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                    btn.Dock = System.Windows.Forms.DockStyle.Top;
                    btn.FadingSpeed = 35;
                    btn.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.Font = new System.Drawing.Font("新宋体", 11.25F);
                    btn.ForeColor = System.Drawing.Color.Green;
                    btn.GroupPos = RibBtnVA.RibBtnVA.e_groupPos.None;
                    btn.Image = global::WorldGeneralLib.Properties.Resources.图像输入;
                    btn.ImageLocation = RibBtnVA.RibBtnVA.e_imagelocation.Left;
                    btn.ImageOffset = 10;
                    btn.ImageOffsetX = 5;
                    btn.IsPressed = false;
                    btn.KeepPress = false;
                    btn.Location = new System.Drawing.Point(0, 0);
                    btn.Margin = new System.Windows.Forms.Padding(5);
                    btn.MaxImageSize = new System.Drawing.Point(48, 48);
                    btn.MenuPos = new System.Drawing.Point(0, 0);
                    btn.Name = "ribBtnVA" + index.ToString();
                    btn.Radius = 6;
                    btn.ShowBase = RibBtnVA.RibBtnVA.e_showbase.Yes;
                    btn.Size = new System.Drawing.Size(256, 54);
                    btn.SplitButton = RibBtnVA.RibBtnVA.e_splitbutton.No;
                    btn.SplitDistance = 0;
                    btn.TabIndex = 1;
                    btn.TextOfsetX = 5;
                    btn.TextOfsetY = 3;
                    btn.TitleForeColor = System.Drawing.Color.Gray;
                    btn.UseVisualStyleBackColor = false;

                    btn.Title = (index + 1).ToString() + ". " + list[index].actionData.Name;
                    btn.Text = " ";

                    btn.Click += new EventHandler(ribBtn_Click);
                    btn.DoubleClick += new EventHandler(ribBtn_DoubleClick);
                    #endregion

                    panelActions.Controls.Add(btn);
                    if(0 == index)
                    {
                        btn.BackColor = Color.PaleTurquoise;
                        btn.ColorBase = Color.PaleTurquoise;
                    }

                }
                if(0!= list.Count)
                {
                    eventRun();
                }
               
            }
            catch (Exception ex)
            {
                panelActions.Controls.Clear();
                MessageBox.Show("刷新流程列表时发生错误！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        #endregion

        #region Events
        private void FormVision_Load(object sender, EventArgs e)
        { 
            labCurrSceneIndex.Text = VisionManage.iCurrSceneIndex.ToString();
            ProcessActionsListRefresh();
        }
        private void ribBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RibBtnVA.RibBtnVA btn = (RibBtnVA.RibBtnVA)sender;
                int index = Convert.ToInt32(btn.Title.Split('.')[0]) - 1;
                if (index == _iCurrProcessIndex)
                    return;
                _iCurrProcessIndex = index;
                foreach (Control ctrl in panelActions.Controls)
                {
                    ctrl.BackColor = Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
                }
                btn.BackColor = Color.PaleTurquoise;
                imageBox1.Image = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[_iCurrProcessIndex].imageResult;


            }
            catch (Exception)
            {
            }
        }
        private void ribBtn_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                RibBtnVA.RibBtnVA btn = (RibBtnVA.RibBtnVA)sender;
                _iCurrProcessIndex = Convert.ToInt32(btn.Title.Split('.')[0]) - 1;
                VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[_iCurrProcessIndex].formAction.ShowDialog();
            }
            catch (Exception)
            {
            }
        }
        private void btnProcessEdit_Click(object sender, EventArgs e)
        {
            FormProcessEdit formProcessEdit = new FormProcessEdit();
            formProcessEdit.ShowDialog();
            ProcessActionsListRefresh();
        }
        private void btnSceneSelc_Click(object sender, EventArgs e)
        {
            int iPreSceneIndex = VisionManage.iCurrSceneIndex;
            FormSceneChange formSceneChange = new FormSceneChange();
            formSceneChange.ShowDialog();
            if(iPreSceneIndex != VisionManage.iCurrSceneIndex)
            {
                labCurrSceneIndex.Text = VisionManage.iCurrSceneIndex.ToString();
                ProcessActionsListRefresh();
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (VisionManage.docVision == null)
                {
                    throw new Exception();
                }
                VisionManage.docVision.SaveDoc();
                MessageBox.Show("保存成功！", "保存", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            VisionManage.listScene[VisionManage.iCurrSceneIndex].SceneExecute();
            int count = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Count;
            imageBox1.Image = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[count - 1].imageResult;
            sw.Stop();
            labCycleTime.Text = Convert.ToString(sw.ElapsedMilliseconds) + "MS";
        }
        #endregion
    }
}
