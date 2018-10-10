namespace WorldGeneralLib.Forms
{
    partial class FormTesting
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
            this.label2 = new System.Windows.Forms.Label();
            this.btnCheckSfcSta = new System.Windows.Forms.Button();
            this.tbBarcode1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnDcForSfcEx = new System.Windows.Forms.Button();
            this.tbDryingTime = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbOilTemperature = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbBlowingTime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDryingPressure = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbSprayingTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbSprayingPressure = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbBarcode2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMesRes = new System.Windows.Forms.TextBox();
            this.panelMES = new BSE.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.panelMES.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Gray;
            this.label2.Location = new System.Drawing.Point(48, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(195, 33);
            this.label2.TabIndex = 3;
            this.label2.Text = "*正常，MES返回值等于0\r\n*异常，MES返回值大于0";
            // 
            // btnCheckSfcSta
            // 
            this.btnCheckSfcSta.Location = new System.Drawing.Point(586, 113);
            this.btnCheckSfcSta.Name = "btnCheckSfcSta";
            this.btnCheckSfcSta.Size = new System.Drawing.Size(164, 25);
            this.btnCheckSfcSta.TabIndex = 2;
            this.btnCheckSfcSta.Text = "Check SFC Status";
            this.btnCheckSfcSta.UseVisualStyleBackColor = true;
            this.btnCheckSfcSta.Click += new System.EventHandler(this.btnCheckSfcSta_Click);
            // 
            // tbBarcode1
            // 
            this.tbBarcode1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBarcode1.Location = new System.Drawing.Point(200, 68);
            this.tbBarcode1.Name = "tbBarcode1";
            this.tbBarcode1.Size = new System.Drawing.Size(550, 21);
            this.tbBarcode1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label1.Location = new System.Drawing.Point(43, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Barcode：  ";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.Gray;
            this.label10.Location = new System.Drawing.Point(55, 316);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 36);
            this.label10.TabIndex = 4;
            this.label10.Text = "*成功，MES返回值等于0\r\n*失败，MES返回值大于0";
            // 
            // btnDcForSfcEx
            // 
            this.btnDcForSfcEx.Location = new System.Drawing.Point(586, 327);
            this.btnDcForSfcEx.Name = "btnDcForSfcEx";
            this.btnDcForSfcEx.Size = new System.Drawing.Size(164, 25);
            this.btnDcForSfcEx.TabIndex = 4;
            this.btnDcForSfcEx.Text = "Data Collect For SFC Ex";
            this.btnDcForSfcEx.UseVisualStyleBackColor = true;
            this.btnDcForSfcEx.Click += new System.EventHandler(this.btnDcForSfcEx_Click);
            // 
            // tbDryingTime
            // 
            this.tbDryingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDryingTime.Location = new System.Drawing.Point(200, 254);
            this.tbDryingTime.Name = "tbDryingTime";
            this.tbDryingTime.Size = new System.Drawing.Size(164, 21);
            this.tbDryingTime.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label9.Location = new System.Drawing.Point(50, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Drying time：  ";
            // 
            // tbOilTemperature
            // 
            this.tbOilTemperature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbOilTemperature.Location = new System.Drawing.Point(586, 254);
            this.tbOilTemperature.Name = "tbOilTemperature";
            this.tbOilTemperature.Size = new System.Drawing.Size(164, 21);
            this.tbOilTemperature.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label8.Location = new System.Drawing.Point(440, 254);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Oil Temperature：  ";
            // 
            // tbBlowingTime
            // 
            this.tbBlowingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBlowingTime.Location = new System.Drawing.Point(586, 227);
            this.tbBlowingTime.Name = "tbBlowingTime";
            this.tbBlowingTime.Size = new System.Drawing.Size(164, 21);
            this.tbBlowingTime.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label7.Location = new System.Drawing.Point(440, 227);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Blowing time：  ";
            // 
            // tbDryingPressure
            // 
            this.tbDryingPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbDryingPressure.Location = new System.Drawing.Point(200, 227);
            this.tbDryingPressure.Name = "tbDryingPressure";
            this.tbDryingPressure.Size = new System.Drawing.Size(164, 21);
            this.tbDryingPressure.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label6.Location = new System.Drawing.Point(50, 226);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(139, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Drying pressure：  ";
            // 
            // tbSprayingTime
            // 
            this.tbSprayingTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSprayingTime.Location = new System.Drawing.Point(586, 201);
            this.tbSprayingTime.Name = "tbSprayingTime";
            this.tbSprayingTime.Size = new System.Drawing.Size(164, 21);
            this.tbSprayingTime.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label5.Location = new System.Drawing.Point(440, 198);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Spraying time：  ";
            // 
            // tbSprayingPressure
            // 
            this.tbSprayingPressure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbSprayingPressure.Location = new System.Drawing.Point(200, 201);
            this.tbSprayingPressure.Name = "tbSprayingPressure";
            this.tbSprayingPressure.Size = new System.Drawing.Size(164, 21);
            this.tbSprayingPressure.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label4.Location = new System.Drawing.Point(50, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(153, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Spraying pressure：  ";
            // 
            // tbBarcode2
            // 
            this.tbBarcode2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbBarcode2.Location = new System.Drawing.Point(200, 282);
            this.tbBarcode2.Name = "tbBarcode2";
            this.tbBarcode2.Size = new System.Drawing.Size(550, 21);
            this.tbBarcode2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 9.75F);
            this.label3.Location = new System.Drawing.Point(50, 280);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Barcode：  ";
            // 
            // tbMesRes
            // 
            this.tbMesRes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbMesRes.Location = new System.Drawing.Point(58, 406);
            this.tbMesRes.Multiline = true;
            this.tbMesRes.Name = "tbMesRes";
            this.tbMesRes.Size = new System.Drawing.Size(692, 275);
            this.tbMesRes.TabIndex = 0;
            // 
            // panelMES
            // 
            this.panelMES.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMES.BackColor = System.Drawing.Color.Transparent;
            this.panelMES.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelMES.CaptionFont = new System.Drawing.Font("新宋体", 14.25F);
            this.panelMES.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelMES.CloseIconForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelMES.CollapsedCaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.panelMES.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelMES.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelMES.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelMES.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelMES.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelMES.Controls.Add(this.label19);
            this.panelMES.Controls.Add(this.label10);
            this.panelMES.Controls.Add(this.tbMesRes);
            this.panelMES.Controls.Add(this.label18);
            this.panelMES.Controls.Add(this.btnDcForSfcEx);
            this.panelMES.Controls.Add(this.label2);
            this.panelMES.Controls.Add(this.tbDryingTime);
            this.panelMES.Controls.Add(this.label17);
            this.panelMES.Controls.Add(this.label9);
            this.panelMES.Controls.Add(this.btnCheckSfcSta);
            this.panelMES.Controls.Add(this.tbOilTemperature);
            this.panelMES.Controls.Add(this.label1);
            this.panelMES.Controls.Add(this.label8);
            this.panelMES.Controls.Add(this.tbBarcode1);
            this.panelMES.Controls.Add(this.tbBlowingTime);
            this.panelMES.Controls.Add(this.tbSprayingPressure);
            this.panelMES.Controls.Add(this.label7);
            this.panelMES.Controls.Add(this.label3);
            this.panelMES.Controls.Add(this.tbDryingPressure);
            this.panelMES.Controls.Add(this.tbBarcode2);
            this.panelMES.Controls.Add(this.label6);
            this.panelMES.Controls.Add(this.label4);
            this.panelMES.Controls.Add(this.tbSprayingTime);
            this.panelMES.Controls.Add(this.label5);
            this.panelMES.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelMES.Image = null;
            this.panelMES.InnerBorderColor = System.Drawing.Color.White;
            this.panelMES.Location = new System.Drawing.Point(262, 14);
            this.panelMES.Name = "panelMES";
            this.panelMES.ShowTransparentBackground = false;
            this.panelMES.ShowXPanderPanelProfessionalStyle = true;
            this.panelMES.Size = new System.Drawing.Size(817, 712);
            this.panelMES.TabIndex = 6;
            this.panelMES.Text = "MES";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(38, 367);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(98, 15);
            this.label19.TabIndex = 18;
            this.label19.Text = "MES数据返回";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(31, 171);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(151, 15);
            this.label18.TabIndex = 11;
            this.label18.Text = "电芯出站与数据采集";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 11.25F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(31, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(103, 15);
            this.label17.TabIndex = 10;
            this.label17.Text = "电芯进站校验";
            // 
            // FormTesting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1312, 750);
            this.Controls.Add(this.panelMES);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTesting";
            this.Text = "FormMesTesting";
            this.SizeChanged += new System.EventHandler(this.FormTesting_SizeChanged);
            this.panelMES.ResumeLayout(false);
            this.panelMES.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCheckSfcSta;
        private System.Windows.Forms.TextBox tbBarcode1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnDcForSfcEx;
        private System.Windows.Forms.TextBox tbDryingTime;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbOilTemperature;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbBlowingTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbDryingPressure;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbSprayingTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbSprayingPressure;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbBarcode2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbMesRes;
        private BSE.Windows.Forms.Panel panelMES;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
    }
}