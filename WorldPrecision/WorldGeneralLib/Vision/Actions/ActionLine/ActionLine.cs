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

namespace WorldGeneralLib.Vision.Actions.Line
{
    public class ActionLine:ActionBase,IAction
    {
        public ActionLineData actionLineData;
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

        public  Point startPoint;

        public  Point endPoint;


        public override void ActionExcute()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc-1].imageResult.Clone();
         
         
            if (0==actionLineData.InputAOIWidth && 0 == actionLineData.InputAOIHeight)
            {
               // CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionLineData.InputAOIX, actionLineData.InputAOIY, actionLineData.InputAOIWidth, actionLineData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }

            
            Image<Gray, byte> _image = _imageInput.Clone();

            switch (actionLineData.strLineType)
            {
                case "霍夫直线":
                     LineSegment2D[][] lines = _imageInput.HoughLines(actionLineData.CThreshold1, actionLineData.CThreshold2, actionLineData.rho, Math.PI / actionLineData.theta, actionLineData.threshold, actionLineData.length, actionLineData.gab);

                    foreach (LineSegment2D[] line in lines)
                    {
                        foreach (LineSegment2D l in line)
                        {
                           _image.Draw(l, new Gray(125), 3);
                        }
                    }
                    break;
                case "最小二乘法":

                    Image<Gray, float> mask = new Image<Gray, float>(5, 5, new Gray(0));
                    Image<Gray, byte> _result = new Image<Gray, byte>(mask.Size);
                    Image<Gray, byte> _input = new Image<Gray, byte>(_imageInput.Width,_imageInput.Height,new Gray(0));
                       

                
                    int[][] point = new int[_imageInput.Height - 2][];
                    int number = 0;
                    int x = 0;
                    int y = 0;

                  
                    float[,] data1 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 20}, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    float[,] data2= { { -5, -5, 0, 5, 5 }, { -5, 0, 0, 0, 5 }, { 0, 0,0, 0, 0 }, { -5, 0, 0, 0, 5 }, { -5, -5,0, 5, 5 } };



                    ConvolutionKernelF convolutionKernelF = new ConvolutionKernelF(data1);
                    ConvolutionKernelF convolutionKernelF2 = new ConvolutionKernelF(data2);


                    _image =new Image<Gray,byte>(_imageInput.Convolution(convolutionKernelF).Bitmap);
                    _image=_image.AbsDiff(new Gray(92));
                    CvInvoke.Threshold(_image, _image, 30, 255, ThresholdType.BinaryInv);


                    Parallel.For(2, _image.Height - 3, item=>
                    {

                        for (int i= 2;i< _image.Width-3; i++)
                        {
                            if (CvInvoke.cvGet2D(_image,  (int)item,i).Equals(new MCvScalar(255)))
                            {
                                point[item] = new int[2];
                                point[item][0] = (int)item;
                                point[item][1] = i;
                                
                                number++;
                                x = item + x;
                                y =i + y;
                                i = _image.Width;
                            }
                            ;
                        }


                    });
                    double xy=0;
                    double powx =0;
                    double k = 0;
                    double b = 0;
                    foreach(int[] p in point)
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
                    startPoint.X = Convert.ToInt16(2 * k + b) + _imageInput.ROI.X;
                    startPoint.Y = 2+ _imageInput.ROI.Y;

                    endPoint.X= Convert.ToInt16((_imageInput.Height - 2) * k + b)+ _imageInput.ROI.X;
                    endPoint.Y = _imageInput.Height - 2 + _imageInput.ROI.Y;

                    break;
                default:
                    break;


            }
           

            sw.Stop();
            _imageResult = _imageInput.Clone();

            _image.CopyTo(_imageResult);
            if (actionLineData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);

            
        }
        public void run(Image<Gray,byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc].imageResult;
            if (0 == actionLineData.InputAOIWidth && 0 == actionLineData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);
            }
            else
            {
                _imageInput.ROI = new Rectangle(actionLineData.InputAOIX, actionLineData.InputAOIY, actionLineData.InputAOIWidth, actionLineData.InputAOIHeight);
                rectangle = new Rectangle(0, 0, _imageInput.Width - 1, _imageInput.Height - 1);

            }


            Image<Gray, byte> _image = _imageInput.Clone();

            switch (actionLineData.strLineType)
            {
                case "霍夫直线":
                    LineSegment2D[][] lines = _imageInput.HoughLines(actionLineData.CThreshold1, actionLineData.CThreshold2, actionLineData.rho, Math.PI / actionLineData.theta, actionLineData.threshold, actionLineData.length, actionLineData.gab);

                    foreach (LineSegment2D[] line in lines)
                    {
                        foreach (LineSegment2D l in line)
                        {
                            _image.Draw(l, new Gray(125), 3);
                        }
                    }
                    break;
                case "最小二乘法":

                    Image<Gray, float> mask = new Image<Gray, float>(5, 5, new Gray(0));
                    Image<Gray, byte> _result = new Image<Gray, byte>(mask.Size);
                    Image<Gray, byte> _input = new Image<Gray, byte>(_imageInput.Width, _imageInput.Height, new Gray(0));



                    int[][] point = new int[_imageInput.Height - 2][];
                    int number = 0;
                    int x = 0;
                    int y = 0;


                    float[,] data1 = { { 1, 1, 1, 1, 1 }, { 0, 1, 1, 1, 1 }, { 0, 0, 1, 1, 20 }, { 0, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1 } };
                    float[,] data2 = { { -5, -5, 0, 5, 5 }, { -5, 0, 0, 0, 5 }, { 0, 0, 0, 0, 0 }, { -5, 0, 0, 0, 5 }, { -5, -5, 0, 5, 5 } };



                    ConvolutionKernelF convolutionKernelF = new ConvolutionKernelF(data1);
                    ConvolutionKernelF convolutionKernelF2 = new ConvolutionKernelF(data2);


                    _image = new Image<Gray, byte>(_imageInput.Convolution(convolutionKernelF).Bitmap);
                    _image = _image.AbsDiff(new Gray(92));
                    CvInvoke.Threshold(_image, _image, 30, 255, ThresholdType.BinaryInv);


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
                    startPoint.X = Convert.ToInt16(2 * k + b) + actionLineData.InputAOIX;
                    startPoint.Y = 2 + actionLineData.InputAOIY;

                    endPoint.X = Convert.ToInt16((_imageInput.Height - 2) * k + b) + actionLineData.InputAOIX;
                    endPoint.Y = _imageInput.Height - 2 + actionLineData.InputAOIY;

                    break;
                default:
                    break;


            }


            sw.Stop();
            _imageResult = _imageInput.Clone();

            _image.CopyTo(_imageResult);
            if (actionLineData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionLine(ActionLineData actionLineData)
        {
            actionData = actionLineData;
            actionData.Name = actionLineData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionLine)(new FormActionLine(actionLineData, this));
            this.actionLineData = actionLineData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/Line Model/model.jpg";
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
