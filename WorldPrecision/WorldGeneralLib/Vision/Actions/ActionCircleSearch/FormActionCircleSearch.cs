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

namespace WorldGeneralLib.Vision.Actions.CircleSearch
{
    public partial class FormActionCircleSearch : Form
    {

        private ActionCircleSearchData _actionCircleSearchData;
        private ActionCircleSearch _actionCircleSearch;
        private CircleF circle;
   
        private bool bMouseDown;
     
    
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionCircleSearch(ActionCircleSearchData data, ActionCircleSearch actionCircleSearch)
        {
            InitializeComponent();
           
            bMouseDown = false;
            _actionCircleSearchData = data;
            _actionCircleSearch = actionCircleSearch;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionCircleSearchData.InputAOIX, _actionCircleSearchData.InputAOIY, _actionCircleSearchData.InputAOIWidth, _actionCircleSearchData.InputAOIHeight);

            rbROIReset.Checked = _actionCircleSearchData.bROIReset;
            

            FormVision.eventRun += new FormVision.formRefresh(init);
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
                dr["value"] =i;
                dtImage.Rows.Add(dr);
            }
            cmbImageSrc.DataSource = dtImage;
            cmbImageSrc.DisplayMember = "name";
            cmbImageSrc.ValueMember = "value";
            cmbImageSrc.SelectedIndex = _actionCircleSearchData.imageSrc;
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

                    _actionCircleSearch.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionCircleSearch.imageTemple.Clone();
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
            PointF pointF = new PointF(_modelImage.Width * e.X / imageBox2.Width, _modelImage.Height * e.Y / imageBox2.Height);
            circle .Center= pointF;
        }

        private void imageBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (!bMouseDown)
            {
                return;
            }
            int x = _modelImage.Width * e.X / imageBox2.Width;
            int y = _modelImage.Height * e.Y / imageBox2.Height;
            circle.Radius =(float) Math.Sqrt(Math.Pow((circle.Center.X - x), 2) + Math.Pow((circle.Center.X - x), 2));



            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nR:{2}", circle.Center.X, circle.Center.Y, circle.Radius);
            Image<Gray, byte> image = _modelImage.Clone();
            image.Draw(circle, new Gray(255), 3);
            imageBox2.Image = image;


        }

        private void imageBox2_MouseUp(object sender, MouseEventArgs e)
        {
            bMouseDown = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            String filename = @".//Parameter/Model/Circle Model/model.jpg";
            if (null == _actionCircleSearch.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
            }
            else
            {
                try
                {
                    _actionCircleSearch.imageTemple.Save(filename);

                    _actionCircleSearchData.imageSrc = cmbImageSrc.SelectedIndex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //圆形区域
            _actionCircleSearchData.InputAOIX = (int)circle.Center.X;
            _actionCircleSearchData.InputAOIY = (int)circle.Center.Y;
            _actionCircleSearchData.ROICircleR = (int)circle.Radius;


            //霍夫变换参数
      

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionCircleSearchData)
            {

                if (null != _actionCircleSearch.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionCircleSearch.imageTemple.Clone();
                        //this.rectangle = new Rectangle(_actionCircleData.InputAOIX, _actionCircleData.InputAOIY, _actionCircleData.InputAOIWidth, _actionCircleData.InputAOIHeight);
                        _imageShow = _modelImage.Clone();
                        //_imageShow.Draw(rectangle, new Gray(255), 3);
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
                imageBox1.Image = _actionCircleSearch.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
           
               if (0 == _actionCircleSearchData.imageSrc)
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
                        _actionCircleSearch.run(new Image<Gray, byte>(mat.Bitmap)); 

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    _actionCircleSearch.ActionExcute();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
               
            _imageShow = _actionCircleSearch.imageResult.Clone();

            if (_actionCircleSearch.imageResult.IsROISet)
            {
                Rectangle rect = new Rectangle(0, 0, _actionCircleSearch.imageInput.Width, _actionCircleSearch.imageInput.Height);
                _imageShow.ROI = rect;


                _imageShow.Draw(_actionCircleSearch.imageResult.ROI, new Gray(255), 3);
              
              


            }
            imageBox1.Image = _imageShow;
        }



        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionCircleSearchData.bROIReset)
            {
                _actionCircleSearchData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionCircleSearchData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void cbCircleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

 

      
    }
}
   
