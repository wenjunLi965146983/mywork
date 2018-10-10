namespace WorldGeneralLib.PLC
{
    partial class PLCDriverForm
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
            this.buttonAddDriver = new System.Windows.Forms.Button();
            this.buttonRemoveDriver = new System.Windows.Forms.Button();
            this.buttonDriverSave = new System.Windows.Forms.Button();
            this.dataGridViewDriver = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.propertyGridDriver = new System.Windows.Forms.PropertyGrid();
            this.textBoxDriverName = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDriver)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonAddDriver
            // 
            this.buttonAddDriver.Location = new System.Drawing.Point(2, 287);
            this.buttonAddDriver.Name = "buttonAddDriver";
            this.buttonAddDriver.Size = new System.Drawing.Size(63, 36);
            this.buttonAddDriver.TabIndex = 12;
            this.buttonAddDriver.Text = "Add";
            this.buttonAddDriver.UseVisualStyleBackColor = true;
            this.buttonAddDriver.Click += new System.EventHandler(this.buttonAddDriver_Click);
            // 
            // buttonRemoveDriver
            // 
            this.buttonRemoveDriver.Location = new System.Drawing.Point(2, 327);
            this.buttonRemoveDriver.Name = "buttonRemoveDriver";
            this.buttonRemoveDriver.Size = new System.Drawing.Size(63, 36);
            this.buttonRemoveDriver.TabIndex = 10;
            this.buttonRemoveDriver.Text = "Remove";
            this.buttonRemoveDriver.UseVisualStyleBackColor = true;
            this.buttonRemoveDriver.Click += new System.EventHandler(this.buttonRemoveDriver_Click);
            // 
            // buttonDriverSave
            // 
            this.buttonDriverSave.Location = new System.Drawing.Point(68, 326);
            this.buttonDriverSave.Name = "buttonDriverSave";
            this.buttonDriverSave.Size = new System.Drawing.Size(162, 36);
            this.buttonDriverSave.TabIndex = 9;
            this.buttonDriverSave.Text = "Save";
            this.buttonDriverSave.UseVisualStyleBackColor = true;
            this.buttonDriverSave.Click += new System.EventHandler(this.buttonDriverSave_Click);
            // 
            // dataGridViewDriver
            // 
            this.dataGridViewDriver.AllowUserToAddRows = false;
            this.dataGridViewDriver.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDriver.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewDriver.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDriver.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            this.dataGridViewDriver.Location = new System.Drawing.Point(5, 3);
            this.dataGridViewDriver.MultiSelect = false;
            this.dataGridViewDriver.Name = "dataGridViewDriver";
            this.dataGridViewDriver.RowHeadersWidth = 20;
            this.dataGridViewDriver.RowTemplate.Height = 23;
            this.dataGridViewDriver.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewDriver.Size = new System.Drawing.Size(225, 274);
            this.dataGridViewDriver.TabIndex = 8;
            this.dataGridViewDriver.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDriver_CellClick);
            this.dataGridViewDriver.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDriver_CellContentClick);
            // 
            // Column1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 180;
            // 
            // propertyGridDriver
            // 
            this.propertyGridDriver.Location = new System.Drawing.Point(244, 5);
            this.propertyGridDriver.Name = "propertyGridDriver";
            this.propertyGridDriver.Size = new System.Drawing.Size(259, 357);
            this.propertyGridDriver.TabIndex = 13;
            // 
            // textBoxDriverName
            // 
            this.textBoxDriverName.Location = new System.Drawing.Point(71, 292);
            this.textBoxDriverName.Name = "textBoxDriverName";
            this.textBoxDriverName.Size = new System.Drawing.Size(158, 23);
            this.textBoxDriverName.TabIndex = 14;
            // 
            // PLCDriverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 369);
            this.Controls.Add(this.textBoxDriverName);
            this.Controls.Add(this.propertyGridDriver);
            this.Controls.Add(this.buttonAddDriver);
            this.Controls.Add(this.buttonRemoveDriver);
            this.Controls.Add(this.buttonDriverSave);
            this.Controls.Add(this.dataGridViewDriver);
            this.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "PLCDriverForm";
            this.Text = "PLCDriverList";
            this.Load += new System.EventHandler(this.PLCDriverForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDriver)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonAddDriver;
        private System.Windows.Forms.Button buttonRemoveDriver;
        private System.Windows.Forms.Button buttonDriverSave;
        private System.Windows.Forms.DataGridView dataGridViewDriver;
        private System.Windows.Forms.PropertyGrid propertyGridDriver;
        private System.Windows.Forms.TextBox textBoxDriverName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
    }
}