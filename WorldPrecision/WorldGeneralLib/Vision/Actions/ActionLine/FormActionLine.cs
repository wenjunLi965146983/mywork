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

namespace WorldGeneralLib.Vision.Actions.Line
{
    public partial class FormActionLine : Form
    {

        private ActionLineData _actionLineData;
        private ActionLine _actionLine;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionLine(ActionLineData data, ActionLine actionLine)
        {
            InitializeComponent();
            bMouseDownIm3 = false;
            bMouseDown = false;
            _actionLineData = data;
            _actionLine = actionLine;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionLineData.InputAOIX, _actionLineData.InputAOIY, _actionLineData.InputAOIWidth, _actionLineData.InputAOIHeight);

            rbROIReset.Checked = _actionLineData.bROIReset;
            tbCThreshold2.Text = Convert.ToString(_actionLineData.CThreshold2);
            tbCThreshold1.Text = Convert.ToString(_actionLineData.CThreshold1);

            tbrho.Text = Convert.ToString(_actionLineData.rho);
            tbtheta.Text = Convert.ToString(_actionLineData.theta);
            tblength.Text = Convert.ToString(_actionLineData.length);
            tbthreshold.Text = Convert.ToString(_actionLineData.threshold);
            tbgab.Text = Convert.ToString(_actionLineData.gab);

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
            cmbImageSrc.SelectedIndex = _actionLineData.imageSrc;
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

                    _actionLine.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionLine.imageTemple.Clone();
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
            String filename = @".//Parameter/Model/Line Model/model.jpg";
            if (null == _actionLine.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
            }
            else
            {
                try
                {
                    _actionLine.imageTemple.Save(filename);
                    _actionLineData.InputAOIX = rectangle.X;
                    _actionLineData.InputAOIY = rectangle.Y;
                    _actionLineData.InputAOIWidth = rectangle.Width;
                    _actionLineData.InputAOIHeight = rectangle.Height;
                    _actionLineData.imageSrc = cmbImageSrc.SelectedIndex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
            _actionLineData.CThreshold1 = Convert.ToInt32(tbCThreshold1.Text);
            _actionLineData.CThreshold2 = Convert.ToInt32(tbCThreshold2.Text);

            _actionLineData.rho = Convert.ToInt32(tbrho.Text);
            _actionLineData.theta = Convert.ToInt32(tbtheta.Text);
            _actionLineData.threshold = Convert.ToInt32(tbthreshold.Text);
            _actionLineData.length = Convert.ToInt32(tblength.Text);
            _actionLineData.gab = Convert.ToInt32(tbgab.Text);


        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionLineData)
            {

                if (null != _actionLine.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionLine.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionLineData.InputAOIX, _actionLineData.InputAOIY, _actionLineData.InputAOIWidth, _actionLineData.InputAOIHeight);
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
                imageBox1.Image = _actionLine.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }


        private void tbCThreshold1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbCThreshold1.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            if (0 == _actionLineData.imageSrc)
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
                        _actionLine.run(new Image<Gray, byte>(mat.Bitmap)); 

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionLine.ActionExcute();
            }
            _imageShow = _actionLine.imageResult.Clone();

            if (_actionLine.imageResult.IsROISet)
            {
                Rectangle rect = new Rectangle(0, 0, _actionLine.imageInput.Width, _actionLine.imageInput.Height);
                _imageShow.ROI = rect;


                _imageShow.Draw(_actionLine.imageResult.ROI, new Gray(255), 3);
                LineSegment2DF line = new LineSegment2DF(_actionLine.startPoint, _actionLine.endPoint);
                _imageShow.Draw(line, new Gray(120), 1);


            }
            imageBox1.Image = _imageShow;
        }



        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionLineData.bROIReset)
            {
                _actionLineData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionLineData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void cbLineType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _actionLineData.strLineType = cbLineType.Text;
        }

        private void tbCThreshold2_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbCThreshold2.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbrho_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbrho.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbtheta_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbtheta.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tblength_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tblength.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbthreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbthreshold.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void tbgab_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbgab.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
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
   
