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

namespace WorldGeneralLib.Vision.Actions.Circle
{
    public class ActionCircle : ActionBase, IAction
    {
        public ActionCircleData actionCircleData;
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

            if (0 == actionCircleData.InputAOIWidth && 0 == actionCircleData.InputAOIHeight)
            {
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionCircleData.InputAOIX, actionCircleData.InputAOIY, actionCircleData.InputAOIWidth, actionCircleData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            Image<Gray, byte> _image = _imageInput.Clone();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int count = 4 * (actionCircleData.ROICircleR / 10);
            if (0 == actionCircleData.InputAOIWidth && 0 == actionCircleData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionCircleData.InputAOIX, actionCircleData.InputAOIY, actionCircleData.InputAOIWidth, actionCircleData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }
            int[][] point = new int[count][];
            int[] status = new int[count];
            Parallel.For(0, count, item =>
            {
                point[item] = new int[3];
                point[item][2] = 6;
                status[item] = 6;
                for (int i = 0; i < actionCircleData.ROICircleR; i++)
                {
                    double angle = 2 * Math.PI * item / count;
                    int x = Convert.ToInt32(i* Math.Cos(angle)) + actionCircleData.InputAOIX;
                    int y = Convert.ToInt32(i * Math.Sin(angle)) + actionCircleData.InputAOIY;
                    int sum = 0;
                    for (int m = 0; m < 5; m++)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            sum += _image.Data[y - 2 + n, x - 2 + m, 0]/255;
                        }
                    
                    }
                    if(i == actionCircleData.ROICircleR-1)
                    {
                        //MessageBox.Show(""); ;
                        
                    }
                    if (sum >5&&sum<15)
                    {
                        point[item][0] = x+actionCircleData.ROICircleR- actionCircleData.InputAOIX;
                        point[item][1] = y + actionCircleData.ROICircleR - actionCircleData.InputAOIY;
                        point[item][2] = 0;
                        status[item] = 0;
                        i = actionCircleData.ROICircleR;
                        _image.Draw(new CircleF(new Point(x,y), 1), new Gray(100),1);

                    }
                   
                }
            });
          
