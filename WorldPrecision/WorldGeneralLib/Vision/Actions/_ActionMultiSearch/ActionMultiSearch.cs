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
using System.Runtime.InteropServices;

namespace WorldGeneralLib.Vision.Actions.MultiSearch

{
    public class ActionMultiSearch : ActionBase,IAction
    {
        public ActionMultiSearchData actionMultiSearchData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageTemple
        {
            get { return _imageTemple; }
            set
            {
                _imageTemple = value;

            }
        }


      
        private Image<Gray, Double> _imageIntegralModel;//图像模版积分图
        public Image<Gray, Double> imageIntegralModel
        {
            get { return _imageIntegralModel; }
            set{ _imageIntegralModel = value; }
        }

        private Image<Gray, Double> _imageIntegralModel2;//图像模版积分图
        public Image<Gray, Double> imageIntegralModel2
        {
            get { return _imageIntegralModel2; }
            set { _imageIntegralModel2 = value; }
        }

        private Image<Gray, Double> _imageIntegralInput;//图像输入积分图
        public Image<Gray, Double> imageIntegralInput
        {
            get { return _imageIntegralInput; }
            set { _imageIntegralInput = value; }
        }

        public Image<Gray, Double> result;
        public Image<Gray, Double> result1;
        public Image<Gray, Double> result2;

        public double dIntegralOfModel;

        public double dIntegralOfModel2;
        public override void ActionExcute()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput=VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc-1].imageResult.Clone();
            if (null != _imageInput)
            {
                if (0 != actionMultiSearchData.InputAOIWidth && 0 != actionMultiSearchData.InputAOIHeight)
                {
                    _imageInput.ROI = new Rectangle(actionMultiSearchData.InputAOIX, actionMultiSearchData.InputAOIY, actionMultiSearchData.InputAOIWidth, actionMultiSearchData.InputAOIHeight);
                }
            }
            else
            {
                return;
            }
            Image<Gray, double> integralOfInput = _imageInput.Integral();
           
            result1 = GetIntegralDiff(integralOfInput, 1, 1, dIntegralOfModel);
          
            result2 = GetIntegralDiff(integralOfInput, actionMultiSearchData.ModelAOIWidth / 4, actionMultiSearchData.ModelAOIHeight / 4, dIntegralOfModel2);
            Image<Gray, byte> _image = new Image<Gray, byte>(result1.Bitmap);

            //_image._EqualizeHist();
           
