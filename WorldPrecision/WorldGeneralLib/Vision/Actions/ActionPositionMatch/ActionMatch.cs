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

namespace WorldGeneralLib.Vision.Actions.Match
{
    public class ActionMatch:ActionBase,IAction
    {
        public  ActionMatchData actionMatchData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageModel
        {
            get { return _imageTemple; }
            set { _imageTemple = value; }
        }
        private Image<Gray, Byte> _imageTempleAOI;//图像模版
        
        private IImage _imageDescript;//描述图像

        //优化内容
        private VectorOfKeyPoint modelKeyPoints;
        private UMat a1;
        private SURF surf;
        private UMat modelDescriptors;
        //输出内容
        public double dResultX { set; get; }
        public double dResultY { set; get; }
        public double dResultAngle{ set; get; }

        public IImage imageDescript
        {
            get { return _imageDescript; }
            set { _imageDescript = value; }
        }
        public override void ActionExcute()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Mat homography = null;
            Mat mask = null;
            
            VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

            Image<Gray, byte> image = imageInput.Clone();
            for (int i = 0; i < actionMatchData.time; i++)
            {
                image =image.PyrDown();
            }
            if(0!=actionMatchData.InputAOIWidth && 0!= actionMatchData.InputAOIHeight)
            {
                image.ROI = new Rectangle(actionMatchData.InputAOIX, actionMatchData.InputAOIY, actionMatchData.InputAOIWidth,actionMatchData.InputAOIHeight);
            }
            PointF center;
            if (null!= modelDescriptors)
            {
                UMat b1 = image.ToUMat();


                UMat observedDescriptors = new UMat();

                //进行检测和计算，把opencv中的两部分和到一起了，分开用也可以
                surf.DetectAndCompute(b1, null, observedKeyPoints, observedDescriptors, false);


                BFMatcher matcher = new BFMatcher(DistanceType.L2Sqr);       //开始进行匹配
                matcher.Add(modelDescriptors);
                matcher.KnnMatch(observedDescriptors, matches, 2, null);
                mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
                mask.SetTo(new MCvScalar(255));
                Features2DToolbox.VoteForUniqueness(matches, 0.8, mask);   //去除重复的匹配

                int Count = CvInvoke.CountNonZero(mask);      //用于寻找模板在图中的位置
                if (Count >= 4)
                {
                    Count = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);
                    if (Count >= 4)
                        homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
                }