            Random random = new Random();
            bool _keepRun = true;
            int numberOfItem = count / 2;
            int _testCount = 0;
            CircleF result = new CircleF();
            try
            {
                while (_keepRun)
                {
                    bool _getRandom = true;
                    int a = -1;
                    int b = -1;
                    int c = -1;
                    int d = -1;
                    int e = -1;
                    int f = -1;
                    CircleF circle1 = new CircleF();
                    CircleF circle2 = new CircleF();
                    int _pointMinDistance = point.Length / 6;



                    int _raCount = 0;

                    while (_getRandom)
                    {

                        _raCount++;

                        if (_raCount > 10000)
                        {
                            // throw (new Exception("角度偏差过大1"));
                            _keepRun = false;
                            _getRandom = false;
                            break;
                        }
                        int rd = random.Next(point.Length - 1);

                        if (-1 == a)
                        {
                            if (point[rd][2] < 4)
                            {
                                a = rd;
                            }
                        }
                        else
                        {
                            if (-1 == b)
                            {

                                if (point[rd][2] < 4 & Math.Abs(a - rd) > _pointMinDistance)
                                {
                                    b = rd;
                                }
                            }
                            else
                            {
                                if (point[rd][2] < 4 & Math.Abs(a - rd) > _pointMinDistance & Math.Abs(b - rd) > _pointMinDistance)
                                {
                                    c = rd;
                                    _getRandom = false;
                                }
                            }
                        }


                    }
                    int[][] _parameter = new int[3][];
                    _parameter[0] = point[a];
                    _parameter[1] = point[b];
                    _parameter[2] = point[c];


                    circle1 = GetCircle(_parameter);

                    _getRandom = true;
                    while (_getRandom)
                    {

                        _raCount++;
                        if (_raCount > 10000)
                        {
                            //throw (new Exception("角度偏差过大1"));
                            _keepRun = false;
                            _getRandom = false;
                        }
                        int rd = random.Next(point.Length - 1);



                        if (-1 == d)
                        {
                            if (point[rd][2] < 4)
                            {
                                d = rd;
                            }
                        }
                        else
                        {
                            if (-1 == e)
                            {

                                if (point[rd][2] < 4 & Math.Abs(d - rd) > _pointMinDistance)
                                {
                                    e = rd;
                                }
                            }
                            else
                            {
                                if (point[rd][2] < 4 & Math.Abs(e - rd) > _pointMinDistance & Math.Abs(d - rd) > _pointMinDistance)
                                {
                                    f = rd;
                                    _getRandom = false;
                                }
                            }
                        }


                    }

                    _parameter[0] = point[d];
                    _parameter[1] = point[e];
                    _parameter[2] = point[f];

                    circle2 = GetCircle(_parameter);


                    if (Math.Abs(circle1.Center.X - circle2.Center.X) < actionCircleData.ROICircleR * 0.2 && Math.Abs(circle1.Center.Y - circle2.Center.Y) < actionCircleData.ROICircleR * 0.2&& Math.Abs(circle1.Radius - circle2.Radius) <5)
                    {
                        _testCount++;
                        if (_testCount > 100)
                        {
                            _keepRun = false;
                        }

                    }
                    else
                    {
                        _testCount = 0;
                        point[a][2]++;
                        point[b][2]++;
                        point[c][2]++;
                        point[d][2]++;
                        point[e][2]++;
                        point[f][2]++;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
               
                   
          
            foreach (int[] p in point)
            {
                if (p[2] >=4)
                {
                    _image.Draw(new CircleF(new Point(p[0], p[1]), 1), new Gray(180),1);
                }
            }
            CircleF _tempCircle = GetCircle(point);
            result = new CircleF(new PointF(_tempCircle.Center.X + actionCircleData.InputAOIX - actionCircleData.ROICircleR, _tempCircle.Center.X + actionCircleData.InputAOIY - actionCircleData.ROICircleR), _tempCircle.Radius);


            sw.Stop();
            _imageResult = _imageInput.Clone();
            _image.Draw(result, new Gray(125),1);
            _image.CopyTo(_imageResult);
            if (actionCircleData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }
        public void run(Image<Gray, byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            int count = 4 * actionCircleData.ROICircleR / 10;


            _imageInput = image;
            if (0 == actionCircleData.InputAOIWidth && 0 == actionCircleData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionCircleData.InputAOIX, actionCircleData.InputAOIY, actionCircleData.InputAOIWidth, actionCircleData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }
            Image<Gray, byte> _image = _imageInput.Clone();
            int[][] point = new int[count][];

            Parallel.For(0, count - 1, item =>
            {
                point[item] = new int[2];
                for (int i = 0; i < actionCircleData.ROICircleR; i++)
                {
                    double angle = 2 * Math.PI * item / count;
                    int x = Convert.ToInt32(i * Math.Cos(angle)) + actionCircleData.InputAOIX;
                    int y = Convert.ToInt32(actionCircleData.ROICircleR * Math.Sin(angle)) + actionCircleData.InputAOIY;
                    int sum = 0;
                    for (int m = 0; m < 5; m++)
                    {
                        for (int n = 0; n < 5; n++)
                        {
                            sum += (_image.Data[y - 3 + n, x - 3 + m, 0]/255);
                        }
                    }
                    if (sum > 10&& sum < 6)
                    {
                        point[item][0] = x;
                        point[item][1] = y;
                        i = actionCircleData.ROICircleR;
                    }

                }
            });
            int[][] center = new int[count / 2][];
            Parallel.For(0, count / 2 - 1, item =>
            {
                center[item] = new int[2];
                center[item][0] = point[item][0] + point[count / 2 + item][0];
                center[item][1] = point[item][1] + point[count / 2 + item][1];
            });
            Random random = new Random();
            bool keepRun = true;
            int number = count / 2;
            while (keepRun)
            {
                bool keepGetRd = true;
                int raCount = 0;
                while (keepGetRd)
                {
                    raCount++;
                    if (raCount > 10000)
                    {
                        throw (new Exception("角度偏差过大1"));
                    }
                    int rd = random.Next(number - 1);

                }
            }


            sw.Stop();
            _imageResult = _imageInput.Clone();

            _image.CopyTo(_imageResult);
            if (actionCircleData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionCircle(ActionCircleData actionCircleData)
        {
            actionData = actionCircleData;
            actionData.Name = actionCircleData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionCircle)(new FormActionCircle(actionCircleData, this));
            this.actionCircleData = actionCircleData;
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

    

