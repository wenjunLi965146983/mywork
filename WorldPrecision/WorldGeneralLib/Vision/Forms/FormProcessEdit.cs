using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Vision;
using WorldGeneralLib.Vision.Actions;
using RibBtnVA;

namespace WorldGeneralLib.Vision.Forms
{
    public partial class FormProcessEdit : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int wndproc);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        public const int GWL_STYLE = -16;
        public const int WS_DISABLED = 0x8000000;


        private int _iCurrProcessIndex = 0;
        public FormProcessEdit()
        {
            InitializeComponent();
        }

        #region Method
        private ActionBase GetNewActionByName(string strName)
        {
           
            switch (strName)
            {
                case "图像输入": return new Actions.Gather.ActionGather(new Actions.Gather.ActionGatherData(strName));
                case "位置修正": return new Actions.Match.ActionMatch(new Actions.Match.ActionMatchData(strName));
                case "测量前处理": return new Actions.PreProcess.ActionPreProcess(new Actions.PreProcess.ActionPreProcessData(strName));
                case "亮度修正过滤": return new Actions.BrightCorrect.ActionBrightCorrect(new Actions.BrightCorrect.ActionBrightCorrectData(strName));
                case "搜索": return new Actions.MultiSearch.ActionMultiSearch(new Actions.MultiSearch.ActionMultiSearchData(strName));
                case "色彩灰度过滤": return new Actions.Threshold.ActionThreshold(new Actions.Threshold.ActionThresholdData(strName));
                case "近似直线": return new Actions.Line.ActionLine(new Actions.Line.ActionLineData(strName));
                case "边缘位置": return new Actions.EdgePosition.ActionEdgePosition(new Actions.EdgePosition.ActionEdgePositionData(strName));
                case "单元计算宏": return new Actions.Calculate.ActionCalculate(new Actions.Calculate.ActionCalculateData(strName));
                case "灵敏搜索": return new Actions.AccurateSearch.ActionAccurateSearch(new Actions.AccurateSearch.ActionAccurateSearchData(strName));
                case "近似圆": return new Actions.Circle.ActionCircle(new Actions.Circle.ActionCircleData(strName));
                case "面积重心": return new Actions.SimpleBlob.ActionSimpleBlob(new Actions.SimpleBlob.ActionSimpleBlobData(strName));
                case "EC 圆搜索": return new Actions.CircleSearch.ActionCircleSearch(new Actions.CircleSearch.ActionCircleSearchData(strName));


                default:
                    MessageBox.Show("Can't find action: " + strName,"Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return null;
            }
        }
        private bool AddNewAction(bool bInsert)
        {
            try
            {
                ActionBase action = GetNewActionByName(treeViewActions.SelectedNode.Text);
                if (null == action)
                {
                    return false;
                }
                

                if (bInsert)
                {

                }
                else
                {
                    VisionManage.docVision.listSceneData[VisionManage.iCurrSceneIndex].listActionData.Add(action.actionData);
                    VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Add(action);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        #endregion

        #region Load
        private void FormProcessEdit_Load(object sender, EventArgs e)
        {
            ActionBtnEnable(false);
            ProcessActionButtonEnable();
            ProcessActionsListRefresh();

            SetWindowLong(btnSave.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnSave.Handle, GWL_STYLE));
            btnSave.ForeColor = Color.DarkGray;
        }
        #endregion

        #region Actions
        private void ActionBtnEnable(bool bEnable)
        {
            if (bEnable)
            {
                SetWindowLong(btnAdd.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnAdd.Handle, GWL_STYLE));
                SetWindowLong(btnInsert.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnInsert.Handle, GWL_STYLE));

                btnAdd.ForeColor = Color.Black;
                btnInsert.ForeColor = Color.Black;
            }
            else
            {
                SetWindowLong(btnAdd.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnAdd.Handle, GWL_STYLE));
                SetWindowLong(btnInsert.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnInsert.Handle, GWL_STYLE));

                btnAdd.ForeColor = Color.DarkGray;
                btnInsert.ForeColor = Color.DarkGray;
            }
        }
        private void treeViewActions_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeViewActions.SelectedNode==null || treeViewActions.SelectedNode.Level != 1)
            {
                ActionBtnEnable(false);
            }
            else
            {
                ActionBtnEnable(true);
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(treeViewActions.SelectedNode == null || treeViewActions.SelectedNode.Level != 1)
            {
                MessageBox.Show("Please choose action first.","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            AddNewAction(false);
            ProcessActionsListRefresh();

            SetWindowLong(btnSave.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnSave.Handle, GWL_STYLE));
            btnSave.ForeColor = Color.Black;
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (treeViewActions.SelectedNode == null || treeViewActions.SelectedNode.Level != 1)
            {
                MessageBox.Show("Please choose action first.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetWindowLong(btnSave.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnSave.Handle, GWL_STYLE));
            btnSave.ForeColor = Color.Black;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(VisionManage.docVision == null)
                {
                    throw new Exception();
                }
                VisionManage.docVision.SaveDoc();
                SetWindowLong(btnSave.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnSave.Handle, GWL_STYLE));
                btnSave.ForeColor = Color.DarkGray;
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败!","错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Action Process
        private void ProcessActionsListRefresh()
        {
            try
            {
                if (VisionManage.iCurrSceneIndex < 0)
                    return;
                panelProcess.Controls.Clear();

                List<ActionBase> list = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction;
                for(int index=list.Count-1;index>=0;index--)
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

                    btn.Title = (index+1).ToString() + ". " + list[index].actionData.Name;
                    btn.Text = " ";

                    btn.Click += new EventHandler(ribBtn_Click);
                    #endregion

                    if(_iCurrProcessIndex == index)
                    {
                        btn.ColorBase = Color.PaleTurquoise;
                        btn.BackColor = btn.ColorBase;
                    }

                    panelProcess.Controls.Add(btn);
                }
            }
            catch (Exception ex)
            {
                panelProcess.Controls.Clear();
                MessageBox.Show("刷新流程列表时发生错误！\r\n" + ex.ToString(),"错误",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
            }
            ProcessActionButtonEnable();
        }
        private void ribBtn_Click(object sender, EventArgs e)
        {
            try
            {
                RibBtnVA.RibBtnVA btn = (RibBtnVA.RibBtnVA)sender;
                foreach (Control ctrl in panelProcess.Controls)
                {
                    ctrl.BackColor = Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(227)))), ((int)(((byte)(227)))));
                }
                btn.BackColor = Color.PaleTurquoise;

                _iCurrProcessIndex = Convert.ToInt32(btn.Title.Split('.')[0]) - 1;

                ProcessActionButtonEnable();
            }
            catch (Exception)
            {
                _iCurrProcessIndex = 0;

                ProcessActionButtonEnable();
            }
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_iCurrProcessIndex < 0 || _iCurrProcessIndex >= VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Count)
                {
                    ProcessActionButtonEnable();
                    return;
                }

                VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.RemoveAt(_iCurrProcessIndex);
                VisionManage.docVision.listSceneData[VisionManage.iCurrSceneIndex].listActionData.RemoveAt(_iCurrProcessIndex);
                ProcessActionsListRefresh();

                ProcessActionButtonEnable();
                SetWindowLong(btnSave.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnSave.Handle, GWL_STYLE));
                btnSave.ForeColor = Color.Black;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void ProcessActionButtonEnable()
        {
            bool bBtnUpEnable = false;
            bool bBtnDownEnable = false;
            bool bBtnDelEnable = false;

            try
            {
                if(VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Count > 0 && _iCurrProcessIndex < VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Count)
                {
                    bBtnUpEnable = _iCurrProcessIndex > 0 ? true : false;
                    bBtnDownEnable = _iCurrProcessIndex < VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction.Count - 1 ? true : false;
                    bBtnDelEnable = true;
                }
            }
            catch (Exception)
            {
            }

            if(bBtnUpEnable)
            {
                SetWindowLong(btnUp.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnUp.Handle, GWL_STYLE));
                btnUp.ForeColor = Color.Black;
            }
            else
            {
                SetWindowLong(btnUp.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnUp.Handle, GWL_STYLE));
                btnUp.ForeColor = Color.DarkGray;
            }

            if(bBtnDownEnable)
            {
                SetWindowLong(btnDown.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnDown.Handle, GWL_STYLE));
                btnDown.ForeColor = Color.Black;
            }
            else
            {
                SetWindowLong(btnDown.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnDown.Handle, GWL_STYLE));
                btnDown.ForeColor = Color.DarkGray;
            }

            if(bBtnDelEnable)
            {
                SetWindowLong(btnDel.Handle, GWL_STYLE, (~WS_DISABLED) & GetWindowLong(btnDel.Handle, GWL_STYLE));
                btnDel.ForeColor = Color.Black;
            }
            else
            {
                SetWindowLong(btnDel.Handle, GWL_STYLE, WS_DISABLED | GetWindowLong(btnDel.Handle, GWL_STYLE));
                btnDel.ForeColor = Color.DarkGray;
            }
        }
        #endregion
    }
}
