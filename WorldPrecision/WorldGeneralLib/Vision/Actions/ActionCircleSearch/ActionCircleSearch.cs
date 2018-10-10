using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldGeneralLib.Vision.Actions;
using HalconDotNet;
using Emgu.CV;
using Emgu.CV.XFeatures2D;
using Emgu.CV.Util;
using Emgu.CV.Features2D;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace WorldGeneralLib.Vision.Actions.CircleSearch
{
    public class ActionCircleSearch : ActionBase, IAction
    {
        public ActionCircleSearchData actionCircleSearchData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageTemple
        {
            get { return _imageTemple; }
            set
            {
                _imageTemple = value;

            }
        }

        private Rectangle rectangle;

        public Point startPoint;

        public Point endPoint;


        public override void ActionExcute()
        {
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc - 1].imageResult.Clone();

            if (0 == actionCircleSearchData.InputAOIWidth && 0 == actionCircleSearchData.InputAOIHeight)
            {
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionCircleSearchData.InputAOIX, actionCircleSearchData.InputAOIY, actionCircleSearchData.InputAOIWidth, actionCircleSearchData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            Image<Gray, byte> _image = _imageInput.Clone();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _image = _image.Canny(100, 255, 1, false);
           

            sw.Stop();
            _imageResult = _imageInput.Clone();
         
            _image.CopyTo(_imageResult);
            if (actionCircleSearchData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }
        public void run(Image<Gray, byte> image)
        {
            _imageInput = image;
           

            if (0 == actionCircleSearchData.InputAOIWidth && 0 == actionCircleSearchData.InputAOIHeight)
            {
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionCircleSearchData.InputAOIX, actionCircleSearchData.InputAOIY, actionCircleSearchData.InputAOIWidth, actionCircleSearchData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            Image<Gray, byte> _image = _imageInput.Clone();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            double dtime =_image.Width < _image.Height ? Math.Log(_image.Width,2) : Math.Log(_image.Height,2);
            int time = Convert.ToInt32(dtime);
            for (int i = 0; i <0; i++)
            {
                if (_image.Width % 2 == 1 && _image.Height % 2 == 0)
                {
                    CvInvoke.Resize(_image, _image, new Size(_image.Width - 1, _image.Height));
                }
                if (_image.Width % 2 == 0 && _image.Height % 2 == 1)
                {
                    CvInvoke.Resize(_image, _image, new Size(_image.Width, _image.Height - 1));
                }
                if (_image.Width % 2 == 1 && _image.Height % 2 == 1)
                {
                    CvInvoke.Resize(_image, _image, new Size(_image.Width - 1, _image.Height - 1));
                }

                _image = _image.PyrDown();
            }
            Image<Gray, byte> map = new Image<Gray, byte>(_image.Size);
            _image = _image.Canny(150, 250);
          


            sw.Stop();
           
            _imageResult = map.Clone();
            //_image.CopyTo(_imageResult);

            if (actionCircleSearchData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionCircleSearch(ActionCircleSearchData actionCircleSearchData)
        {
            actionData = actionCircleSearchData;
            actionData.Name = actionCircleSearchData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionCircleSearch)(new FormActionCircleSearch(actionCircleSearchData, this));
            this.actionCircleSearchData = actionCircleSearchData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/Circle Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);
            }
            catch (Exception)
            {

            }

        }

        private Point CalculateCircleCenter(int[] pointA,int[] pointB,int[] pointC,out int radius)
        {
            Point result = new Point();
            float a = pointB[1] - pointA[1];
            float b = pointC[1] - pointA[1];

            if (a == 0)
            {
                a = 0.001f;
            }
            if (b == 0)
            {
                b = 0.001f;
            }

            float k1 = (pointA[0] - pointB[0]) / a;
            float k2 = (pointA[0] - pointC[0]) / b;

            float b1 = ((pointA[1] + pointB[1]) - k1 * (pointA[0] + pointB[0])) / 2;
            float b2 = ((pointA[1] + pointC[1]) - k2 * (pointA[0] + pointC[0])) / 2;
            if (k1 == k2)
            {
                result.X = Convert.ToInt32((b2 - b1) / (0.001));
            }
            else
            {
                result.X = Convert.ToInt32((b2 - b1) / (k1 - k2));
            }
           
            result.Y = Convert.ToInt32(result.X * k1 + b1);


            radius =(int) Math.Sqrt(Math.Pow((result.X- pointA[0]),2)+ Math.Pow((result.Y - pointA[1]), 2));
           
            return result;
        }

        private CircleF GetCircle(int[][] points)
        {
            int number = 0;
            double x1 = 0;
            double y1 = 0;
            double x2 = 0;
            double y2 = 0;
            double x3 = 0;
            double y3 = 0;
            double x1y1 = 0;
            double x1y2 = 0;
            double x2y1 = 0;


            foreach (int[] point in points)
            {
                if (point[2] < 4)
                {
                    number++;
                    x1 += point[0];
                    y1 += point[1];
                    x2 += point[0] * point[0];
                    y2 += point[1] * point[1];
                    x3 += point[0] * point[0] * point[0];
                    y3 += point[1] * point[1] * point[1];
                    x1y1 += point[0] * point[1];
                    x1y2 += point[0] * point[1] * point[1];
                    x2y1 += point[0] * point[0] * point[1];

                }
               
            }
            double C, D, E, G, H, N;
            double a, b, c;
            C = number * x2 - x1 * x1;
            D = number * x1y1 - x1 * y1;
            E = number * x3 + number * x1y2 - (x2 + y2) * x1;
            G = number * y2 - y1 * y1;
            H = number * x2y1 + number * y3 - (x2 + y2) * y1;
            a = (H * D - E * G) / (C * G - D * D);
            b = (H * C - E * D) / (D * D - G * C);
            c = -(a * x1 + b * y1 + x2 + y2) / number;

            float A, B, R;
            A = (float)a / (-2);
            B = (float)b / (-2);
            R = (float)Math.Sqrt(a * a + b * b - 4 * c) / 2;
            return new CircleF(new PointF(A,B), R);
        }
       
    }
}

    

