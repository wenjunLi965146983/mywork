using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;
using WorldGeneralLib.Table;
using WorldGeneralLib.Data;
using WorldGeneralLib.IO;
using WorldGeneralLib.Forms.ToolForms;
using System.IO;

namespace WorldGeneralLib.Forms
{
    public partial class FormSysParam : Form
    {
        public FormBase formHardware;
        public FormBase formTableSys;
        public FormBase formIO;
        public FormBase formDataSysView;
        public FormBase formAlarmManage;
        public FormSysParam()
        {
            InitializeComponent();
        }

        #region Load
        private void Init()
        {
            HardwareTreeViewInit();
            TableTreeViewInit();
            TreeViewIOInit();
            TreeViewDataGroupInit();
            AlarmManageViewInit();

            if (null == DataManage.formDataSysView)
            {
                DataManage.formDataSysView = new FormDataSysView();
            }
            DataManage.formDataSysView.TopLevel = false;
            DataManage.formDataSysView.Dock = DockStyle.Fill;
            formDataSysView.panelMain.Controls.Add(DataManage.formDataSysView);
            DataManage.formDataSysView.Show();

            if (null == TableManage.formTableSysView)
            {
                TableManage.formTableSysView = new FormTableSysView();
            }
            TableManage.formTableSysView.TopLevel = false;
            TableManage.formTableSysView.Dock = DockStyle.Fill;
            formTableSys.panelMain.Controls.Add(TableManage.formTableSysView);
            TableManage.formTableSysView.Show();

            if (null == MainModule.alarmManage.formAlarmManage)
            {
                MainModule.alarmManage.formAlarmManage = new Alarm.FormAlarmManage();
            }
            MainModule.alarmManage.formAlarmManage.TopLevel = false;
            MainModule.alarmManage.formAlarmManage.Dock = DockStyle.Fill;
            formAlarmManage.panelMain.Controls.Add(MainModule.alarmManage.formAlarmManage);
            MainModule.alarmManage.formAlarmManage.Show();

            if (null == IOManage.formIOSetting)
            {
                IOManage.formIOSetting = new FormIOSetting();
            }
            IOManage.formIOSetting.TopLevel = false;
            IOManage.formIOSetting.Dock = DockStyle.Fill;
            formIO.panelMain.Controls.Add(IOManage.formIOSetting);
            IOManage.formIOSetting.Show();
        }
        private void FormSysParam_Load(object sender, EventArgs e)
        {
            formTableSys = new FormBase();
            formTableSys.TopLevel = false;
            panelMain.Controls.Add(formTableSys);
            formTableSys.Dock = DockStyle.Fill;

            formHardware = new FormBase();
            formHardware.TopLevel = false;
            panelMain.Controls.Add(formHardware);
            formHardware.Dock = DockStyle.Fill;

            formIO = new FormBase();
            formIO.TopLevel = false;
            panelMain.Controls.Add(formIO);
            formIO.Dock = DockStyle.Fill;

            formDataSysView = new FormBase();
            formDataSysView.TopLevel = false;
            panelMain.Controls.Add(formDataSysView);
            formDataSysView.Dock = DockStyle.Fill;

            formAlarmManage = new FormBase();
            formAlarmManage.TopLevel = false;
            panelMain.Controls.Add(formAlarmManage);
            formAlarmManage.Dock = DockStyle.Fill;

            formHardware.Show();
            Init();
        }
        #endregion

        #region ContextMenuStrip
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                #region Hardware
                if (treeViewHardware.SelectedNode != null)
                {
                    if (treeViewHardware.SelectedNode.Level == 1)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemAdd);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        toolStripMenuItemAdd.DropDownItems.Clear();

                        string strVender = GetVenderByString(treeViewHardware.SelectedNode.Name).ToString();
                        string strType = GetHardwareTypeByString(treeViewHardware.SelectedNode.Parent.Name).ToString();
                        string strTemp = string.Format("{0}_{1}",strVender,strType);
                        foreach (string s in Enum.GetNames(typeof(HardwareSeries)))
                        {
                            if(s.Contains(strTemp))
                            {
                                ToolStripMenuItem menuItem = new ToolStripMenuItem();
                                menuItem.Text = s;
                                menuItem.Size = new Size(192, 22);
                                toolStripMenuItemAdd.DropDownItems.Add(menuItem);
                                menuItem.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
                            }
                        }

