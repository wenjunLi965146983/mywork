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

namespace WorldGeneralLib.Vision.Actions.MultiSearch
{
    public class ActionMultiSearchData : ActionDataBase
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
        public bool bDirect { set; get; }

        public bool bEqualize { set; get; }
        public ActionMultiSearchData()
        {
            Name = "搜索";

            Group = ActionGroup.GroupDetectionAndMeasurement;
            Type = ActionType.ActionMultiSearch;


        }
   

        public ActionMultiSearchData(string strName):this()
        {
            Name = strName;
        }
    }
}
