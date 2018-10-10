namespace WorldGeneralLib.Forms
{
    partial class FormUserParam
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Table");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("User Parameter");
            this.panelMain = new System.Windows.Forms.Panel();
            this.splitter1 = new BSE.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new BSE.Windows.Forms.Panel();
            this.xPanderPanelList2 = new BSE.Windows.Forms.XPanderPanelList();
            this.xPanderPanelTable = new BSE.Windows.Forms.XPanderPanel();
            this.treeViewTable = new System.Windows.Forms.TreeView();
            this.xPanderPanelParameter = new BSE.Windows.Forms.XPanderPanel();
            this.treeViewDataGroup = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xPanderPanelList2.SuspendLayout();
            this.xPanderPanelTable.SuspendLayout();
            this.xPanderPanelParameter.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(289, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(809, 643);
            this.panelMain.TabIndex = 5;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(286, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 643);
            this.splitter1.TabIndex = 4;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panelMain);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 643);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.BorderColor = System.Drawing.Color.Empty;
            this.panel3.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.CaptionForeColor = System.Drawing.Color.DarkSlateGray;
            this.panel3.CloseIconForeColor = System.Drawing.Color.Empty;
            this.panel3.CollapsedCaptionForeColor = System.Drawing.Color.Red;
            this.panel3.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panel3.ColorCaptionGradientEnd = System.Drawing.Color.CornflowerBlue;
            this.panel3.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panel3.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panel3.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panel3.ColorScheme = BSE.Windows.Forms.ColorScheme.Custom;
            this.panel3.Controls.Add(this.xPanderPanelList2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel3.Image = null;
            this.panel3.InnerBorderColor = System.Drawing.Color.Empty;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panel3.ShowExpandIcon = true;
            this.panel3.ShowTransparentBackground = false;
            this.panel3.Size = new System.Drawing.Size(286, 643);
            this.panel3.TabIndex = 3;
            this.panel3.Text = "User Parameter";
            // 
            // xPanderPanelList2
            // 
            this.xPanderPanelList2.CaptionHeight = 20;
            this.xPanderPanelList2.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Flat;
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelTable);
            this.xPanderPanelList2.Controls.Add(this.xPanderPanelParameter);
            this.xPanderPanelList2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList2.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList2.Location = new System.Drawing.Point(1, 26);
            this.xPanderPanelList2.Name = "xPanderPanelList2";
            this.xPanderPanelList2.ShowCloseIcon = true;
            this.xPanderPanelList2.ShowExpandIcon = true;
            this.xPanderPanelList2.Size = new System.Drawing.Size(284, 616);
            this.xPanderPanelList2.TabIndex = 0;
            this.xPanderPanelList2.Text = "xPanderPanelList2";
            // 
            // xPanderPanelTable
            // 
            this.xPanderPanelTable.BackColor = System.Drawing.Color.Transparent;
            this.xPanderPanelTable.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelTable.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelTable.CaptionHeight = 20;
            this.xPanderPanelTable.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelTable.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelTable.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelTable.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelTable.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelTable.Controls.Add(this.treeViewTable);
            this.xPanderPanelTable.Expand = true;
            this.xPanderPanelTable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelTable.Image = null;
            this.xPanderPanelTable.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelTable.IsClosable = false;
            this.xPanderPanelTable.Name = "xPanderPanelTable";
            this.xPanderPanelTable.Size = new System.Drawing.Size(284, 596);
            this.xPanderPanelTable.TabIndex = 1;
            this.xPanderPanelTable.Text = "Table Configuration";
            this.xPanderPanelTable.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelTable_ExpandClick);
            // 
            // treeViewTable
            // 
            this.treeViewTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewTable.Location = new System.Drawing.Point(0, 20);
            this.treeViewTable.Name = "treeViewTable";
            treeNode1.Name = "tables";
            treeNode1.Text = "Table";
            this.treeViewTable.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeViewTable.Size = new System.Drawing.Size(284, 576);
            this.treeViewTable.TabIndex = 0;
            this.treeViewTable.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTable_AfterSelect);
            // 
            // xPanderPanelParameter
            // 
            this.xPanderPanelParameter.BackColor = System.Drawing.Color.Transparent;
            this.xPanderPanelParameter.BorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanelParameter.CaptionForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelParameter.CaptionHeight = 20;
            this.xPanderPanelParameter.CloseIconForeColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.xPanderPanelParameter.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.xPanderPanelParameter.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.xPanderPanelParameter.ColorFlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanelParameter.ColorFlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanelParameter.Controls.Add(this.treeViewDataGroup);
            this.xPanderPanelParameter.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.xPanderPanelParameter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanelParameter.Image = null;
            this.xPanderPanelParameter.InnerBorderColor = System.Drawing.Color.Empty;
            this.xPanderPanelParameter.IsClosable = false;
            this.xPanderPanelParameter.Name = "xPanderPanelParameter";
            this.xPanderPanelParameter.Size = new System.Drawing.Size(284, 20);
            this.xPanderPanelParameter.TabIndex = 3;
            this.xPanderPanelParameter.Text = "Parameter Configuration";
            this.xPanderPanelParameter.ExpandClick += new System.EventHandler<System.EventArgs>(this.xPanderPanelParameter_ExpandClick);
            // 
            // treeViewDataGroup
            // 
            this.treeViewDataGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewDataGroup.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.treeViewDataGroup.Location = new System.Drawing.Point(0, 20);
            this.treeViewDataGroup.Name = "treeViewDataGroup";
            treeNode2.Name = "DataGroup";
            treeNode2.Text = "User Parameter";
            this.treeViewDataGroup.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.treeViewDataGroup.Size = new System.Drawing.Size(284, 0);
            this.treeViewDataGroup.TabIndex = 0;
            this.treeViewDataGroup.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewDataGroup_AfterSelect);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(121, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // FormUserParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1098, 643);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormUserParam";
            this.Text = "FormUserParam";
            this.Load += new System.EventHandler(this.FormUserParam_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.xPanderPanelList2.ResumeLayout(false);
            this.xPanderPanelTable.ResumeLayout(false);
            this.xPanderPanelParameter.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelMain;
        private BSE.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panel1;
        private BSE.Windows.Forms.Panel panel3;
        private BSE.Windows.Forms.XPanderPanelList xPanderPanelList2;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelParameter;
        private System.Windows.Forms.TreeView treeViewDataGroup;
        private BSE.Windows.Forms.XPanderPanel xPanderPanelTable;
        private System.Windows.Forms.TreeView treeViewTable;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
    }
}