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

namespace WorldGeneralLib.Vision.Actions.BrightCorrect
{
    public class ActionBrightCorrectData : ActionDataBase
    {
       
        private int _iScale;
        public int iScale
        {
            set { _iScale = value; }
            get { return _iScale; }
        }
      
        private int _iInputScale;
        public int iInputScale
        {
            set { _iInputScale = value; }
            get { return _iInputScale; }
        }
        private bool _bDirect;
        public bool bDirect
        {
            set { _bDirect = value; }
            get { return _bDirect; }
        }
        private bool _bBrightDirect;
        public bool bBrightDirect
        {
            set { _bBrightDirect = value; }
            get { return _bBrightDirect; }
        }
        private bool _bEqualize;
        public bool bEqualize
        {
            set { _bEqualize = value; }
            get { return _bEqualize; }
        }

        public ActionBrightCorrectData()
        {
            Name = "亮度修正过滤";
          
            Group = ActionGroup.GroupEnhance;
            Type = ActionType.ActionBrightCorrect;


        }
   

        public ActionBrightCorrectData(string strName):this()
        {
            Name = strName;
        }
    }
}
