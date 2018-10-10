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

namespace WorldGeneralLib.Vision.Actions.Match
{
    public partial class FormActionMatch : Form
    {
        private ActionMatchData _actionMatchData;
        private ActionMatch _actionMatch;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionMatch(ActionMatchData data, ActionMatch _actionMatch)
        {
            InitializeComponent();
            _actionMatchData = data;
            this._actionMatch = _actionMatch;
            this.tbOffsetX.Text = Convert.ToString(_actionMatchData.fOffsetX);
            this.tbOffsetY.Text = Convert.ToString(_actionMatchData.fOffsetY);
            this.tbAngle.Text = Convert.ToString(_actionMatchData.fOffsetAngle);
            this.tbThreshold.Text = Convert.ToString(_actionMatchData.fThreshlod);
            this.tbKeyPoint.Text = Convert.ToString(_actionMatchData.iKeyPointNumber);
            this.tbTime.Text= Convert.ToString(_actionMatchData.time);
            bMouseDownIm3 = false;
            bMouseDown = false;

            FormVision.eventRun +=new FormVision.formRefresh(init);

        }
        public void init()
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
            cmbImageSrc.SelectedIndex = _actionMatchData.imageSrc;
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

                    _actionMatch.imageModel = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionMatch.imageModel.Clone();

                    for (int i = 0; i < _actionMatchData.time; i++)
                    {

                        _modelImage = _modelImage.PyrDown();
                    }          
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
            rectangle.X = _modelImage.Width* e.X / imageBox2.Width;
            rectangle.Y = _modelImage.Height * e.Y / imageBox2.Height;

        }

