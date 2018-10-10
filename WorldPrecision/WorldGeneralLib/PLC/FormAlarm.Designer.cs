﻿namespace WorldGeneralLib.PLC
{
    partial class FormAlarm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarm));
            this.listViewAlarmCur = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listViewAlarmHis = new System.Windows.Forms.ListView();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxAlramOff = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listViewAlarmCur
            // 
            this.listViewAlarmCur.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAlarmCur.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewAlarmCur.LargeImageList = this.imageList1;
            this.listViewAlarmCur.Location = new System.Drawing.Point(3, 3);
            this.listViewAlarmCur.Name = "listViewAlarmCur";
            this.listViewAlarmCur.Size = new System.Drawing.Size(721, 348);
            this.listViewAlarmCur.SmallImageList = this.imageList1;
            this.listViewAlarmCur.TabIndex = 2;
            this.listViewAlarmCur.UseCompatibleStateImageBehavior = false;
            this.listViewAlarmCur.View = System.Windows.Forms.View.Details;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "stop.ico");
            this.imageList1.Images.SetKeyName(1, "warn.ico");
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.Location = new System.Drawing.Point(41, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(735, 382);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listViewAlarmCur);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(727, 354);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "现在报警";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listViewAlarmHis);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(727, 354);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "历史报警";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listViewAlarmHis
            // 
            this.listViewAlarmHis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.listViewAlarmHis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewAlarmHis.Font = new System.Drawing.Font("楷体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listViewAlarmHis.Location = new System.Drawing.Point(3, 3);
            this.listViewAlarmHis.Name = "listViewAlarmHis";
            this.listViewAlarmHis.Size = new System.Drawing.Size(721, 348);
            this.listViewAlarmHis.SmallImageList = this.imageList1;
            this.listViewAlarmHis.TabIndex = 3;
            this.listViewAlarmHis.UseCompatibleStateImageBehavior = false;
            this.listViewAlarmHis.View = System.Windows.Forms.View.Details;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(325, 401);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 52);
            this.button1.TabIndex = 5;
            this.button1.Text = "ReSet";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxAlramOff
            // 
            this.checkBoxAlramOff.AutoSize = true;
            this.checkBoxAlramOff.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxAlramOff.Location = new System.Drawing.Point(676, 419);
            this.checkBoxAlramOff.Name = "checkBoxAlramOff";
            this.checkBoxAlramOff.Size = new System.Drawing.Size(96, 18);
            this.checkBoxAlramOff.TabIndex = 6;
            this.checkBoxAlramOff.Text = "屏蔽蜂鸣器";
            this.checkBoxAlramOff.UseVisualStyleBackColor = true;
            this.checkBoxAlramOff.CheckedChanged += new System.EventHandler(this.checkBoxAlramOff_CheckedChanged);
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(806, 456);
            this.Controls.Add(this.checkBoxAlramOff);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormAlarm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormAlarm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FormAlarm_MouseUp);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListView listViewAlarmCur;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        public System.Windows.Forms.ListView listViewAlarmHis;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxAlramOff;
    }
}