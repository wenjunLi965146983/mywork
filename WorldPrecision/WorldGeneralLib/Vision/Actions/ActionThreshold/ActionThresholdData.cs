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

namespace WorldGeneralLib.Vision.Actions.Threshold
{
    public class ActionThresholdData : ActionDataBase
    {
       
       
        private String _strThresholdType;
        public String strThresholdType
        {
            set { _strThresholdType = value; }
            get { return _strThresholdType; }
        }
        private int _minValue;
        public int minValue
        {
            set { _minValue = value; }
            get { return _minValue; }
        }
        private int _maxValue;
        public int maxValue
        {
            set { _maxValue = value; }
            get { return _maxValue; }
        }
      
       
      
        public ActionThresholdData()
        {
            Name = "色彩灰度过滤";
            Type = ActionType.ActionThreshold;
            Group = ActionGroup.GroupEnhance;
           
        }
        private bool _bROIReset;
        public bool bROIReset
        {
            set { _bROIReset = value; }
            get { return _bROIReset; }
        }

        public ActionThresholdData(string strName):this()
        {
            Name = strName;
        }
    }
}
