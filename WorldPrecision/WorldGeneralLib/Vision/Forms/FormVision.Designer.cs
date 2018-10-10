namespace WorldGeneralLib.Vision.Forms
{
    partial class FormVision
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.labErrMsg = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.labCurrSceneIndex = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labCycleTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.labResult = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelRight = new BSE.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelActions = new System.Windows.Forms.Panel();
            this.panelActionResult = new System.Windows.Forms.Panel();
            this.lbActionResult = new System.Windows.Forms.ListBox();
            this.panelTool = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnExecute = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSceneSelc = new System.Windows.Forms.Button();
            this.btnProcessEdit = new System.Windows.Forms.Button();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.imageBox1 = new Emgu.CV.UI.ImageBox();
            this.panelTop.SuspendLayout();
            this.panelRight.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelActionResult.SuspendLayout();
            this.panelTool.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.labErrMsg);
            this.panelTop.Controls.Add(this.label11);
            this.panelTop.Controls.Add(this.labCurrSceneIndex);
            this.panelTop.Controls.Add(this.label7);
            this.panelTop.Controls.Add(this.labCycleTime);
            this.panelTop.Controls.Add(this.label4);
            this.panelTop.Controls.Add(this.label3);
            this.panelTop.Controls.Add(this.labResult);
            this.panelTop.Controls.Add(this.label6);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Margin = new System.Windows.Forms.Padding(0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(756, 70);
            this.panelTop.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Silver;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(756, 1);
            this.label1.TabIndex = 20;
            // 
            // labErrMsg
            // 
            this.labErrMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labErrMsg.AutoSize = true;
            this.labErrMsg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labErrMsg.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labErrMsg.ForeColor = System.Drawing.Color.Red;
            this.labErrMsg.Location = new System.Drawing.Point(612, 40);
            this.labErrMsg.Name = "labErrMsg";
            this.labErrMsg.Size = new System.Drawing.Size(142, 23);
            this.labErrMsg.TabIndex = 17;
            this.labErrMsg.Text = "Connection lost.";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label11.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(503, 40);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(147, 23);
            this.label11.TabIndex = 16;
            this.label11.Text = "Error message： ";
            // 
            // labCurrSceneIndex
            // 
            this.labCurrSceneIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labCurrSceneIndex.AutoSize = true;
            this.labCurrSceneIndex.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labCurrSceneIndex.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCurrSceneIndex.ForeColor = System.Drawing.Color.White;
            this.labCurrSceneIndex.Location = new System.Drawing.Point(388, 41);
            this.labCurrSceneIndex.Name = "labCurrSceneIndex";
            this.labCurrSceneIndex.Size = new System.Drawing.Size(20, 23);
            this.labCurrSceneIndex.TabIndex = 13;
            this.labCurrSceneIndex.Text = "0";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label7.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(323, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Scene：";
            // 
            // labCycleTime
            // 
            this.labCycleTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labCycleTime.AutoSize = true;
            this.labCycleTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labCycleTime.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labCycleTime.ForeColor = System.Drawing.Color.White;
            this.labCycleTime.Location = new System.Drawing.Point(149, 33);
            this.labCycleTime.Name = "labCycleTime";
            this.labCycleTime.Size = new System.Drawing.Size(77, 35);
            this.labCycleTime.TabIndex = 11;
            this.labCycleTime.Text = "0 ms";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Font = new System.Drawing.Font("微软雅黑", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(149, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "Scene group: 0 ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Font = new System.Drawing.Font("微软雅黑", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Brown;
            this.label3.Location = new System.Drawing.Point(137, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(614, 61);
            this.label3.TabIndex = 7;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labResult
            // 
            this.labResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.labResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labResult.Font = new System.Drawing.Font("微软雅黑", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labResult.ForeColor = System.Drawing.Color.Red;
            this.labResult.Location = new System.Drawing.Point(6, 5);
            this.labResult.Name = "labResult";
            this.labResult.Size = new System.Drawing.Size(125, 61);
            this.labResult.TabIndex = 6;
            this.labResult.Text = "NG";
            this.labResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label6.Location = new System.Drawing.Point(0, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(756, 1);
            this.label6.TabIndex = 5;
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
            this.panelRight.Controls.Add(this.panel1);
            this.panelRight.Controls.Add(this.panelActionResult);
            this.panelRight.Controls.Add(this.panelTool);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelRight.Image = null;
            this.panelRight.InnerBorderColor = System.Drawing.Color.White;
            this.panelRight.Location = new System.Drawing.Point(759, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.ShowExpandIcon = true;
            this.panelRight.ShowXPanderPanelProfessionalStyle = true;
            this.panelRight.Size = new System.Drawing.Size(242, 625);
            this.panelRight.TabIndex = 21;
            this.panelRight.Text = "综合设置";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.panelActions);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(1, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 294);
            this.panel1.TabIndex = 3;
            // 
            // panelActions
            // 
            this.panelActions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelActions.AutoScroll = true;
            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Location = new System.Drawing.Point(2, 2);
            this.panelActions.Name = "panelActions";
            this.panelActions.Size = new System.Drawing.Size(236, 290);
            this.panelActions.TabIndex = 2;
            // 
            // panelActionResult
            // 
            this.panelActionResult.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelActionResult.Controls.Add(this.lbActionResult);
            this.panelActionResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActionResult.Location = new System.Drawing.Point(1, 483);
            this.panelActionResult.Name = "panelActionResult";
            this.panelActionResult.Size = new System.Drawing.Size(240, 141);
            this.panelActionResult.TabIndex = 1;
            // 
            // lbActionResult
            // 
            this.lbActionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbActionResult.Font = new System.Drawing.Font("新宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbActionResult.FormattingEnabled = true;
            this.lbActionResult.ItemHeight = 16;
            this.lbActionResult.Items.AddRange(new object[] {
            "[1. 图像输入]",
            "",
            "判定：OK",
            "相机NO.：0"});
            this.lbActionResult.Location = new System.Drawing.Point(0, 0);
            this.lbActionResult.Name = "lbActionResult";
            this.lbActionResult.Size = new System.Drawing.Size(240, 141);
            this.lbActionResult.TabIndex = 0;
            // 
            // panelTool
            // 
            this.panelTool.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panelTool.Controls.Add(this.tabControl1);
            this.panelTool.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTool.Location = new System.Drawing.Point(1, 26);
            this.panelTool.Name = "panelTool";
            this.panelTool.Size = new System.Drawing.Size(240, 163);
            this.panelTool.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(240, 163);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage2.Controls.Add(this.btnExecute);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(232, 134);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "工具";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnExecute
            // 
            this.btnExecute.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnExecute.Location = new System.Drawing.Point(5, 5);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(100, 30);
            this.btnExecute.TabIndex = 2;
            this.btnExecute.Text = "执行";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage1.Controls.Add(this.btnSave);
            this.tabPage1.Controls.Add(this.btnSceneSelc);
            this.tabPage1.Controls.Add(this.btnProcessEdit);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(232, 134);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "系统";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(5, 41);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSceneSelc
            // 
            this.btnSceneSelc.Location = new System.Drawing.Point(123, 6);
            this.btnSceneSelc.Name = "btnSceneSelc";
            this.btnSceneSelc.Size = new System.Drawing.Size(100, 30);
            this.btnSceneSelc.TabIndex = 4;
            this.btnSceneSelc.Text = "场景切换";
            this.btnSceneSelc.UseVisualStyleBackColor = true;
            this.btnSceneSelc.Click += new System.EventHandler(this.btnSceneSelc_Click);
            // 
            // btnProcessEdit
            // 
            this.btnProcessEdit.Location = new System.Drawing.Point(5, 5);
            this.btnProcessEdit.Name = "btnProcessEdit";
            this.btnProcessEdit.Size = new System.Drawing.Size(100, 30);
            this.btnProcessEdit.TabIndex = 3;
            this.btnProcessEdit.Text = "流程编辑";
            this.btnProcessEdit.UseVisualStyleBackColor = true;
            this.btnProcessEdit.Click += new System.EventHandler(this.btnProcessEdit_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(756, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 625);
            this.splitter1.TabIndex = 22;
            this.splitter1.TabStop = false;
            // 
            // imageBox1
            // 
            this.imageBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.imageBox1.FunctionalMode = Emgu.CV.UI.ImageBox.FunctionalModeOption.PanAndZoom;
            this.imageBox1.Location = new System.Drawing.Point(0, 69);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(754, 555);
            this.imageBox1.TabIndex = 23;
            this.imageBox1.TabStop = false;
            // 
            // FormVision
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 625);
            this.Controls.Add(this.imageBox1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelRight);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormVision";
            this.Text = "FormVision";
            this.Load += new System.EventHandler(this.FormVision_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelActionResult.ResumeLayout(false);
            this.panelTool.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label labErrMsg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labCurrSceneIndex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labCycleTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labResult;
        private System.Windows.Forms.Label label6;
        private BSE.Windows.Forms.Panel panelRight;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelTool;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Panel panelActionResult;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ListBox lbActionResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSceneSelc;
        private System.Windows.Forms.Button btnProcessEdit;
        private Emgu.CV.UI.ImageBox imageBox1;
    }
}