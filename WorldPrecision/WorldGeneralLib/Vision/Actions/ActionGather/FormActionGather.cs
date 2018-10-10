using Basler.Pylon;
using Emgu.CV;
using Emgu.CV.Structure;
using PylonC.NETSupportLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.Vision.Actions.Gather
{
    public partial class FormActionGather : Form
    {
        private ActionGatherData _actionData;
        private ActionGather _actionGather;
        private bool _bCamRun;
        public FormActionGather(ActionGatherData data, ActionGather actionGather)
        {
            InitializeComponent();
            _actionData = data;
            _actionGather = actionGather;
        }

        #region Method
        private void ShowCamParam()
        {
            try
            {
                CameraParam cam = _actionData.listCamParam[cmbDefaultCameraList.SelectedIndex];

                //相机设定
                tbShutterSpeed.Text = cam.iShutterSpeed.ToString();
                tbGain.Text = cam.iGain.ToString();

                radBtnIOCtrl.Checked = cam.ledCtrl == LedControl.IO ? true : false;
                radBtnLedController.Checked = !radBtnIOCtrl.Checked;

                if (cmbCtrlCard.Items.Count > 0)
                {
                    int index = cmbCtrlCard.FindStringExact(cam.strControlCardName);
                    cmbCtrlCard.SelectedIndex = index > -1 ? index : -1;
                }
                tbIONum.Text = cam.iIoNum.ToString();

                if (cmbLedController.Items.Count > 0)
                {
                    int index = cmbLedController.FindStringExact(cam.strLedControllerName);
                    cmbLedController.SelectedIndex = index > -1 ? index : -1;
                }
                tbLumiance.Text = cam.iLuminance.ToString();
                tbChannel.Text = cam.iChannel.ToString();

                if (cmbCameraBinding.Items.Count > 0)
                {
                    int index = cmbCameraBinding.FindStringExact(cam.strCamBingding);
                    cmbCameraBinding.SelectedIndex = index > -1 ? index : -1;
                }

                tbWhiteBalanceR.Text = cam.iWhiteBalanceR.ToString();
                tbWhiteBalanceG.Text = cam.iWhiteBalanceG.ToString();
                tbWhiteBalanceB.Text = cam.iWhiteBalanceB.ToString();

                radBtnImageSrcLocal.Checked = _actionData.eimageSrc == ImageSource.Local ? true : false;
                radBtnImageSrcCamera.Checked = !radBtnImageSrcLocal.Checked;
                tbPath.Text = _actionData.strLocalImgSrcPath;
                if (_actionData.iSrcCamIndex < -1 || _actionData.iSrcCamIndex >= VisionManage.MaxCameraCount)
                {
                    _actionData.iSrcCamIndex = -1;
                }
                cmbCamSel.SelectedIndex = _actionData.iSrcCamIndex;
            }
            catch (Exception)
            {
            }
        }
        private bool SaveSetting()
        {
            try
            {
                CameraParam cam = _actionData.listCamParam[cmbDefaultCameraList.SelectedIndex];

                int.TryParse(tbShutterSpeed.Text, out cam.iShutterSpeed);
                int.TryParse(tbGain.Text, out cam.iGain);

                cam.ledCtrl = radBtnIOCtrl.Checked ? LedControl.IO : LedControl.LedController;
                if (null != cmbCtrlCard.SelectedItem)
                    cam.strControlCardName = cmbCtrlCard.SelectedItem.ToString();
                int.TryParse(tbIONum.Text, out cam.iIoNum);

                if (null != cmbLedController.SelectedItem)
                    cam.strLedControllerName = cmbLedController.SelectedItem.ToString();
                int.TryParse(tbLumiance.Text, out cam.iLuminance);
                int.TryParse(tbChannel.Text, out cam.iChannel);

                if (null != cmbCameraBinding.SelectedItem)
                    cam.strCamBingding = cmbCameraBinding.SelectedItem.ToString();

                int.TryParse(tbWhiteBalanceR.Text, out cam.iWhiteBalanceR);
                int.TryParse(tbWhiteBalanceG.Text, out cam.iWhiteBalanceG);
                int.TryParse(tbWhiteBalanceB.Text, out cam.iWhiteBalanceB);

                _actionData.eimageSrc = radBtnImageSrcLocal.Checked ? ImageSource.Local : ImageSource.Camera;
                _actionData.strLocalImgSrcPath = tbPath.Text;
                _actionData.iSrcCamIndex = cmbCamSel.SelectedIndex;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private void RefreshComboBoxList()
        {
            try
            {
                cmbDefaultCameraList.Items.Clear();
                cmbCamSel.Items.Clear();
                for (int index = 0; index < VisionManage.MaxCameraCount; index++)
                {
                    cmbDefaultCameraList.Items.Add("相机 " + index.ToString());
                    cmbCamSel.Items.Add("相机 " + index.ToString());
                }
                if (cmbDefaultCameraList.Items.Count > 0)
                {
                    cmbDefaultCameraList.SelectedIndex = 0;
                }

                cmbCameraBinding.Items.Clear();
                cmbCtrlCard.Items.Clear();
                cmbLedController.Items.Clear();
                foreach (HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    if (item.Type == HardwareType.Camera)
                    {
                        cmbCameraBinding.Items.Add(item.Name);
                    }
                    if (item.Type == HardwareType.MotionCard || item.Type == HardwareType.OutputCard || item.Type == HardwareType.InputOutputCard)
                    {
                        cmbCtrlCard.Items.Add(item.Name);
                    }
                    if (item.Type == HardwareType.LedController)
                    {
                        cmbLedController.Items.Add(item.Name);
                    }
                }

                if (null != cmbDefaultCameraList.SelectedItem)
                {
                    tbCurrCam.Text = cmbDefaultCameraList.SelectedItem.ToString();
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Events
        private void FormActionGather_Load(object sender, EventArgs e)
        {
            RefreshComboBoxList();
            this.cmbDefaultCameraList.SelectedIndexChanged += new System.EventHandler(this.cmbDefaultCameraList_SelectedIndexChanged);
            ShowCamParam();
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            SaveSetting();
        }

        private void cmbDefaultCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCurrCam.Text = cmbDefaultCameraList.SelectedItem.ToString();
            ShowCamParam();
        }

        #region 图像输入
        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.Title = "图像选择";
                fileDialog.RestoreDirectory = false;
                if (fileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                tbPath.Text = fileDialog.FileName;
            }
            catch (Exception)
            {
            }
        }
        private void radBtnImageSrcLocal_CheckedChanged(object sender, EventArgs e)
        {
            tbPath.Enabled = radBtnImageSrcLocal.Checked;
            btnOpen.Enabled = radBtnImageSrcLocal.Checked;

            cmbCamSel.Enabled = !radBtnImageSrcLocal.Checked;
        }

        private void radBtnImageSrcCamera_CheckedChanged(object sender, EventArgs e)
        {
            tbPath.Enabled = !radBtnImageSrcCamera.Checked;
            btnOpen.Enabled = !radBtnImageSrcCamera.Checked;

            cmbCamSel.Enabled = radBtnImageSrcCamera.Checked;
        }
        #endregion

        #endregion

        private void btnCamOpen_Click(object sender, EventArgs e)
        {
            if ("打开" == btnCamOpen.Text)
            {
                _actionGather.Init();
                if(null != _actionGather.aca500M)
                {
                    btnCamOpen.Text = "关闭";
                }
                else
                {
                    MessageBox.Show("没有发现相机");
                }
                
            }
            else
            {
                if (null != _actionGather.aca500M)
                {
                    _actionGather.aca500M.Close();
                }
                btnCamOpen.Text = "打开";
            }

        }

        private void cmbCameraBinding_SelectedIndexChanged(object sender, EventArgs e)
        {
            _actionData.strCameraName = cmbCameraBinding.SelectedItem.ToString();

        }

        private void btnSingle_Click(object sender, EventArgs e)
        {
            if (!_bCamRun && null != _actionGather.aca500M)
            {
                if (_actionGather.aca500M.aca500M_Camera.IsOpen)
                {
                    _actionGather.aca500M.GrabImage(out IGrabResult grabResult);
                    byte[] b = grabResult.PixelData as byte[];
                    Bitmap bitmap = null;
                    BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
                    BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
                    Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
                    imageBox1.Image = image;
                }

            }

        }

        private void btnMulti_Click(object sender, EventArgs e)
        {

            if (null != _actionGather.aca500M)
            {
                if (_actionGather.aca500M.aca500M_Camera.IsOpen)
                {
                    var task = new Task(() =>
                    {
                        _bCamRun = true;

                        while (_bCamRun)
                        {

                            IGrabResult grabResult;
                            Bitmap bitmap = null;
                            byte[] b;


                            bool succesed = _actionGather.aca500M.GrabImage(out grabResult);
                            if (succesed)
                            {

                                b = grabResult.PixelData as byte[];
                            }
                            else
                            {
                                MessageBox.Show("抓取图失败");
                                return;

                            }


                            BitmapFactory.CreateBitmap(out bitmap, grabResult.Width, grabResult.Height, false);
                            BitmapFactory.UpdateBitmap(bitmap, b, grabResult.Width, grabResult.Height, false);
                            Image<Gray, byte> image = new Image<Gray, byte>(bitmap);
                            imageBox1.Image = image;
                          
                            Thread.Sleep(10);
                        }




                    });

                    if ("连拍" == btnMulti.Text)
                    {
                        task.Start();
                        btnMulti.Text = "停止";
                    }
                    else
                    {
                        _bCamRun = false;
                        btnMulti.Text = "连拍";
                        Thread.Sleep(1000);

                    }
                }

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (null != _actionGather.aca500M)
            {
                if (_actionGather.aca500M.aca500M_Camera.IsOpen)
                {
                    _actionGather.aca500M.cameraData.exposureTime = Convert.ToInt32(tbShutterSpeed.Text);
                    _actionGather.aca500M.cameraData.gain = Convert.ToInt32(tbGain.Text);
                    _actionGather.aca500M.SetParameter();
                    return;
                }
            }

            MessageBox.Show("参数下载失败");
        }

        private void tbShutterSpeed_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([-]?)([0-9]*)$");
            if (!rg.IsMatch(tbShutterSpeed.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }

        }

        private void tbGain_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([-]?)([0-9]*)$");
            if (!rg.IsMatch(tbShutterSpeed.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void btnUpLoad_Click(object sender, EventArgs e)
        {
            if (null != _actionGather.aca500M)
            {
                if (_actionGather.aca500M.aca500M_Camera.IsOpen)
                {
                    _actionGather.aca500M.GetParameter();
                    tbShutterSpeed.Text= Convert.ToString(_actionGather.aca500M.cameraData.exposureTime);
                    tbGain.Text= Convert.ToString(_actionGather.aca500M.cameraData.gain);
                    return;
                }
            }
           
             MessageBox.Show("参数上传失败");
           
        }
    }
}
