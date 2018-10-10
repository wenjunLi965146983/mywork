﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WorldGeneralLib.Vision.Actions.Line
{
    public class ActionLineData : ActionDataBase
    {
       
        
        private String _strLineType;
        public String strLineType
        {
            set { _strLineType = value; }
            get { return _strLineType; }
        }
   
        private int _CThreshold1;
        public int CThreshold1
        {
            set { _CThreshold1 = value; }
            get { return _CThreshold1; }
        }
        private int _CThreshold2;
        public int CThreshold2
        {
            set { _CThreshold2 = value; }
            get { return _CThreshold2; }
        }
        private int _rho;//极径
        public int rho
        {
            set { _rho = value; }
            get { return _rho; }
        }
        private int _theta;//极径
        public int theta
        {
            set { _theta = value; }
            get { return _theta; }
        }
        private int _threshold;//极径
        public int threshold
        {
            set { _threshold = value; }
            get { return _threshold; }
        }
        private int _length;//极径
        public int length
        {
            set { _length = value; }
            get { return _length; }
        }
        private int _gab;//极径
        public int gab
        {
            set { _gab = value; }
            get { return _gab; }
        }

        

        public ActionLineData()
        {
            Name = "近似直线";
            Type = ActionType.ActionLine;
            Group = ActionGroup.GroupAssist;
           
        }
        public ActionLineData(string strName):this()
        {
            Name = strName;
        }
    }
}
