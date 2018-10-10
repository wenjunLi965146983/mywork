using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using WorldGeneralLib.Functions;
using WorldGeneralLib.Vision.Forms;
using WorldGeneralLib.Login;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.PLC;
using WorldGeneralLib.Table;
using WorldGeneralLib.Data;
using WorldGeneralLib.IO;
using WorldGeneralLib.Forms.TipsForm;
using WorldGeneralLib.Company.Catl.Andon;
using WorldGeneralLib.Company.Catl.MES.G08;

namespace WorldGeneralLib.Forms
{
    public partial class FormMain : Form
    {
        //Main Forms
        public Form formStart;
        public Form formManualEx;
        public FormManual formManual;
        public FormIoMonitor formIoMonitor;
        public FormUserParam formUserParam;
        public FormSysParam formSysParam;
        public FormVision formVision;
        public FormTesting formTesting;
        public FormOutput formOutput;
        public FormOperator formOperator;

        //Andon && MES
        public Andon andon;
        public  MESClass mes;

        //机器启停复位
        public bool bClrFlag = false;       //Alarm clear button pressed
        public bool bRunFlag = false;
        public MacHomeSta macHomeSta = MacHomeSta.WaittingReset;

        public bool bExit = false;
        public bool bLoginSwitch;
        public MacSta macSta = MacSta.Stop;

        public delegate void EventTableDataReLoad();
        public delegate void EventStartButtonPushedHandler();
        public delegate void EventStopButtonPushedHandler();
        public delegate void EventResetButtonPushedHandler();
        public delegate void EventClearButtonPressedHandler();
        public delegate void EventClearButtonReleasedHandler();

        public event EventTableDataReLoad eventTableDataReLoad;
        public event EventStartButtonPushedHandler eventStartButtonPushedHandler;
        public event EventStopButtonPushedHandler eventStopButtonPushedHandler;
        public event EventResetButtonPushedHandler eventResetButtonPushedHandler;
        public event EventClearButtonPressedHandler eventClearButtonPressedHandler;
        public event EventClearButtonReleasedHandler eventClearButtonReleasedHandler;

        public FormMain()
        {
            InitializeComponent();

            //andon = new Andon();
            //andon.bAndonSwitch = true;
            mes = new MESClass();
        }

