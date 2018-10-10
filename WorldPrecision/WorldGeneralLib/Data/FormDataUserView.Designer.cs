namespace WorldGeneralLib.Data
{
    partial class FormDataUserView
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDataUserView));
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.dataItemName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataItemType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataItemValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataItemRemark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain = new BSE.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolBarBtnSave = new System.Windows.Forms.ToolStripButton();
            this.toorBtnAdd = new System.Windows.Forms.ToolStripButton();
            this.toolBarBtnRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripBtnExport = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.panelMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.AllowUserToAddRows = false;
            this.dataGridViewItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.dataGridViewItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewItems.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Tahoma", 8.25F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataItemName,
            this.dataItemType,
            this.dataItemValue,
            this.dataItemRemark});
            this.dataGridViewItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewItems.Location = new System.Drawing.Point(1, 55);
            this.dataGridViewItems.MultiSelect = false;
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.RowHeadersVisible = false;
            this.dataGridViewItems.RowTemplate.Height = 23;
            this.dataGridViewItems.Size = new System.Drawing.Size(963, 582);
            this.dataGridViewItems.TabIndex = 0;
            // 
            // dataItemName
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataItemName.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataItemName.HeaderText = "Name";
            this.dataItemName.Name = "dataItemName";
            this.dataItemName.ReadOnly = true;
            this.dataItemName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataItemType
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataItemType.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataItemType.HeaderText = "Type";
            this.dataItemType.Name = "dataItemType";
            this.dataItemType.ReadOnly = true;
            this.dataItemType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataItemValue
            // 
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Blue;
            this.dataItemValue.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataItemValue.HeaderText = "Value";
            this.dataItemValue.Name = "dataItemValue";
            this.dataItemValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataItemRemark
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Blue;
            this.dataItemRemark.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataItemRemark.HeaderText = "Remark";
            this.dataItemRemark.Name = "dataItemRemark";
            this.dataItemRemark.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.Transparent;
            this.panelMain.BorderColor = System.Drawing.Color.Empty;
            this.panelMain.CaptionFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelMain.CaptionForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelMain.CloseIconForeColor = System.Drawing.Color.Empty;
            this.panelMain.CollapsedCaptionForeColor = System.Drawing.Color.Empty;
            this.panelMain.ColorCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.panelMain.ColorCaptionGradientEnd = System.Drawing.SystemColors.ButtonShadow;
            this.panelMain.ColorCaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.panelMain.ColorContentPanelGradientBegin = System.Drawing.Color.Empty;
            this.panelMain.ColorContentPanelGradientEnd = System.Drawing.Color.Empty;
            this.panelMain.Controls.Add(this.dataGridViewItems);
            this.panelMain.Controls.Add(this.toolStrip1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panelMain.Image = null;
            this.panelMain.InnerBorderColor = System.Drawing.Color.Empty;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.PanelStyle = BSE.Windows.Forms.PanelStyle.Office2007;
            this.panelMain.ShowTransparentBackground = false;
            this.panelMain.ShowXPanderPanelProfessionalStyle = true;
            this.panelMain.Size = new System.Drawing.Size(965, 638);
            this.panelMain.TabIndex = 18;
            this.panelMain.Text = "Configuratoion";
            this.panelMain.SizeChanged += new System.EventHandler(this.panelMain_SizeChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarBtnSave,
            this.toorBtnAdd,
            this.toolBarBtnRemove,
            this.toolStripBtnExport});
            this.toolStrip1.Location = new System.Drawing.Point(1, 26);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(963, 29);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip3";
            // 
            // toolBarBtnSave
            // 
            this.toolBarBtnSave.Image = ((System.Drawing.Image)(resources.GetObject("toolBarBtnSave.Image")));
            this.toolBarBtnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarBtnSave.Name = "toolBarBtnSave";
            this.toolBarBtnSave.Size = new System.Drawing.Size(67, 26);
            this.toolBarBtnSave.Text = "Save  ";
            this.toolBarBtnSave.Click += new System.EventHandler(this.toolBarBtnSave_Click);
            // 
            // toorBtnAdd
            // 
            this.toorBtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("toorBtnAdd.Image")));
            this.toorBtnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toorBtnAdd.Name = "toorBtnAdd";
            this.toorBtnAdd.Size = new System.Drawing.Size(64, 26);
            this.toorBtnAdd.Text = "Add  ";
            this.toorBtnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toorBtnAdd.ToolTipText = "Add new item";
            this.toorBtnAdd.Visible = false;
            // 
            // toolBarBtnRemove
            // 
            this.toolBarBtnRemove.Image = ((System.Drawing.Image)(resources.GetObject("toolBarBtnRemove.Image")));
            this.toolBarBtnRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarBtnRemove.Name = "toolBarBtnRemove";
            this.toolBarBtnRemove.Size = new System.Drawing.Size(87, 26);
            this.toolBarBtnRemove.Text = "Remove  ";
            this.toolBarBtnRemove.ToolTipText = "Remove item";
            this.toolBarBtnRemove.Visible = false;
            // 
            // toolStripBtnExport
            // 
            this.toolStripBtnExport.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtnExport.Image")));
            this.toolStripBtnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtnExport.Name = "toolStripBtnExport";
            this.toolStripBtnExport.Size = new System.Drawing.Size(78, 26);
            this.toolStripBtnExport.Text = "Export  ";
            this.toolStripBtnExport.ToolTipText = "Export as cs file";
            this.toolStripBtnExport.Visible = false;
            // 
            // FormDataUserView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 638);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormDataUserView";
            this.Text = "FormDataUserView";
            this.Load += new System.EventHandler(this.FormDataUserView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private BSE.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolBarBtnSave;
        private System.Windows.Forms.ToolStripButton toorBtnAdd;
        private System.Windows.Forms.ToolStripButton toolBarBtnRemove;
        private System.Windows.Forms.ToolStripButton toolStripBtnExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataItemName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataItemType;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataItemValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataItemRemark;
    }
}