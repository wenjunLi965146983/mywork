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

namespace WorldGeneralLib.Vision.Actions.Threshold
{
    public class ActionThreshold:ActionBase,IAction
    {
        public ActionThresholdData actionThresholdData;
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
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc-1].imageResult.Clone();
         
         
            if (0==actionThresholdData.InputAOIWidth && 0 == actionThresholdData.InputAOIHeight)
            {
                //CvInvoke.cvResetImageROI(_imageInput);

            }
            else
            {
                Rectangle rectangle = new Rectangle(actionThresholdData.InputAOIX, actionThresholdData.InputAOIY, actionThresholdData.InputAOIWidth, actionThresholdData.InputAOIHeight);
                _imageInput.ROI = rectangle;
            }
            Image<Gray,byte> _image=new Image<Gray, byte>(new Size(actionThresholdData.InputAOIWidth, actionThresholdData.InputAOIHeight));
            ThresholdType thresholdType;
            _image = _imageInput.Clone();
            switch (actionThresholdData.strThresholdType)
            {
                case "二值化":
                    thresholdType = ThresholdType.Binary;
                    break;
                case "二值化反转":
                    thresholdType = ThresholdType.BinaryInv;
                    break;
             

                default:
                    thresholdType = ThresholdType.Binary;
                    break;
            }




            CvInvoke.Threshold(_imageInput, _image,actionThresholdData.minValue, actionThresholdData.maxValue, thresholdType);
            sw.Stop();
            _imageResult = _imageInput.Clone(); ;

            _image.CopyTo(_imageResult);
            if (actionThresholdData.bROIReset)
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


            if (0 == actionThresholdData.InputAOIWidth && 0 == actionThresholdData.InputAOIHeight)
            {
                CvInvoke.cvResetImageROI(_imageInput);

            }
            else
            {
                Rectangle rectangle = new Rectangle(actionThresholdData.InputAOIX, actionThresholdData.InputAOIY, actionThresholdData.InputAOIWidth, actionThresholdData.InputAOIHeight);
                _imageInput.ROI = rectangle;
            }
            Image<Gray, byte> _image = new Image<Gray, byte>(new Size(actionThresholdData.InputAOIWidth, actionThresholdData.InputAOIHeight));
            ThresholdType thresholdType;
            _image = _imageInput.Clone();
            switch (actionThresholdData.strThresholdType)
            {
                case "二值化":
                    thresholdType = ThresholdType.Binary;
                    break;
                case "二值化反转":
                    thresholdType = ThresholdType.BinaryInv;
                    break;


                default:
                    thresholdType = ThresholdType.Binary;
                    break;
            }




            CvInvoke.Threshold(_imageInput, _image, actionThresholdData.minValue, actionThresholdData.maxValue, thresholdType);
            sw.Stop();
            _imageResult = _imageInput.Clone(); ;

            _image.CopyTo(_imageResult);
            if (actionThresholdData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);


        }

        public ActionThreshold(ActionThresholdData actionThresholdData)
        {
            actionData = actionThresholdData;
            actionData.Name = actionThresholdData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionThreshold)(new FormActionThreshold(actionThresholdData, this));
            this.actionThresholdData = actionThresholdData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/Threshold Model/model.jpg";
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