        private void imageBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bMouseDown)
            {
                return;
            }
            int x=_modelImage.Width* e.X / imageBox2.Width;
            int y=_modelImage.Height* e.Y / imageBox2.Height;
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
                rectangle.Height = _modelImage.Height < y - rectangle.X ? _modelImage.Height - rectangle.Y : y - rectangle.Y;
            }
            else
            {

                rectangle.Y = y;
            }

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
            String filename = @".//Parameter/Model/Match Model/model.jpg";
            if (null == _actionMatch.imageModel)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionMatch.imageModel.Save(filename);
                _actionMatchData.ModelAOIX = rectangle.X;
                _actionMatchData.ModelAOIY = rectangle.Y;
                _actionMatchData.ModelAOIHeight = rectangle.Height;
                _actionMatchData.ModelAOIWidth = rectangle.Width;
                _actionMatchData.imageSrc = cmbImageSrc.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _actionMatchData.InputAOIX = rectROI.X;
            _actionMatchData.InputAOIY = rectROI.Y;
            _actionMatchData.InputAOIWidth = rectROI.Width;
            _actionMatchData.InputAOIHeight = rectROI.Height;

            int i = Convert.ToInt32(tbTime.Text);
            if (i < 4)
            {
                _actionMatchData.time = i;
            }
            else
            {
                try
                {
                    tbTime.Text = _actionMatchData.time.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("缩放倍数为0-4的整数");
                }


            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (_actionMatchData != null)
            {

                if (null != _actionMatch.imageModel)
                {
                    try
                    {

                        _modelImage = _actionMatch.imageModel.Clone();

                        for (int i = 0; i < _actionMatchData.time; i++)
                        {

                            _modelImage = _modelImage.PyrDown();
                        }
                        this.rectangle = new Rectangle(_actionMatchData.ModelAOIX, _actionMatchData.ModelAOIY, _actionMatchData.ModelAOIWidth, _actionMatchData.ModelAOIHeight);
                        Image<Gray, byte> image = _modelImage.Clone();
                        image.Draw(rectangle, new Gray(255), 3);
                        imageBox2.Image = image;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (0 == _actionMatchData.imageSrc)
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

                
                        _actionMatch.run(new Image<Gray, byte>(mat.Bitmap));
                       
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("图片格式错误");
                    }
                }
            }
            else
            {
                _actionMatch.ActionExcute();
            }

            labelResult.Text = string.Format("中心坐标：X:{0},Y:{1};偏移角度：{2}", _actionMatch.dResultX, _actionMatch.dResultY, _actionMatch.dResultAngle);
            imageBox1.Image = _actionMatch.imageResult;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            _actionMatch.Init();
          
        }

        private void button3_Click(object sender, EventArgs e)//ROI显示
        {
            if (null != _actionMatch.imageModel)
            {
                try
                {
                    imageInput = _actionMatch.imageModel.Clone();
                    for (int i = 0; i < _actionMatchData.time; i++)
                    {
                        imageInput = imageInput.PyrDown();
                    }
                    this.rectROI = new Rectangle(_actionMatchData.InputAOIX, _actionMatchData.InputAOIY, _actionMatchData.InputAOIWidth,_actionMatchData.InputAOIHeight);
                    Image<Gray, byte> image = imageInput.Clone();
                    if (0 != _actionMatchData.InputAOIWidth && 0 != _actionMatchData.InputAOIHeight)
                    {
                        image.Draw(rectROI, new Gray(255), 3);
                    }
                
                    imageBox3.Image = image;
                }
                catch (Exception)
                {
                    MessageBox.Show("图片格式错误");
                }
            }

        }


        private void imageBox3_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDownIm3 = true;
            rectROI.X = imageInput.Width* e.X/ imageBox3.Width;
            rectROI.Y =imageInput.Height*e.Y/ imageBox3.Height;
        }

        private void imageBox3_MouseMove(object sender, MouseEventArgs e)
        {

            if (!bMouseDownIm3)
            {
                return;
            }
            int x = imageInput.Width * e.X / imageBox3.Width;
            int y= imageInput.Height * e.Y / imageBox3.Height;
            if (x > rectROI.X)
            {
                rectROI.Width = imageInput.Width < x - rectROI.X ? imageInput.Width - rectROI.X : x - rectROI.X;
            }
            else
            {
                rectROI.X = x;
            }
            if (y> rectROI.Y)
            {
                rectROI.Height = imageInput.Height<y-rectROI.X ? imageInput.Height - rectROI.Y : y - rectROI.Y;
            }
            else
            {

                rectROI.Y = y;
            }

            Image<Gray, byte> image = imageInput.Clone();
            image.Draw(rectROI, new Gray(255), 3);
            imageBox3.Image = image;
        }

        private void imageBox3_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDownIm3 = false;
        }



        private void tbTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex(@"^\d{1}$");
            if (!rg.IsMatch(e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void imageBox1_Click(object sender, EventArgs e)
        {
            if (bImageShow)
            {
                imageBox1.Image = _actionMatch.imageDescript;
                bImageShow = false;
            }
            else
            {
                imageBox1.Image = _actionMatch.imageResult;
                bImageShow = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            _actionMatchData.fOffsetX = Convert.ToInt32(tbOffsetX.Text);
            _actionMatchData.fOffsetY = Convert.ToInt32(tbOffsetY.Text);
            _actionMatchData.fOffsetAngle = (float)Convert.ToDouble(tbAngle.Text);
            _actionMatchData.iKeyPointNumber= Convert.ToInt32(tbKeyPoint.Text);
            _actionMatchData.fThreshlod= (float)Convert.ToDouble(tbThreshold.Text);
        }

        private void tbOffsetX_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([-]?)([0-9]*)$");
            if (!rg.IsMatch(tbOffsetX.Text+e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbOffsetY_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([-]?)([0-9]*)$");
            if (!rg.IsMatch(tbOffsetY.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbAngle_KeyPress(object sender, KeyPressEventArgs e)
        {

            Regex rg = new Regex("^[0-9]*[.]?[0-9]*$");
            if (!rg.IsMatch(tbAngle.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbKeyPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbKeyPoint.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^^[0-9]*[.]?[0-9]?$");
            if (!rg.IsMatch(tbThreshold.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void cmbDefaultCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rectangle.X = 0;
            rectangle.Y = 0;
            rectangle.Width = 0;
            rectangle.Height = 0;
        }

        private void ROIClear_Click(object sender, EventArgs e)
        {
            rectROI.X = 0;
            rectROI.Y = 0;
            rectROI.Width = 0;
            rectROI.Height = 0;
        }
    }
      
}
   
