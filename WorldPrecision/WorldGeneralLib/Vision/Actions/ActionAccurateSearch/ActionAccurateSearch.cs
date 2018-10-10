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

namespace WorldGeneralLib.Vision.Actions.AccurateSearch
{
    public class ActionAccurateSearch:ActionBase,IAction
    {
        public  ActionAccurateSearchData actionAccurateSearchData;
        private Image<Gray, Byte> _imageTemple;//图像模版

        public Image<Gray, Byte> imageModel
        {
            get { return _imageTemple; }
            set
            {
                _imageTemple = value;

            }
        }
        private Image<Gray, Byte> _imageTempleAOI;//图像模版
        
        private IImage _imageDescript;//描述图像

        //优化内容
        private VectorOfKeyPoint modelKeyPoints;
        private UMat a1;
        private SURF surf;
        private UMat modelDescriptors;

        //结果Position
        public Position[] doublePosition;

        public Image<Gray, float> imageCon;
        public List<Position> listPosition;
    
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
            for (int i = 0; i <actionAccurateSearchData.time; i++)
            {
                image =image.PyrDown();
            }
            if(0!=actionAccurateSearchData.InputAOIWidth && 0!=actionAccurateSearchData.InputAOIHeight)
            {
                image.ROI = new Rectangle(actionAccurateSearchData.InputAOIX,actionAccurateSearchData.InputAOIY,actionAccurateSearchData.InputAOIWidth,actionAccurateSearchData.InputAOIHeight);
            }
           

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
                //VectorOfPoint vp = new VectorOfPoint(points2);
               // CvInvoke.Polylines(result1, vp, true, new MCvScalar(255, 0, 0, 255), 15);
              // actionAccurateSearchData.fResultX = (points[0].X + points[1].X + points[2].X + points[3].X) / 4* ((float)Math.Pow(2,actionAccurateSearchData.time));
              // actionAccurateSearchData.fResultY = (points[0].Y + points[1].Y + points[2].Y + points[3].Y) / 4 * ((float)Math.Pow(2,actionAccurateSearchData.time));

                //Point point1 = new Point(Convert.ToInt32((points[0].X + points[3].X) / 2), Convert.ToInt32((points[0].Y + points[3].Y) / 2));
                //Point point2 = new Point(Convert.ToInt32((points[1].X + points[2].X) / 2), Convert.ToInt32((points[1].Y + points[2].Y) / 2));
                //CvInvoke.Line(result1, point1, point2, new MCvScalar(255, 0, 0), 1, Emgu.CV.CvEnum.LineType.EightConnected, 0);

              // actionAccurateSearchData.fResultAngle = Math.Atan2((point2.Y - point1.Y), (point2.X - point1.X)) * 180 / Math.PI;
                imageDescript = result1;
            }
            else
            {
                actionRes = ActionResponse.NG;
                return;
            }
           
