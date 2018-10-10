namespace WorldGeneralLib.Alarm
{
    partial class FormAlarmManage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarmManage));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.toolBarBtnRemove = new System.Windows.Forms.ToolStripButton();
            this.toorBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolBarBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripBtnImport = new System.Windows.Forms.ToolStripButton();
            this.panelConfig = new BSE.Windows.Forms.Panel();
            this.btnExport = new RibbonStyle.RibbonMenuButton();
            this.MenuStripExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemExprotCsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemExportItems = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemAlarmKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemAlarmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAlarmSrc = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.ItemAlarmMsg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel5 = new BSE.Windows.Forms.Panel();
            this.plcScanItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            this.panelConfig.SuspendLayout();
            this.MenuStripExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plcScanItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 605);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(951, 3);
            this.splitter1.TabIndex = 26;
            this.splitter1.TabStop = false;
            // 
            // toolBarBtnRemove
            // 
            this.toolBarBtnRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolBarBtnRemove.Image")));
            this.toolBarBtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarBtnRemove.Name = "toolBarBtnRemove";
            this.toolBarBtnRemove.Size = new System.Drawing.Size(87, 26);
            this.toolBarBtnRemove.Text = "Remove  ";
            this.toolBarBtnRemove.ToolTipText = "Remove item";
            this.toolBarBtnRemove.Click += new System.EventHandler(this.toolBarBtnRemove_Click);
            // 
            // toorBtnAdd
            // 
            this.toorBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("toorBtnAdd.Image")));
            this.toorBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toorBtnAdd.Name = "toorBtnAdd";
            this.toorBtnAdd.Size = new System.Drawing.Size(64, 26);
            this.toorBtnAdd.Text = "Add  ";
            this.toorBtnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toorBtnAdd.ToolTipText = "Add new item";
            this.toorBtnAdd.Click += new System.EventHandler(this.toorBtnAdd_Click);
            // 
            // toolBarBtnSave
            // 
            this.toolBarBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolBarBtnSave.Image")));
            this.toolBarBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarBtnSave.Name = "toolBarBtnSave";
            this.toolBarBtnSave.Size = new System.Drawing.Size(67, 26);
            this.toolBarBtnSave.Text = "Save  ";
            this.toolBarBtnSave.Click += new System.EventHandler(this.toolBarBtnSave_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarBtnSave,
            this.toorBtnAdd,
            this.toolBarBtnRemove,
            this.toolStripBtnImport});
            this.toolStrip1.Location = new System.Drawing.Point(1, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(949, 29);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip3";
            // 
            // toolStripBtnImport
            // 
            this.toolStripBtnImport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnImport.Image")));
            this.toolStripBtnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnImport.Name = "toolStripBtnImport";
            this.toolStripBtnImport.Size = new System.Drawing.Size(72, 26);
            this.toolStripBtnImport.Text = "Import";
            this.toolStripBtnImport.ToolTipText = "Export as cs file";
            this.toolStripBtnImport.Click += new System.EventHandler(this.toolStripBtnImport_Click);
            // 
            // panelConfig
            // 
            this.panelConfig.BackColor = System.Drawing.Color.Transparent;
            this.panelConfig.BorderColor = System.Drawing.Color.Empty;
            this.panelConfig.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelConfig.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelConfig.CloseIconForeColor = System.Drawing.Color.Empty;
            this.panelConfig.CollapsedCaptionForeColor = System.Drawing.Color.Empty;
            this.panelConfig.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelConfig.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelConfig.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelConfig.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelConfig.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelConfig.Controls.Add(this.btnExport);
            this.panelConfig.Controls.Add(this.dataGridView1);
            this.panelConfig.Controls.Add(this.toolStrip1);
            this.panelConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConfig.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelConfig.Image = null;
            this.panelConfig.InnerBorderColor = System.Drawing.Color.Empty;
            this.panelConfig.Location = new System.Drawing.Point(0, 0);
            this.panelConfig.Margin = new System.Windows.Forms.Padding(0);
            this.panelConfig.Name = "panelConfig";
            this.panelConfig.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panelConfig.ShowTransparentBackground = false;
            this.panelConfig.ShowXPanderPanelProfessionalStyle = true;
            this.panelConfig.Size = new System.Drawing.Size(951, 608);
            this.panelConfig.TabIndex = 7;
            this.panelConfig.Text = "Alarm Items Edit";
            this.panelConfig.SizeChanged += new System.EventHandler(this.panelConfig_SizeChanged);
            // 
            // btnExport
            // 
            this.btnExport.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToRight;
            this.btnExport.BackColor = System.Drawing.Color.AliceBlue;
            this.btnExport.ColorBase = System.Drawing.Color.Transparent;
            this.btnExport.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btnExport.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnExport.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.btnExport.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnExport.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnExport.ContextMenuStrip = this.MenuStripExport;
            this.btnExport.FadingSpeed = 20;
            this.btnExport.FlatAppearance.BorderSize = 0;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnExport.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.btnExport.ImageOffset = 2;
            this.btnExport.ImageOffsetX = 0;
            this.btnExport.IsPressed = false;
            this.btnExport.KeepPress = false;
            this.btnExport.Location = new System.Drawing.Point(315, 28);
            this.btnExport.MaxImageSize = new System.Drawing.Point(0, 0);
            this.btnExport.MenuPos = new System.Drawing.Point(0, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Radius = 4;
            this.btnExport.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.btnExport.Size = new System.Drawing.Size(103, 25);
            this.btnExport.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
            this.btnExport.SplitDistance = 20;
            this.btnExport.TabIndex = 26;
            this.btnExport.Text = "Export";
            this.btnExport.TextOfsetX = 2;
            this.btnExport.TextOfsetY = 0;
            this.btnExport.Title = "";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // MenuStripExport
            // 
            this.MenuStripExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MenuStripExport.DropShadowEnabled = false;
            this.MenuStripExport.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStripExport.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStripExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExprotCsFile,
            this.ToolStripMenuItemExportItems});
            this.MenuStripExport.Name = "Paste";
            this.MenuStripExport.Size = new System.Drawing.Size(151, 48);
            // 
            // toolStripMenuItemExprotCsFile
            // 
            this.toolStripMenuItemExprotCsFile.Name = "toolStripMenuItemExprotCsFile";
            this.toolStripMenuItemExprotCsFile.Size = new System.Drawing.Size(150, 22);
            this.toolStripMenuItemExprotCsFile.Text = "Export CS File";
            this.toolStripMenuItemExprotCsFile.Click += new System.EventHandler(this.toolStripMenuItemExprotCsFile_Click);
            // 
            // ToolStripMenuItemExportItems
            // 
            this.ToolStripMenuItemExportItems.Name = "ToolStripMenuItemExportItems";
            this.ToolStripMenuItemExportItems.Size = new System.Drawing.Size(150, 22);
            this.ToolStripMenuItemExportItems.Text = "Export xml File";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ItemAlarmKey,
            this.ItemAlarmName,
            this.ColumnAlarmSrc,
            this.ItemAlarmMsg,
            this.ItemRemark});
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(8, 61);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(934, 536);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.SizeChanged += new System.EventHandler(this.dataGridView1_SizeChanged);
            // 
            // ItemAlarmKey
            // 
            this.ItemAlarmKey.HeaderText = "Alarm Key";
            this.ItemAlarmKey.Name = "ItemAlarmKey";
            // 
            // ItemAlarmName
            // 
            this.ItemAlarmName.HeaderText = "Alarm Name";
            this.ItemAlarmName.Name = "ItemAlarmName";
            this.ItemAlarmName.Width = 110;
            // 
            // ColumnAlarmSrc
            // 
            this.ColumnAlarmSrc.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ColumnAlarmSrc.HeaderText = "Alarm Source";
            this.ColumnAlarmSrc.Items.AddRange(new object[] {
            "BIT",
            "INT16",
            "UINT16",
            "INT32",
            "UINT32",
            "REAL"});
            this.ColumnAlarmSrc.Name = "ColumnAlarmSrc";
            this.ColumnAlarmSrc.Width = 110;
            // 
            // ItemAlarmMsg
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.ItemAlarmMsg.DefaultCellStyle = dataGridViewCellStyle3;
            this.ItemAlarmMsg.HeaderText = "Alarm Message";
            this.ItemAlarmMsg.Name = "ItemAlarmMsg";
            this.ItemAlarmMsg.Width = 110;
            // 
            // ItemRemark
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            this.ItemRemark.DefaultCellStyle = dataGridViewCellStyle4;
            this.ItemRemark.HeaderText = "Remark";
            this.ItemRemark.Name = "ItemRemark";
            this.ItemRemark.Width = 110;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel5.CaptionFont = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold);
            this.panel5.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel5.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panel5.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panel5.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panel5.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel5.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panel5.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panel5.Controls.Add(this.splitter1);
            this.panel5.Controls.Add(this.panelConfig);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel5.Image = null;
            this.panel5.InnerBorderColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.PanelStyle = BSE.Windows.Forms.PanelStyle.None;
            this.panel5.Size = new System.Drawing.Size(951, 608);
            this.panel5.TabIndex = 19;
            this.panel5.Text = "panel5";
            // 
            // plcScanItemsBindingSource
            // 
            this.plcScanItemsBindingSource.DataSource = typeof(WorldGeneralLib.Hardware.PlcScanItems);
            // 
            // FormAlarmManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 608);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAlarmManage";
            this.Text = "FormAlarmManage";
            this.Load += new System.EventHandler(this.FormAlarmManage_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panelConfig.ResumeLayout(false);
            this.MenuStripExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plcScanItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource plcScanItemsBindingSource;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolStripButton toolBarBtnRemove;
        private System.Windows.Forms.ToolStripButton toorBtnAdd;
        private System.Windows.Forms.ToolStripButton toolBarBtnSave;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private BSE.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.DataGridView dataGridView1;
        private BSE.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemAlarmKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemAlarmName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnAlarmSrc;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemAlarmMsg;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemRemark;
        private RibbonStyle.RibbonMenuButton btnExport;
        private System.Windows.Forms.ContextMenuStrip MenuStripExport;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExprotCsFile;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemExportItems;
        private System.Windows.Forms.ToolStripButton toolStripBtnImport;
    }
}