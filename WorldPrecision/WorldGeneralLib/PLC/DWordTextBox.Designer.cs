namespace WorldGeneralLib.PLC
{
    partial class DWordTextBox
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxDisplay = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxDisplay
            // 
            this.textBoxDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxDisplay.Location = new System.Drawing.Point(0, 0);
            this.textBoxDisplay.Name = "textBoxDisplay";
            this.textBoxDisplay.Size = new System.Drawing.Size(117, 21);
            this.textBoxDisplay.TabIndex = 0;
            this.textBoxDisplay.Text = "AAAA";
            this.textBoxDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxDisplay.Enter += new System.EventHandler(this.textBoxDisplay_Enter);
            this.textBoxDisplay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDisplay_KeyDown);
            this.textBoxDisplay.Leave += new System.EventHandler(this.textBoxDisplay_Leave);
            // 
            // DWordTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBoxDisplay);
            this.Name = "DWordTextBox";
            this.Size = new System.Drawing.Size(117, 23);
            this.Load += new System.EventHandler(this.DWordTextBox_Load);
            this.BackColorChanged += new System.EventHandler(this.DWordTextBox_FontChanged);
            this.FontChanged += new System.EventHandler(this.DWordTextBox_FontChanged);
            this.ForeColorChanged += new System.EventHandler(this.DWordTextBox_FontChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.DWordTextBox_KeyPress);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDisplay;
    }
}
