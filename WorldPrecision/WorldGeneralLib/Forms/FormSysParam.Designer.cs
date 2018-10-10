namespace WorldGeneralLib.Forms
{
    partial class FormSysParam
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Panasonic");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Omron");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Siemens");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("PLC", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Demo");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("Googol Tech");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Lead Shine");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("Advan Tech");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("Motion Card", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Demo");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Lead Shine");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Googol Tech");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Advan Tech");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("Input Card", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Demo");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Lead Shine");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("Googol Tech");
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("Advan Tech");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("Output Card", new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode16,
            treeNode17,
            treeNode18});
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("Demo");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("Lead Shine");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("Googol Tech");
            System.Windows.Forms.TreeNode treeNode23 = new System.Windows.Forms.TreeNode("Advan Tech");
            System.Windows.Forms.TreeNode treeNode24 = new System.Windows.Forms.TreeNode("InputOutput Card", new System.Windows.Forms.TreeNode[] {
            treeNode20,
            treeNode21,
            treeNode22,
            treeNode23});
            System.Windows.Forms.TreeNode treeNode25 = new System.Windows.Forms.TreeNode("Robot Yamaha");
            System.Windows.Forms.TreeNode treeNode26 = new System.Windows.Forms.TreeNode("Robot Espon");
            System.Windows.Forms.TreeNode treeNode27 = new System.Windows.Forms.TreeNode("Robot", new System.Windows.Forms.TreeNode[] {
            treeNode25,
            treeNode26});
            System.Windows.Forms.TreeNode treeNode28 = new System.Windows.Forms.TreeNode("Cognex");
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("Keyence");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("CodeReader", new System.Windows.Forms.TreeNode[] {
            treeNode28,
            treeNode29});
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("Basler");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("ImagingSource");
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("Camera", new System.Windows.Forms.TreeNode[] {
            treeNode31,
            treeNode32});
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("Tables");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("Input");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("Output");
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("Parameter");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSysParam));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemRename = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProperty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panel3 = new BSE.Windows.Forms.Panel();
            this.xPanderPanelList2 = new BSE.Windows.Forms.XPanderPanelList();
            this.xPanderPanelHardware = new BSE.Windows.Forms.XPanderPanel();
            this.treeViewHardware = new System.Windows.Forms.TreeView();
            this.xPanderPanelTable = new BSE.Windows.Forms.XPanderPanel();
            this.treeViewTable = new System.Windows.Forms.TreeView();
            this.xPanderPanelIO = new BSE.Windows.Forms.XPanderPanel();
            this.treeViewIO = new System.Windows.Forms.TreeView();
            this.xPanderPanelParameter = new BSE.Windows.Forms.XPanderPanel();
            this.btnDataGroupExport = new System.Windows.Forms.Button();
            this.treeViewDataGroup = new System.Windows.Forms.TreeView();
            this.xPanderPanelAlarmManage = new BSE.Windows.Forms.XPanderPanel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xPanderPanelList2.SuspendLayout();
            this.xPanderPanelHardware.SuspendLayout();
            this.xPanderPanelTable.SuspendLayout();
            this.xPanderPanelIO.SuspendLayout();
            this.xPanderPanelParameter.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAdd,
            this.toolStripMenuItemRemove,
            this.toolStripMenuItemRename,
            this.toolStripMenuItemProperty,
            this.导出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 114);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripMenuItemAdd
            // 
            this.toolStripMenuItemAdd.Name = "toolStripMenuItemAdd";
            this.toolStripMenuItemAdd.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemAdd.Text = "添加";
            this.toolStripMenuItemAdd.Click += new System.EventHandler(this.toolStripMenuItemAdd_Click);
            // 
            // toolStripMenuItemRemove
            // 
            this.toolStripMenuItemRemove.Name = "toolStripMenuItemRemove";
            this.toolStripMenuItemRemove.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemRemove.Text = "移除";
            this.toolStripMenuItemRemove.Click += new System.EventHandler(this.toolStripMenuItemRemove_Click);
            // 
            // toolStripMenuItemRename
            // 
            this.toolStripMenuItemRename.Name = "toolStripMenuItemRename";
            this.toolStripMenuItemRename.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemRename.Text = "重命名";
            this.toolStripMenuItemRename.Click += new System.EventHandler(this.toolStripMenuItemRename_Click);
            // 
            // toolStripMenuItemProperty
            // 
            this.toolStripMenuItemProperty.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.toolStripMenuItemProperty.Name = "toolStripMenuItemProperty";
            this.toolStripMenuItemProperty.Size = new System.Drawing.Size(112, 22);
            this.toolStripMenuItemProperty.Text = "属性";
            this.toolStripMenuItemProperty.Click += new System.EventHandler(this.toolStripMenuItemProperty_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItem2.Text = "1";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(83, 22);
            this.toolStripMenuItem3.Text = "2";
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panelMain);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 853);
            this.panel1.TabIndex = 2;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelMain.Location = new System.Drawing.Point(289, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(754, 853);
            this.panelMain.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(286, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 853);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderColor = System.Drawing.Color.Gray;
            this.panel3.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.CaptionForeColor = System.Drawing.Color.DarkSlateGray;
            this.panel3.CloseIconForeColor = System.Drawing.Color.Empty;
            this.panel3.CollapsedCaptionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.panel3.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.panel3.ColorCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.panel3.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(216)))), ((int)(((byte)(255)))));
            this.panel3.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panel3.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panel3.ColorScheme = BSE.Windows.Forms.ColorScheme.Custom;
            this.panel3.Controls.Add(this.xPanderPanelList2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Image = null;
            this.panel3.InnerBorderColor = System.Drawing.Color.Empty;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panel3.ShowExpandIcon = true;
            this.panel3.ShowTransparentBackground = false;
            this.panel3.Size = new System.Drawing.Size(286, 853);
            this.panel3.TabIndex = 3;
            this.panel3.Text = "System Configuratoion";
            // 
            // xPanderPanelList2
            // 
            this.xPanderPanelList2.CaptionHeight = 20;
            this.xPanderPanelList2.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Flat;
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelHardware);
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelTable);
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelIO);
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelParameter);
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelAlarmManage);
            this.xPanderPanelList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList2.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList2.Location = new System.Drawing.Point(1, 26);
            this.xPanderPanelList2.Name = "xPanderPanelList2";
            this.xPanderPanelList2.ShowCloseIcon = true;
            this.xPanderPanelList2.ShowExpandIcon = true;
            this.xPanderPanelList2.Size = new System.Drawing.Size(284, 826);
            this.xPanderPanelList2.TabIndex = 0;
            this.xPanderPanelList2.Text = "xPanderPanelList2";
            // 
            // xPanderPanelHardware
            // 
            this.xPanderPanelHardware.BackColor = System.Drawing.Color.Transparent;
            this.xPanderPanelHardware.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelHardware.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelHardware.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelHardware.CaptionHeight = 20;
            this.xPanderPanelHardware.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelHardware.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelHardware.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelHardware.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelHardware.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelHardware.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelHardware.Controls.Add(this.treeViewHardware);
            this.xPanderPanelHardware.Expand = true;
            this.xPanderPanelHardware.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelHardware.Image = null;
            this.xPanderPanelHardware.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelHardware.IsClosable = false;
            this.xPanderPanelHardware.Name = "xPanderPanelHardware";
            this.xPanderPanelHardware.Size = new System.Drawing.Size(284, 746);
            this.xPanderPanelHardware.TabIndex = 0;
            this.xPanderPanelHardware.Text = "Hardware Configuration";
            this.xPanderPanelHardware.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelHardware_ExpandClick);
            // 
            // treeViewHardware
            // 
            this.treeViewHardware.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewHardware.LabelEdit = true;
            this.treeViewHardware.LineColor = System.Drawing.Color.DarkSlateGray;
            this.treeViewHardware.Location = new System.Drawing.Point(0, 20);
            this.treeViewHardware.Name = "treeViewHardware";
            treeNode1.ContextMenuStrip = this.contextMenuStrip1;
            treeNode1.Name = "PlcPanasonic";
            treeNode1.Text = "Panasonic";
            treeNode2.ContextMenuStrip = this.contextMenuStrip1;
            treeNode2.Name = "PlcOmron";
            treeNode2.Text = "Omron";
            treeNode3.ContextMenuStrip = this.contextMenuStrip1;
            treeNode3.Name = "PlcSiemens";
            treeNode3.Text = "Siemens";
            treeNode4.Name = "PLC";
            treeNode4.Text = "PLC";
            treeNode5.ContextMenuStrip = this.contextMenuStrip1;
            treeNode5.Name = "MCDemo";
            treeNode5.Text = "Demo";
            treeNode6.ContextMenuStrip = this.contextMenuStrip1;
            treeNode6.Name = "MCGoogolTech";
            treeNode6.Text = "Googol Tech";
            treeNode7.ContextMenuStrip = this.contextMenuStrip1;
            treeNode7.Name = "MCLeadShine";
            treeNode7.Text = "Lead Shine";
            treeNode8.ContextMenuStrip = this.contextMenuStrip1;
            treeNode8.Name = "MCAdvanTech";
            treeNode8.Text = "Advan Tech";
            treeNode9.Name = "MotionCard";
            treeNode9.Text = "Motion Card";
            treeNode10.ContextMenuStrip = this.contextMenuStrip1;
            treeNode10.Name = "InputCardDemo";
            treeNode10.Text = "Demo";
            treeNode11.ContextMenuStrip = this.contextMenuStrip1;
            treeNode11.Name = "InputCardLeadShine";
            treeNode11.Text = "Lead Shine";
            treeNode12.ContextMenuStrip = this.contextMenuStrip1;
            treeNode12.Name = "InputCardGoogolTech";
            treeNode12.Text = "Googol Tech";
            treeNode13.ContextMenuStrip = this.contextMenuStrip1;
            treeNode13.Name = "InputCardAdvanTech";
            treeNode13.Text = "Advan Tech";
            treeNode14.Name = "InputCard";
            treeNode14.Text = "Input Card";
            treeNode15.ContextMenuStrip = this.contextMenuStrip1;
            treeNode15.Name = "OutputCardDemo";
            treeNode15.Text = "Demo";
            treeNode16.ContextMenuStrip = this.contextMenuStrip1;
            treeNode16.Name = "OutputCardLeadShine";
            treeNode16.Text = "Lead Shine";
            treeNode17.ContextMenuStrip = this.contextMenuStrip1;
            treeNode17.Name = "OutputCardGoogolTech";
            treeNode17.Text = "Googol Tech";
            treeNode18.ContextMenuStrip = this.contextMenuStrip1;
            treeNode18.Name = "OutputCardAdvanTech";
            treeNode18.Text = "Advan Tech";
            treeNode19.Name = "OutputCard";
            treeNode19.Text = "Output Card";
            treeNode20.ContextMenuStrip = this.contextMenuStrip1;
            treeNode20.Name = "InputOutputCardDemo";
            treeNode20.Text = "Demo";
            treeNode21.ContextMenuStrip = this.contextMenuStrip1;
            treeNode21.Name = "InputOutputCardLeadShine";
            treeNode21.Text = "Lead Shine";
            treeNode22.ContextMenuStrip = this.contextMenuStrip1;
            treeNode22.Name = "InputOutputCardGoogolTech";
            treeNode22.Text = "Googol Tech";
            treeNode23.ContextMenuStrip = this.contextMenuStrip1;
            treeNode23.Name = "InputOutputCardAdvanTech";
            treeNode23.Text = "Advan Tech";
            treeNode24.Name = "InputOutputCard";
            treeNode24.Text = "InputOutput Card";
            treeNode25.ContextMenuStrip = this.contextMenuStrip1;
            treeNode25.Name = "RobotYamaha";
            treeNode25.Text = "Robot Yamaha";
            treeNode26.ContextMenuStrip = this.contextMenuStrip1;
            treeNode26.Name = "RobotEspon";
            treeNode26.Text = "Robot Espon";
            treeNode27.Name = "Robot";
            treeNode27.Text = "Robot";
            treeNode28.ContextMenuStrip = this.contextMenuStrip1;
            treeNode28.Name = "CodeReaderCognex";
            treeNode28.Text = "Cognex";
            treeNode29.ContextMenuStrip = this.contextMenuStrip1;
            treeNode29.Name = "CodeReaderKeyence";
            treeNode29.Text = "Keyence";
            treeNode30.Name = "CodeReader";
            treeNode30.Text = "CodeReader";
            treeNode31.ContextMenuStrip = this.contextMenuStrip1;
            treeNode31.Name = "CameraBasler";
            treeNode31.Text = "Basler";
            treeNode32.ContextMenuStrip = this.contextMenuStrip1;
            treeNode32.Name = "CameraImagingSource";
            treeNode32.Text = "ImagingSource";
            treeNode33.Name = "Camera";
            treeNode33.Text = "Camera";
            this.treeViewHardware.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode9,
            treeNode14,
            treeNode19,
            treeNode24,
            treeNode27,
            treeNode30,
            treeNode33});
            this.treeViewHardware.Size = new System.Drawing.Size(284, 726);
            this.treeViewHardware.TabIndex = 0;
            this.treeViewHardware.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewHardware_AfterLabelEdit);
            this.treeViewHardware.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewHardware_AfterSelect);
            // 
            // xPanderPanelTable
            // 
            this.xPanderPanelTable.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelTable.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelTable.CaptionHeight = 20;
            this.xPanderPanelTable.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelTable.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelTable.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelTable.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelTable.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelTable.Controls.Add(this.treeViewTable);
            this.xPanderPanelTable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelTable.Image = null;
            this.xPanderPanelTable.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.IsClosable = false;
            this.xPanderPanelTable.Name = "xPanderPanelTable";
            this.xPanderPanelTable.Size = new System.Drawing.Size(284, 20);
            this.xPanderPanelTable.TabIndex = 1;
            this.xPanderPanelTable.Text = "Table Configuration";
            this.xPanderPanelTable.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelTable_ExpandClick);
            // 
            // treeViewTable
            // 
            this.treeViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTable.LabelEdit = true;
            this.treeViewTable.Location = new System.Drawing.Point(0, 20);
            this.treeViewTable.Name = "treeViewTable";
            treeNode34.ContextMenuStrip = this.contextMenuStrip1;
            treeNode34.Name = "rootNode";
            treeNode34.Text = "Tables";
            this.treeViewTable.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode34});
            this.treeViewTable.Size = new System.Drawing.Size(284, 0);
            this.treeViewTable.TabIndex = 0;
            this.treeViewTable.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewTable_AfterLabelEdit);
            this.treeViewTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTable_AfterSelect);
            // 
            // xPanderPanelIO
            // 
            this.xPanderPanelIO.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelIO.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelIO.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelIO.CaptionHeight = 20;
            this.xPanderPanelIO.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelIO.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelIO.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelIO.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelIO.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelIO.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelIO.Controls.Add(this.treeViewIO);
            this.xPanderPanelIO.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelIO.Image = null;
            this.xPanderPanelIO.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelIO.IsClosable = false;
            this.xPanderPanelIO.Name = "xPanderPanelIO";
            this.xPanderPanelIO.Size = new System.Drawing.Size(284, 20);
            this.xPanderPanelIO.TabIndex = 2;
            this.xPanderPanelIO.Text = "IO Configuration";
            this.xPanderPanelIO.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelIO_ExpandClick);
            // 
            // treeViewIO
            // 
            this.treeViewIO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewIO.LabelEdit = true;
            this.treeViewIO.Location = new System.Drawing.Point(0, 20);
            this.treeViewIO.Name = "treeViewIO";
            treeNode35.ContextMenuStrip = this.contextMenuStrip1;
            treeNode35.Name = "Input";
            treeNode35.Text = "Input";
            treeNode36.ContextMenuStrip = this.contextMenuStrip1;
            treeNode36.Name = "Output";
            treeNode36.Text = "Output";
            this.treeViewIO.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode35,
            treeNode36});
            this.treeViewIO.Size = new System.Drawing.Size(284, 0);
            this.treeViewIO.TabIndex = 0;
            this.treeViewIO.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewIO_AfterLabelEdit);
            this.treeViewIO.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewIO_AfterSelect);
            // 
            // xPanderPanelParameter
            // 
            this.xPanderPanelParameter.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelParameter.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelParameter.CaptionHeight = 20;
            this.xPanderPanelParameter.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelParameter.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelParameter.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelParameter.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelParameter.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelParameter.Controls.Add(this.btnDataGroupExport);
            this.xPanderPanelParameter.Controls.Add(this.treeViewDataGroup);
            this.xPanderPanelParameter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelParameter.Image = null;
            this.xPanderPanelParameter.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.IsClosable = false;
            this.xPanderPanelParameter.Name = "xPanderPanelParameter";
            this.xPanderPanelParameter.Size = new System.Drawing.Size(284, 20);
            this.xPanderPanelParameter.TabIndex = 3;
            this.xPanderPanelParameter.Text = "Parameter Configuration";
            this.xPanderPanelParameter.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelParameter_ExpandClick);
            // 
            // btnDataGroupExport
            // 
            this.btnDataGroupExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDataGroupExport.Location = new System.Drawing.Point(19, -792);
            this.btnDataGroupExport.Name = "btnDataGroupExport";
            this.btnDataGroupExport.Size = new System.Drawing.Size(247, 23);
            this.btnDataGroupExport.TabIndex = 1;
            this.btnDataGroupExport.Text = "Export( *.cs )";
            this.btnDataGroupExport.UseVisualStyleBackColor = true;
            this.btnDataGroupExport.Click += new System.EventHandler(this.btnDataGroupExport_Click);
            // 
            // treeViewDataGroup
            // 
            this.treeViewDataGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataGroup.LabelEdit = true;
            this.treeViewDataGroup.Location = new System.Drawing.Point(0, 20);
            this.treeViewDataGroup.Name = "treeViewDataGroup";
            treeNode37.ContextMenuStrip = this.contextMenuStrip1;
            treeNode37.Name = "DataGroup";
            treeNode37.Text = "Parameter";
            this.treeViewDataGroup.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode37});
            this.treeViewDataGroup.Size = new System.Drawing.Size(284, 0);
            this.treeViewDataGroup.TabIndex = 0;
            this.treeViewDataGroup.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewDataGroup_AfterLabelEdit);
            this.treeViewDataGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDataGroup_AfterSelect);
            // 
            // xPanderPanelAlarmManage
            // 
            this.xPanderPanelAlarmManage.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelAlarmManage.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelAlarmManage.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelAlarmManage.CaptionHeight = 20;
            this.xPanderPanelAlarmManage.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelAlarmManage.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelAlarmManage.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelAlarmManage.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelAlarmManage.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelAlarmManage.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelAlarmManage.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelAlarmManage.Image = null;
            this.xPanderPanelAlarmManage.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelAlarmManage.IsClosable = false;
            this.xPanderPanelAlarmManage.Name = "xPanderPanelAlarmManage";
            this.xPanderPanelAlarmManage.Size = new System.Drawing.Size(284, 20);
            this.xPanderPanelAlarmManage.TabIndex = 4;
            this.xPanderPanelAlarmManage.Text = "Machine Alarm Manage";
            this.xPanderPanelAlarmManage.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelAlarmManage_ExpandClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            this.imageList1.Images.SetKeyName(4, "");
            this.imageList1.Images.SetKeyName(5, "");
            // 
            // FormSysParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1043, 853);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormSysParam";
            this.Text = "FormSysParam";
            this.Load += new System.EventHandler(this.FormSysParam_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.xPanderPanelList2.ResumeLayout(false);
            this.xPanderPanelHardware.ResumeLayout(false);
            this.xPanderPanelTable.ResumeLayout(false);
            this.xPanderPanelIO.ResumeLayout(false);
            this.xPanderPanelParameter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private BSE.Windows.Forms.Panel panel3;
        private BSE.Windows.Forms.XPanderPanelList xPanderPanelList2;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelHardware;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelTable;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelIO;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelParameter;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TreeView treeViewHardware;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAdd;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProperty;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TreeView treeViewDataGroup;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRemove;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemRename;
        private System.Windows.Forms.TreeView treeViewIO;
        private System.Windows.Forms.Button btnDataGroupExport;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TreeView treeViewTable;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelAlarmManage;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}