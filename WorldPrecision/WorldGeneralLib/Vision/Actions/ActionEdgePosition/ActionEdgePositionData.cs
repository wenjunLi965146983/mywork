using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.EdgePosition
{
    public class ActionEdgePositionData : ActionDataBase
    {
       
        private int _direct;
        public int direct
        {
            set { _direct = value; }
            get { return _direct; }
        }
        private int _threshold;
        public int threshold
        {
            set { _threshold = value; }
            get { return _threshold; }
        }
        private int _maxValue;
        public int maxValue
        {
            set { _maxValue = value; }
            get { return _maxValue; }
        }

        public ActionEdgePositionData()
        {
            Name = "边缘位置";
            Type = ActionType.ActionEdgePosition;
            Group = ActionGroup.GroupDetectionAndMeasurement;
           
        }


        public ActionEdgePositionData(string strName):this()
        {
            Name = strName;
        }
    }
}
