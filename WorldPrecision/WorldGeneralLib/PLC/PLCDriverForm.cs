using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WorldGeneralLib.PLC
{
    public partial class PLCDriverForm : Form
    {
        public delegate void PLCSelect(object sender, string PLCName);
        public event PLCSelect PLCSelectEvent;
        public string[] strPLCName;
        public PLCDriverForm()
        {
            InitializeComponent();
            int i = 0;
            if (PLCDriverManageClass.docPlc == null)
                return;
            foreach (KeyValuePair<string, PLCDriverInfo> posInfoPair in PLCDriverManageClass.docPlc.dicPlcInfo)
            {

                i = dataGridViewDriver.Rows.Add();
                dataGridViewDriver.Rows[i].Cells[0].Value = posInfoPair.Key;
            }
            if (dataGridViewDriver.Rows.Count > 0)
            {
                strPLCName = new string[dataGridViewDriver.Rows.Count];
                propertyGridDriver.SelectedObject = PLCDriverManageClass.docPlc.dicPlcInfo[dataGridViewDriver.Rows[0].Cells[0].Value.ToString()];
                for (int j = 0; j < dataGridViewDriver.Rows.Count; j++)
                {
                    strPLCName[j] = dataGridViewDriver.Rows[j].Cells[0].Value.ToString();
                }
            }
        }


        private void buttonAddDriver_Click(object sender, EventArgs e)
        {
            if (PLCDriverManageClass.docPlc.dicPlcInfo.Keys.Contains(textBoxDriverName.Text))
            {
                MessageBox.Show("名称重复");
                return;
            }

            PLCDriverManageClass.docPlc.dicPlcInfo.Add(textBoxDriverName.Text, new PLCDriverInfo());
            int j = 0;
            j = dataGridViewDriver.Rows.Add();
            dataGridViewDriver.Rows[j].Cells[0].Value = textBoxDriverName.Text;

        }

        private void buttonRemoveDriver_Click(object sender, EventArgs e)
        {
            if (dataGridViewDriver.SelectedRows.Count > 0)
            {
                PLCDriverManageClass.docPlc.dicPlcInfo.Remove(dataGridViewDriver.SelectedRows[0].Cells[0].Value.ToString());
                dataGridViewDriver.Rows.Remove(dataGridViewDriver.SelectedRows[0]);

            }
            else
            {
                MessageBox.Show("没有选中行！");
            }
        }

        private void buttonDriverSave_Click(object sender, EventArgs e)
        {
            PLCDriverManageClass.docPlc.SaveDoc();
        }

        private void PLCDriverForm_Load(object sender, EventArgs e)
        {

        }

        private void dataGridViewDriver_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewDriver.SelectedRows.Count > 0)
            {
                propertyGridDriver.SelectedObject = PLCDriverManageClass.docPlc.dicPlcInfo[dataGridViewDriver.SelectedRows[0].Cells[0].Value.ToString()];
                PLCSelectEvent(this, dataGridViewDriver.SelectedRows[0].Cells[0].Value.ToString());
            }
        }

        private void dataGridViewDriver_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
