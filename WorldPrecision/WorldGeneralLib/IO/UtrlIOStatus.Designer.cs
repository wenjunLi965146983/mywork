namespace WorldGeneralLib.IO
{
    partial class UtrlIOStatus
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSta = new System.Windows.Forms.Button();
            this.labText = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btnSta
            // 
            this.btnSta.BackColor = System.Drawing.Color.White;
            this.btnSta.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSta.FlatAppearance.BorderSize = 2;
            this.btnSta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSta.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSta.Location = new System.Drawing.Point(1, 2);
            this.btnSta.Name = "btnSta";
            this.btnSta.Size = new System.Drawing.Size(18, 18);
            this.btnSta.TabIndex = 27;
            this.btnSta.UseVisualStyleBackColor = false;
            this.btnSta.Click += new System.EventHandler(this.btnSta_Click);
            // 
            // labText
            // 
            this.labText.AutoSize = true;
            this.labText.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labText.Location = new System.Drawing.Point(25, 2);
            this.labText.Name = "labText";
            this.labText.Size = new System.Drawing.Size(32, 17);
            this.labText.TabIndex = 28;
            this.labText.Text = "Text";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UtrlIOStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Controls.Add(this.labText);
            this.Controls.Add(this.btnSta);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "UtrlIOStatus";
            this.Size = new System.Drawing.Size(181, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSta;
        public System.Windows.Forms.Label labText;
        private System.Windows.Forms.Timer timer1;
    }
}
