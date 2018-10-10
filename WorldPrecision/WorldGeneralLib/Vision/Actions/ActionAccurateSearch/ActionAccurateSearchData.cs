using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.AccurateSearch

{
    

    
    public class ActionAccurateSearchData : ActionDataBase
    {
        
        private int _iKeyPointNumber;//角点数量
        public int iKeyPointNumber
        {
            get { return _iKeyPointNumber; }
            set { _iKeyPointNumber = value; }
        }
        private float _fThreshlod;
        public float fThreshlod
        {
            get { return _fThreshlod; }
            set
            {
                if (value > 0 && value < 1)
                {
                    _fThreshlod = value;
                }
               
            }
        }
    
      
        private int _time;
        public int time
        {
            set
            {
                if(value>=0&&value<4)
                {
                    _time = value;
                }
               
             }
            get
            {
                return _time;
            }
        }
        public ActionAccurateSearchData()
        {
            Name = "位置修正";
            _iKeyPointNumber = 1000;
            fThreshlod = 0.8F;
            Type = ActionType.ActionAccurateSearch;
            Group = ActionGroup.GroupDetectionAndMeasurement;
            _time =0;
          
        }

        public ActionAccurateSearchData(string strName):this()
        {
            Name = strName;
        }
    }
}
