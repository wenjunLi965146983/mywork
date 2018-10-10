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

namespace WorldGeneralLib.Vision.Actions.MultiSearch
{
    public partial class FormActionMultiSearch : Form
    {

        private ActionMultiSearchData _actionMultiSearchData;
        private ActionMultiSearch _actionMultiSearch;
        private Rectangle rectangle;
        private Rectangle rectAOI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
       
        private int bImageShow;
        private Image<Gray, byte> _imageShow;
        private Image<Gray, byte> _imageModelShow;

        private Image<Gray, byte> _modelImage;
        public FormActionMultiSearch(ActionMultiSearchData data, ActionMultiSearch actionMultiSearch)
        {
            InitializeComponent();
            bMouseDownIm3 = false;
            bMouseDown = false;
            _actionMultiSearchData = data;
            _actionMultiSearch = actionMultiSearch;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionMultiSearchData.InputAOIX, _actionMultiSearchData.InputAOIY, _actionMultiSearchData.InputAOIWidth, _actionMultiSearchData.InputAOIHeight);
            
            rbROIReset.Checked = _actionMultiSearchData.bROIReset;

            label1.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionMultiSearchData.ModelAOIX, _actionMultiSearchData.ModelAOIY, _actionMultiSearchData.ModelAOIWidth, _actionMultiSearchData.ModelAOIHeight);


