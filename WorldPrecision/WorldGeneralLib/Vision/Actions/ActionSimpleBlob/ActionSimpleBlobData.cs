using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.SimpleBlob

{
    

    
    public class ActionSimpleBlobData : ActionDataBase
    {
        public int blobColor { set; get; }
        public bool filterByArea { set; get; }
        public bool filterByCircularity { set; get; }
        public bool filterByConvexity { set; get; }
        public bool filterByInertia { set; get; }
        public float maxArea { set; get; }
        public float minArea { set; get; }
        public float maxCircularity { set; get; }
        public float minCircularity { set; get; }
        public float maxConvexity { set; get; }
        public float minConvexity { set; get; }
        public float minInertiaRatio { set; get; }
        public float minDistance { set; get; }
        public float minRepeatability { set; get; }

        public float maxThreshold { set; get; }
        public float minThreshold { set; get; }
        public float thresholdStep { set; get; }

        public ActionSimpleBlobData()
        {
            Name = "面积重心";
            
            Type = ActionType.ActionSimpleBlob;
            Group = ActionGroup.GroupDetectionAndMeasurement;
           
          
        }

        public ActionSimpleBlobData(string strName):this()
        {
            Name = strName;
        }
    }
}
