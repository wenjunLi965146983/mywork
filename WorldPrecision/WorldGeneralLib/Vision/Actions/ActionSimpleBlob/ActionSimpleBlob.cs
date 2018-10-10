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

namespace WorldGeneralLib.Vision.Actions.SimpleBlob
{
    public class ActionSimpleBlob:ActionBase,IAction
    {
        public ActionSimpleBlobData actionSimpleBlobData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        //参数
        private SimpleBlobDetectorParams sBParameter;
        private SimpleBlobDetector sBDetector;

        public Image<Gray, Byte> imageModel
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
            _imageInput = VisionManage.listScene[VisionManage.iCurrSceneIndex].listAction[actionData.imageSrc - 1].imageResult.Clone();


            Image<Gray, byte> _image = imageInput.Clone();
           
            if(0!=actionSimpleBlobData.InputAOIWidth && 0!=actionSimpleBlobData.InputAOIHeight)
            {
                _image.ROI = new Rectangle(actionSimpleBlobData.InputAOIX,actionSimpleBlobData.InputAOIY,actionSimpleBlobData.InputAOIWidth,actionSimpleBlobData.InputAOIHeight);
            }
            Mat mask = new Mat(_image.Size,DepthType.Cv16S,1);
            VectorOfKeyPoint vectorOfKeyPoint = new VectorOfKeyPoint();
            MKeyPoint[] keyPoint;
          
            keyPoint=sBDetector.Detect(_image);
           

            for (int i = 0; i < keyPoint.Length; i++)
            {
                CircleF cf1 = new CircleF(keyPoint[i].Point, 6);
                _image.Draw(cf1, new Gray(100), 2, Emgu.CV.CvEnum.LineType.EightConnected, 0);  
            }


            _imageResult = _imageInput.Clone();
       
            _image.CopyTo(_imageResult);
            if (actionSimpleBlobData.bROIReset)
            {
                CvInvoke.cvResetImageROI(_imageResult);
            }
            CvInvoke.cvResetImageROI(_imageInput);
            sw.Stop();
        }
        public void run(Image<Gray,byte> image)
        {
        }
        public ActionSimpleBlob(ActionSimpleBlobData actionSimpleBlobData)
        {
            actionData =actionSimpleBlobData;
            actionData.Name =actionSimpleBlobData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionSimpleBlob)(new FormActionSimpleBlob(actionSimpleBlobData,this));
            this.actionSimpleBlobData =actionSimpleBlobData;
            Init();
        }
        public override void Init()
        {
            base.Init();
            sBParameter = new SimpleBlobDetectorParams();

            sBParameter.blobColor = (byte)actionSimpleBlobData.blobColor;

            sBParameter.FilterByArea = actionSimpleBlobData.filterByArea;
            sBParameter.FilterByCircularity = actionSimpleBlobData.filterByCircularity;
            sBParameter.FilterByConvexity = actionSimpleBlobData.filterByConvexity;
            sBParameter.FilterByInertia = actionSimpleBlobData.filterByInertia;

            sBParameter.MaxArea = actionSimpleBlobData.maxArea;
            sBParameter.MinArea = actionSimpleBlobData.minArea;

            sBParameter.MaxCircularity = actionSimpleBlobData.maxCircularity;
            sBParameter.MinCircularity = actionSimpleBlobData.minCircularity;

            sBParameter.MaxConvexity = actionSimpleBlobData.maxConvexity;
            sBParameter.MinConvexity = actionSimpleBlobData.minConvexity;

            sBParameter.MinInertiaRatio = actionSimpleBlobData.minInertiaRatio;

            sBParameter.MinDistBetweenBlobs = actionSimpleBlobData.minDistance;

            sBDetector = new SimpleBlobDetector(sBParameter);
           
           String filename = @".//Parameter/Model/SimpleBlob Model/model.jpg";
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
    
}
