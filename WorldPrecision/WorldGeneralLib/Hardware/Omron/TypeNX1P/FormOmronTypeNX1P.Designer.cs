namespace WorldGeneralLib.Hardware.Omron.TypeNX1P
{
    partial class FormOmronTypeNX1P
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOmronTypeNX1P));
            this.chkMonitor = new System.Windows.Forms.CheckBox();
            this.labelConnSta = new System.Windows.Forms.Label();
            this.btnConn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.ipAddressControl1 = new IPAddressControlLib.IPAddressControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnAddressType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDataType = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IoIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ItemRefresh = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelConfig = new BSE.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBarBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toorBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolBarBtnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnExport = new System.Windows.Forms.ToolStripButton();
            this.panel4 = new BSE.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.ipAddressControlPC = new IPAddressControlLib.IPAddressControl();
            this.label17 = new System.Windows.Forms.Label();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panel5 = new BSE.Windows.Forms.Panel();
            this.plcScanItemsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelConfig.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.plcScanItemsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // chkMonitor
            // 
            this.chkMonitor.AutoSize = true;
            this.chkMonitor.BackColor = System.Drawing.Color.AliceBlue;
            this.chkMonitor.Location = new System.Drawing.Point(305, 33);
            this.chkMonitor.Name = "chkMonitor";
            this.chkMonitor.Size = new System.Drawing.Size(62, 17);
            this.chkMonitor.TabIndex = 13;
            this.chkMonitor.Text = "Monitor";
            this.chkMonitor.UseVisualStyleBackColor = false;
            // 
            // labelConnSta
            // 
            this.labelConnSta.AutoSize = true;
            this.labelConnSta.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelConnSta.ForeColor = System.Drawing.Color.DarkRed;
            this.labelConnSta.Location = new System.Drawing.Point(3, 124);
            this.labelConnSta.Name = "labelConnSta";
            this.labelConnSta.Size = new System.Drawing.Size(182, 13);
            this.labelConnSta.TabIndex = 12;
            this.labelConnSta.Text = "Failed to connect to PLC.";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(517, 63);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(101, 51);
            this.btnConn.TabIndex = 11;
            this.btnConn.Text = "Connect";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(61, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(175, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Remote(PLC) IP address :";
            // 
            // ipAddressControl1
            // 
            this.ipAddressControl1.AllowInternalTab = false;
            this.ipAddressControl1.AutoHeight = true;
            this.ipAddressControl1.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControl1.Location = new System.Drawing.Point(259, 66);
            this.ipAddressControl1.MinimumSize = new System.Drawing.Size(90, 21);
            this.ipAddressControl1.Name = "ipAddressControl1";
            this.ipAddressControl1.ReadOnly = false;
            this.ipAddressControl1.Size = new System.Drawing.Size(197, 21);
            this.ipAddressControl1.TabIndex = 1;
            this.ipAddressControl1.Text = "...";
            this.ipAddressControl1.TextChanged += new System.EventHandler(this.ipAddressControl1_TextChanged);
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
            this.ItemName,
            this.ColumnAddressType,
            this.Address,
            this.ColumnDataType,
            this.Value,
            this.IoIndex,
            this.ItemRefresh});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView1.GridColor = System.Drawing.Color.Gray;
            this.dataGridView1.Location = new System.Drawing.Point(8, 63);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1141, 408);
            this.dataGridView1.TabIndex = 15;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.SizeChanged += new System.EventHandler(this.dataGridView1_SizeChanged);
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // ItemName
            // 
            this.ItemName.HeaderText = "Name";
            this.ItemName.Name = "ItemName";
            this.ItemName.Width = 80;
            // 
            // ColumnAddressType
            // 
            this.ColumnAddressType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ColumnAddressType.HeaderText = "Address Type";
            this.ColumnAddressType.Items.AddRange(new object[] {
            "CIO",
            "WR",
            "DM"});
            this.ColumnAddressType.Name = "ColumnAddressType";
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            this.Address.Width = 80;
            // 
            // ColumnDataType
            // 
            this.ColumnDataType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ColumnDataType.HeaderText = "Data Type";
            this.ColumnDataType.Items.AddRange(new object[] {
            "BIT",
            "INT16",
            "UINT16",
            "INT32",
            "UINT32",
            "REAL"});
            this.ColumnDataType.Name = "ColumnDataType";
            // 
            // Value
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Value.DefaultCellStyle = dataGridViewCellStyle3;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.ReadOnly = true;
            this.Value.Width = 80;
            // 
            // IoIndex
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.RoyalBlue;
            this.IoIndex.DefaultCellStyle = dataGridViewCellStyle4;
            this.IoIndex.HeaderText = "IO Index";
            this.IoIndex.Name = "IoIndex";
            this.IoIndex.Width = 80;
            // 
            // ItemRefresh
            // 
            this.ItemRefresh.HeaderText = "Refresh";
            this.ItemRefresh.Name = "ItemRefresh";
            this.ItemRefresh.Width = 80;
            // 
            // panelConfig
            // 
            this.panelConfig.BackColor = System.Drawing.Color.Transparent;
            this.panelConfig.BorderColor = System.Drawing.Color.Empty;
            this.panelConfig.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelConfig.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelConfig.CloseIconForeColor = System.Drawing.Color.Empty;
            this.panelConfig.CollapsedCaptionForeColor = System.Drawing.Color.Empty;
            this.panelConfig.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelConfig.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelConfig.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelConfig.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelConfig.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelConfig.Controls.Add(this.chkMonitor);
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
            this.panelConfig.Size = new System.Drawing.Size(1156, 493);
            this.panelConfig.TabIndex = 7;
            this.panelConfig.Text = "Configuratoion";
            this.panelConfig.SizeChanged += new System.EventHandler(this.panelConfig_SizeChanged);
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
            this.toolStripBtnExport});
            this.toolStrip1.Location = new System.Drawing.Point(1, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1154, 29);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip3";
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
            // toolStripBtnExport
            // 
            this.toolStripBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnExport.Image")));
            this.toolStripBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnExport.Name = "toolStripBtnExport";
            this.toolStripBtnExport.Size = new System.Drawing.Size(78, 26);
            this.toolStripBtnExport.Text = "Export  ";
            this.toolStripBtnExport.ToolTipText = "Export as cs file";
            this.toolStripBtnExport.Click += new System.EventHandler(this.toolStripBtnExport_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Transparent;
            this.panel4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel4.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel4.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panel4.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panel4.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panel4.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel4.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panel4.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.ipAddressControlPC);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.btnConn);
            this.panel4.Controls.Add(this.labelConnSta);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.ipAddressControl1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel4.Image = null;
            this.panel4.InnerBorderColor = System.Drawing.Color.White;
            this.panel4.Location = new System.Drawing.Point(0, 493);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panel4.ShowExpandIcon = true;
            this.panel4.ShowXPanderPanelProfessionalStyle = true;
            this.panel4.Size = new System.Drawing.Size(1156, 148);
            this.panel4.TabIndex = 26;
            this.panel4.Text = "Configuration";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Local(PC) IP address :";
            // 
            // ipAddressControlPC
            // 
            this.ipAddressControlPC.AllowInternalTab = false;
            this.ipAddressControlPC.AutoHeight = true;
            this.ipAddressControlPC.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressControlPC.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressControlPC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressControlPC.Location = new System.Drawing.Point(259, 93);
            this.ipAddressControlPC.MinimumSize = new System.Drawing.Size(90, 21);
            this.ipAddressControlPC.Name = "ipAddressControlPC";
            this.ipAddressControlPC.ReadOnly = false;
            this.ipAddressControlPC.Size = new System.Drawing.Size(197, 21);
            this.ipAddressControlPC.TabIndex = 19;
            this.ipAddressControlPC.Text = "...";
            this.ipAddressControlPC.TextChanged += new System.EventHandler(this.ipAddressControlPC_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label17.Location = new System.Drawing.Point(35, 39);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(133, 15);
            this.label17.TabIndex = 17;
            this.label17.Text = "Communication ";
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 490);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1156, 3);
            this.splitter1.TabIndex = 26;
            this.splitter1.TabStop = false;
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
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel5.Image = null;
            this.panel5.InnerBorderColor = System.Drawing.Color.White;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.PanelStyle = BSE.Windows.Forms.PanelStyle.None;
            this.panel5.Size = new System.Drawing.Size(1156, 641);
            this.panel5.TabIndex = 18;
            this.panel5.Text = "panel5";
            // 
            // plcScanItemsBindingSource
            // 
            this.plcScanItemsBindingSource.DataSource = typeof(WorldGeneralLib.Hardware.PlcScanItems);
            // 
            // FormOmronTypeNX1P
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1156, 641);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(601, 596);
            this.Name = "FormOmronTypeNX1P";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FORmOmronTypeNJ";
            this.Load += new System.EventHandler(this.FormOmronTypeNJ_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelConfig.ResumeLayout(false);
            this.panelConfig.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.plcScanItemsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label labelConnSta;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.CheckBox chkMonitor;
        private IPAddressControlLib.IPAddressControl ipAddressControl1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private BSE.Windows.Forms.Panel panelConfig;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBarBtnSave;
        private System.Windows.Forms.ToolStripButton toorBtnAdd;
        private System.Windows.Forms.ToolStripButton toolBarBtnRemove;
        private System.Windows.Forms.ToolStripButton toolStripBtnExport;
        private BSE.Windows.Forms.Panel panel4;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label17;
        private BSE.Windows.Forms.Panel panel5;
        private System.Windows.Forms.BindingSource plcScanItemsBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemName;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnAddressType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnDataType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.DataGridViewTextBoxColumn IoIndex;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ItemRefresh;
        private System.Windows.Forms.Label label1;
        private IPAddressControlLib.IPAddressControl ipAddressControlPC;
    }
}