        #region MenuStrip1
        #region Open
        private void OpenFile()
        {
            try
            {
                bool bErr = false;
                string strFullPath = "";
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Multiselect = false;
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                dialog.Title = "配方读取";
                
                dialog.RestoreDirectory = false;
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                strFullPath = dialog.FileName;
                TableDoc doc = TableDoc.LoadObj(strFullPath, ref bErr);
                if(bErr)
                {
                    MessageBox.Show("不是正确的配方文件！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }

                TableManage.docTable = doc;
                TableManage.InitTables();
                if(null != eventTableDataReLoad)
                {
                    this.eventTableDataReLoad();
                }
                MessageBox.Show("加载成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            catch (Exception ex)
            {
                TableManage.strConfigFile = string.Empty;
                MessageBox.Show("加载失败！\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        #endregion
        #region Save
        private void saveSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(TableManage.strConfigFile))
                {
                    if (!TableManage.docTable.SaveDoc(TableManage.strConfigFile))
                    {
                        throw new Exception();
                    }
                }
                SaveHardwareDoc();
                SaveAlarmDoc();
                MessageBox.Show("保存成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        #endregion
        #region Save As
        private void SaveAs()
        {
            try
            {
                if(null == TableManage.docTable)
                {
                    MessageBox.Show("请先加载配置文件！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                string strFullPath = "";
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "另存为";
                dialog.Filter = "xml files (*.xml)|*.xml";
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                strFullPath = dialog.FileName;
                SaveHardwareDoc();
                SaveAlarmDoc();
                if(!TableManage.docTable.SaveDoc(strFullPath))
                {
                    throw new Exception();
                }
                MessageBox.Show("另存为成功！", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
        }
        #endregion
        #region Exit
        private void exitQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        #endregion
        #endregion

        #region ToolStatusStrip
        public void SetEStopStatus(bool bOn)
        {
            try
            {
                Action action = () =>
                {
                    toolStripStatusESTOP.BackColor = bOn ? Color.Red : Color.Green;
                };
                statusBar.Invoke(action);
            }
            catch (Exception)
            {

            }
        }
        public void SetDoorStatus(bool bOn)
        {
            try
            {
                Action action = () =>
                {
                    toolStripStatusDoor.BackColor = bOn ? Color.Red : Color.Green;
                };
                statusBar.Invoke(action);
            }
            catch (Exception)
            {

            }
        }
        public void SetPressureStatus(bool bOn)
        {
            try
            {
                Action action = () =>
                {
                    toolStripStatusPressure.BackColor = bOn ? Color.Red : Color.Green;
                };
                statusBar.Invoke(action);
            }
            catch (Exception)
            {

            }
        }

        public void SetCodeReaderStatus(int index,bool bOn)
        {
            try
            {
                Action action = () =>
                {
                    if(1 == index)
                    {
                        toolStripStatusCodeReader1.BackColor = bOn ? Color.Green : Color.Red;
                    }
                    else
                    {
                        toolStripStatusCodeReader2.BackColor = bOn ? Color.Green : Color.Red;
                    }
                };
                statusBar.Invoke(action);
            }
            catch (Exception)
            {

            }
        }
        public void SetPLCStatus(int index, bool bOn)
        {
            try
            {
                Action action = () =>
                {
                    if (1 == index)
                    {
                        toolStripStatusMainPLC.BackColor = bOn ? Color.Green : Color.Red;
                    }
                    else
                    {
                        toolStripStatusRemotePLC.BackColor = bOn ? Color.Green : Color.Red;
                    }
                };
                statusBar.Invoke(action);
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region Methods
        private void SetViewMenuItems()
        {
            this.viewToolStripMenuItem.DropDownItems.Clear();
            ArrayList basePanels = BSE.Windows.Forms.PanelSettingsManager.FindPanels(true, this.Controls);
            foreach (BSE.Windows.Forms.BasePanel basePanel in basePanels)
            {
                BSE.Windows.Forms.Panel panel = basePanel as BSE.Windows.Forms.Panel;
                if ((panel != null) && ((panel.Dock != DockStyle.Fill) || (panel.Dock != DockStyle.None)) && (panel.ShowCloseIcon == true))
                {
                    ToolStripMenuItem menuItem = new ToolStripMenuItem();
                    menuItem.Text = panel.Text;
                    menuItem.Image = panel.Image;
                    menuItem.Tag = panel;
                    menuItem.Click += new EventHandler(ViewMenuItemsClick);
                    if (panel.Visible == true)
                    {
                        menuItem.Checked = true;
                    }
                    this.viewToolStripMenuItem.DropDownItems.Add(menuItem);
                }
            }

            ToolStripMenuItem layoutItem = new ToolStripMenuItem();
            layoutItem.Text = "Layout by Xml File";
            layoutItem.Click += new EventHandler(ViewMenuItemsClick);
            //if (panel.Visible == true)
            //{
            //    menuItem.Checked = true;
            //}
            this.viewToolStripMenuItem.DropDownItems.Add(layoutItem);
        }
        private void ViewMenuItemsClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                BSE.Windows.Forms.Panel panel = menuItem.Tag as BSE.Windows.Forms.Panel;
                if (panel != null)
                {
                    panel.Visible = !panel.Visible;
                }
            }
        }
        private void SetMenuBtnChoosed(object sender)
        {
            foreach (Control ctrl in panelMenuBtns.Controls)
            {
                if (ctrl is RibbonStyle.RibbonMenuButton)
                {
                    ((RibbonStyle.RibbonMenuButton)ctrl).ColorBase = Color.Transparent;
                }
            }
            if (sender is RibbonStyle.RibbonMenuButton)
            {
                ((RibbonStyle.RibbonMenuButton)sender).ColorBase = Color.FromArgb(38, 135, 251);
            }

        }
        private void SaveHardwareDoc()
        {
            if (null != HardwareManage.docHardware)
            {
                HardwareManage.docHardware.SaveDoc();
            }
            if (null != PLCDriverManageClass.docPlc)
            {
                PLCDriverManageClass.docPlc.SaveDoc();
            }
            if (null != TableManage.docTable)
            {
                TableManage.docTable.SaveDoc();
            }
            if (null != DataManage.docData)
            {
                DataManage.docData.SaveDataDoc();
            }
            if (null != IOManage.docIO)
            {
                IOManage.docIO.SaveDoc();
            }
        }
        private void SaveAlarmDoc()
        {
            if (null != MainModule.alarmManage.docAlarm)
            {
                MainModule.alarmManage.docAlarm.SaveDoc();
            }
        }

        private void Exit()
        {
            bExit = true;
            foreach(KeyValuePair<string, HardwareBase> item in HardwareManage.dicHardwareDriver)
            {
                item.Value.Close();
            }

            formStart.Close();
            formManual.Close();
            formIoMonitor.Close();
            formUserParam.Close();
            formSysParam.Close();

            this.Close();
        }
        public FormOutput CreateNewOutputWindow(string strTitle , NLog.Logger logger,bool bShowTaskStaListview)
        {
            try
            {
                FormOutput form = null;

                form = new FormOutput(strTitle, logger, bShowTaskStaListview);
                form.Show(dockPanel1);

                return form;
            }
            catch (Exception)
            {
            }
            return null;
        }
        #endregion

        #region Event Handlers
        #region Menu
        private void userManageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 2)
            {
                FormTips formTips = new FormTips(3, false);
                formTips.SetTipsText("抱歉，您没有权限进行此操作，请与管理员联系。");
                formTips.ShowDialog();
                return;
            }

            if (string.IsNullOrEmpty(LoginManage.strCurrUserName))
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
            }
            else
            {
                FormUserManage formUserManage = new FormUserManage();
                formUserManage.ShowDialog();
            }
        }
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginManage.strCurrUserName = string.Empty;
            LoginManage.iCurrUserLevel = -1;
        }
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginManage.strCurrUserName))
            {
                FormLogin formLogin = new FormLogin();
                formLogin.ShowDialog();
            }
        }
        private void btnStartView_Click(object sender, EventArgs e)
        {
            SetMenuBtnChoosed(sender);

            formManual.Visible = false;
            formIoMonitor.Visible = false;
            formTesting.Visible = false;
            formUserParam.Visible = false;
            formSysParam.Visible = false;
            formVision.Visible = false;

            formStart.Visible = true;
        }
        private void btnManualView_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 1)
            {
                FormTips formTips = new FormTips(3, false);
                formTips.SetTipsText("抱歉，您没有权限进行此操作，请与工程师联系。");
                formTips.ShowDialog();
                return;
            }

            SetMenuBtnChoosed(sender);
            formStart.Visible = false;
            formIoMonitor.Visible = false;
            formTesting.Visible = false;
            formUserParam.Visible = false;
            formSysParam.Visible = false;
            formVision.Visible = false;

            formManual.Visible = true;
        }
        private void btnMonitorView_Click(object sender, EventArgs e)
        {
            SetMenuBtnChoosed(sender);
            formStart.Visible = false;
            formTesting.Visible = false;
            formUserParam.Visible = false;
            formSysParam.Visible = false;
            formManual.Visible = false;
            formVision.Visible = false;

            formIoMonitor.Visible = true;
        }
        private void btnTestingView_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 1)
            {
                FormTips formTips = new FormTips(3, false);
                formTips.SetTipsText("抱歉，您没有权限进行此操作，请与工程师联系。");
                formTips.ShowDialog();
                return;
            }

            panelBottom.Visible = false;
            splitterBottom.Visible = false;

            SetMenuBtnChoosed(sender);
            formStart.Visible = false;
            formIoMonitor.Visible = false;
            formManual.Visible = false;
            formUserParam.Visible = false;
            formSysParam.Visible = false;
            formVision.Visible = false;

            formTesting.Visible = true;
        }
        private void ribbtnVision_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 1)
            {
                FormTips formTips = new FormTips(3, false);
                formTips.SetTipsText("抱歉，您没有权限进行此操作，请与工程师联系。");
                formTips.ShowDialog();
                return;
            }

