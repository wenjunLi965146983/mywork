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

namespace WorldGeneralLib.Vision.Actions.PreProcess
{
    public class ActionPreProcessData : ActionDataBase
    {
       
       
        private String _strProcessType;
        public String strProcessType
        {
            set { _strProcessType = value; }
            get { return _strProcessType; }
        }

        private int _iFilterSize;
        public int iFilterSize
        {
            set
            {
                if (value % 2 > 0)
                {
                    _iFilterSize = value;
                }
                else
                {
                    MessageBox.Show("滤波器的卷积核需为奇数");
                }
            }
            get { return _iFilterSize; }
        }
        private int _iMorSize;
        public int iMorSize
        {
            set
            {
                if (value % 2 > 0)
                {
                    _iMorSize = value;
                }
                else
                {
                    MessageBox.Show("形态学的卷积核需为奇数");
                }

            }
            get { return _iMorSize; }
        }
        private int _iTimes;
        public int iTimes
        {
            set { _iTimes = value; }
            get { return _iTimes; }
        }
        private int _iFTime;
        public int iFTime
        {
            set { _iFTime = value; }
            get { return _iFTime; }
        }
        public ActionPreProcessData()
        {
            Name = "测量前处理";
            Type = ActionType.ActionPreProcess;
            Group = ActionGroup.GroupEnhance;
            if (0 == _iMorSize)
            {
                _iMorSize = 3;
            }
            if (0 == _iFilterSize)
            {
                _iFilterSize = 3;
            }
        }

        public ActionPreProcessData(string strName):this()
        {
            Name = strName;
        }
    }
}