           // CvInvoke.Threshold(_image, result1,80,100, ThresholdType.BinaryInv);
            //CvInvoke.Threshold(result2, result2, 80, 100, ThresholdType.BinaryInv);
            result = new Image<Gray, double>(result1.Size);
            CvInvoke.Add(result1, result2, result);
            CvInvoke.Threshold(result, result, 100, 255, ThresholdType.BinaryInv);
            //detector(result);
            sw.Stop();
        }
        public void run(Image<Gray,byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = image;
            if (null != _imageInput)
            {
                if (0 != actionMultiSearchData.InputAOIWidth && 0 != actionMultiSearchData.InputAOIHeight)
                {
                    _imageInput.ROI = new Rectangle(actionMultiSearchData.InputAOIX, actionMultiSearchData.InputAOIY, actionMultiSearchData.InputAOIWidth, actionMultiSearchData.InputAOIHeight);
                }
            }
            else
            {
                return;
            }
            Image<Gray, double> integralOfInput = _imageInput.Integral();

            result1 = GetIntegralDiff(integralOfInput, 1, 1, dIntegralOfModel);

            result2 = GetIntegralDiff(integralOfInput, actionMultiSearchData.ModelAOIWidth / 4, actionMultiSearchData.ModelAOIHeight / 4, dIntegralOfModel2);
            Image<Gray, byte> _image = new Image<Gray, byte>(result1.Bitmap);

            //_image._EqualizeHist();

            // CvInvoke.Threshold(_image, result1,80,100, ThresholdType.BinaryInv);
            //CvInvoke.Threshold(result2, result2, 80, 100, ThresholdType.BinaryInv);
            result = new Image<Gray, double>(result1.Size);
            CvInvoke.Add(result1, result2, result);
            CvInvoke.Threshold(result, result, 100, 255, ThresholdType.BinaryInv);
            //detector(result);
            sw.Stop();
        }



        public Image<Gray,double> GetIntegralDiff(Image<Gray, double> integralOfInput,int startPointX, int startPointY,double integralThreshold)
        {

            Image<Gray, Double> point1;
            Image<Gray, Double> point2;
            Image<Gray, Double> point3;
            Image<Gray, Double> point4;
            Image<Gray, double> _result;
            if (0 != actionMultiSearchData.ModelAOIWidth && 0 != actionMultiSearchData.ModelAOIHeight)
            {

                Size size = new Size(integralOfInput.Width - actionMultiSearchData.ModelAOIWidth, integralOfInput.Height - actionMultiSearchData.ModelAOIHeight);
                point1 = new Image<Gray, double>(size);
                point2 = new Image<Gray, double>(size);
                point3 = new Image<Gray, double>(size);
                point4 = new Image<Gray, double>(size);
                _result = new Image<Gray, double>(size);
                integralOfInput.ROI = new Rectangle(startPointX, startPointY, integralOfInput.Width - actionMultiSearchData.ModelAOIWidth, integralOfInput.Height - actionMultiSearchData.ModelAOIHeight);
                integralOfInput.CopyTo(point1);
                CvInvoke.cvResetImageROI(integralOfInput);
                integralOfInput.ROI = new Rectangle(actionMultiSearchData.ModelAOIWidth-startPointX+1, startPointY, integralOfInput.Width - actionMultiSearchData.ModelAOIWidth, integralOfInput.Height - actionMultiSearchData.ModelAOIHeight);
                integralOfInput.CopyTo(point2);
                CvInvoke.cvResetImageROI(integralOfInput);
                integralOfInput.ROI = new Rectangle(startPointX, actionMultiSearchData.ModelAOIHeight- startPointY+1, integralOfInput.Width - actionMultiSearchData.ModelAOIWidth, integralOfInput.Height - actionMultiSearchData.ModelAOIHeight);
                integralOfInput.CopyTo(point3);
                CvInvoke.cvResetImageROI(integralOfInput);
                integralOfInput.ROI = new Rectangle(actionMultiSearchData.ModelAOIWidth- startPointX+1, actionMultiSearchData.ModelAOIHeight- startPointY+1, integralOfInput.Width - actionMultiSearchData.ModelAOIWidth, integralOfInput.Height - actionMultiSearchData.ModelAOIHeight);
                integralOfInput.CopyTo(point4);

                CvInvoke.Subtract(point4, point2, _result);
                CvInvoke.Add(_result, point1, _result);
                CvInvoke.Subtract(_result, point3, _result);
            }
            else
            {
                return null;
            }
            double maxValue = 0;
            double minValue = 0;
            Point maxValuePoint = new Point();
            Point minValuePoint = new Point();
            CvInvoke.MinMaxLoc(_result, ref minValue, ref maxValue, ref minValuePoint, ref maxValuePoint);
            if (integralThreshold < minValue)
            {

            }
            else
            {

                Image<Gray, Double> image = new Image<Gray, double>(_result.Width, _result.Height, new Gray(integralThreshold));
                CvInvoke.AbsDiff(_result, image, _result);

            }
            if (maxValue- integralThreshold> integralThreshold- minValue)
            {
                _result=_result.Mul(100/(maxValue - integralThreshold));
            }
            else
            {
                _result = _result.Mul(100 / (integralThreshold - minValue));
            }
            CvInvoke.cvResetImageROI(integralOfInput);
            return _result;
        }
        private MKeyPoint[] detector(Image<Gray, Double> src)
        {
            Image<Gray, byte> _image = new Image<Gray, byte>(src.Bitmap);

           
            SimpleBlobDetectorParams simpleBlobDetectorParams = new SimpleBlobDetectorParams();
            simpleBlobDetectorParams.MinArea =_imageTemple.Width*_imageTemple.Height/2;
            //simpleBlobDetectorParams.MaxArea = 100000000;
            simpleBlobDetectorParams.ThresholdStep = 10;
            simpleBlobDetectorParams.MinThreshold = 50;
            simpleBlobDetectorParams.FilterByArea = true;
            simpleBlobDetectorParams.FilterByCircularity = false;
            simpleBlobDetectorParams.FilterByColor = false;
            simpleBlobDetectorParams.FilterByConvexity = false;
            simpleBlobDetectorParams.FilterByInertia = false;


            simpleBlobDetectorParams.MaxThreshold = 255;
            simpleBlobDetectorParams.MinThreshold = 50;

            SimpleBlobDetector simpleBlobDetector = new SimpleBlobDetector(simpleBlobDetectorParams);

            MKeyPoint[] keyPoint = simpleBlobDetector.Detect(_image);

            
           
            return keyPoint;
        }

        public ActionMultiSearch(ActionMultiSearchData actionMultiSearchData)
        {
            actionData = actionMultiSearchData;
            actionData.Name = actionMultiSearchData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionMultiSearch)(new FormActionMultiSearch(actionMultiSearchData, this));
            this.actionMultiSearchData = actionMultiSearchData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/MultiSearch Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);
            }
            catch (Exception)
            {
              
            }
            if (null != _imageTemple)
            {
                if(0!= actionMultiSearchData.ModelAOIWidth&&0!= actionMultiSearchData.ModelAOIHeight)
                {
                    _imageTemple.ROI = new Rectangle(actionMultiSearchData.ModelAOIX, actionMultiSearchData.ModelAOIY, actionMultiSearchData.ModelAOIWidth, actionMultiSearchData.ModelAOIHeight);
                }
                _imageIntegralModel = _imageTemple.Integral();
                dIntegralOfModel =Convert.ToDouble(_imageIntegralModel.Data.GetValue(_imageTemple.Height, _imageTemple.Width,0));

                _imageTemple.ROI = new Rectangle(actionMultiSearchData.ModelAOIWidth/4, actionMultiSearchData.ModelAOIHeight/4, actionMultiSearchData.ModelAOIWidth/2, actionMultiSearchData.ModelAOIHeight/2);
                _imageIntegralModel2 = _imageTemple.Integral();
                dIntegralOfModel2 = Convert.ToDouble(_imageIntegralModel.Data.GetValue(_imageTemple.Height, _imageTemple.Width, 0));

                CvInvoke.cvResetImageROI(_imageTemple);
            }
          
         }


            
     }

        //Action处理方法
        //TODO
    
}