            actionRes = ActionResponse.OK;
            sw.Stop();
        }
        public void run(Image<Gray,byte> image)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            Mat mask = null;
            _imageInput = image;
            VectorOfKeyPoint observedKeyPoints = new VectorOfKeyPoint();
            VectorOfVectorOfDMatch matches = new VectorOfVectorOfDMatch();

            Image<Gray, byte> _image = imageInput.Clone();
            for (int i = 0; i <actionAccurateSearchData.time; i++)
            {
                _image = _image.PyrDown();
            }
            if (0 !=actionAccurateSearchData.InputAOIWidth && 0 !=actionAccurateSearchData.InputAOIHeight)
            {
                _image.ROI = new Rectangle(actionAccurateSearchData.InputAOIX,actionAccurateSearchData.InputAOIY,actionAccurateSearchData.InputAOIWidth,actionAccurateSearchData.InputAOIHeight);
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

            
            

            Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);
            byte[] maskForSizeAndOrientation = mask.GetData();




            MDMatch[][] vectorOfDMatch = matches.ToArrayOfArray();

            bool keepDetect=true;
            Mat result1 = new Mat();
            result1 = _imageInput.Convert<Gray, byte>().Mat;
            Features2DToolbox.DrawMatches(_imageTempleAOI.Convert<Gray, byte>().Mat, modelKeyPoints, _image.Convert<Gray, byte>().Mat, observedKeyPoints, matches, result1, new MCvScalar(255, 0, 255), new MCvScalar(0, 255, 255), mask);
            int Count = CvInvoke.CountNonZero(mask);



            actionRes = ActionResponse.NG;
            List<Position> posList;
            listPosition=new List<Position>();

            imageCon = FindSeedPoint(observedKeyPoints, vectorOfDMatch,mask,out  posList);
            double maxValue=0;
            double minValue=0;
            Point maxPoint = new Point();
            Point minPoint = new Point();
            Exception exception = new Exception("循环超时");
            while (keepDetect)
            {
                CvInvoke.MinMaxLoc(imageCon, ref minValue, ref maxValue, ref minPoint, ref maxPoint);
                if (maxValue > 10)
                {
                    List<double[]> angleList = new List<double[]>();
                    double angleSin = 0;
                    double angleCos = 0;
                    double angle= 0;
                    List<int> status = new List<int>();
                    int count = 0;

                    int totalCount = 0;
                    //求出角度
                    foreach (Position p in posList)
                    {
                        if(Math.Abs(p.X- maxPoint.X)<16&& Math.Abs(p.Y - maxPoint.Y) < 16)
                        {
                            double[] item = new double[2];
                            item[0] = p.sin;
                            item[1] = p.cos;
                            angleList.Add(item);
                            status.Add(1);
                        }
                    }
                    bool keepCalcurateAngle = true;
                    int numberOfItem = status.Count;

                    Random random = new Random();

                    while (keepCalcurateAngle)
                    {
                        
                        int a = -1;
                        int b = -1;
                        bool keepGetRd = true;
                        int raCount = 0;
                        while (keepGetRd)
                        {
                            raCount++;
                            if (raCount > 10000)
                            {
                                throw (new Exception("角度偏差过大1"));
                            }
                            int rd=random.Next(status.Count - 1);
                            if(0!= status[rd]&&rd!=a)
                            {
                                if (-1 == a)
                                {
                                    a = rd;
                                }
                                else
                                {
                                    b = rd;
                                    keepGetRd = false;
                                }
                            }
                        }
                        if (Math.Abs(angleList[a][0] - angleList[b][0]) < 0.1&& Math.Abs(angleList[a][1] - angleList[b][1]) < 0.1)
                        {
                            ++count;
                            angleSin = angleSin + angleList[a][0];
                            angleCos = angleCos + angleList[a][1];
                        }
                        else
                        {
                            angleSin = 0;
                            angleCos = 0;
                            count = 0;
                            status[a] = 0;
                            status[b] = 0;
                            numberOfItem = numberOfItem - 2;
                        }
                        if (count > 99)
                        {
                            keepCalcurateAngle = false;
                        }
                        if(numberOfItem< status.Count / 3| numberOfItem < 5)
                        {
                            angleSin = 0;
                            angleCos = 0;
                            count = 0;
                            keepCalcurateAngle = false;
                            //throw (new Exception("角度计算错误"));
                        }

                        totalCount++;
                        if (totalCount > 2000)
                        {

                            throw (new Exception("角度偏差过大2"));
                        }
                    }
                
                    if(Math.Abs(angleSin)> Math.Abs(angleCos))
                    {
                        angle = 180* Math.Acos(angleCos/100)/Math.PI;
                        if (angleSin<0)
                        {
                            angle = 360 - angle;
                        }
                       
                    }
                    else
                    {
                        angle = 180 * Math.Asin(angleSin/100) / Math.PI;
                       
                            if (angleCos < 0)
                            {
                            angle = 180 - angle;
                            }
                            else
                            {
                                if (angle < 0)
                                {
                                angle = 360+ angle;
                                }
                            } 
                    }
                    listPosition.Add(new Position(maxPoint.X, maxPoint.Y, angle, angleSin / 100, angleCos / 100));

                    //画框
                  
                    Point[] rectPoint = new Point[4];
                    int CenterX = _imageTempleAOI.Width / 2;//模版尺寸
                    int CenterY = _imageTempleAOI.Height / 2;
                    double sin = angleSin / 100;
                    double Cos = angleCos / 100;



                    rectPoint[0].X=(int)(maxPoint.X- CenterX * Cos + CenterY * sin);
                    rectPoint[0].Y = (int)(maxPoint.Y - CenterY * Cos - CenterX  * sin);

                    rectPoint[1].X = (int)(maxPoint.X - CenterX * Cos - CenterY * sin);
                    rectPoint[1].Y = (int)(maxPoint.Y + CenterY * Cos - CenterX * sin);

                    

                    rectPoint[2].X = (int)(maxPoint.X + CenterX * Cos-CenterY * sin);
                    rectPoint[2].Y = (int)(maxPoint.Y +CenterY * Cos + CenterX * sin);

                    rectPoint[3].X = (int)(maxPoint.X + CenterX * Cos+CenterY * sin);
                    rectPoint[3].Y = (int)(maxPoint.Y - CenterY * Cos + CenterX * sin);

                    VectorOfPoint vp = new VectorOfPoint(rectPoint);
                    CvInvoke.Polylines(result1, vp, true, new MCvScalar(255, 0, 0, 255), 2);
                    //覆盖值




                    
                     int leng=Math.Min(_imageTempleAOI.Width, _imageTempleAOI.Height);
                    int startPX = maxPoint.X - leng / 2;
                    int startPY = maxPoint.Y - leng / 2;


                    Parallel.For(0, leng, item =>
                    {
                        for(int i = 0; i < leng; i++)
                        {
                            imageCon.Data[startPY+i, startPX + item,0] = 0;
                        }
                    });

                   



                }
                else
                {
                    keepDetect = false;
                }


            }

            
            

            imageDescript = result1;
            actionRes = ActionResponse.OK;
            sw.Stop();
        }
        public ActionAccurateSearch(ActionAccurateSearchData actionAccurateSearchData)
        {
            actionData =actionAccurateSearchData;
            actionData.Name =actionAccurateSearchData.Name;
            actionRes = ActionResponse.NonExecution;
            formAction = (FormActionAccurateSearch)(new FormActionAccurateSearch(actionAccurateSearchData,this));
            this.actionAccurateSearchData =actionAccurateSearchData;
            Init();
        }
        public override void Init()
        {
            base.Init();
            String filename = @".//Parameter/Model/AccurateSearch Model/model.jpg";
            try
            {
                Mat mat = CvInvoke.Imread(filename, Emgu.CV.CvEnum.ImreadModes.AnyColor);
                _imageTemple = new Image<Gray, byte>(mat.Bitmap);       
            }
            catch (Exception)
            {
              
            }
            if (actionAccurateSearchData.ModelAOIWidth != 0 &&actionAccurateSearchData.ModelAOIHeight != 0)
            {

                Image<Gray, byte> image = imageModel.Clone();
               for(int i = 0; i <actionAccurateSearchData.time; i++)
                {
                    image = image.PyrDown();
                }
                image.ROI=new Rectangle(actionAccurateSearchData.ModelAOIX,actionAccurateSearchData.ModelAOIY,actionAccurateSearchData.ModelAOIWidth,actionAccurateSearchData.ModelAOIHeight);
                try
                {
                    _imageTempleAOI = new Image<Gray, byte>(new Size(actionAccurateSearchData.ModelAOIWidth,actionAccurateSearchData.ModelAOIHeight));
                    image.CopyTo(_imageTempleAOI);
                    CvInvoke.cvResetImageROI(_imageTempleAOI);
                    a1 = _imageTempleAOI.ToUMat();
                    modelKeyPoints = new VectorOfKeyPoint();

                    surf = new SURF(actionAccurateSearchData.iKeyPointNumber,1,1);
                    modelDescriptors = new UMat();
                    surf.DetectAndCompute(a1, null, modelKeyPoints, modelDescriptors, false);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }  
            }  
        }



        public Image<Gray, float> FindSeedPoint(VectorOfKeyPoint observedKeyPoints,MDMatch[][] mDMatch, Mat mask, out List<Position> posList)
        {
            int CenterX = _imageTempleAOI.Width/2;
            int CenterY = _imageTempleAOI.Height/2;
            posList = new List<Position>();
            Image<Gray, byte> image = new Image<Gray, byte>(imageInput.Width, imageInput.Height, new Gray(0));
            //Image<Gray, byte> image = imageInput;
            byte[] maskData = mask.GetData();
            GCHandle maskHanlde = GCHandle.Alloc(maskData, GCHandleType.Pinned);

            using (Mat m = new Mat(maskData.Length, 1, DepthType.Cv8U, 1, maskHanlde.AddrOfPinnedObject(), 1))
                for (int i=0; i < mDMatch.Length; i++)
                {
                    if (0 != maskData[i])
                    {
                        int keyPointIn = mDMatch[i][0].QueryIdx;
                        int keyPointMo = mDMatch[i][0].TrainIdx;

                        double angleModel = modelKeyPoints[keyPointMo].Angle;
                        double angleInput = observedKeyPoints[keyPointIn].Angle;

                        float xModel = modelKeyPoints[keyPointMo].Point.X;
                        float yModel = modelKeyPoints[keyPointMo].Point.Y;

                        float xInput = observedKeyPoints[keyPointIn].Point.X;
                        float yInput = observedKeyPoints[keyPointIn].Point.Y;

                        double angle =(Math.PI *(angleInput - angleModel))/180;
                        double a = Math.Sin(angle);
                        double b = Math.Cos(angle);
                        double  xInc = (CenterX - xModel) * Math.Cos(angle)-(CenterY-yModel)* Math.Sin(angle);
                        double  yInc = (CenterY - yModel) * Math.Cos(angle)+(CenterX - xModel) * Math.Sin(angle);



                      



                        int CenterInputX = Convert.ToInt32(xInput + xInc);
                        int CenterInputY = Convert.ToInt32(yInput + yInc);
                        if(CenterInputX>0&& CenterInputX< image.Width&& CenterInputY>0&& CenterInputY < image.Height)
                        {
                            image.Data[CenterInputY, CenterInputX, 0] += 1;
                            int k = image.Data[CenterInputY, CenterInputX, 0];
                            Position position = new Position(CenterInputX, CenterInputY,0, a,b);
                            posList.Add(position);
                            
                        }

                        
                    }
                }


            float[,] data1=new float[31,31];
            for(int i = 0; i < 31; i++)
            {
                for(int k = 0; k < 31; k++)
                {
                    if (i > 13 && i < 17 && k > 13 && k < 17)
                    {
                        data1[i, k] = 2;
                    }
                    else
                    {
                        data1[i, k] = 1;
                    }
                    
                }
            }
             ConvolutionKernelF convolutionKernelF = new ConvolutionKernelF(data1);
            Image<Gray,float> re=image.Convolution(convolutionKernelF,0,BorderType.Constant);
           
            return re;

            

        }

    }
    public class Position
    {
        public Double X
        {
            set;get;
        }
        public Double Y
        {
            set; get;
        }
        public Double Angle
        {
            set; get;
        }
        public Double sin;
        public Double cos;
        public Position(Double X, Double Y, Double Angle, Double sin,Double cos)
        {
            this.X = X;
            this.Y = Y;
            this.Angle = Angle;
            this.sin = sin;
            this.cos = cos;
        }
    }

}
