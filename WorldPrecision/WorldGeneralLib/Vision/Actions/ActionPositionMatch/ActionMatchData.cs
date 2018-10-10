using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.Match
{
    

    
    public class ActionMatchData : ActionDataBase
    {
        
        public float fOffsetAngle
        {
            set;get;
        }

        public float fOffsetX
        {
            set; get;
        }
        public float fOffsetY
        {
            set; get;
        }
        public int iKeyPointNumber
        {
            set; get;
        }
        public float fThreshlod
        {
            set; get;
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
        public ActionMatchData()
        {
            Name = "位置修正";
           
            Type = ActionType.ActionMatch;
            Group = ActionGroup.GroupEnhance;
            _time =0;
          
        }

        public ActionMatchData(string strName):this()
        {
            Name = strName;
        }
    }
}
