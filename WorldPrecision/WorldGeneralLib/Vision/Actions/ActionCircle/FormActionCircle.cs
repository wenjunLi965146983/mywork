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

namespace WorldGeneralLib.Vision.Actions.Circle
{
    public partial class FormActionCircle : Form
    {

        private ActionCircleData _actionCircleData;
        private ActionCircle _actionCircle;
        private CircleF circle;
   
        private bool bMouseDown;
     
    
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionCircle(ActionCircleData data, ActionCircle actionCircle)
        {
            InitializeComponent();
           
            bMouseDown = false;
            _actionCircleData = data;
            _actionCircle = actionCircle;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionCircleData.InputAOIX, _actionCircleData.InputAOIY, _actionCircleData.InputAOIWidth, _actionCircleData.InputAOIHeight);

            rbROIReset.Checked = _actionCircleData.bROIReset;
            

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
            cmbImageSrc.SelectedIndex = _actionCircleData.imageSrc;
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

                    _actionCircle.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionCircle.imageTemple.Clone();
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
            if (null == _actionCircle.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
            }
            else
            {
                try
                {
                    _actionCircle.imageTemple.Save(filename);
                  
                    _actionCircleData.imageSrc = cmbImageSrc.SelectedIndex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            //圆形区域
            _actionCircleData.InputAOIX = (int)circle.Center.X;
            _actionCircleData.InputAOIY = (int)circle.Center.Y;
            _actionCircleData.ROICircleR = (int)circle.Radius;


            //霍夫变换参数
      

        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionCircleData)
            {

                if (null != _actionCircle.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionCircle.imageTemple.Clone();
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
                imageBox1.Image = _actionCircle.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
           
               if (0 == _actionCircleData.imageSrc)
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
                        _actionCircle.run(new Image<Gray, byte>(mat.Bitmap)); 

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
                    _actionCircle.ActionExcute();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
               
            _imageShow = _actionCircle.imageResult.Clone();

            if (_actionCircle.imageResult.IsROISet)
            {
                Rectangle rect = new Rectangle(0, 0, _actionCircle.imageInput.Width, _actionCircle.imageInput.Height);
                _imageShow.ROI = rect;


                _imageShow.Draw(_actionCircle.imageResult.ROI, new Gray(255), 3);
              
              


            }
            imageBox1.Image = _imageShow;
        }



        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionCircleData.bROIReset)
            {
                _actionCircleData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionCircleData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void cbCircleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

 

      
    }
}
   
