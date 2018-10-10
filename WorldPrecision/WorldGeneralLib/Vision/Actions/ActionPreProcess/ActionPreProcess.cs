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

namespace WorldGeneralLib.Vision.Actions.PreProcess
{
    public class ActionPreProcess:ActionBase,IAction
    {
        public ActionPreProcessData actionPreProcessData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageTemple
        {
            get { return _imageTemple; }
            set
            {
                _imageTemple = value;

            }
        }





        public override void ActionExcute()
        {
           
    
            Point anchor = new Point(-1, -1);
            if (actionData.imageSrc > 0)
            {
                _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc - 1].imageResult.Clone();
            }
            
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(actionPreProcessData.iMorSize, actionPreProcessData.iMorSize), anchor);
            if (0==actionPreProcessData.InputAOIWidth && 0 == actionPreProcessData.InputAOIHeight)
            {
                //CvInvoke.cvResetImageROI(_imageInput);

            }
            else
            {
                Rectangle rectangle = new Rectangle(actionPreProcessData.InputAOIX, actionPreProcessData.InputAOIY, actionPreProcessData.InputAOIWidth, actionPreProcessData.InputAOIHeight);
                _imageInput.ROI = rectangle;
            }
            Image<Gray,byte> _image=new Image<Gray, byte>(new Size(actionPreProcessData.InputAOIWidth, actionPreProcessData.InputAOIHeight));
            _image = _imageInput.Clone();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            switch (actionPreProcessData.strProcessType)
            {
                case "高斯滤波":
                    _image = _imageInput.Clone();
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                         
                        _image = _image.SmoothGaussian(actionPreProcessData.iFilterSize);
                       
                    break;
                case "中值滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothMedian(actionPreProcessData.iFilterSize);
                    break;
                case "均值滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothBlur(actionPreProcessData.iFilterSize, actionPreProcessData.iFilterSize);
                    break;
                case "双边滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothBilatral(actionPreProcessData.iFilterSize,3,3);
                    break;
                case "膨胀":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Dilate, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "侵蚀":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Erode, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "开运算":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Open, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "闭运算":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Close, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "黑帽":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Blackhat, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "白帽":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Tophat, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                default:
                    break;
            }
            sw.Stop();
            _imageResult = _imageInput.Clone(); ;

            _image.CopyTo(_imageResult);
            if (actionPreProcessData.bROIReset)
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
            Point anchor = new Point(-1, -1);
            Mat element = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(actionPreProcessData.iMorSize, actionPreProcessData.iMorSize), anchor);
            if (0 == actionPreProcessData.InputAOIWidth && 0 == actionPreProcessData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);

            }
            else
            {
                Rectangle rectangle = new Rectangle(actionPreProcessData.InputAOIX, actionPreProcessData.InputAOIY, actionPreProcessData.InputAOIWidth, actionPreProcessData.InputAOIHeight);
                _imageInput.ROI = rectangle;
            }
            Image<Gray, byte> _image = new Image<Gray, byte>(new Size(actionPreProcessData.InputAOIWidth, actionPreProcessData.InputAOIHeight));
            _image = _imageInput.Clone();
            switch (actionPreProcessData.strProcessType)
            {
                case "高斯滤波":
                    _image = _imageInput.Clone();
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothGaussian(actionPreProcessData.iFilterSize);
                    break;
                case "中值滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothMedian(actionPreProcessData.iFilterSize);
                    break;
                case "均值滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothBlur(actionPreProcessData.iFilterSize, actionPreProcessData.iFilterSize);
                    break;
                case "双边滤波":
                    for (int i = 0; i < actionPreProcessData.iFTime; i++)
                        _image = _image.SmoothBilatral(actionPreProcessData.iFilterSize, 3, 3);
                    break;
                case "膨胀":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Dilate, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "侵蚀":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Erode, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "开运算":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Open, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "闭运算":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Close, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "黑帽":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Blackhat, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                case "白帽":
                    Emgu.CV.CvInvoke.MorphologyEx(_imageInput, _image, MorphOp.Tophat, element, anchor, actionPreProcessData.iTimes, BorderType.Constant, new MCvScalar(0));
                    break;
                default:
                    break;
            }
            sw.Stop();
            _imageResult = _imageInput.Clone(); ;

            _image.CopyTo(_imageResult);
            if (actionPreProcessData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionPreProcess(ActionPreProcessData actionPreProcessData)
        {
            actionData = actionPreProcessData;
            actionData.Name = actionPreProcessData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionPreProcess)(new FormActionPreProcess(actionPreProcessData, this));
            this.actionPreProcessData = actionPreProcessData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/PreProcess Model/model.jpg";
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
