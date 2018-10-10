using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.Vision.Actions
{
    public class ActionDataBase
    {
        private string _strName;
        public string Name
        {
            get { return _strName; }
            set { _strName = value; }
        }
        private int _imageSrc;
        public int imageSrc
        {
            set { _imageSrc = value; }
            get { return _imageSrc; }
        }
        public ActionGroup Group;
        public ActionType Type;


        
        public int InputAOIX//Input 搜索区域
        {
            get;
            set;
        }
    
        public int InputAOIY
        {
            get;
            set;
        }
      
        public int InputAOIWidth
        {
            set;get;
        }
       
        public int InputAOIHeight
        {
            get;
            set;
        }

        public int ModelAOIX//模版区域
        {
            get;
            set;
        }

        public int ModelAOIY
        {
            get;
            set;
        }

        public int ModelAOIWidth
        {
            set; get;
        }

        public int ModelAOIHeight
        {
            get;
            set;
        }

        public bool bROIReset
        {
            set;
            get;
        }
    }
}
