using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WorldGeneralLib.TaskBase
{
    public class TaskInfo
    {
        public int iTaskStep;
        public int iTaskAlarmStep;
        public string strTaskMes;
        public bool bTaskOnGoing;
        public bool bTaskAlarm;
        public bool bTaskFinish;
        public HiPerfTimer htTimer;

        public TaskInfo()
        {
            iTaskStep=0;
            iTaskAlarmStep = 0;
            strTaskMes="";
            bTaskOnGoing=false;
            bTaskAlarm=false;
            bTaskFinish=false;
            htTimer=new HiPerfTimer();
        }
    }
}
