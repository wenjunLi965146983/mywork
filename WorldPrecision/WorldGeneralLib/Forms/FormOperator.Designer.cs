namespace WorldGeneralLib.Forms
{
    partial class FormOperator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperator));
            this.btnMacOp = new RibbonStyle.RibbonMenuButton();
            this.MenuStripMacOp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemAlarmClear = new System.Windows.Forms.ToolStripMenuItem();
            this.machineResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.MenuStripMacOp.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnMacOp
            // 
            this.btnMacOp.Arrow = RibbonStyle.RibbonMenuButton.e_arrow.ToDown;
            this.btnMacOp.BackColor = System.Drawing.Color.Transparent;
            this.btnMacOp.ColorBase = System.Drawing.Color.Transparent;
            this.btnMacOp.ColorBaseStroke = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))), ((int)(((byte)(76)))));
            this.btnMacOp.ColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(78)))));
            this.btnMacOp.ColorOnStroke = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(177)))), ((int)(((byte)(118)))));
            this.btnMacOp.ColorPress = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnMacOp.ColorPressStroke = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnMacOp.ContextMenuStrip = this.MenuStripMacOp;
            this.btnMacOp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnMacOp.FadingSpeed = 20;
            this.btnMacOp.FlatAppearance.BorderSize = 0;
            this.btnMacOp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMacOp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMacOp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnMacOp.GroupPos = RibbonStyle.RibbonMenuButton.e_groupPos.None;
            this.btnMacOp.Image = ((System.Drawing.Image)(resources.GetObject("btnMacOp.Image")));
            this.btnMacOp.ImageLocation = RibbonStyle.RibbonMenuButton.e_imagelocation.Left;
            this.btnMacOp.ImageOffset = 5;
            this.btnMacOp.ImageOffsetX = 2;
            this.btnMacOp.IsPressed = false;
            this.btnMacOp.KeepPress = false;
            this.btnMacOp.Location = new System.Drawing.Point(0, 0);
            this.btnMacOp.MaxImageSize = new System.Drawing.Point(24, 26);
            this.btnMacOp.MenuPos = new System.Drawing.Point(0, 0);
            this.btnMacOp.Name = "btnMacOp";
            this.btnMacOp.Radius = 6;
            this.btnMacOp.ShowBase = RibbonStyle.RibbonMenuButton.e_showbase.Yes;
            this.btnMacOp.Size = new System.Drawing.Size(208, 38);
            this.btnMacOp.SplitButton = RibbonStyle.RibbonMenuButton.e_splitbutton.Yes;
            this.btnMacOp.SplitDistance = 25;
            this.btnMacOp.TabIndex = 5;
            this.btnMacOp.Text = "START";
            this.btnMacOp.TextOfsetX = 2;
            this.btnMacOp.TextOfsetY = 0;
            this.btnMacOp.Title = "MACHINE OPERATION";
            this.btnMacOp.UseVisualStyleBackColor = true;
            this.btnMacOp.Click += new System.EventHandler(this.btnMacOp_Click);
            // 
            // MenuStripMacOp
            // 
            this.MenuStripMacOp.AutoSize = false;
            this.MenuStripMacOp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MenuStripMacOp.DropShadowEnabled = false;
            this.MenuStripMacOp.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuStripMacOp.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStripMacOp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAlarmClear,
            this.machineResetToolStripMenuItem});
            this.MenuStripMacOp.Name = "Paste";
            this.MenuStripMacOp.Size = new System.Drawing.Size(250, 78);
            // 
            // toolStripMenuItemAlarmClear
            // 
            this.toolStripMenuItemAlarmClear.AutoSize = false;
            this.toolStripMenuItemAlarmClear.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItemAlarmClear.Image")));
            this.toolStripMenuItemAlarmClear.Name = "toolStripMenuItemAlarmClear";
            this.toolStripMenuItemAlarmClear.Size = new System.Drawing.Size(250, 26);
            this.toolStripMenuItemAlarmClear.Text = "ALARM CLEAR( Long Press )            ";
            this.toolStripMenuItemAlarmClear.MouseDown += new System.Windows.Forms.MouseEventHandler(this.toolStripMenuItemAlarmClear_MouseDown);
            this.toolStripMenuItemAlarmClear.MouseLeave += new System.EventHandler(this.toolStripMenuItemAlarmClear_MouseLeave);
            this.toolStripMenuItemAlarmClear.MouseUp += new System.Windows.Forms.MouseEventHandler(this.toolStripMenuItemAlarmClear_MouseUp);
            // 
            // machineResetToolStripMenuItem
            // 
            this.machineResetToolStripMenuItem.AutoSize = false;
            this.machineResetToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("machineResetToolStripMenuItem.Image")));
            this.machineResetToolStripMenuItem.Name = "machineResetToolStripMenuItem";
            this.machineResetToolStripMenuItem.Size = new System.Drawing.Size(250, 26);
            this.machineResetToolStripMenuItem.Text = "MACHINE RESET           ";
            this.machineResetToolStripMenuItem.Click += new System.EventHandler(this.machineResetToolStripMenuItem_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "media_playback_start_32px_539991_easyicon.net.png");
            this.imageList1.Images.SetKeyName(1, "Stop_red_32px.png");
            this.imageList1.Images.SetKeyName(2, "back_33.208633093525px_1179394_easyicon.net.png");
            // 
            // FormOperator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Yellow;
            this.ClientSize = new System.Drawing.Size(208, 38);
            this.Controls.Add(this.btnMacOp);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormOperator";
            this.Text = "FormOperator";
            this.Load += new System.EventHandler(this.FormOperator_Load);
            this.MenuStripMacOp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private RibbonStyle.RibbonMenuButton btnMacOp;
        private System.Windows.Forms.ContextMenuStrip MenuStripMacOp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAlarmClear;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem machineResetToolStripMenuItem;
    }
}