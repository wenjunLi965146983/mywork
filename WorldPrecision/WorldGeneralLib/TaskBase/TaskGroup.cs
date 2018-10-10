 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using WorldGeneralLib.Alarm;
using System.Threading;
using WorldGeneralLib.Forms.TipsForm;
using System.Drawing;

namespace WorldGeneralLib.TaskBase
{
    public class TaskGroup
    {
        public TaskInfo taskFresh;
        public List<TaskUnit> listTask;
        public List<bool> bPreOnGoingList;
        public FormOutput formOutput = null;
        private int _iPeriod;
        public TaskGroup()
        {
            _iPeriod = 10;
            taskFresh = new TaskInfo();
            listTask = new List<TaskUnit>();
            bPreOnGoingList = new List<bool>();
        }

        public TaskGroup(FormOutput formOutput) : this()
        {
            this.formOutput = formOutput;
        }

        public void AddTaskUnit(TaskUnit task)
        {
            listTask.Add(task);
            bPreOnGoingList.Add(false);
        }
        public void StartThread()
        {
            Thread SupplyThread = new Thread(threadFunction);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThreadAlwayScan()
        {
            Thread SupplyThread = new Thread(threadFunctionAlwayScan);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThread(int period)
        {
            _iPeriod = period;
            Thread SupplyThread = new Thread(threadFunction);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        public void StartThreadAlwayScan(int period)
        {
            _iPeriod = period;
            Thread SupplyThread = new Thread(threadFunctionAlwayScan);
            SupplyThread.IsBackground = true;
            SupplyThread.Start();
        }
        private void threadFunction()
        {
            Thread.Sleep(1000);

            while (true)
            {
                System.Threading.Thread.Sleep(_iPeriod);
                //if (MainModule.MainFrm.bEstop || MainModule.MainFrm.bDoorOpen || (!MainModule.MainFrm.bHomeReady))
                //{
                //    continue;
                //}
                foreach (TaskUnit taskItem in listTask)
                {
                    taskItem.Process();
                }
                RefreshTaskStatus();
            }

        }
        private void threadFunctionAlwayScan()
        {
            Thread.Sleep(1000);

            while (true)
            {
                System.Threading.Thread.Sleep(_iPeriod);
                foreach (TaskUnit taskItem in listTask)
                {
                    taskItem.Process();
                }
                RefreshTaskStatus();
            }
        }

        private void RefreshTaskStatus()
        {
            try
            {
                if (null == formOutput)
                    return;
                ListView lv = formOutput.GetListViewHandle();
                if (lv == null)
                    return;
                switch (taskFresh.iTaskStep)
                {
                    case 0:
                        {
                            taskFresh.htTimer.Start();
                            taskFresh.iTaskStep = 10;
                        }
                        break;
                    case 10:
                        if (taskFresh.htTimer.TimeUp(0.1))
                        {
                            try
                            {
                                if (lv != null)
                                {
                                    Action action = () =>
                                    {
                                        if (lv.Items.Count != listTask.Count)
                                        {
                                            lv.Items.Clear();
                                            foreach (TaskUnit item in listTask)
                                            {
                                                ListViewItem lvi = lv.Items.Add(item.strName);
                                                lvi.UseItemStyleForSubItems = false;
                                                lvi.SubItems.Add("");
                                                lvi.SubItems.Add("");
                                                lvi.SubItems.Add("");
                                                lvi.SubItems.Add("");
                                                lvi.SubItems.Add("");
                                            }
                                        }

                                        lv.BeginUpdate();
                                        for (int i = 0; i < listTask.Count; i++)
                                        {
                                            #region TaskRefresh
                                            if (listTask[i].taskInfo.bTaskOnGoing)
                                            {
                                                lv.Items[i].SubItems[1].Text = "OnGoing";
                                                lv.Items[i].SubItems[1].BackColor = Color.Green;
                                                lv.Items[i].SubItems[2].Text = "Step: " + listTask[i].taskInfo.iTaskStep.ToString();
                                                lv.Items[i].SubItems[3].Text = "CT: " + listTask[i].taskInfo.htTimer.Duration.ToString("0.0") + " s";
                                                lv.Items[i].SubItems[4].Text = "";
                                            }
                                            else if(listTask[i].taskInfo.bTaskFinish)
                                            {
                                                lv.Items[i].SubItems[1].Text = "Finish";
                                                lv.Items[i].SubItems[1].BackColor = lv.BackColor;
                                                lv.Items[i].SubItems[2].Text = "Step: " + listTask[i].taskInfo.iTaskStep.ToString();
                                                lv.Items[i].SubItems[3].Text = " ";
                                                lv.Items[i].SubItems[4].Text = "";
                                            }
                                            else if(listTask[i].taskInfo.bTaskAlarm)
                                            {
                                                lv.Items[i].SubItems[1].Text = "Alarm";
                                                lv.Items[i].SubItems[1].BackColor = Color.Red;
                                                lv.Items[i].SubItems[2].Text = "Step: " + listTask[i].taskInfo.iTaskStep.ToString();
                                                lv.Items[i].SubItems[3].Text = " ";
                                                if(listTask[i].taskInfo.iTaskStep > 0)
                                                {
                                                    lv.Items[i].SubItems[4].Text = "报警解除后，任务将继续执行";
                                                }
                                                else
                                                {
                                                    lv.Items[i].SubItems[4].Text = "报警解除后，任务重新执行";
                                                }
                                            }
                                            else
                                            {
                                                lv.Items[i].SubItems[1].Text = "Idle";
                                                lv.Items[i].SubItems[1].BackColor = lv.BackColor;
                                                lv.Items[i].SubItems[2].Text = "Step: " + listTask[i].taskInfo.iTaskStep.ToString();
                                                lv.Items[i].SubItems[3].Text = " ";
                                                lv.Items[i].SubItems[4].Text = "";
                                            }

                                            if (bPreOnGoingList[i] != listTask[i].taskInfo.bTaskOnGoing)
                                            {
                                                if (listTask[i].taskInfo.bTaskOnGoing)
                                                {
                                                    listTask[i].taskInfo.htTimer.Start();
                                                }
                                            }
                                            bPreOnGoingList[i] = listTask[i].taskInfo.bTaskOnGoing;
                                            #endregion
                                        }
                                        lv.EndUpdate();
                                    };
                                    lv.Invoke(action);

                                }
                            }
                            catch
                            {
                                //MessageBox.Show(ex.Message);
                            }
                            taskFresh.iTaskStep = 20;
                        }
                        break;
                    case 20:
                        if (true)
                        {
                            taskFresh.iTaskStep = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
        }
        public void AddRunMessage(string strMessage)
        {
            try
            {
                if(formOutput != null)
                {
                    formOutput.AddRunMessage(strMessage);
                }
            }
            catch (Exception)
            {

            }
        }

        public void AddRunMessage(string strMessage, OutputLevel level)
        {
            try
            {
                if (formOutput != null)
                {
                    formOutput.AddRunMessage(strMessage , level);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
