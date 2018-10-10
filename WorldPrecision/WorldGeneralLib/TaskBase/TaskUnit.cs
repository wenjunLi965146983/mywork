using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldGeneralLib.TaskBase
{
    public class TaskUnit
    {
        public TaskGroup taskGroup;
        public string strName;
        public TaskInfo taskInfo;
        public HiPerfTimer taskHiperTimer;
        public bool bManualStart = false;
        public TaskUnit(string name, TaskGroup taskGroup)
        {
            strName = name;
            this.taskGroup = taskGroup;
            taskInfo = new TaskInfo();
            taskHiperTimer = new HiPerfTimer();
        }
        virtual public void Process()
        {
            bool bAutoTrag = false;
            bool bManualTrag = false;
            bool bTragCondition = false;
            bTragCondition = true;
            if (taskInfo.bTaskAlarm)
            {
                //if (MainModule.MainFrm.bResetPress)
                //{
                //    taskInfo.bTaskAlarm = false;
                //    taskHiperTimer.Start();
                //}
                return;
            }
            //bAutoTrag = MainModule.MainFrm.bAuto && (!taskInfo.bTaskFinish) && (!taskInfo.bTaskOnGoing);
            bManualTrag = bManualStart;
            switch (taskInfo.iTaskStep)
            {
                case 0://判断是否有触发 
                    if ((bAutoTrag | bManualTrag) && bTragCondition)
                    {
                        bManualStart = false;
                        taskHiperTimer.Start();
                        taskInfo.bTaskOnGoing = true;
                        taskGroup.AddRunMessage("任务开始了");
                        taskInfo.iTaskStep = 10;
                    }
                    break;
                case 10:
                    {
                        if (taskHiperTimer.TimeUp(1))
                        {
                            taskHiperTimer.Start();
                            taskInfo.iTaskStep = 20;
                            taskGroup.AddRunMessage("任务10了");
                        }
                    }
                    break;
                case 20:
                    {
                        if (taskHiperTimer.TimeUp(1))
                        {
                            taskHiperTimer.Start();
                            taskInfo.iTaskStep = 30;
                            taskGroup.AddRunMessage("任务20了");
                        }
                    }
                    break;
                case 30:
                    {
                        if (taskHiperTimer.TimeUp(1))
                        {
                            taskHiperTimer.Start();
                            taskInfo.iTaskStep = 40;
                            taskGroup.AddRunMessage("任务30了");
                        }
                    }
                    break;
                case 40:
                    {
                        if (taskHiperTimer.TimeUp(1))
                        {
                            taskHiperTimer.Start();
                            taskInfo.iTaskStep = 50;
                            taskInfo.bTaskOnGoing = false;
                            taskGroup.AddRunMessage("任务40了");
                        }
                    }
                    break;
                case 50:
                    {
                        if (taskHiperTimer.TimeUp(1))
                        {
                            taskHiperTimer.Start();
                            taskInfo.iTaskStep = 0;
                            taskGroup.AddRunMessage("任务50了");
                            taskInfo.bTaskOnGoing = false;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