            panelBottom.Visible = false;
            splitterBottom.Visible = false;

            SetMenuBtnChoosed(sender);
            formStart.Visible = false;
            formIoMonitor.Visible = false;
            formManual.Visible = false;
            formUserParam.Visible = false;
            formSysParam.Visible = false;
            formTesting.Visible = false;

            formVision.Visible = true;
        }
        private void btnUserParamView_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 1)
            {
                FormTips tipsFrm = new FormTips(3, false);
                tipsFrm.SetTipsText("抱歉，您没有权限进行此操作，请与工程师联系。");
                tipsFrm.ShowDialog();
                return;
            }
            panelBottom.Visible = false;
            splitterBottom.Visible = false;

            formStart.Visible = false;
            formTesting.Visible = false;
            formManual.Visible = false;
            formIoMonitor.Visible = false;
            formSysParam.Visible = false;
            formVision.Visible = false;

            formUserParam.Visible = true;
            SetMenuBtnChoosed(sender);
        }
        private void btnSysParamView_Click(object sender, EventArgs e)
        {
            if (LoginManage.iCurrUserLevel < 2)
            {
                FormTips tipsFrm = new FormTips(3, false);
                tipsFrm.SetTipsText("抱歉，您没有权限进行此操作，请与管理员联系。");
                tipsFrm.ShowDialog();
                return;
            }
            panelBottom.Visible = false;
            splitterBottom.Visible = false;

            formStart.Visible = false;
            formTesting.Visible = false;
            formManual.Visible = false;
            formIoMonitor.Visible = false;
            formUserParam.Visible = false;
            formVision.Visible = false;

            formSysParam.Visible = true;
            SetMenuBtnChoosed(sender);
        }
        #endregion
        #region Load & Exit
        private void EventsInit()
        {
            this.eventTableDataReLoad += new EventTableDataReLoad(formSysParam.EventTableDataReLoadHandler);
            this.eventTableDataReLoad += new EventTableDataReLoad(formUserParam.EventTableDataReLoadHandler);
            this.eventTableDataReLoad += new EventTableDataReLoad(formManual.EventTableDataReLoadHandler);
        }
        private void InitMainForms()
        {
            try
            {
                formOperator.TopLevel = false;
                panelMacOperator.Controls.Add(formOperator);
                formOperator.Dock = DockStyle.Left;
                formOperator.Show();

                formManual.TopLevel = false;
                panelMain.Controls.Add(formManual);
                formManual.Dock = DockStyle.Fill;
                formManual.Hide();

                formTesting.TopLevel = false;
                panelMain.Controls.Add(formTesting);
                formTesting.Dock = DockStyle.Fill;
                formTesting.Hide();

                formIoMonitor.TopLevel = false;
                panelMain.Controls.Add(formIoMonitor);
                formIoMonitor.Dock = DockStyle.Fill;
                formIoMonitor.Hide();

                formVision.TopLevel = false;
                panelMain.Controls.Add(formVision);
                formVision.Dock = DockStyle.Fill;
                formVision.Hide();

                formUserParam.TopLevel = false;
                panelMain.Controls.Add(formUserParam);
                formUserParam.Dock = DockStyle.Fill;
                formUserParam.Hide();

                formSysParam.TopLevel = false;
                panelMain.Controls.Add(formSysParam);
                formSysParam.Dock = DockStyle.Fill;
                formSysParam.Hide();

                formStart.TopLevel = false;
                panelMain.Controls.Add(formStart);
                formStart.Dock = DockStyle.Fill;
                formStart.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void MainForm_Load(object sender, System.EventArgs e)
        {
            MainModule.LoadDoc();
            MainModule.InitHardware();
            InitMainForms();
            EventsInit();

            timerRefresh.Start();
            //andon.StartAndonThread();

            SetViewMenuItems();
            SetMenuBtnChoosed(btnStartView);
            formStart.Show();
        }
        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            panelTopMid.Width = this.Size.Width * 2 / 5;
            panelTopMid.Location = new Point((this.Size.Width - panelTopMid.Width) / 2, 0);
            picBoxUser.Location = new Point(panelTopMid.Location.X + panelTopMid.Width + 27, 13);
            labCurrUser.Location = new Point(picBoxUser.Location.X + 35, 20);

            panelMenuBtns.Width = panelTopMid.Width;
            panelMenuBtns.Location = new Point(panelTopMid.Location.X, 1);
            int iTotalWidth = 0;
            int iCurrLocationX = 0;
            int iSpace = 12;
            foreach (Control ctrl in panelMenuBtns.Controls)
            {
                if(ctrl.Visible)
                {
                    iTotalWidth += ctrl.Width;
                    iTotalWidth += iSpace;
                }
            }
            if (iTotalWidth > 0)
            {
                iTotalWidth -= iSpace;
                if (panelMenuBtns.Width > iTotalWidth)
                    iCurrLocationX = (panelMenuBtns.Width - iTotalWidth) / 2;
                else
                    iCurrLocationX = iSpace;
                foreach (Control ctrl in panelMenuBtns.Controls)
                {
                    if(ctrl.Visible)
                    {
                        ctrl.Location = new Point(iCurrLocationX, (panelMenuBtns.Height - ctrl.Height) / 2);
                        iCurrLocationX += (ctrl.Width + iSpace);
                    }
                }
            }
        }
        private void btnMiniSize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Exit();
        }
        private void panelLeft_CloseClick(object sender, EventArgs e)
        {
            if (sender is BSE.Windows.Forms.Panel panel)
            {
                panel.Visible = false;
                SetViewMenuItems();
            }
        }
        private void panelLeft_VisibleChanged(object sender, EventArgs e)
        {
            splitterLeft.Visible = panelLeft.Visible;
            SetViewMenuItems();
        }
        private void panelRight_CloseClick(object sender, EventArgs e)
        {
            if (sender is BSE.Windows.Forms.Panel panel)
            {
                panel.Visible = false;
                SetViewMenuItems();
            }
        }
        private void panelRight_VisibleChanged(object sender, EventArgs e)
        {
            splitterRight.Visible = panelRight.Visible;
            SetViewMenuItems();
        }
        private void panelBottom_CloseClick(object sender, EventArgs e)
        {
            if (sender is BSE.Windows.Forms.Panel panel)
            {
                panel.Visible = false;
                SetViewMenuItems();
            }
        }
        private void panelBottom_VisibleChanged(object sender, EventArgs e)
        {
            splitterBottom.Visible = panelBottom.Visible;
            SetViewMenuItems();
        }
        #endregion
        #region Toolbar Button Event Handler
        private void toolBarBtnOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        private void toorBtnPanelLeft_Click(object sender, EventArgs e)
        {
            panelLeft.Visible = !panelLeft.Visible;
            splitterLeft.Visible = panelLeft.Visible;
        }
        private void toolBarBtnPanelBottom_Click(object sender, EventArgs e)
        {
            panelBottom.Visible = !panelBottom.Visible;
            splitterBottom.Visible = panelBottom.Visible;
        }
        private void toolBarBtnPanelRight_Click(object sender, EventArgs e)
        {
            panelRight.Visible = !panelRight.Visible;
            splitterRight.Visible = panelRight.Visible;
        }
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            SaveHardwareDoc();
            SaveAlarmDoc();
        }
        #endregion
        #region Mac operator events
        public void EventStartButtonPushed(object sender, EventArgs e)
        {
            eventStartButtonPushedHandler?.Invoke();
        }
        public void EventStopButtonPushed(object sender, EventArgs e)
        {
            eventStopButtonPushedHandler?.Invoke();
        }
        public void EventResetButtonPushed(object sender, EventArgs e)
        {
            eventResetButtonPushedHandler?.Invoke();
        }
        public void EventClearButtonPressed(object sender, EventArgs e)
        {
            eventClearButtonPressedHandler?.Invoke();
        }
        public void EventClearButtonReleased(object sender, EventArgs e)
        {
            eventClearButtonReleasedHandler?.Invoke();
        }
        #endregion
        #endregion

        #region Timer
        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(LoginManage.strCurrUserName))
                {
                    logoutToolStripMenuItem.Enabled = true;
                    loginToolStripMenuItem.Enabled = false;
                    userManageToolStripMenuItem.Enabled = true;
                    labCurrUser.Text = LoginManage.strCurrUserName;
                }
                else
                {
                    logoutToolStripMenuItem.Enabled = false;
                    loginToolStripMenuItem.Enabled = true;
                    userManageToolStripMenuItem.Enabled = false;
                    labCurrUser.Text = "未登录";
                }

                if(macHomeSta != MacHomeSta.Reseted)
                {
                    if (toolStripStatusHomeReady.BackColor != Color.Red)
                        toolStripStatusHomeReady.BackColor = Color.Red;
                }
                else
                {
                    if (toolStripStatusHomeReady.BackColor != Color.Green)
                        toolStripStatusHomeReady.BackColor = Color.Green;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

    }
}