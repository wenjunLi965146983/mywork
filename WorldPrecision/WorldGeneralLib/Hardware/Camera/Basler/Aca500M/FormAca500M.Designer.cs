namespace WorldGeneralLib.Hardware.Camera.Basler.Aca500M
{
    partial class FormAca500M
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAca500M));
            this.panelMain = new BSE.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBarBtnSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new BSE.Windows.Forms.Panel();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.MultiShot = new System.Windows.Forms.Button();
            this.btnSingleShot = new System.Windows.Forms.Button();
            this.labConnSta = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.panelAndon = new BSE.Windows.Forms.Panel();
            this.labelSerialNumber = new System.Windows.Forms.Label();
            this.btnSearchCamera = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbCamera = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.panelAndon.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelMain.CaptionFont = new System.Drawing.Font("Segoe UI", 9F);
            this.panelMain.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelMain.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelMain.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panelMain.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelMain.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelMain.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelMain.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelMain.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelMain.Controls.Add(this.toolStrip1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelMain.Image = null;
            this.panelMain.InnerBorderColor = System.Drawing.Color.Black;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(4);
            this.panelMain.Name = "panelMain";
            this.panelMain.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panelMain.ShowTransparentBackground = false;
            this.panelMain.ShowXPanderPanelProfessionalStyle = true;
            this.panelMain.Size = new System.Drawing.Size(1148, 871);
            this.panelMain.TabIndex = 8;
            this.panelMain.Text = "BaslerAca500M";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarBtnSave});
            this.toolStrip1.Location = new System.Drawing.Point(1, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1146, 36);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip3";
            // 
            // toolBarBtnSave
            // 
            this.toolBarBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolBarBtnSave.Image")));
            this.toolBarBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarBtnSave.Name = "toolBarBtnSave";
            this.toolBarBtnSave.Size = new System.Drawing.Size(75, 33);
            this.toolBarBtnSave.Text = "Save  ";
            this.toolBarBtnSave.Click += new System.EventHandler(this.toolBarBtnSave_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panelAndon);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 26);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.panel1.Size = new System.Drawing.Size(1146, 844);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel3.CaptionFont = new System.Drawing.Font("Microsoft YaHei UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.panel3.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panel3.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panel3.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panel3.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel3.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panel3.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panel3.Controls.Add(this.imageBox1);
            this.panel3.Controls.Add(this.MultiShot);
            this.panel3.Controls.Add(this.btnSingleShot);
            this.panel3.Controls.Add(this.labConnSta);
            this.panel3.Controls.Add(this.btnOpen);
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Image = null;
            this.panel3.InnerBorderColor = System.Drawing.Color.White;
            this.panel3.Location = new System.Drawing.Point(608, 55);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(513, 540);
            this.panel3.TabIndex = 27;
            this.panel3.Text = "````";
            // 
            // imageBox1
            // 
            this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.imageBox1.Location = new System.Drawing.Point(45, 54);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(440, 365);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            // 
            // MultiShot
            // 
            this.MultiShot.Location = new System.Drawing.Point(279, 444);
            this.MultiShot.Name = "MultiShot";
            this.MultiShot.Size = new System.Drawing.Size(75, 30);
            this.MultiShot.TabIndex = 29;
            this.MultiShot.Text = "Multi";
            this.MultiShot.UseVisualStyleBackColor = true;
            this.MultiShot.Click += new System.EventHandler(this.MultiShot_Click);
            // 
            // btnSingleShot
            // 
            this.btnSingleShot.Location = new System.Drawing.Point(146, 444);
            this.btnSingleShot.Name = "btnSingleShot";
            this.btnSingleShot.Size = new System.Drawing.Size(90, 30);
            this.btnSingleShot.TabIndex = 27;
            this.btnSingleShot.Text = "Single";
            this.btnSingleShot.UseVisualStyleBackColor = true;
            this.btnSingleShot.Click += new System.EventHandler(this.btnSingleShot_Click);
            // 
            // labConnSta
            // 
            this.labConnSta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labConnSta.AutoSize = true;
            this.labConnSta.BackColor = System.Drawing.Color.Red;
            this.labConnSta.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labConnSta.Location = new System.Drawing.Point(16, 498);
            this.labConnSta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labConnSta.Name = "labConnSta";
            this.labConnSta.Size = new System.Drawing.Size(118, 20);
            this.labConnSta.TabIndex = 25;
            this.labConnSta.Text = "Connect failed.";
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpen.Location = new System.Drawing.Point(20, 444);
            this.btnOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(95, 31);
            this.btnOpen.TabIndex = 26;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // panelAndon
            // 
            this.panelAndon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelAndon.BackColor = System.Drawing.Color.Transparent;
            this.panelAndon.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelAndon.CaptionFont = new System.Drawing.Font("新宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelAndon.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelAndon.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelAndon.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panelAndon.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelAndon.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelAndon.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelAndon.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelAndon.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelAndon.Controls.Add(this.labelSerialNumber);
            this.panelAndon.Controls.Add(this.btnSearchCamera);
            this.panelAndon.Controls.Add(this.label3);
            this.panelAndon.Controls.Add(this.label7);
            this.panelAndon.Controls.Add(this.cmbCamera);
            this.panelAndon.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelAndon.Image = null;
            this.panelAndon.InnerBorderColor = System.Drawing.Color.White;
            this.panelAndon.Location = new System.Drawing.Point(8, 55);
            this.panelAndon.Margin = new System.Windows.Forms.Padding(4);
            this.panelAndon.Name = "panelAndon";
            this.panelAndon.ShowTransparentBackground = false;
            this.panelAndon.ShowXPanderPanelProfessionalStyle = true;
            this.panelAndon.Size = new System.Drawing.Size(948, 540);
            this.panelAndon.TabIndex = 6;
            this.panelAndon.Text = "通讯设置";
            // 
            // labelSerialNumber
            // 
            this.labelSerialNumber.AutoSize = true;
            this.labelSerialNumber.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelSerialNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelSerialNumber.Location = new System.Drawing.Point(3, 176);
            this.labelSerialNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelSerialNumber.Name = "labelSerialNumber";
            this.labelSerialNumber.Size = new System.Drawing.Size(81, 20);
            this.labelSerialNumber.TabIndex = 26;
            this.labelSerialNumber.Text = "00000000";
            // 
            // btnSearchCamera
            // 
            this.btnSearchCamera.Location = new System.Drawing.Point(466, 79);
            this.btnSearchCamera.Name = "btnSearchCamera";
            this.btnSearchCamera.Size = new System.Drawing.Size(107, 27);
            this.btnSearchCamera.TabIndex = 25;
            this.btnSearchCamera.Text = "Search";
            this.btnSearchCamera.UseVisualStyleBackColor = true;
            this.btnSearchCamera.Click += new System.EventHandler(this.btnSearchCamera_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(3, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "相机查找：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Location = new System.Drawing.Point(3, 144);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(130, 20);
            this.label7.TabIndex = 6;
            this.label7.Text = "SeriralNumber：";
            // 
            // cmbCamera
            // 
            this.cmbCamera.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCamera.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbCamera.FormattingEnabled = true;
            this.cmbCamera.Location = new System.Drawing.Point(8, 79);
            this.cmbCamera.Margin = new System.Windows.Forms.Padding(4);
            this.cmbCamera.Name = "cmbCamera";
            this.cmbCamera.Size = new System.Drawing.Size(445, 28);
            this.cmbCamera.TabIndex = 1;
            this.cmbCamera.SelectedIndexChanged += new System.EventHandler(this.cmbCamera_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormAca500M
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 871);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAca500M";
            this.Text = "FormKeyenceSR700";
            this.Load += new System.EventHandler(this.FormAca500M_Load);
            this.panelMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.panelAndon.ResumeLayout(false);
            this.panelAndon.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private BSE.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBarBtnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label labConnSta;
        private System.Windows.Forms.Timer timer1;
        private BSE.Windows.Forms.Panel panelAndon;
        private System.Windows.Forms.Button btnSearchCamera;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbCamera;
        private BSE.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label labelSerialNumber;
        private System.Windows.Forms.Button btnSingleShot;
        private System.Windows.Forms.Button MultiShot;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}