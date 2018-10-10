namespace WorldGeneralLib.Hardware.Camera.ImagingSource.GigE
{
    partial class FormImagingSourceGigE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImagingSourceGigE));
            this.panelMain = new BSE.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBarBtnSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
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
            this.panelMain.Name = "panelMain";
            this.panelMain.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panelMain.ShowTransparentBackground = false;
            this.panelMain.ShowXPanderPanelProfessionalStyle = true;
            this.panelMain.Size = new System.Drawing.Size(859, 516);
            this.panelMain.TabIndex = 8;
            this.panelMain.Text = "ImagingSource GigE";
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
            this.toolStrip1.Size = new System.Drawing.Size(857, 29);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 26;
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
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(857, 489);
            this.panel1.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(11, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(835, 420);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Setting";
            // 
            // FormImagingSourceGigE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 516);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormImagingSourceGigE";
            this.Text = "FormImagingSourceGigE";
            this.panelMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BSE.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBarBtnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}