            if (null != _actionMultiSearchData)
            {

                if (null != _actionMultiSearch.imageTemple)
                {
                    try
                    {
                        _modelImage = _actionMultiSearch.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionMultiSearchData.InputAOIX, _actionMultiSearchData.InputAOIY, _actionMultiSearchData.InputAOIWidth, _actionMultiSearchData.InputAOIHeight);
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
            if (null != _actionMultiSearchData)
            {

                if (null != _actionMultiSearch.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionMultiSearch.imageTemple.Clone();
                        this.rectAOI= new Rectangle(_actionMultiSearchData.ModelAOIX, _actionMultiSearchData.ModelAOIY, _actionMultiSearchData.ModelAOIWidth, _actionMultiSearchData.ModelAOIHeight);
                        _imageModelShow = _modelImage.Clone();
                        _imageModelShow.Draw(rectangle, new Gray(255), 3);
                        imageBox3.Image = _imageModelShow;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
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
            cmbImageSrc.SelectedIndex = _actionMultiSearchData.imageSrc;
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

                    _actionMultiSearch.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionMultiSearch.imageTemple.Clone();
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
            String filename = @".//Parameter/Model/MultiSearch Model/model.jpg";
            if (null == _actionMultiSearch.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
                return;
            }
            try
            {
                _actionMultiSearch.imageTemple.Save(filename);
                _actionMultiSearchData.InputAOIX = rectangle.X;
                _actionMultiSearchData.InputAOIY = rectangle.Y;
                _actionMultiSearchData.InputAOIWidth = rectangle.Width;
                _actionMultiSearchData.InputAOIHeight = rectangle.Height;

                _actionMultiSearchData.ModelAOIX = rectAOI.X;
                _actionMultiSearchData.ModelAOIY= rectAOI.Y;
                _actionMultiSearchData.ModelAOIWidth = rectAOI.Width;
                _actionMultiSearchData.ModelAOIHeight = rectAOI.Height;

                _actionMultiSearchData.imageSrc = cmbImageSrc.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
          
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionMultiSearchData)
            {

                if (null != _actionMultiSearch.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionMultiSearch.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionMultiSearchData.InputAOIX, _actionMultiSearchData.InputAOIY, _actionMultiSearchData.InputAOIWidth, _actionMultiSearchData.InputAOIHeight);
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

            switch (bImageShow)
            {
                case 1:
                    imageBox1.Image = _actionMultiSearch.result;
                    bImageShow = 2;
                    label7.Text = "1";
                    break;
                case 2:
                    imageBox1.Image = _actionMultiSearch.result1;
                    bImageShow = 3;
                    label7.Text = "2";
                    break;
                case 3:
                    imageBox1.Image = _actionMultiSearch.result2;
                    bImageShow = 4;
                    label7.Text = "3";
                    break;
                case 4:
                    imageBox1.Image = _actionMultiSearch.imageInput;
                    bImageShow = 1;
                    label7.Text = "4";
                    break;
                default:
              
                    imageBox1.Image = _actionMultiSearch.result;
                    bImageShow = 2;
                    label7.Text = "输出";
                    break;
            }







        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (0 == _actionMultiSearchData.imageSrc)
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

                      
                        _actionMultiSearch.run(new Image<Gray, byte>(mat.Bitmap));
                        //_imageShow =_actionMultiSearch.imageResult.Clone();

                        //if(0!=rectangle.Width&&0!= rectangle.Height)
                        //{
                        //    Rectangle rect = new Rectangle(0, 0, _actionMultiSearch.imageInput.Width, _actionMultiSearch.imageInput.Height);
                        //    _imageShow.ROI = rect;


                        //    _imageShow.Draw(rectangle, new Gray(255), 3);


                        //}
                        imageBox1.Image = _actionMultiSearch.result;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionMultiSearch.ActionExcute();
                imageBox1.Image = _actionMultiSearch.result;
            }
        }

        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionMultiSearchData.bROIReset)
            {
                _actionMultiSearchData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionMultiSearchData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }
        private void rbDirect_Click(object sender, EventArgs e)
        {
            if (_actionMultiSearchData.bDirect)
            {
                _actionMultiSearchData.bDirect = false;
                rbDirect.Checked = false;

            }
            else
            {
               
                _actionMultiSearchData.bDirect = true;
                rbDirect.Checked = true;
            }
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
                

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void rbEqualize_Click(object sender, EventArgs e)
        {
            if (_actionMultiSearchData.bEqualize)
            {
                rbEqualize.Checked = false;
                _actionMultiSearchData.bEqualize = false;

            }
            else
            {
                rbEqualize.Checked = true;
                _actionMultiSearchData.bEqualize = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (null != _actionMultiSearchData)
            {

                if (null != _actionMultiSearch.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionMultiSearch.imageTemple.Clone();
                        this.rectAOI = new Rectangle(_actionMultiSearchData.ModelAOIX, _actionMultiSearchData.ModelAOIY, _actionMultiSearchData.ModelAOIWidth, _actionMultiSearchData.ModelAOIHeight);
                        _imageModelShow = _modelImage.Clone();
                        _imageModelShow.Draw(rectangle, new Gray(255), 3);
                        imageBox3.Image = _imageModelShow;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
        }

        private void imageBox3_MouseDown(object sender, MouseEventArgs e)
        {
            bMouseDownIm3 = true;
            rectAOI.X = _modelImage.Width * e.X / imageBox2.Width;
            rectAOI.Y = _modelImage.Height * e.Y / imageBox2.Height;
        }

        private void imageBox3_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bMouseDownIm3)
            {
                return;
            }
            int x = _modelImage.Width * e.X / imageBox2.Width;
            int y = _modelImage.Height * e.Y / imageBox2.Height;
            if (x > rectAOI.X)
            {
                rectAOI.Width = _modelImage.Width < x - rectAOI.X ? _modelImage.Width - rectAOI.X : x - rectAOI.X;
            }
            else
            {
                rectAOI.X = x;
            }
            if (y > rectAOI.Y)
            {
                rectAOI.Height = _modelImage.Height < y - rectAOI.Y ? _modelImage.Height - rectAOI.Y : y - rectAOI.Y;
            }
            else
            {

                rectAOI.Y = y;
            }
            label1.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", rectAOI.X, rectAOI.Y, rectAOI.Width, rectAOI.Height);
            Image<Gray, byte> image = _modelImage.Clone();
            image.Draw(rectAOI, new Gray(255), 3);
            imageBox3.Image = image;
        }

        private void imageBox3_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDownIm3 = false;
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
   
