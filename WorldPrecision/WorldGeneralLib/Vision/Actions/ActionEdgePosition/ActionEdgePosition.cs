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

namespace WorldGeneralLib.Vision.Actions.EdgePosition
{
    public class ActionEdgePosition:ActionBase,IAction
    {
        public ActionEdgePositionData actionEdgePositionData;
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

        public  Point edgePoint;

        public override void ActionExcute()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc-1].imageResult.Clone();
         
         
            if (0==actionEdgePositionData.InputAOIWidth && 0 == actionEdgePositionData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionEdgePositionData.InputAOIX, actionEdgePositionData.InputAOIY, actionEdgePositionData.InputAOIWidth, actionEdgePositionData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }

            int[][] point = new int[_imageInput.Height - 2][];
            int number = 0;
            int x = 0;
            int y = 0;
            ConvolutionKernelF convolutionKernelF;

            Image<Gray, byte> _image = _imageInput.Clone();
            CvInvoke.Threshold(_image,_image, actionEdgePositionData.threshold, actionEdgePositionData.maxValue,ThresholdType.Binary);
            

            switch (actionEdgePositionData.direct)
            {
                case 0:
                    float[,] data1 = { { 1, 1, 1, 1, 0 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 30}, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 0} };
                    convolutionKernelF = new ConvolutionKernelF(data1);
                    break;
                case 1:
                    float[,] data2 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data2);
                    break;
                case 2:
                    float[,] data3 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data3);
                    break;
                case 3:
                    float[,] data4 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data4);
                    
                    break;
                default:
                    return;


            }
            _image = new Image<Gray, byte>(_imageInput.Convolution(convolutionKernelF).Bitmap);
            _image = _image.AbsDiff(new Gray(92));
            CvInvoke.Threshold(_image, _image, 30, 255, ThresholdType.BinaryInv);
            if (actionEdgePositionData.direct < 3)
            {

                Parallel.For(2, _image.Height - 3, item =>
                {

                    for (int i = 2; i < _image.Width - 3; i++)
                    {
                        if (CvInvoke.cvGet2D(_image, (int)item, i).Equals(new MCvScalar(255)))
                        {
                            point[item] = new int[2];
                            point[item][0] = (int)item;
                            point[item][1] = i;

                            number++;
                            x = item + x;
                            y = i + y;
                            i = _image.Width;
                        }
                        ;
                    }


                });
                double xy = 0;
                double powx = 0;
                double k = 0;
                double b = 0;
                foreach (int[] p in point)
                {
                    if (null != p)
                    {
                        xy = xy + p[0] * p[1];
                        powx = powx + p[0] * p[0];
                    }
                    double a = powx - x * x / number;
                    if (0 == a) { a = 0.000001; }
                    k = (xy - (y * x) / number) / a;
                    b = y / number - k * x / number;
                }
                edgePoint.X =x/number + actionEdgePositionData.InputAOIX;
                edgePoint.Y = y/number + actionEdgePositionData.InputAOIY;


            }
            else
            {
                Parallel.For(2, _image.Width - 3, item =>
                {

                    for (int i = 2; i < _image.Height - 3; i++)
                    {
                        if (CvInvoke.cvGet2D(_image, (int)item, i).Equals(new MCvScalar(255)))
                        {
                            point[item] = new int[2];
                            point[item][0] = (int)item;
                            point[item][1] = i;

                            number++;
                            x = item + x;
                            y = i + y;
                            i = _image.Width;
                        }
                        ;
                    }
                    double xy = 0;
                    double powx = 0;
                    double k = 0;
                    double b = 0;
                    foreach (int[] p in point)
                    {
                        if (null != p)
                        {
                            xy = xy + p[0] * p[1];
                            powx = powx + p[0] * p[0];
                        }
                        double a = powx - x * x / number;
                        if (0 == a) { a = 0.000001; }
                        k = (xy - (y * x) / number) / a;
                        b = y / number - k * x / number;
                    }
                    edgePoint.X = Convert.ToInt16(2 * k + b) + actionEdgePositionData.InputAOIX;
                    edgePoint.Y = 2 + actionEdgePositionData.InputAOIY;



                });
            }




            sw.Stop();
            _imageResult = _imageInput.Clone();

            _image.CopyTo(_imageResult);
            if (actionEdgePositionData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);

            
        }
        public void run(Image<Gray,byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = image;

            if (0 == actionEdgePositionData.InputAOIWidth && 0 == actionEdgePositionData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionEdgePositionData.InputAOIX, actionEdgePositionData.InputAOIY, actionEdgePositionData.InputAOIWidth, actionEdgePositionData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }

            int[][] point = new int[_imageInput.Height - 2][];
            int number = 0;
            int x = 0;
            int y = 0;
            ConvolutionKernelF convolutionKernelF;

            Image<Gray, byte> _image = _imageInput.Clone();
            CvInvoke.Threshold(_image, _image, actionEdgePositionData.threshold, actionEdgePositionData.maxValue, ThresholdType.Binary);


            switch (actionEdgePositionData.direct)
            {
                case 0:
                    float[,] data1 = { { 1, 1, 1, 1, 0 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 30 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 0 } };
                    convolutionKernelF = new ConvolutionKernelF(data1);
                    break;
                case 1:
                    float[,] data2 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data2);
                    break;
                case 2:
                    float[,] data3 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data3);
                    break;
                case 3:
                    float[,] data4 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 10 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    convolutionKernelF = new ConvolutionKernelF(data4);

                    break;
                default:
                    return;


            }
            _image = new Image<Gray, byte>(_imageInput.Convolution(convolutionKernelF).Bitmap);
            _image = _image.AbsDiff(new Gray(92));
            CvInvoke.Threshold(_image, _image, 30, 255, ThresholdType.BinaryInv);
            if (actionEdgePositionData.direct < 3)
            {

                Parallel.For(2, _image.Height - 3, item =>
                {

                    for (int i = 2; i < _image.Width - 3; i++)
                    {
                        if (CvInvoke.cvGet2D(_image, (int)item, i).Equals(new MCvScalar(255)))
                        {
                            point[item] = new int[2];
                            point[item][0] = (int)item;
                            point[item][1] = i;

                            number++;
                            x = item + x;
                            y = i + y;
                            i = _image.Width;
                        }
                        ;
                    }


                });
                double xy = 0;
                double powx = 0;
                double k = 0;
                double b = 0;
                foreach (int[] p in point)
                {
                    if (null != p)
                    {
                        xy = xy + p[0] * p[1];
                        powx = powx + p[0] * p[0];
                    }
                    double a = powx - x * x / number;
                    if (0 == a) { a = 0.000001; }
                    k = (xy - (y * x) / number) / a;
                    b = y / number - k * x / number;
                }
                edgePoint.X = x / number + actionEdgePositionData.InputAOIX;
                edgePoint.Y = y / number + actionEdgePositionData.InputAOIY;


            }
            else
            {
                Parallel.For(2, _image.Width - 3, item =>
                {

                    for (int i = 2; i < _image.Height - 3; i++)
                    {
                        if (CvInvoke.cvGet2D(_image, (int)item, i).Equals(new MCvScalar(255)))
                        {
                            point[item] = new int[2];
                            point[item][0] = (int)item;
                            point[item][1] = i;

                            number++;
                            x = item + x;
                            y = i + y;
                            i = _image.Width;
                        }
                        ;
                    }
                    double xy = 0;
                    double powx = 0;
                    double k = 0;
                    double b = 0;
                    foreach (int[] p in point)
                    {
                        if (null != p)
                        {
                            xy = xy + p[0] * p[1];
                            powx = powx + p[0] * p[0];
                        }
                        double a = powx - x * x / number;
                        if (0 == a) { a = 0.000001; }
                        k = (xy - (y * x) / number) / a;
                        b = y / number - k * x / number;
                    }
                    edgePoint.X = Convert.ToInt16(2 * k + b) + actionEdgePositionData.InputAOIX;
                    edgePoint.Y = 2 + actionEdgePositionData.InputAOIY;



                });
            }




            sw.Stop();
            _imageResult = _imageInput.Clone();

            _image.CopyTo(_imageResult);
            if (actionEdgePositionData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionEdgePosition(ActionEdgePositionData actionEdgePositionData)
        {
            actionData = actionEdgePositionData;
            actionData.Name = actionEdgePositionData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionEdgePosition)(new FormActionEdgePosition(actionEdgePositionData, this));
            this.actionEdgePositionData = actionEdgePositionData;
            Init();
        }
        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/EdgePosition Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);       
            }
            catch (Exception)
            {
              
            }
          
            }
            
        }

        //Action处理方法
        //TODO
    
}
