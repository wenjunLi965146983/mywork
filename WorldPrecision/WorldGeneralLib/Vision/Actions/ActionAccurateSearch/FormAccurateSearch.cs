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

namespace WorldGeneralLib.Vision.Actions.AccurateSearch
{
    public partial class FormActionAccurateSearch : Form
    {
        private ActionAccurateSearchData _actionAccurateSearchData;
        private ActionAccurateSearch _actionAccurateSearch;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionAccurateSearch(ActionAccurateSearchData data, ActionAccurateSearch _actionAccurateSearch)
        {
            InitializeComponent();
            _actionAccurateSearchData = data;
            this._actionAccurateSearch = _actionAccurateSearch;
           
          
            this.tbTime.Text= Convert.ToString(_actionAccurateSearchData.time);
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
            cmbImageSrc.SelectedIndex = _actionAccurateSearchData.imageSrc;
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

                    _actionAccurateSearch.imageModel = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionAccurateSearch.imageModel.Clone();

                    for (int i = 0; i < _actionAccurateSearchData.time; i++)
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
        private void FormActionAccurateSearch_Load(object sender, EventArgs e)
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
            String filename = @".//Parameter/Model/AccurateSearch Model/model.jpg";
            if (null == _actionAccurateSearch.imageModel)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionAccurateSearch.imageModel.Save(filename);
                _actionAccurateSearchData.ModelAOIX = rectangle.X;
                _actionAccurateSearchData.ModelAOIY = rectangle.Y;
                _actionAccurateSearchData.ModelAOIHeight = rectangle.Height;
                _actionAccurateSearchData.ModelAOIWidth = rectangle.Width;
                _actionAccurateSearchData.imageSrc = cmbImageSrc.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            _actionAccurateSearchData.InputAOIX = rectROI.X;
            _actionAccurateSearchData.InputAOIY = rectROI.Y;
            _actionAccurateSearchData.InputAOIWidth = rectROI.Width;
            _actionAccurateSearchData.InputAOIHeight = rectROI.Height;

            int i = Convert.ToInt32(tbTime.Text);
            if (i < 4)
            {
                _actionAccurateSearchData.time = i;
            }
            else
            {
                try
                {
                    tbTime.Text = _actionAccurateSearchData.time.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("缩放倍数为0-4的整数");
                }


            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (_actionAccurateSearchData != null)
            {

                if (null != _actionAccurateSearch.imageModel)
                {
                    try
                    {

                        _modelImage = _actionAccurateSearch.imageModel.Clone();

                        for (int i = 0; i < _actionAccurateSearchData.time; i++)
                        {

                            _modelImage = _modelImage.PyrDown();
                        }
                        this.rectangle = new Rectangle(_actionAccurateSearchData.ModelAOIX, _actionAccurateSearchData.ModelAOIY, _actionAccurateSearchData.ModelAOIWidth, _actionAccurateSearchData.ModelAOIHeight);
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
            if (0 == _actionAccurateSearchData.imageSrc)
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

                
                        _actionAccurateSearch.run(new Image<Gray, byte>(mat.Bitmap));
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionAccurateSearch.ActionExcute();
            }

           // labelResult.Text = string.Format("中心坐标：X:{0},Y:{1};偏移角度：{2}", _actionAccurateSearchData.fResultX, _actionAccurateSearchData.fResultY, _actionAccurateSearchData.fResultAngle);
            imageBox1.Image = _actionAccurateSearch.imageResult;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            _actionAccurateSearch.Init();
          
        }

        private void button3_Click(object sender, EventArgs e)//ROI显示
        {
            if (null != _actionAccurateSearch.imageModel)
            {
                try
                {
                    imageInput = _actionAccurateSearch.imageModel.Clone();
                    for (int i = 0; i < _actionAccurateSearchData.time; i++)
                    {
                        imageInput = imageInput.PyrDown();
                    }
                    this.rectROI = new Rectangle(_actionAccurateSearchData.InputAOIX, _actionAccurateSearchData.InputAOIY, _actionAccurateSearchData.InputAOIWidth,_actionAccurateSearchData.InputAOIHeight);
                    Image<Gray, byte> image = imageInput.Clone();
                    if (0 != _actionAccurateSearchData.InputAOIWidth && 0 != _actionAccurateSearchData.InputAOIHeight)
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
                imageBox1.Image = _actionAccurateSearch.imageDescript;
                bImageShow = false;
            }
            else
            {
                imageBox1.Image = _actionAccurateSearch.imageCon;
                bImageShow = true;
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
           
        }

        private void tbOffsetX_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void tbOffsetY_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void tbAngle_KeyPress(object sender, KeyPressEventArgs e)
        {

          
        }

        private void tbKeyPoint_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if ( 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void cmbDefaultCameraList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
      
}
   
