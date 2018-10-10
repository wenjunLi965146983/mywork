namespace WorldGeneralLib.Vision.Actions.AccurateSearch
{
    partial class FormActionAccurateSearch
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabImageSrcLocal = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnModelOpen = new System.Windows.Forms.Button();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tbTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.button3 = new System.Windows.Forms.Button();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panelRight = new BSE.Windows.Forms.Panel();
            this.cmbImageSrc = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabImageSrcLocal.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            this.panelRight.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panelRight);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1465, 879);
            this.panel1.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabImageSrcLocal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(271, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1194, 879);
            this.panel3.TabIndex = 26;
            // 
            // tabImageSrcLocal
            // 
            this.tabImageSrcLocal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabImageSrcLocal.Controls.Add(this.tabPage2);
            this.tabImageSrcLocal.Controls.Add(this.tabPage3);
            this.tabImageSrcLocal.Controls.Add(this.tabPage1);
            this.tabImageSrcLocal.Font = new System.Drawing.Font("新宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabImageSrcLocal.Location = new System.Drawing.Point(4, 4);
            this.tabImageSrcLocal.Margin = new System.Windows.Forms.Padding(4);
            this.tabImageSrcLocal.Name = "tabImageSrcLocal";
            this.tabImageSrcLocal.SelectedIndex = 0;
            this.tabImageSrcLocal.Size = new System.Drawing.Size(1190, 909);
            this.tabImageSrcLocal.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.btnShow);
            this.tabPage3.Controls.Add(this.btnModelOpen);
            this.tabPage3.Controls.Add(this.imageBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1182, 879);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "模版设定";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // btnShow
            // 
            this.btnShow.Location = new System.Drawing.Point(26, 41);
            this.btnShow.Margin = new System.Windows.Forms.Padding(4);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(100, 32);
            this.btnShow.TabIndex = 6;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // btnModelOpen
            // 
            this.btnModelOpen.Location = new System.Drawing.Point(26, 101);
            this.btnModelOpen.Margin = new System.Windows.Forms.Padding(4);
            this.btnModelOpen.Name = "btnModelOpen";
            this.btnModelOpen.Size = new System.Drawing.Size(100, 32);
            this.btnModelOpen.TabIndex = 4;
            this.btnModelOpen.Text = "Open";
            this.btnModelOpen.UseVisualStyleBackColor = true;
            this.btnModelOpen.Click += new System.EventHandler(this.btnModelOpen_Click);
            // 
            // imageBox2
            // 
            this.imageBox2.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox2.Location = new System.Drawing.Point(235, 12);
            this.imageBox2.Name = "imageBox2";
            this.imageBox2.Size = new System.Drawing.Size(648, 486);
            this.imageBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox2.TabIndex = 3;
            this.imageBox2.TabStop = false;
            this.imageBox2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox2_MouseDown);
            this.imageBox2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox2_MouseMove);
            this.imageBox2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox2_MouseUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel4);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1182, 879);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "ROI设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.tbTime);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.imageBox3);
            this.panel4.Controls.Add(this.button3);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1193, 770);
            this.panel4.TabIndex = 0;
            // 
            // tbTime
            // 
            this.tbTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbTime.Location = new System.Drawing.Point(118, 90);
            this.tbTime.Margin = new System.Windows.Forms.Padding(4);
            this.tbTime.Name = "tbTime";
            this.tbTime.Size = new System.Drawing.Size(133, 26);
            this.tbTime.TabIndex = 10;
            this.tbTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbTime_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(21, 96);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "缩放倍数：";
            // 
            // imageBox3
            // 
            this.imageBox3.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.Minimum;
            this.imageBox3.Location = new System.Drawing.Point(272, 14);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(648, 486);
            this.imageBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox3.TabIndex = 8;
            this.imageBox3.TabStop = false;
            this.imageBox3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageBox3_MouseDown);
            this.imageBox3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.imageBox3_MouseMove);
            this.imageBox3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.imageBox3_MouseUp);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(69, 201);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(100, 32);
            this.button3.TabIndex = 6;
            this.button3.Text = "Open";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(267, 0);
            this.splitter1.Margin = new System.Windows.Forms.Padding(4);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(4, 879);
            this.splitter1.TabIndex = 24;
            this.splitter1.TabStop = false;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.Transparent;
            this.panelRight.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelRight.CaptionFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelRight.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelRight.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelRight.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panelRight.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelRight.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelRight.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelRight.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelRight.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelRight.Controls.Add(this.cmbImageSrc);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelRight.Image = null;
            this.panelRight.InnerBorderColor = System.Drawing.Color.White;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Margin = new System.Windows.Forms.Padding(4);
            this.panelRight.Name = "panelRight";
            this.panelRight.ShowExpandIcon = true;
            this.panelRight.ShowXPanderPanelProfessionalStyle = true;
            this.panelRight.Size = new System.Drawing.Size(267, 879);
            this.panelRight.TabIndex = 23;
            this.panelRight.Text = "图像选择";
            // 
            // cmbImageSrc
            // 
            this.cmbImageSrc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbImageSrc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImageSrc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbImageSrc.FormattingEnabled = true;
            this.cmbImageSrc.Items.AddRange(new object[] {
            "相机 0",
            "相机 1",
            "相机 2",
            "相机 3",
            "相机 4",
            "相机 5",
            "相机 6",
            "相机 7"});
            this.cmbImageSrc.Location = new System.Drawing.Point(19, 44);
            this.cmbImageSrc.Margin = new System.Windows.Forms.Padding(4);
            this.cmbImageSrc.Name = "cmbImageSrc";
            this.cmbImageSrc.Size = new System.Drawing.Size(225, 28);
            this.cmbImageSrc.TabIndex = 7;
            this.cmbImageSrc.SelectedIndexChanged += new System.EventHandler(this.cmbDefaultCameraList_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.imageBox1);
            this.panel2.Location = new System.Drawing.Point(16, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1150, 561);
            this.panel2.TabIndex = 0;
            // 
            // imageBox1
            // 
            this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.imageBox1.Location = new System.Drawing.Point(4, 4);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(1143, 543);
            this.imageBox1.TabIndex = 2;
            this.imageBox1.TabStop = false;
            this.imageBox1.Click += new System.EventHandler(this.imageBox1_Click);
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(1063, 779);
            this.btnTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(100, 32);
            this.btnTest.TabIndex = 3;
            this.btnTest.Text = "Init";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(897, 679);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(897, 639);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.btnTest);
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1182, 879);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "参数设定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // FormActionAccurateSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1465, 879);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormActionAccurateSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像匹配";
            this.Load += new System.EventHandler(this.FormActionAccurateSearch_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabImageSrcLocal.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private BSE.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panel3;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabImageSrcLocal;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cmbImageSrc;
        private System.Windows.Forms.Button btnModelOpen;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label label6;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Panel panel2;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}