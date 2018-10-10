using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Vision.Forms;

namespace WorldGeneralLib.Vision.Actions.PreProcess
{
    public partial class FormActionPreProcess : Form
    {

        private ActionPreProcessData _actionPreProcessData;
        private ActionPreProcess _actionPreProcess;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;
    

        private Image<Gray, byte> _modelImage;
        public FormActionPreProcess(ActionPreProcessData data, ActionPreProcess actionPreProcess)
        {
            InitializeComponent();
            bMouseDownIm3 = false;
            bMouseDown = false;
            _actionPreProcessData = data;
            _actionPreProcess = actionPreProcess;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionPreProcessData.InputAOIX, _actionPreProcessData.InputAOIY, _actionPreProcessData.InputAOIWidth, _actionPreProcessData.InputAOIHeight);
            
            rbROIReset.Checked = _actionPreProcessData.bROIReset;
            tbFileTimes.Text = Convert.ToString(_actionPreProcessData.iFTime);
            tbFilterSize.Text = Convert.ToString(_actionPreProcessData.iFilterSize);
            tbTimes.Text = Convert.ToString(_actionPreProcessData.iTimes);
            tbMorSize.Text = Convert.ToString(_actionPreProcessData.iMorSize);

            if (null != _actionPreProcessData.strProcessType)
            {
                foreach (var item in cbFilterType.Items)
                {
                    if (_actionPreProcessData.strProcessType == item.ToString())
                    {
                        cbFilterType.Text = _actionPreProcessData.strProcessType;
                    }
                }
                foreach (var item in cbMorphology.Items)
                {
                    if (_actionPreProcessData.strProcessType == item.ToString())
                    {
                        cbMorphology.Text = _actionPreProcessData.strProcessType;
                    }
                }
            }
          
            FormVision.eventRun += new FormVision.formRefresh(init);
           
        }
        private void init()
        {
            List<ActionBase> list = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction;
            DataTable dtImage = new DataTable();
            dtImage.Columns.Add("name");
            dtImage.Columns.Add("value");
            int i = 0;
            DataRow dr = dtImage.NewRow();
            dr["name"] = i.ToString() + ":本地相册" ;
            dr["value"] = i;
            dtImage.Rows.Add(dr);
            foreach (ActionBase action in list)
            {
                i++;
                dr = dtImage.NewRow();
                dr["name"] = i.ToString() + ":" + action.actionData.Name;
                dr["value"] = i;
                dtImage.Rows.Add(dr);
            }
            cmbImageSrc.DataSource = dtImage;
            cmbImageSrc.DisplayMember = "name";
            cmbImageSrc.ValueMember = "value";
            cmbImageSrc.SelectedIndex = _actionPreProcessData.imageSrc;

        }
        private void btnModelOpen_Click(object sender, EventArgs e)
        {

            OpenFileDialog lvse = new OpenFileDialog();
            lvse.Title = "选择图片";
            lvse.InitialDirectory = "";
            lvse.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.gif;*png";
            lvse.FilterIndex = 1;


            if (lvse.ShowDialog() == DialogResult.OK)
            {

                Mat mat = CvInvoke.Imread(lvse.FileName, Emgu.CV.CvEnum.ImreadModes.AnyColor);


                try
                {

                    _actionPreProcess.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionPreProcess.imageTemple.Clone();
                    imageBox2.Image = _modelImage;
                }
                catch (Exception)
                {
                    MessageBox.Show("图片格式错误");
                }
            }
        }
        private void FormActionMatch_Load(object sender, EventArgs e)
        {

        }

        private void imageBox2_MouseDown(object sender, MouseEventArgs e)
        {

            bMouseDown = true;
            rectangle.X = _modelImage.Width * e.X / imageBox2.Width;
            rectangle.Y = _modelImage.Height * e.Y / imageBox2.Height;

        }

