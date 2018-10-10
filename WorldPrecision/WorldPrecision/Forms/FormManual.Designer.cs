namespace WorldPrecision.Forms
{
    partial class FormManual
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.tbCtrlIOView = new System.Windows.Forms.TabControl();
            this.tabPageAxisIO = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panelMain.SuspendLayout();
            this.tbCtrlIOView.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.White;
            this.panelMain.Controls.Add(this.tbCtrlIOView);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(654, 268);
            this.panelMain.TabIndex = 4;
            // 
            // tbCtrlIOView
            // 
            this.tbCtrlIOView.Controls.Add(this.tabPageAxisIO);
            this.tbCtrlIOView.Controls.Add(this.tabPage2);
            this.tbCtrlIOView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlIOView.Location = new System.Drawing.Point(0, 0);
            this.tbCtrlIOView.Name = "tbCtrlIOView";
            this.tbCtrlIOView.SelectedIndex = 0;
            this.tbCtrlIOView.Size = new System.Drawing.Size(654, 268);
            this.tbCtrlIOView.TabIndex = 26;
            this.tbCtrlIOView.SelectedIndexChanged += new System.EventHandler(this.tbCtrlIOView_SelectedIndexChanged);
            // 
            // tabPageAxisIO
            // 
            this.tabPageAxisIO.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageAxisIO.Location = new System.Drawing.Point(4, 22);
            this.tabPageAxisIO.Name = "tabPageAxisIO";
            this.tabPageAxisIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAxisIO.Size = new System.Drawing.Size(646, 242);
            this.tabPageAxisIO.TabIndex = 0;
            this.tabPageAxisIO.Text = "Axis IO";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(646, 242);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Extern IO";
            // 
            // FormManual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(654, 268);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormManual";
            this.Text = "FormManual";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormManual_Load);
            this.panelMain.ResumeLayout(false);
            this.tbCtrlIOView.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.TabControl tbCtrlIOView;
        private System.Windows.Forms.TabPage tabPageAxisIO;
        private System.Windows.Forms.TabPage tabPage2;
    }
}