                Mat result1 = new Mat();
                Features2DToolbox.DrawMatches(_imageTempleAOI.Convert<Gray, byte>().Mat, modelKeyPoints, image.Convert<Gray, byte>().Mat, observedKeyPoints, matches, result1, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), mask);
                //绘制匹配的关系图
                if (homography != null)     //如果在图中找到了模板，就把它画出来
                {
                    Rectangle rect = new Rectangle(Point.Empty, _imageTempleAOI.Size);
                    PointF[] points = new PointF[]
                    {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                    };
                    points = CvInvoke.PerspectiveTransform(points, homography);
                    Point[] points2 = Array.ConvertAll<PointF, Point>(points, Point.Round);
                    VectorOfPoint vp = new VectorOfPoint(points2);
                    CvInvoke.Polylines(result1, vp, true, new MCvScalar(255, 0, 0, 255), 15);
                    dResultX = (points[0].X + points[1].X + points[2].X + points[3].X) / 4 * ((float)Math.Pow(2, actionMatchData.time));
                    dResultY = (points[0].Y + points[1].Y + points[2].Y + points[3].Y) / 4 * ((float)Math.Pow(2, actionMatchData.time));

                    Point point1 = new Point(Convert.ToInt32((points[0].X + points[3].X) / 2), Convert.ToInt32((points[0].Y + points[3].Y) / 2));
                    Point point2 = new Point(Convert.ToInt32((points[1].X + points[2].X) / 2), Convert.ToInt32((points[1].Y + points[2].Y) / 2));
                    CvInvoke.Line(result1, point1, point2, new MCvScalar(255, 0, 0), 1, Emgu.CV.CvEnum.LineType.EightConnected, 0);

                    dResultAngle = Math.Atan2((point2.Y - point1.Y), (point2.X - point1.X)) * 180 / Math.PI;
                    imageDescript = result1;
                }
                else
                {
                    actionRes = ActionResponse.NG;
                    return;
                }
                center = new PointF((float)dResultX, (float)dResultY);
            }

            else
            {
                center = new PointF(imageInput.Width/2, imageInput.Height / 2);
            }

           
            Mat rotation = new Mat();
            CvInvoke.GetRotationMatrix2D(center, dResultAngle+ actionMatchData.fOffsetAngle, 1, rotation);

            Image<Gray, float> mat = new Image<Gray, float>(new Size(3, 2));
            CvInvoke.cvSet2D(mat, 0, 2, new MCvScalar(actionMatchData.fOffsetX- dResultX));
            CvInvoke.cvSet2D(mat, 1, 2, new MCvScalar(actionMatchData.fOffsetY- dResultY));
            CvInvoke.cvSet2D(mat, 0, 0, new MCvScalar(1));
            CvInvoke.cvSet2D(mat, 1, 1, new MCvScalar(1));
            System.Drawing.Size roisize = new Size(imageInput.Bitmap.Width, imageInput.Bitmap.Height);
            try
            {
                if(null== imageResult)
                {
                    imageResult = new Image<Gray, byte>(imageInput.Size);
                }
                imageInput.Draw(new CircleF(center, 3), new Gray(255), 3);
                CvInvoke.WarpAffine(imageInput, imageResult, rotation, roisize);
                CvInvoke.WarpAffine(imageResult, imageResult, mat, roisize);
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

            actionRes = ActionResponse.OK;
            sw.Stop();
        }
        public void run(Image<Gray,byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Mat homography = null;
            Mat mask = null;
            _imageInput = image;
            VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

            Image<Gray, byte> _image = imageInput.Clone();
            for (int i = 0; i < actionMatchData.time; i++)
            {
                _image = _image.PyrDown();
            }
            if (0 != actionMatchData.InputAOIWidth && 0 != actionMatchData.InputAOIHeight)
            {
                _image.ROI = new Rectangle(actionMatchData.InputAOIX, actionMatchData.InputAOIY, actionMatchData.InputAOIWidth, actionMatchData.InputAOIHeight);
            }


            UMat b1 = _image.ToUMat();


            UMat observedDescriptors = new UMat();

            //进行检测和计算，把opencv中的两部分和到一起了，分开用也可以
            surf.DetectAndCompute(b1, null, observedKeyPoints, observedDescriptors, false);


            BFMatcher matcher = new BFMatcher(DistanceType.L2Sqr);       //开始进行匹配
            matcher.Add(modelDescriptors);
            matcher.KnnMatch(observedDescriptors, matches, 2, null);
            mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(255));
            Features2DToolbox.VoteForUniqueness(matches, 0.8, mask);   //去除重复的匹配

            int Count = CvInvoke.CountNonZero(mask);      //用于寻找模板在图中的位置
            if (Count >= 4)
            {
                Count = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);
                if (Count >= 4)
                    homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
            }

            Mat result1 = new Mat();
            Features2DToolbox.DrawMatches(_imageTempleAOI.Convert<Gray, byte>().Mat, modelKeyPoints, _image.Convert<Gray, byte>().Mat, observedKeyPoints, matches, result1, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), mask);
            //绘制匹配的关系图
            if (homography != null)     //如果在图中找到了模板，就把它画出来
            {
                Rectangle rect = new Rectangle(Point.Empty, _imageTempleAOI.Size);
                PointF[] points = new PointF[]
                {
                  new PointF(rect.Left, rect.Bottom),
                  new PointF(rect.Right, rect.Bottom),
                  new PointF(rect.Right, rect.Top),
                  new PointF(rect.Left, rect.Top)
                };
                points = CvInvoke.PerspectiveTransform(points, homography);
                Point[] points2 = Array.ConvertAll<PointF, Point>(points, Point.Round);
                VectorOfPoint vp = new VectorOfPoint(points2);
                CvInvoke.Polylines(result1, vp, true, new MCvScalar(255, 0, 0, 255), 15);
                dResultX = (points[0].X + points[1].X + points[2].X + points[3].X) / 4 * ((float)Math.Pow(2, actionMatchData.time));
                dResultY = (points[0].Y + points[1].Y + points[2].Y + points[3].Y) / 4 * ((float)Math.Pow(2, actionMatchData.time));

                Point point1 = new Point(Convert.ToInt32((points[0].X + points[3].X) / 2), Convert.ToInt32((points[0].Y + points[3].Y) / 2));
                Point point2 = new Point(Convert.ToInt32((points[1].X + points[2].X) / 2), Convert.ToInt32((points[1].Y + points[2].Y) / 2));
                CvInvoke.Line(result1, point1, point2, new MCvScalar(255, 0, 0), 1, Emgu.CV.CvEnum.LineType.EightConnected, 0);

                dResultAngle = Math.Atan2((point2.Y - point1.Y), (point2.X - point1.X)) * 180 / Math.PI;
                imageDescript = result1;
            }
            else
            {
                actionRes = ActionResponse.NG;
                return;
            }
            PointF center = new PointF((float)dResultX, (float)dResultY);
            Mat rotation = new Mat();
            CvInvoke.GetRotationMatrix2D(center, dResultAngle + actionMatchData.fOffsetAngle, 1, rotation);

            Image<Gray, float> mat = new Image<Gray, float>(new Size(3, 2));
            CvInvoke.cvSet2D(mat, 0, 2, new MCvScalar(actionMatchData.fOffsetX - dResultX));
            CvInvoke.cvSet2D(mat, 1, 2, new MCvScalar(actionMatchData.fOffsetY - dResultY));
            CvInvoke.cvSet2D(mat, 0, 0, new MCvScalar(1));
            CvInvoke.cvSet2D(mat, 1, 1, new MCvScalar(1));
            System.Drawing.Size roisize = new Size(imageInput.Bitmap.Width, imageInput.Bitmap.Height);
            try
            {
                if (null == imageResult)
                {
                    imageResult = new Image<Gray, byte>(imageInput.Size);
                }
                imageInput.Draw(new CircleF(center, 3), new Gray(255), 3);
                CvInvoke.WarpAffine(imageInput, imageResult, rotation, roisize);
                CvInvoke.WarpAffine(imageResult, imageResult, mat, roisize);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            actionRes = ActionResponse.OK;
            sw.Stop();
        }
        public ActionMatch(ActionMatchData actionMatchData)
        {
            actionData = actionMatchData;
            actionData.Name = actionMatchData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionMatch)(new FormActionMatch(actionMatchData,this));
            this.actionMatchData = actionMatchData;
            Init();
        }
        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/Match Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);       
            }
            catch (Exception)
            {
              
            }
            if (actionMatchData.ModelAOIWidth != 0 && actionMatchData.ModelAOIHeight != 0)
            {

                Image<Gray, byte> image = imageModel.Clone();
               for(int i = 0; i < actionMatchData.time; i++)
                {
                    image = image.PyrDown();
                }
                image.ROI=new Rectangle(actionMatchData.ModelAOIX, actionMatchData.ModelAOIY, actionMatchData.ModelAOIWidth, actionMatchData.ModelAOIHeight);
                try
                {
                    _imageTempleAOI = new Image<Gray, byte>(new Size(actionMatchData.ModelAOIWidth, actionMatchData.ModelAOIHeight));
                    image.CopyTo(_imageTempleAOI);
                    CvInvoke.cvResetImageROI(_imageTemple);
                    a1 = _imageTempleAOI.ToUMat();
                    modelKeyPoints = new VectorOfKeyPoint();

                    surf = new SURF(actionMatchData.iKeyPointNumber,1,1);
                    modelDescriptors = new UMat();
                    surf.DetectAndCompute(a1, null, modelKeyPoints, modelDescriptors, false);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
               
            }
            
        }

        //Action处理方法
        //TODO
    }
}
