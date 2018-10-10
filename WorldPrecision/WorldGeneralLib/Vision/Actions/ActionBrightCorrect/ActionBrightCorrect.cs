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

namespace WorldGeneralLib.Vision.Actions.BrightCorrect

{
    public class ActionBrightCorrect : ActionBase,IAction
    {
        public ActionBrightCorrectData actionBrightCorrectData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageTemple
        {
            get { return _imageTemple; }
            set
            {
                _imageTemple = value;

            }
        }
      
      

      
        private Image<Gray, Byte> _imageBright;//图像模版
        public Image<Gray, Byte> imageBright
        {
            get { return _imageBright; }
            set
            {
                _imageBright = value;

            }
        }

        public override void ActionExcute()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc-1].imageResult.Clone();
            ActionBase action= VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionBrightCorrectData.imageSrc];
            
            Image<Gray, byte> _image;
           
            if (0 != actionBrightCorrectData.InputAOIWidth && 0 != actionBrightCorrectData.InputAOIHeight)
            {
                Rectangle rectROI = new Rectangle(actionBrightCorrectData.InputAOIX, actionBrightCorrectData.InputAOIY, actionBrightCorrectData.InputAOIWidth, actionBrightCorrectData.InputAOIHeight);
                _imageInput.ROI = rectROI;
                 _image = new Image<Gray, byte>(new Size(actionBrightCorrectData.InputAOIWidth, actionBrightCorrectData.InputAOIHeight));
            }
            else
            {
                _image = new Image<Gray, byte>(_imageInput.Size);
            }
          
            _imageResult =_imageInput.Clone();
            
            
            if (null != _imageBright)
            {
                if (_imageBright.Size == _image.Size)
                {
                    _imageBright.CopyTo(_image);
                    if (actionBrightCorrectData.bDirect)
                    {
                        CvInvoke.Subtract(_imageInput, _image, _imageResult);
                    }
                    else
                    {
                        CvInvoke.Add(_imageInput, _image, _imageResult);
                    }
                }
                else
                {
                    _imageBright = null;
                }
                
            }
            if (actionBrightCorrectData.bEqualize)
            {
                CvInvoke.EqualizeHist(_imageResult, _imageResult);
            }
           
            
            if (actionBrightCorrectData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);
            sw.Stop();

        }
        public void run(Image<Gray,Byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            imageInput = image;
            Image<Gray, byte> _image;

            if (0 != actionBrightCorrectData.InputAOIWidth && 0 != actionBrightCorrectData.InputAOIHeight)
            {
                Rectangle rectROI = new Rectangle(actionBrightCorrectData.InputAOIX, actionBrightCorrectData.InputAOIY, actionBrightCorrectData.InputAOIWidth, actionBrightCorrectData.InputAOIHeight);
                _imageInput.ROI = rectROI;
                _image = new Image<Gray, byte>(new Size(actionBrightCorrectData.InputAOIWidth, actionBrightCorrectData.InputAOIHeight));
            }
            else
            {
                _image = new Image<Gray, byte>(_imageInput.Size);
            }

            _imageResult = _imageInput.Clone();
            
          
           ///<summary>亮度叠加

            if (null != _imageBright)
            {
                if (_imageBright.Size == _image.Size)
                {
                    _imageBright.CopyTo(_image);
                    if (actionBrightCorrectData.bDirect)
                    {
                        CvInvoke.Subtract(_imageInput, _image, _imageResult);
                    }
                    else
                    {
                        CvInvoke.Add(_imageInput, _image, _imageResult);
                    }
                }
                else
                {
                    _imageBright = null;
                }
               
            }

            ///<summary>亮度均衡化
            if (actionBrightCorrectData.bEqualize)
            {
                CvInvoke.EqualizeHist(_imageResult, _imageResult);
            }

            ///<summary>ROI清除
            if (actionBrightCorrectData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);
            sw.Stop();

        }

        public ActionBrightCorrect(ActionBrightCorrectData actionBrightCorrectData)
        {
            actionData = actionBrightCorrectData;
            actionData.Name = actionBrightCorrectData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionBrightCorrect)(new FormActionBrightCorrect(actionBrightCorrectData, this));
            this.actionBrightCorrectData = actionBrightCorrectData;
            Init();
        }




        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/BrightCorrect Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);

                filename = @".//Parameter/Model/BrightCorrect Model/Brightmodel.jpg";
                mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
               
             
                _imageBright = new Image<Gray, byte>(mat.Bitmap);
                
            }
            catch (Exception)
            {
              
            }
          
         }
            
     }
    
}
