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

namespace WorldGeneralLib.Vision.Actions.SimpleBlob
{
    public partial class FormActionSimpleBlob : Form
    {
        private ActionSimpleBlobData _actionSimpleBlobData;
        private ActionSimpleBlob _actionSimpleBlob;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionSimpleBlob(ActionSimpleBlobData data, ActionSimpleBlob _actionSimpleBlob)
        {
            InitializeComponent();
            _actionSimpleBlobData = data;
            this._actionSimpleBlob = _actionSimpleBlob;
           
    
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
            cmbImageSrc.SelectedIndex = _actionSimpleBlobData.imageSrc;

            
            tbColor.Text = _actionSimpleBlobData.blobColor.ToString();
            bFilterByArea.Checked = _actionSimpleBlobData.filterByArea;
            cbMaxArea.Text = _actionSimpleBlobData.maxArea.ToString();
            cbMinArea.Text = _actionSimpleBlobData.minArea.ToString();
            tbMin.Text = _actionSimpleBlobData.thresholdStep.ToString();
            tbMaxThreshold.Text = _actionSimpleBlobData.maxThreshold.ToString();
            tbMinThreshold.Text = _actionSimpleBlobData.minThreshold.ToString();
            tbStep.Text = _actionSimpleBlobData.thresholdStep.ToString();
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

                    _actionSimpleBlob.imageModel = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionSimpleBlob.imageModel.Clone();
        
                    imageBox2.Image = _modelImage;
                }
                catch (Exception)
                {
                    MessageBox.Show("图片格式错误");
                }
            }
        }
        private void FormActionSimpleBlob_Load(object sender, EventArgs e)
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
            String filename = @".//Parameter/Model/SimpleBlob Model/model.jpg";
            if (null == _actionSimpleBlob.imageModel)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionSimpleBlob.imageModel.Save(filename);
                _actionSimpleBlobData.ModelAOIX = rectangle.X;
                _actionSimpleBlobData.ModelAOIY = rectangle.Y;
                _actionSimpleBlobData.ModelAOIHeight = rectangle.Height;
                _actionSimpleBlobData.ModelAOIWidth = rectangle.Width;
                _actionSimpleBlobData.imageSrc = cmbImageSrc.SelectedIndex;

                _actionSimpleBlobData.InputAOIX = rectROI.X;
                _actionSimpleBlobData.InputAOIY = rectROI.Y;
                _actionSimpleBlobData.InputAOIWidth = rectROI.Width;
                _actionSimpleBlobData.InputAOIHeight = rectROI.Height;

                _actionSimpleBlobData.blobColor = Convert.ToInt32(tbColor.Text);
                _actionSimpleBlobData.filterByArea = bFilterByArea.Checked;

                _actionSimpleBlobData.maxArea = Convert.ToSingle(cbMaxArea.Text);
                _actionSimpleBlobData.minArea = Convert.ToSingle(cbMinArea.Text);

                _actionSimpleBlobData.minRepeatability = Convert.ToInt32(tbMin.Text);

                _actionSimpleBlobData.thresholdStep = Convert.ToSingle(tbStep.Text);

                _actionSimpleBlobData.maxThreshold = Convert.ToSingle(tbMaxThreshold.Text);
                _actionSimpleBlobData.minThreshold = Convert.ToSingle(tbMinThreshold.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
       

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (_actionSimpleBlobData != null)
            {

                if (null != _actionSimpleBlob.imageModel)
                {
                    try
                    {

                        _modelImage = _actionSimpleBlob.imageModel.Clone();

                  
                        this.rectangle = new Rectangle(_actionSimpleBlobData.ModelAOIX, _actionSimpleBlobData.ModelAOIY, _actionSimpleBlobData.ModelAOIWidth, _actionSimpleBlobData.ModelAOIHeight);
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
            if (0 == _actionSimpleBlobData.imageSrc)
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

                
                        _actionSimpleBlob.run(new Image<Gray, byte>(mat.Bitmap));
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionSimpleBlob.ActionExcute();
            }

           // labelResult.Text = string.Format("中心坐标：X:{0},Y:{1};偏移角度：{2}", _actionSimpleBlobData.fResultX, _actionSimpleBlobData.fResultY, _actionSimpleBlobData.fResultAngle);
            imageBox1.Image = _actionSimpleBlob.imageResult;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            _actionSimpleBlob.Init();
          
        }

        private void button3_Click(object sender, EventArgs e)//ROI显示
        {
            if (null != _actionSimpleBlob.imageModel)
            {
                try
                {
                    imageInput = _actionSimpleBlob.imageModel.Clone();
              
                    this.rectROI = new Rectangle(_actionSimpleBlobData.InputAOIX, _actionSimpleBlobData.InputAOIY, _actionSimpleBlobData.InputAOIWidth,_actionSimpleBlobData.InputAOIHeight);
                    Image<Gray, byte> image = imageInput.Clone();
                    if (0 != _actionSimpleBlobData.InputAOIWidth && 0 != _actionSimpleBlobData.InputAOIHeight)
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
                imageBox1.Image = _actionSimpleBlob.imageInput;
                bImageShow = false;
            }
            else
            {
                imageBox1.Image = _actionSimpleBlob.imageResult;
                bImageShow = true;
            }
        }

       

    }
      
}
   
