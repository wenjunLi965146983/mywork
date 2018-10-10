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

namespace WorldGeneralLib.Vision.Actions.Calculate
{
    public class ActionCalculateData : ActionDataBase
    {

       
        public String strExpression
        {
            set;
            get;
        }

     
        public ActionCalculateData()
        {
            Name = "单元计算宏";
            Type = ActionType.ActionCalculate;
            Group = ActionGroup.GroupAssist;
           
        }
       
        public ActionCalculateData(string strName):this()
        {
            Name = strName;
            strExpression = "";
        }
    }
}