        private void imageBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bMouseDown)
            {
                return;
            }
            int x = _modelImage.Width * e.X / imageBox2.Width;
            int y = _modelImage.Height * e.Y / imageBox2.Height;
            if (x > rectangle.X)
            {
                rectangle.Width = _modelImage.Width < x - rectangle.X ? _modelImage.Width - rectangle.X : x - rectangle.X;
            }
            else
            {
                rectangle.X = x;
            }
            if (y > rectangle.Y)
            {
                rectangle.Height = _modelImage.Height < y - rectangle.Y ? _modelImage.Height - rectangle.Y : y - rectangle.Y;
            }
            else
            {

                rectangle.Y = y;
            }
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            Image<Gray, byte> image = _modelImage.Clone();
            image.Draw(rectangle, new Gray(255), 3);
            imageBox2.Image = image;


        }

        private void imageBox2_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String filename = @".//Parameter/Model/PreProcess Model/model.jpg";
            if (null == _actionPreProcess.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionPreProcess.imageTemple.Save(filename);
                _actionPreProcessData.InputAOIX = rectangle.X;
                _actionPreProcessData.InputAOIY = rectangle.Y;
                _actionPreProcessData.InputAOIWidth = rectangle.Width;
                _actionPreProcessData.InputAOIHeight = rectangle.Height;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            _actionPreProcessData.iFTime = Convert.ToInt32(tbFileTimes.Text);
            _actionPreProcessData.iFilterSize = Convert.ToInt32(tbFilterSize.Text);
            _actionPreProcessData.iTimes = Convert.ToInt32(tbTimes.Text);
            _actionPreProcessData.iMorSize = Convert.ToInt32(tbMorSize.Text);
            _actionPreProcessData.imageSrc = cmbImageSrc.SelectedIndex;
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionPreProcessData)
            {

                if (null != _actionPreProcess.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionPreProcess.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionPreProcessData.InputAOIX, _actionPreProcessData.InputAOIY, _actionPreProcessData.InputAOIWidth, _actionPreProcessData.InputAOIHeight);
                        _imageShow = _modelImage.Clone();
                        _imageShow.Draw(rectangle, new Gray(255), 3);
                        imageBox2.Image = _imageShow;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {
            if (bImageShow)
            {
                imageBox1.Image = _imageShow;
                bImageShow = false;
                label7.Text = "输出";
            }
            else
            {
                imageBox1.Image = _actionPreProcess.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }

        private void tbMorSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbMorSize.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbTimes_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbTimes.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbFilterSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbFilterSize.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (0 == _actionPreProcessData.imageSrc)
            {
                OpenFileDialog lvse = new OpenFileDialog();
                lvse.Title = "选择图片";
                lvse.InitialDirectory = "";
                lvse.Filter = "图片文件|*.bmp;*.jpg;*.jpeg;*.gif;*png";
                lvse.FilterIndex = 1;


                if (lvse.ShowDialog() == DialogResult.OK)
                {

                    Mat mat = CvInvoke.Imread(lvse.FileName, Emgu.CV.CvEnum.ImreadModes.AnyColor);


                    try
                    {


                        _actionPreProcess.run(new Image<Gray, byte>(mat.Bitmap));


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionPreProcess.ActionExcute();
            }
                _imageShow = _actionPreProcess.imageResult.Clone();

                if (_imageShow.IsROISet)
                {
                    Rectangle rect = new Rectangle(0, 0, _actionPreProcess.imageInput.Width, _actionPreProcess.imageInput.Height);
                    _imageShow.ROI = rect;


                    _imageShow.Draw(_actionPreProcess.imageResult.ROI, new Gray(255), 3);


                }
                imageBox1.Image = _imageShow;

           
        }

        private void cbFilterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 != cbFilterType.SelectedIndex)
            {
                _actionPreProcessData.strProcessType = cbFilterType.Text;
                cbMorphology.SelectedIndex = -1;
            }
        }

        private void cbMorphology_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (-1 != cbMorphology.SelectedIndex)
            {
                _actionPreProcessData.strProcessType = cbMorphology.Text;
                cbFilterType.SelectedIndex = -1;
            }
           
        }

        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionPreProcessData.bROIReset)
            {
                _actionPreProcessData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionPreProcessData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rectangle.X = 0;
            rectangle.Y = 0;
            rectangle.Width = 0;
            rectangle.Height = 0;
        }
    }
}
   
