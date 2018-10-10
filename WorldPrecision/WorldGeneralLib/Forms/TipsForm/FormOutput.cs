using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Functions;
using WeifenLuo.WinFormsUI.Docking;
using WorldGeneralLib.Alarm;
using System.Runtime.InteropServices;

namespace WorldGeneralLib.Forms.TipsForm
{
    public partial class FormOutput : DockContent
    {
        public string strTitle = "Task Output";
        public bool bShowLastestMsg = true;
        public NLog.Logger logger = null;

        public FormOutput()
        {
            InitializeComponent();
        }
        public FormOutput(string strTitle, NLog.Logger logger, bool bShowTaskStaListView):this()
        {
            this.strTitle = strTitle;
            this.logger = logger;
            panel1.Visible = bShowTaskStaListView;
        }

        private void FormOutput_Load(object sender, EventArgs e)
        {
            SetTitle(strTitle);
        }

        #region Set Title
        public void SetTitle(string title)
        {
            strTitle = title;
            this.Text = strTitle;
        }
        #endregion
        #region Write output info
        public void AddRunMessage(string strMsg)
        {
            try
            {
                string strTemp = string.Format("{0}  {1}", DateTime.Now.ToString(), strMsg);
                if (listBox1.InvokeRequired)
                {
                    Action action = () =>
                    {
                        if (listBox1.Items.Count > 2000)
                            listBox1.Items.Clear();
                        listBox1.Items.Add(strTemp);
                        if (bShowLastestMsg)
                        {
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        }
                    };
                    this.Invoke(action);
                }
                else
                {
                    if (listBox1.Items.Count > 2000)
                        listBox1.Items.Clear();
                    listBox1.Items.Add(strTemp);
                    if (bShowLastestMsg)
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        public void AddRunMessage(string strMsg , OutputLevel level)
        {
            try
            {
                string strTemp = string.Format("{0}  {1}", DateTime.Now.ToString(), strMsg);
                if (listBox1.InvokeRequired)
                {
                    Action action = () =>
                    {
                        if (listBox1.Items.Count > 2000)
                            listBox1.Items.Clear();
                        listBox1.Items.Add(strTemp);
                        if(bShowLastestMsg)
                        {
                            listBox1.SelectedIndex = listBox1.Items.Count - 1;
                        }     
                    };
                    this.Invoke(action);
                }
                else
                {
                    if (listBox1.Items.Count > 2000)
                        listBox1.Items.Clear();
                    listBox1.Items.Add(strTemp);
                    if (bShowLastestMsg)
                    {
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                    }
                }
                if(null != logger)
                {
                    switch (level)
                    {
                        case OutputLevel.Trace: logger.Trace(strMsg); break;
                        case OutputLevel.Debug: logger.Debug(strMsg); break;
                        case OutputLevel.Info: logger.Info(strMsg); break;
                        case OutputLevel.Warn: logger.Warn(strMsg); break;
                        case OutputLevel.Error: logger.Error(strMsg); break;
                        case OutputLevel.Fatal: logger.Fatal(strMsg); break;
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        #endregion

        public ListView GetListViewHandle()
        {
            return this.lvTaskStatus;
        }
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void showLastestMessageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bShowLastestMsg = !bShowLastestMsg;
        }
    }
}
