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

namespace WorldGeneralLib.Vision.Actions.BrightCorrect
{
    public partial class FormActionBrightCorrect : Form
    {

        private ActionBrightCorrectData _actionBrightCorrectData;
        private ActionBrightCorrect _actionBrightCorrect;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionBrightCorrect(ActionBrightCorrectData data, ActionBrightCorrect actionBrightCorrect)
        {
            InitializeComponent();
            bMouseDownIm3 = false;
            bMouseDown = false;
            _actionBrightCorrectData = data;
            _actionBrightCorrect = actionBrightCorrect;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionBrightCorrectData.InputAOIX, _actionBrightCorrectData.InputAOIY, _actionBrightCorrectData.InputAOIWidth, _actionBrightCorrectData.InputAOIHeight);
            
            rbROIReset.Checked = _actionBrightCorrectData.bROIReset;
            tbScale.Value = _actionBrightCorrectData.iScale;
            label6.Text = Convert.ToString(_actionBrightCorrectData.iScale);
            rbDirect.Checked = _actionBrightCorrectData.bDirect;
           

            tbInputScale.Value = _actionBrightCorrectData.iInputScale;
            label1.Text = Convert.ToString(_actionBrightCorrectData.iInputScale);
            rbBrightDirect.Checked = _actionBrightCorrectData.bBrightDirect;

            rbEqualize.Checked = _actionBrightCorrectData.bEqualize;

            if (null != _actionBrightCorrect.imageBright)
            {
                imageBox3.Image = _actionBrightCorrect.imageBright;
            }
            if (null != _actionBrightCorrect.imageTemple)
            {
                imageBox2.Image = _actionBrightCorrect.imageTemple;
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
            dr["name"] = i.ToString() + ":本地相册";
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
            cmbImageSrc.SelectedIndex = _actionBrightCorrectData.imageSrc;
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

                    _actionBrightCorrect.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionBrightCorrect.imageTemple.Clone();
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
            String filename = @".//Parameter/Model/BrightCorrect Model/model.jpg";
            if (null == _actionBrightCorrect.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionBrightCorrect.imageTemple.Save(filename);
                _actionBrightCorrectData.InputAOIX = rectangle.X;
                _actionBrightCorrectData.InputAOIY = rectangle.Y;
                _actionBrightCorrectData.InputAOIWidth = rectangle.Width;
                _actionBrightCorrectData.InputAOIHeight = rectangle.Height;

                filename = @".//Parameter/Model/BrightCorrect Model/brightmodel.jpg";
                if(null!=_actionBrightCorrect.imageBright)
                _actionBrightCorrect.imageBright.Save(filename);
                _actionBrightCorrectData.imageSrc = cmbImageSrc.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionBrightCorrectData)
            {

                if (null != _actionBrightCorrect.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionBrightCorrect.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionBrightCorrectData.InputAOIX, _actionBrightCorrectData.InputAOIY, _actionBrightCorrectData.InputAOIWidth, _actionBrightCorrectData.InputAOIHeight);
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
                imageBox1.Image = _actionBrightCorrect.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }
   

        private void button1_Click(object sender, EventArgs e)
        {
            if (0 == _actionBrightCorrectData.imageSrc)
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

                        Image<Gray, byte> image = new Image<Gray, byte>(mat.Bitmap);
                        _actionBrightCorrect.run(image);
                     

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionBrightCorrect.ActionExcute();
              
            }
            _imageShow = _actionBrightCorrect.imageResult.Clone();

            if (_actionBrightCorrect.imageResult.IsROISet)
            {
                Rectangle rect = new Rectangle(0, 0, _actionBrightCorrect.imageInput.Width, _actionBrightCorrect.imageInput.Height);
                _imageShow.ROI = rect;


                _imageShow.Draw(_actionBrightCorrect.imageResult.ROI, new Gray(255), 3);


            }
            imageBox1.Image = _imageShow;

        }

       

       

        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionBrightCorrectData.bROIReset)
            {
                _actionBrightCorrectData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionBrightCorrectData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void btnCreateBriModel_Click(object sender, EventArgs e)
        {
            try
            {
                if(0!=_actionBrightCorrectData.InputAOIWidth&&0!= _actionBrightCorrectData.InputAOIHeight)
                {
                    _actionBrightCorrect.imageBright = new Image<Gray, byte>(_actionBrightCorrectData.InputAOIWidth, _actionBrightCorrectData.InputAOIHeight, new Gray(_actionBrightCorrectData.iScale));
                }
                else
                {
                    _actionBrightCorrect.imageBright = new Image<Gray, byte>(_actionBrightCorrect.imageTemple.Width, _actionBrightCorrect.imageTemple.Height, new Gray(_actionBrightCorrectData.iScale));
                }
            }
                
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            imageBox3.Image = _actionBrightCorrect.imageBright;
        }

        private void tbScale_ValueChanged(object sender, EventArgs e)
        {
            _actionBrightCorrectData.iScale = tbScale.Value;
            label6.Text = Convert.ToString(tbScale.Value);
        }

      

        private void rbDirect_Click(object sender, EventArgs e)
        {
            if (_actionBrightCorrectData.bDirect)
            {
                _actionBrightCorrectData.bDirect = false;
                rbDirect.Checked = false;

            }
            else
            {
               
                _actionBrightCorrectData.bDirect = true;
                rbDirect.Checked = true;
            }
        }

        private void tbInputScale_ValueChanged(object sender, EventArgs e)
        {
             _actionBrightCorrectData.iInputScale = tbInputScale.Value;
            label1.Text = Convert.ToString(tbInputScale.Value);
        }

        private void btnBriModel_Click(object sender, EventArgs e)
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
                    Image<Gray,byte> image = new Image<Gray, byte>(mat.Bitmap);
                    if (0 != rectangle.Width && 0 != rectangle.Height)
                    {
                       
                        image.ROI = rectangle;

                    }
                    _actionBrightCorrect.imageBright = image;
                    imageBox3.Image = _actionBrightCorrect.imageBright;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbBrightDirect_Click(object sender, EventArgs e)
        {

            if (_actionBrightCorrectData.bBrightDirect)
            {
                _actionBrightCorrectData.bBrightDirect = false;
                rbBrightDirect.Checked = false;

            }
            else
            {

                _actionBrightCorrectData.bBrightDirect = true;
                rbBrightDirect.Checked = true;
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            Image<Gray,byte> image=new Image<Gray, byte>(_actionBrightCorrect.imageBright.Width, _actionBrightCorrect.imageBright.Height, new Gray(_actionBrightCorrectData.iInputScale));
            if (_actionBrightCorrectData.bBrightDirect)
            {
               
             
                CvInvoke.Subtract(_actionBrightCorrect.imageBright, image, _actionBrightCorrect.imageBright);
            }
            else
            {
                CvInvoke.Add(_actionBrightCorrect.imageBright, image, _actionBrightCorrect.imageBright);
            }
            imageBox3.Image = _actionBrightCorrect.imageBright;
        }

        private void rbEqualize_Click(object sender, EventArgs e)
        {
            if (_actionBrightCorrectData.bEqualize)
            {
                rbEqualize.Checked = false;
                _actionBrightCorrectData.bEqualize = false;

            }
            else
            {
                rbEqualize.Checked = true;
                _actionBrightCorrectData.bEqualize = true;
            }
        }

        private void cmbImageSrc_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
   