                        return;
                    }
                    if (treeViewHardware.SelectedNode.Level == 2)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemRemove);
                        contextMenuStrip1.Items.Add(toolStripMenuItemRename);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        return;
                    }
                }
                #endregion
                #region Table
                if (treeViewTable.SelectedNode != null)
                {
                    if (treeViewTable.SelectedNode.Level == 0)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemAdd);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        contextMenuStrip1.Items.Add(导出ToolStripMenuItem);
                        toolStripMenuItemAdd.DropDownItems.Clear();

                        return;
                    }
                    if (treeViewTable.SelectedNode.Level == 1)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemRemove);
                        contextMenuStrip1.Items.Add(toolStripMenuItemRename);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        return;
                    }
                }
                #endregion
                #region IO
                if (treeViewIO.SelectedNode != null)
                {
                    if (treeViewIO.SelectedNode.Level == 0)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemAdd);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        contextMenuStrip1.Items.Add(导出ToolStripMenuItem);
                        toolStripMenuItemAdd.DropDownItems.Clear();

                        return;
                    }
                    if (treeViewIO.SelectedNode.Level == 1)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemRemove);
                        contextMenuStrip1.Items.Add(toolStripMenuItemRename);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        return;
                    }
                }
                #endregion
                #region Data
                if (treeViewDataGroup.SelectedNode != null)
                {
                    if (treeViewDataGroup.SelectedNode.Level == 0)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemAdd);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        toolStripMenuItemAdd.DropDownItems.Clear();

                        return;
                    }
                    if (treeViewDataGroup.SelectedNode.Level == 1)
                    {
                        contextMenuStrip1.Items.Clear();
                        contextMenuStrip1.Items.Add(toolStripMenuItemRemove);
                        contextMenuStrip1.Items.Add(toolStripMenuItemRename);
                        contextMenuStrip1.Items.Add(toolStripMenuItemProperty);
                        return;
                    }
                }
                #endregion
            }
            catch (Exception)
            {
            }

        }
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region Hardware
                if (null != treeViewHardware.SelectedNode)
                {
                    try
                    {
                        if (1 != treeViewHardware.SelectedNode.Level)
                            return;

                        string strType = treeViewHardware.SelectedNode.Parent.Name;
                        string strVender = treeViewHardware.SelectedNode.Name;
                        string strSeries = ((ToolStripMenuItem)sender).Text;

                        HardwareType type = GetHardwareTypeByString(strType);
                        HardwareVender vender = GetVenderByString(strVender);
                        HardwareSeries series = (HardwareSeries)Enum.Parse(typeof(HardwareSeries), strSeries);

                        string strHardwareName = HardwareManage.AddNewHardware(vender, type, series);
                        if (string.IsNullOrEmpty(strHardwareName))
                        {
                            throw new Exception();
                        }
                        TreeNode newNode = new TreeNode();
                        newNode.Name = strHardwareName;
                        newNode.Text = strHardwareName;
                        

                        treeViewHardware.SelectedNode.Nodes.Add(newNode);
                        treeViewHardware.SelectedNode = newNode;
                        newNode.ContextMenuStrip = contextMenuStrip1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed.\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
                #region Table
                if (null != treeViewTable.SelectedNode && 0 == treeViewTable.SelectedNode.Level)
                {
                    try
                    {
                        string strNewTableName = TableManage.AddNewTable();
                        if (strNewTableName == null)
                        {
                            throw new Exception();
                        }

                        TreeNode newNode = new TreeNode();
                        newNode.Text = strNewTableName;
                        newNode.Name = strNewTableName;
                        newNode.ContextMenuStrip = contextMenuStrip1;

                        if (!treeViewTable.SelectedNode.Nodes.ContainsKey(newNode.Name))
                        {
                            treeViewTable.SelectedNode.Nodes.Add(newNode);
                        }
                        treeViewTable.SelectedNode = newNode;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed.\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
                #region IO
                if (null != treeViewIO.SelectedNode && 0 == treeViewIO.SelectedNode.Level)
                {
                    bool bInput = false;
                    if (treeViewIO.SelectedNode.Name == "Input")
                        bInput = true;
                    try
                    {
                        string strNewName = IOManage.AddIO(bInput);
                        TreeNode newNode = new TreeNode();
                        newNode.Name = strNewName ?? throw new Exception();
                        newNode.Text = strNewName;

                        treeViewIO.SelectedNode.Nodes.Add(newNode);
                        treeViewIO.SelectedNode = newNode;
                        newNode.ContextMenuStrip = contextMenuStrip1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed.\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
                #region DataGroup
                if (null != treeViewDataGroup.SelectedNode && 0 == treeViewDataGroup.SelectedNode.Level)
                {
                    try
                    {
                        string strNewGroupName = DataManage.AddNewDataGroup();
                        if (strNewGroupName == null)
                        {
                            throw new Exception();
                        }

                        TreeNode newNode = new TreeNode();
                        newNode.Name = strNewGroupName;
                        newNode.Text = strNewGroupName;

                        treeViewDataGroup.SelectedNode.Nodes.Add(newNode);
                        treeViewDataGroup.SelectedNode = newNode;
                        newNode.ContextMenuStrip = contextMenuStrip1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed.\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
            }
            catch (Exception)
            {
            }
        }
        private void toolStripMenuItemRemove_Click(object sender, EventArgs e)
        {
            try
            {
                #region Hardware
                if (null != treeViewHardware.SelectedNode)
                {
                    if (2 == treeViewHardware.SelectedNode.Level)
                    {
                        if (HardwareManage.RemoveHardware(treeViewHardware.SelectedNode.Name))
                        {
                            treeViewHardware.Nodes.Remove(treeViewHardware.SelectedNode);
                            MessageBox.Show("Remove successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        else
                        {
                            MessageBox.Show("Remove unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                    }
                }
                #endregion
                #region Table
                else if (null != treeViewTable.SelectedNode && 1 == treeViewTable.SelectedNode.Level)
                {
                    try
                    {
                        if (TableManage.RemoveTable(treeViewTable.SelectedNode.Name))
                        {
                            treeViewTable.Nodes.Remove(treeViewTable.SelectedNode);
                            MessageBox.Show("Remove successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        else
                        {
                            MessageBox.Show("Remove unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion
                #region IO
                else if (null != treeViewIO.SelectedNode && 1 == treeViewIO.SelectedNode.Level)
                {
                    try
                    {
                        if ("Input" == treeViewIO.SelectedNode.Parent.Name)
                        {
                            if (IOManage.RemoveIO(true, treeViewIO.SelectedNode.Name))
                            {
                                treeViewIO.Nodes.Remove(treeViewIO.SelectedNode);
                                MessageBox.Show("Remove successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            }
                            else
                            {
                                MessageBox.Show("Remove unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            }
                        }
                        else
                        {
                            if (IOManage.RemoveIO(false, treeViewIO.SelectedNode.Name))
                            {
                                treeViewIO.Nodes.Remove(treeViewIO.SelectedNode);
                                MessageBox.Show("Remove successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            }
                            else
                            {
                                MessageBox.Show("Remove unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion
                #region DataGroup
                else if (null != treeViewDataGroup.SelectedNode && 1 == treeViewDataGroup.SelectedNode.Level)
                {
                    try
                    {
                        if (DataManage.RemoveDataGroup(treeViewDataGroup.SelectedNode.Name))
                        {
                            treeViewDataGroup.Nodes.Remove(treeViewDataGroup.SelectedNode);
                            MessageBox.Show("Remove successful.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                        else
                        {
                            MessageBox.Show("Remove unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        }
                    }
                    catch
                    {
                    }
                }
                #endregion
            }
            catch (Exception)
            {
            }
        }
        private void toolStripMenuItemRename_Click(object sender, EventArgs e)
        {
            try
            {
                #region Hardware
                if (null != treeViewHardware.SelectedNode)
                {
                    if (2 == treeViewHardware.SelectedNode.Level)
                    {
                        treeViewHardware.SelectedNode.BeginEdit();
                    }
                }
                #endregion
                #region Table
                if (null != treeViewTable.SelectedNode && 1 == treeViewTable.SelectedNode.Level)
                {
                    treeViewTable.SelectedNode.BeginEdit();
                }
                #endregion
            }
            catch (Exception)
            {
            }
        }
        private void toolStripMenuItemProperty_Click(object sender, EventArgs e)
        {

        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                #region Table
                if(null  != treeViewTable.SelectedNode && 0 == treeViewTable.SelectedNode.Level)
                {
                    if (WriteTableDataToFileAsCS())
                    {
                        string strFilePath = Application.StartupPath + "\\Export\\Tables\\";
                        DialogResult dRet;
                        dRet = MessageBox.Show("导出成功！\r\n是否打开文件夹？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        if (DialogResult.Yes == dRet)
                        {
                            if (Directory.Exists(strFilePath))
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(strFilePath);
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("导出失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
                #region IO
                if (null != treeViewIO.SelectedNode && 0 == treeViewIO.SelectedNode.Level)
                {
                    if (WriteIoDataToFileAsCS())
                    {
                        string strFilePath = Application.StartupPath + "\\Export\\IO\\";
                        DialogResult dRet;
                        dRet = MessageBox.Show("导出成功！\r\n是否打开文件夹？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        if (DialogResult.Yes == dRet)
                        {
                            if (Directory.Exists(strFilePath))
                            {
                                try
                                {
                                    System.Diagnostics.Process.Start(strFilePath);
                                }
                                catch (Exception)
                                {
                                }

                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("导出失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    }
                }
                #endregion
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Hardware
        private HardwareVender GetVenderByString(string strVender)
        {
            try
            {
                switch (strVender)
                {
                    case "RobotYamaha": return HardwareVender.Yamaha;
                    case "PlcPanasonic": return HardwareVender.Panasonic;
                    case "PlcOmron": return HardwareVender.Omron;
                    case "PlcSiemens": return HardwareVender.Siemens;
                    case "MCDemo": return HardwareVender.Demo;
                    case "MCGoogolTech": return HardwareVender.GoogolTech;
                    case "MCLeadShine": return HardwareVender.LeadShine;
                    case "MCAdvenTech": return HardwareVender.AdvanTech;
                    case "InputCardDemo": return HardwareVender.Demo;
                    case "InputCardGoogolTech": return HardwareVender.GoogolTech;
                    case "InputCardLeadShine": return HardwareVender.LeadShine;
                    case "InputCardAdvenTech": return HardwareVender.AdvanTech;
                    case "OutputCardDemo": return HardwareVender.Demo;
                    case "OutputCardGoogolTech": return HardwareVender.GoogolTech;
                    case "OutputCardLeadShine": return HardwareVender.LeadShine;
                    case "OutputCardAdvenTech": return HardwareVender.AdvanTech;
                    case "InputOutputCardDemo": return HardwareVender.Demo;
                    case "InputOutputCardGoogolTech": return HardwareVender.GoogolTech;
                    case "InputOutputCardLeadShine": return HardwareVender.LeadShine;
                    case "InputOutputCardAdvenTech": return HardwareVender.AdvanTech;
                    case "CameraBasler": return HardwareVender.Basler;
                    case "CameraImagingSource":return HardwareVender.ImagingSource;
                    case "CodeReaderCognex":return HardwareVender.Cognex;
                    case "CodeReaderKeyence":return HardwareVender.Keyence;
                    default: return HardwareVender.Demo;
                }
            }
            catch (Exception)
            {
                return HardwareVender.Demo;
            }
        }
        private HardwareType GetHardwareTypeByString(string strType)
        {
            switch(strType)
            {
                case "PLC": return HardwareType.PLC; 
                case "MotionCard":return HardwareType.MotionCard;
                case "InputCard":return HardwareType.InputCard;
                case "OutputCard":return HardwareType.OutputCard;
                case "InputOutputCard":return HardwareType.InputOutputCard;
                case "Robot":return HardwareType.Robot;
                case "Camera":return HardwareType.Camera;
                case "CodeReader":return HardwareType.CodeReader;
                default: return HardwareType.Unknow;
            }
        }
        private void HardwareTreeViewInit()
        {
            try
            {
                foreach (HardwareData data in HardwareManage.docHardware.listHardwareData)
                {
                    TreeNode newNode = null;
                    switch(data.Type)
                    {
                        case HardwareType.PLC:
                            #region Panasonic
                            if(data.Vender == HardwareVender.Panasonic)
                            {
                                newNode = new TreeNode
                                {
                                    Text = data.Text,
                                    Name = data.Name,
                                    //ContextMenuStrip = contextMenuStrip2
                                };
                                if (!treeViewHardware.Nodes["PLC"].Nodes["PlcPanasonic"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["PLC"].Nodes["PlcPanasonic"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region Omron
                            if(data.Vender == HardwareVender.Omron)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["PLC"].Nodes["PlcOmron"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["PLC"].Nodes["PlcOmron"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region Siemens
                            if (data.Vender == HardwareVender.Siemens)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["PLC"].Nodes["PlcSiemens"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["PLC"].Nodes["PlcSiemens"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.MotionCard:
                            #region Demo
                            if (data.Vender == HardwareVender.Demo)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["MotionCard"].Nodes["MCDemo"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["MotionCard"].Nodes["MCDemo"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region GoogolTech
                            if (data.Vender == HardwareVender.GoogolTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["MotionCard"].Nodes["MCGoogolTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["MotionCard"].Nodes["MCGoogolTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region AdvanTech
                            if (data.Vender == HardwareVender.AdvanTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["MotionCard"].Nodes["MCAdvanTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["MotionCard"].Nodes["MCAdvanTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region LeadShine
                            if (data.Vender == HardwareVender.LeadShine)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["MotionCard"].Nodes["MCLeadShine"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["MotionCard"].Nodes["MCLeadShine"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.InputCard:
                            #region Demo
                            if (data.Vender == HardwareVender.Demo)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputCard"].Nodes["InputCardDemo"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputCard"].Nodes["InputCardDemo"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region GoogolTech
                            if (data.Vender == HardwareVender.GoogolTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputCard"].Nodes["InputCardGoogolTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputCard"].Nodes["InputCardGoogolTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region AdvanTech
                            if (data.Vender == HardwareVender.AdvanTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputCard"].Nodes["InputCardAdvanTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputCard"].Nodes["InputCardAdvanTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region LeadShine
                            if (data.Vender == HardwareVender.LeadShine)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputCard"].Nodes["InputCardLeadShine"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputCard"].Nodes["InputCardLeadShine"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.OutputCard:
                            #region Demo
                            if (data.Vender == HardwareVender.Demo)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardDemo"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardDemo"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region GoogolTech
                            if (data.Vender == HardwareVender.GoogolTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardGoogolTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardGoogolTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region AdvanTech
                            if (data.Vender == HardwareVender.AdvanTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardAdvanTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardAdvanTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region LeadShine
                            if (data.Vender == HardwareVender.LeadShine)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardLeadShine"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["OutputCard"].Nodes["OutputCardLeadShine"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.InputOutputCard:
                            #region Demo
                            if (data.Vender == HardwareVender.Demo)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardDemo"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardDemo"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region GoogolTech
                            if (data.Vender == HardwareVender.GoogolTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardGoogolTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardGoogolTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region AdvanTech
                            if (data.Vender == HardwareVender.AdvanTech)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardAdvanTech"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardAdvanTech"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region LeadShine
                            if (data.Vender == HardwareVender.LeadShine)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardLeadShine"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["InputOutputCard"].Nodes["InputOutputCardLeadShine"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.Robot:
                            #region Yamaha
                            if (data.Vender == HardwareVender.Yamaha)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["Robot"].Nodes["RobotYamaha"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["Robot"].Nodes["RobotYamaha"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region Epson

                            #endregion
                            break;
                        case HardwareType.CodeReader:
                            #region Cognex

                            #endregion
                            #region Keyence
                            if (data.Vender == HardwareVender.Keyence)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["CodeReader"].Nodes["CodeReaderKeyence"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["CodeReader"].Nodes["CodeReaderKeyence"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                        case HardwareType.Camera:
                            #region Basler
                            if (data.Vender == HardwareVender.Basler)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["Camera"].Nodes["CameraBaslerSource"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["Camera"].Nodes["CameraBaslerSource"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            #region ImagingSource
                            if (data.Vender == HardwareVender.ImagingSource)
                            {
                                newNode = new TreeNode();
                                newNode.Text = data.Text;
                                newNode.Name = data.Name;

                                if (!treeViewHardware.Nodes["Camera"].Nodes["CameraImagingSource"].Nodes.ContainsKey(newNode.Name))
                                {
                                    treeViewHardware.Nodes["Camera"].Nodes["CameraImagingSource"].Nodes.Add(newNode);
                                }
                            }
                            #endregion
                            break;
                    }
                    if (newNode != null)
                    {
                        newNode.ContextMenuStrip = contextMenuStrip1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void treeViewHardware_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewHardware.SelectedNode == null)
                return;
            if (2 != treeViewHardware.SelectedNode.Level)
                return;
            try
            {
                if (!HardwareManage.dicSettingForms.ContainsKey(treeViewHardware.SelectedNode.Name))
                    return;
                formHardware.panelMain.Controls.Clear();
                HardwareManage.dicSettingForms[treeViewHardware.SelectedNode.Name].TopLevel = false;
                HardwareManage.dicSettingForms[treeViewHardware.SelectedNode.Name].Dock = DockStyle.Fill;
                formHardware.panelMain.Controls.Add(HardwareManage.dicSettingForms[treeViewHardware.SelectedNode.Name]);
                HardwareManage.dicSettingForms[treeViewHardware.SelectedNode.Name].Show();
            }
            catch (Exception)
            {
            }

        }
        private void xPanderPanelHardware_ExpandClick(object sender, EventArgs e)
        {
            treeViewTable.SelectedNode = null;
            treeViewIO.SelectedNode = null;
            treeViewDataGroup.SelectedNode = null;

            formTableSys.Hide();
            formIO.Hide();
            formDataSysView.Hide();
            formAlarmManage.Hide();

            formHardware.Show();
        }
        private void treeViewHardware_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (null == treeViewHardware.SelectedNode)
                {
                    return;
                }
                if (2 != treeViewHardware.SelectedNode.Level || e.Label == null)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (string.IsNullOrEmpty(e.Label) || e.Label.Contains(" ") || e.Label.Contains(" "))
                {
                    MessageBox.Show("Illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    e.CancelEdit = true;
                }
                else
                {
                    e.Node.EndEdit(true);
                }

                if (HardwareManage.RenameHardware(e.Node.Name, e.Label))
                {
                    e.Node.Name = e.Label;
                    MessageBox.Show("Successfuly renamed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Rename unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

        }
        #endregion

        #region Table
        private void TableTreeViewInit()
        {
            try
            {
                treeViewTable.Nodes[0].Nodes.Clear();
                foreach (TableData data in TableManage.docTable.listTableData)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = data.Name;
                    newNode.Name = data.Name;
                    newNode.ContextMenuStrip = contextMenuStrip1;
                    treeViewTable.Nodes[0].Nodes.Add(newNode);
                }
                if(treeViewTable.Nodes[0].Nodes.Count > 0)
                {
                    treeViewTable.SelectedNode = treeViewTable.Nodes[0].Nodes[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void treeViewTable_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewTable.SelectedNode == null)
                return;
            if (1 != treeViewTable.SelectedNode.Level)
                return;
            try
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(treeViewTable.SelectedNode.Name))
                    return;
                TableManage.formTableSysView.ShowTable(treeViewTable.SelectedNode.Name);
            }
            catch (Exception)
            {
            }
        }
        private void treeViewTable_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (null == treeViewTable.SelectedNode)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (1 != treeViewTable.SelectedNode.Level || e.Label == null)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (string.IsNullOrEmpty(e.Label) || e.Label.Contains(" ") || e.Label.Contains(" "))
                {
                    MessageBox.Show("Illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    e.CancelEdit = true;
                }
                else
                {
                    e.Node.EndEdit(true);
                }

                if (TableManage.TableRename(e.Node.Name, e.Label))
                {
                    e.Node.Name = e.Label;
                    MessageBox.Show("Successfuly renamed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    TableManage.formTableSysView.ShowTable(e.Label);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                e.CancelEdit = true;
                MessageBox.Show("Rename unsuccessful.");
            }
        }
        private void xPanderPanelTable_ExpandClick(object sender, EventArgs e)
        {
            treeViewHardware.SelectedNode = null;
            treeViewIO.SelectedNode = null;
            treeViewDataGroup.SelectedNode = null;

            formHardware.Hide();
            formIO.Hide();
            formDataSysView.Hide();
            formAlarmManage.Hide();

            formTableSys.Show();
            treeViewTable.ExpandAll();
            if (treeViewTable.SelectedNode == null && treeViewTable.Nodes[0].Nodes.Count > 0)
            {
                treeViewTable.SelectedNode = treeViewTable.Nodes[0].Nodes[0];
            }
        }
        public void EventTableDataReLoadHandler()
        {
            try
            {
                TableTreeViewInit();
            }
            catch (Exception)
            {
            }
        }
        private string GetTableContent(string strName)
        {
            string strContent = "";
            try
            {
                if (string.IsNullOrEmpty(strName))
                {
                    throw new Exception("名称不能为空!");
                }
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.Table.NameItems\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + strName + "\r\n";
                strContent += "   {\r\n";

                foreach (TableData item in TableManage.docTable.listTableData)
                {
                    strContent += "      public static string ";
                    strContent += item.Name;
                    strContent += " = ";
                    strContent += "\"" + item.Name + "\"";
                    strContent += " ;\r\n";
                }

                strContent += "   }\r\n";
                strContent += "}\r\n";
                return strContent;
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }
        private bool WriteTableDataToFileAsCS()
        {
            string strContent = GetTableContent("TableName");
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }

            try
            {
                string strPath = Application.StartupPath + "\\Export\\Tables\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                File.WriteAllText(strPath + "TableName.cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion

        #region IO
        private void TreeViewIOInit()
        {
            try
            {
                foreach (IOData data in IOManage.docIO.listInput)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = data.Name;
                    newNode.Name = data.Name;
                    newNode.ContextMenuStrip = contextMenuStrip1;

                    if (!treeViewIO.Nodes["Input"].Nodes.ContainsKey(newNode.Name))
                    {
                        treeViewIO.Nodes["Input"].Nodes.Add(newNode);
                    }
                }

                foreach (IOData data in IOManage.docIO.listOutput)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = data.Name;
                    newNode.Name = data.Name;
                    newNode.ContextMenuStrip = contextMenuStrip1;

                    if (!treeViewIO.Nodes["Output"].Nodes.ContainsKey(newNode.Name))
                    {
                        treeViewIO.Nodes["Output"].Nodes.Add(newNode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void treeViewIO_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewIO.SelectedNode == null)
                return;
            if (1 != treeViewIO.SelectedNode.Level)
                return;
            try
            {
                TreeNode node = treeViewIO.SelectedNode;

                if (node.Parent.Name.Equals("Input"))
                {
                    if (!IOManage.docIO.dicInput.ContainsKey(node.Name))
                        return;
                    IOManage.formIOSetting.ShowIOSetting(node.Name, true);
                }
                else if (node.Parent.Name.Equals("Output"))
                {
                    if (!IOManage.docIO.dicOutput.ContainsKey(node.Name))
                        return;
                    IOManage.formIOSetting.ShowIOSetting(node.Name, false);
                }
            }
            catch (Exception)
            {
            }
        }
        private void treeViewIO_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (null == treeViewIO.SelectedNode)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (1 != treeViewIO.SelectedNode.Level || e.Label == null)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (string.IsNullOrEmpty(e.Label) || e.Label.Contains(" ") || e.Label.Contains(" "))
                {
                    MessageBox.Show("Illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    e.CancelEdit = true;
                }
                else
                {
                    e.Node.EndEdit(true);
                }

                bool bInput = true;
                if (treeViewIO.SelectedNode.Parent.Name.Equals("Output"))
                    bInput = false;
                if (IOManage.IORename(e.Node.Name, e.Label,bInput))
                {
                    e.Node.Name = e.Label;
                    MessageBox.Show("Successfuly renamed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    IOManage.formIOSetting.ShowIOSetting(e.Label,bInput);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                e.CancelEdit = true;
                MessageBox.Show("Rename unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void xPanderPanelIO_ExpandClick(object sender, EventArgs e)
        {
            treeViewHardware.SelectedNode = null;
            treeViewTable.SelectedNode = null;
            treeViewDataGroup.SelectedNode = null;

            formTableSys.Hide();
            formHardware.Hide();
            formDataSysView.Hide();
            formAlarmManage.Hide();

            formIO.Show();
        }
        private string GetIoContent(string strName)
        {
            string strContent = "";
            try
            {
                if (string.IsNullOrEmpty(strName))
                {
                    throw new Exception("名称不能为空!");
                }
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.IO.NameItems\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + strName + "\r\n";
                strContent += "   {\r\n";

                if (strName.Equals("InputName"))
                {
                    foreach (IOData item in IOManage.docIO.listInput)
                    {
                        strContent += "      public static string ";
                        strContent += item.Name;
                        strContent += " = ";
                        strContent += "\"" + item.Name + "\"";
                        strContent += " ;\r\n";
                    }
                }
                else if (strName.Equals("OutputName"))
                {
                    foreach (IOData item in IOManage.docIO.listOutput)
                    {
                        strContent += "      public static string ";
                        strContent += item.Name;
                        strContent += " = ";
                        strContent += "\"" + item.Name + "\"";
                        strContent += " ;\r\n";
                    }
                }
                else
                {
                    return string.Empty;
                }

                strContent += "   }\r\n";
                strContent += "}\r\n";
                return strContent;
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }
        private bool WriteIoDataToFileAsCS()
        {
            string strContent = GetIoContent(treeViewIO.SelectedNode.Name + "Name");
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }

            try
            {
                string strPath = Application.StartupPath + "\\Export\\IO\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                File.WriteAllText(strPath + treeViewIO.SelectedNode.Name + "Name"+".cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion

        #region Data Group
        private void TreeViewDataGroupInit()
        {
            try
            {
                foreach (DataGroup item in DataManage.docData.listDataGroup)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.strGroupName;
                    newNode.Name = item.strGroupName;
                    newNode.ContextMenuStrip = contextMenuStrip1;

                    if (!treeViewDataGroup.Nodes["DataGroup"].Nodes.ContainsKey(newNode.Name))
                    {
                        treeViewDataGroup.Nodes["DataGroup"].Nodes.Add(newNode);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void treeViewDataGroup_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (null != treeViewDataGroup.SelectedNode && treeViewDataGroup.SelectedNode.Level == 1)
                {
                    DataManage.formDataSysView.ShowDataItems(treeViewDataGroup.SelectedNode.Name);
                }
            }
            catch
            {
            }
        }
        private void btnDataGroupExport_Click(object sender, EventArgs e)
        {
            DataManage.DataGroupExport();
        }
        private void xPanderPanelParameter_ExpandClick(object sender, EventArgs e)
        {
            treeViewHardware.SelectedNode = null;
            treeViewTable.SelectedNode = null;
            treeViewIO.SelectedNode = null;

            formTableSys.Hide();
            formIO.Hide();
            formHardware.Hide();
            formAlarmManage.Hide();

            formDataSysView.Show();
        }
        private void treeViewDataGroup_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (null == treeViewDataGroup.SelectedNode)
                {
                    return;
                }
                if (1 != treeViewDataGroup.SelectedNode.Level || e.Label == null)
                {
                    e.CancelEdit = true;
                    return;
                }
                if (string.IsNullOrEmpty(e.Label) || e.Label.Contains(" ") || e.Label.Contains(" "))
                {
                    MessageBox.Show("Illegal character.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    e.CancelEdit = true;
                }
                else
                {
                    e.Node.EndEdit(true);
                }

                if (DataManage.RenameDataGroup(e.Node.Name, e.Label))
                {
                    e.Node.Name = e.Label;
                    MessageBox.Show("Successfuly renamed.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Rename unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        #endregion

        #region Alarm Manage
        private void AlarmManageViewInit()
        {

        }
        private void xPanderPanelAlarmManage_ExpandClick(object sender, EventArgs e)
        {
            formTableSys.Hide();
            formIO.Hide();
            formHardware.Hide();
            formDataSysView.Hide();

            formAlarmManage.Show();
        }
        #endregion
    }
}

