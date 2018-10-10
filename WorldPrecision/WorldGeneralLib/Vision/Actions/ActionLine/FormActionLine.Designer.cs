using System;
using System.Windows.Forms;

namespace WorldGeneralLib.Vision.Actions.Line
{
    partial class FormActionLine
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
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.tbgab = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLineType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbthreshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tblength = new System.Windows.Forms.TextBox();
            this.tbCThreshold1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbtheta = new System.Windows.Forms.TextBox();
            this.tbCThreshold2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbrho = new System.Windows.Forms.TextBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label8 = new System.Windows.Forms.Label();
            this.rbROIReset = new System.Windows.Forms.RadioButton();
            this.btnShow = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnModelOpen = new System.Windows.Forms.Button();
            this.imageBox2 = new Emgu.CV.UI.ImageBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imageBox3 = new Emgu.CV.UI.ImageBox();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panelRight = new BSE.Windows.Forms.Panel();
            this.cmbImageSrc = new System.Windows.Forms.ComboBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabImageSrcLocal.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).BeginInit();
            this.panelRight.SuspendLayout();
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
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.tabControl1);
            this.tabPage2.Controls.Add(this.btnSave);
            this.tabPage2.Controls.Add(this.button1);
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(20, 580);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(510, 267);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.tbgab);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.cbLineType);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label2);
            this.tabPage4.Controls.Add(this.tbthreshold);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Controls.Add(this.tblength);
            this.tabPage4.Controls.Add(this.tbCThreshold1);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.tbtheta);
            this.tabPage4.Controls.Add(this.tbCThreshold2);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.tbrho);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(502, 237);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "霍夫变换";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(264, 148);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 17);
            this.label11.TabIndex = 17;
            this.label11.Text = "最大间隔：";
            // 
            // tbgab
            // 
            this.tbgab.Location = new System.Drawing.Point(373, 145);
            this.tbgab.Name = "tbgab";
            this.tbgab.Size = new System.Drawing.Size(100, 26);
            this.tbgab.TabIndex = 18;
            this.tbgab.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbgab_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(264, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "最小交点：";
            // 
            // cbLineType
            // 
            this.cbLineType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLineType.FormattingEnabled = true;
            this.cbLineType.Items.AddRange(new object[] {
            "霍夫拟合",
            "最小二乘法"});
            this.cbLineType.Location = new System.Drawing.Point(104, 6);
            this.cbLineType.Name = "cbLineType";
            this.cbLineType.Size = new System.Drawing.Size(141, 24);
            this.cbLineType.TabIndex = 0;
            this.cbLineType.SelectedIndexChanged += new System.EventHandler(this.cbLineType_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(264, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "长度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "过滤类型：";
            // 
            // tbthreshold
            // 
            this.tbthreshold.Location = new System.Drawing.Point(373, 94);
            this.tbthreshold.Name = "tbthreshold";
            this.tbthreshold.Size = new System.Drawing.Size(100, 26);
            this.tbthreshold.TabIndex = 16;
            this.tbthreshold.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbthreshold_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "第一阀值：";
            // 
            // tblength
            // 
            this.tblength.Location = new System.Drawing.Point(373, 48);
            this.tblength.Name = "tblength";
            this.tblength.Size = new System.Drawing.Size(100, 26);
            this.tblength.TabIndex = 14;
            this.tblength.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tblength_KeyPress);
            // 
            // tbCThreshold1
            // 
            this.tbCThreshold1.Location = new System.Drawing.Point(111, 48);
            this.tbCThreshold1.Name = "tbCThreshold1";
            this.tbCThreshold1.Size = new System.Drawing.Size(100, 26);
            this.tbCThreshold1.TabIndex = 2;
            this.tbCThreshold1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCThreshold1_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(98, 17);
            this.label9.TabIndex = 4;
            this.label9.Text = "第二阀值：";
            // 
            // tbtheta
            // 
            this.tbtheta.Location = new System.Drawing.Point(111, 191);
            this.tbtheta.Name = "tbtheta";
            this.tbtheta.Size = new System.Drawing.Size(100, 26);
            this.tbtheta.TabIndex = 12;
            this.tbtheta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbtheta_KeyPress);
            // 
            // tbCThreshold2
            // 
            this.tbCThreshold2.Location = new System.Drawing.Point(111, 94);
            this.tbCThreshold2.Name = "tbCThreshold2";
            this.tbCThreshold2.Size = new System.Drawing.Size(100, 26);
            this.tbCThreshold2.TabIndex = 5;
            this.tbCThreshold2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbCThreshold2_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "极角：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "极径：";
            // 
            // tbrho
            // 
            this.tbrho.Location = new System.Drawing.Point(111, 145);
            this.tbrho.Name = "tbrho";
            this.tbrho.Size = new System.Drawing.Size(100, 26);
            this.tbrho.TabIndex = 10;
            this.tbrho.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbrho_KeyPress);
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(502, 237);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "最小二乘法";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(591, 641);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 32);
            this.button1.TabIndex = 8;
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.imageBox1);
            this.panel2.Location = new System.Drawing.Point(16, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1150, 561);
            this.panel2.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.OliveDrab;
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.Font = new System.Drawing.Font("新宋体", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(568, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(95, 40);
            this.label7.TabIndex = 4;
            this.label7.Text = "输出";
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
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.btnClear);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.rbROIReset);
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
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(23, 216);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 17);
            this.label8.TabIndex = 8;
            // 
            // rbROIReset
            // 
            this.rbROIReset.AutoSize = true;
            this.rbROIReset.Location = new System.Drawing.Point(26, 170);
            this.rbROIReset.Name = "rbROIReset";
            this.rbROIReset.Size = new System.Drawing.Size(128, 21);
            this.rbROIReset.TabIndex = 7;
            this.rbROIReset.TabStop = true;
            this.rbROIReset.Text = "输出清除ROI";
            this.rbROIReset.UseVisualStyleBackColor = true;
            this.rbROIReset.Click += new System.EventHandler(this.rbROIReset_Click);
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
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(591, 727);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "确定";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.imageBox2.Location = new System.Drawing.Point(293, 12);
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
            this.panel4.Controls.Add(this.imageBox3);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1193, 770);
            this.panel4.TabIndex = 0;
            // 
            // imageBox3
            // 
            this.imageBox3.Location = new System.Drawing.Point(0, 0);
            this.imageBox3.Name = "imageBox3";
            this.imageBox3.Size = new System.Drawing.Size(0, 0);
            this.imageBox3.TabIndex = 2;
            this.imageBox3.TabStop = false;
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
            this.cmbImageSrc.SelectedIndexChanged += new System.EventHandler(this.cmbImageSrc_SelectedIndexChanged);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(26, 352);
            this.btnClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 32);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // FormActionLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1465, 879);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormActionLine";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "图像匹配";
            this.Load += new System.EventHandler(this.FormActionMatch_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabImageSrcLocal.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox2)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox3)).EndInit();
            this.panelRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }





        #endregion

        private System.Windows.Forms.Panel panel1;
        private BSE.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panel3;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.TabControl tabImageSrcLocal;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cmbImageSrc;
        private System.Windows.Forms.Panel panel2;
        private Emgu.CV.UI.ImageBox imageBox1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnModelOpen;
        private Emgu.CV.UI.ImageBox imageBox2;
        private System.Windows.Forms.Button btnShow;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel4;
        private Emgu.CV.UI.ImageBox imageBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbCThreshold1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbLineType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton rbROIReset;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbCThreshold2;
        private System.Windows.Forms.Label label9;
        private TextBox tbgab;
        private Label label11;
        private TextBox tbthreshold;
        private Label label5;
        private TextBox tblength;
        private Label label6;
        private TextBox tbtheta;
        private Label label3;
        private TextBox tbrho;
        private Label label4;
        private TabControl tabControl1;
        private TabPage tabPage4;
        private TabPage tabPage5;
        private Button btnClear;
    }
}