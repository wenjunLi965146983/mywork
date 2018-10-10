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

namespace WorldGeneralLib.Vision.Actions.EdgePosition
{
    public partial class FormActionEdgePosition : Form
    {

        private ActionEdgePositionData _actionEdgePositionData;
        private ActionEdgePosition _actionEdgePosition;
        private Rectangle rectangle;
        private Rectangle rectROI;
        private bool bMouseDown;
        private bool bMouseDownIm3;
        private Image<Gray, byte> imageInput;
        private bool bImageShow;
        private Image<Gray, byte> _imageShow;

        private Image<Gray, byte> _modelImage;
        public FormActionEdgePosition(ActionEdgePositionData data, ActionEdgePosition actionEdgePosition)
        {
            InitializeComponent();
            bMouseDownIm3 = false;
            bMouseDown = false;
            _actionEdgePositionData = data;
            _actionEdgePosition = actionEdgePosition;
            label8.Text = String.Format("ROI:X:{0},Y{1}\r\nWidth:{2},Height:{3}", _actionEdgePositionData.InputAOIX, _actionEdgePositionData.InputAOIY, _actionEdgePositionData.InputAOIWidth, _actionEdgePositionData.InputAOIHeight);

            rbROIReset.Checked = _actionEdgePositionData.bROIReset;
            cbDirect.SelectedIndex = _actionEdgePositionData.direct;
            tbThreshold.Text = Convert.ToString(_actionEdgePositionData.threshold);
            cbMaxValue.Text = Convert.ToString(_actionEdgePositionData.maxValue);
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
            cmbImageSrc.SelectedIndex = _actionEdgePositionData.imageSrc;
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

                    _actionEdgePosition.imageTemple = new Image<Gray, byte>(mat.Bitmap);
                    _modelImage = _actionEdgePosition.imageTemple.Clone();
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
            String filename = @".//Parameter/Model/EdgePosition Model/model.jpg";
            if (null == _actionEdgePosition.imageTemple)
            {
                MessageBox.Show("Can't Find any Model");
            }
            else
            {
                try
                {
                    _actionEdgePosition.imageTemple.Save(filename);
                    _actionEdgePositionData.InputAOIX = rectangle.X;
                    _actionEdgePositionData.InputAOIY = rectangle.Y;
                    _actionEdgePositionData.InputAOIWidth = rectangle.Width;
                    _actionEdgePositionData.InputAOIHeight = rectangle.Height;
                    _actionEdgePositionData.imageSrc = cmbImageSrc.SelectedIndex;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            _actionEdgePositionData.direct = cbDirect.SelectedIndex;
            _actionEdgePositionData.threshold = Convert.ToInt32(tbThreshold.Text);
            _actionEdgePositionData.maxValue = Convert.ToInt32(cbMaxValue.Text);


        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            if (null != _actionEdgePositionData)
            {

                if (null != _actionEdgePosition.imageTemple)
                {
                    try
                    {

                        _modelImage = _actionEdgePosition.imageTemple.Clone();
                        this.rectangle = new Rectangle(_actionEdgePositionData.InputAOIX, _actionEdgePositionData.InputAOIY, _actionEdgePositionData.InputAOIWidth, _actionEdgePositionData.InputAOIHeight);
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
                imageBox1.Image = _actionEdgePosition.imageInput;
                label7.Text = "输入";
                bImageShow = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (0 == _actionEdgePositionData.imageSrc)
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
                        _actionEdgePosition.run(new Image<Gray, byte>(mat.Bitmap));


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                _actionEdgePosition.ActionExcute();

            }
            _imageShow = _actionEdgePosition.imageResult.Clone();

            if (_actionEdgePosition.imageResult.IsROISet)
            {
                Rectangle rect = new Rectangle(0, 0, _actionEdgePosition.imageInput.Width, _actionEdgePosition.imageInput.Height);
                _imageShow.ROI = rect;
                _imageShow.Draw(_actionEdgePosition.imageResult.ROI, new Gray(255), 3);
                _imageShow.Draw(new CircleF(_actionEdgePosition.edgePoint, 3), new Gray(120), 3);
            }
            imageBox1.Image = _imageShow;


        }

        private void rbROIReset_Click(object sender, EventArgs e)
        {
            if (_actionEdgePositionData.bROIReset)
            {
                _actionEdgePositionData.bROIReset = false;
                rbROIReset.Checked = false;
            }
            else
            {
                _actionEdgePositionData.bROIReset = true;
                rbROIReset.Checked = true;
            }
        }

        private void cbEdgePositionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void tbThreshold_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(tbThreshold.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
            {
                e.Handled = true;
            }
        }

        private void cbMaxValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            Regex rg = new Regex("^([0-9]*)$");
            if (!rg.IsMatch(cbMaxValue.Text + e.KeyChar.ToString()) && 8 != e.KeyChar)
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
   
