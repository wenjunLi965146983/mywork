using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Data
{
    public partial class FormDataUserView : Form
    {
        public FormDataUserView()
        {
            InitializeComponent();
        }

        private string _strCurrGroupName = string.Empty;
        private void SetDataGridViewStyle()
        {
            dataGridViewItems.Columns[0].Width = panelMain.Width * 2 / 10;
            dataGridViewItems.Columns[1].Width = panelMain.Width * 1 / 10;
            dataGridViewItems.Columns[2].Width = panelMain.Width * 2 / 10;
            dataGridViewItems.Columns[3].Width = panelMain.Width * 5 / 10 - 5;
        }
        public void ShowDataItems(string strGroupName)
        {
            dataGridViewItems.Rows.Clear();
            if (!DataManage.docData.dicDataGroup.ContainsKey(strGroupName))
            {
                _strCurrGroupName = string.Empty;
                return;
            }
            _strCurrGroupName = strGroupName;
            try
            {
                dataGridViewItems.Rows.Clear();
                foreach (DataItem item in DataManage.docData.dicDataGroup[strGroupName].listDataItem)
                {
                    if (!item.bVisible)
                        continue;
                    DataGridViewRow rowAdd = new DataGridViewRow();
                    DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                    NameCell.Value = item.strItemName;

                    DataGridViewTextBoxCell DataTypeCell = new DataGridViewTextBoxCell();
                    DataTypeCell.Value = item.dataType;

                    DataTypeCell.Value = item.dataType;
                    DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                    ValueCell.Value = item.objValue;

                    DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                    RemarkCell.Value = item.strItemRemark;

                    rowAdd.Cells.Add(NameCell);
                    rowAdd.Cells.Add(DataTypeCell);
                    rowAdd.Cells.Add(ValueCell);
                    rowAdd.Cells.Add(RemarkCell);
                    dataGridViewItems.Rows.Add(rowAdd);
                }
            }
            catch
            {
            }
        }
        private void dataGridViewItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewItems.EndEdit();
                string strItemName = (string)dataGridViewItems.Rows[e.RowIndex].Cells[0].Value;
                if (string.IsNullOrEmpty(strItemName))
                {
                    return;
                }

                //Remove old item form dicDataItem
                DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Remove(strItemName);

                //Update values
                foreach(DataItem item in DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem)
                {
                    if(item.strItemName.Equals(strItemName))
                    {
                        item.objValue = dataGridViewItems.Rows[e.RowIndex].Cells[2].Value.ToString();
                        item.strItemRemark = dataGridViewItems.Rows[e.RowIndex].Cells[3].Value.ToString();
                        //Add new item to dicDataItem
                        DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Add(strItemName, item);
                        dataGridViewItems.Rows[e.RowIndex].Selected = true;
                        break;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FormDataUserView_Load(object sender, EventArgs e)
        {
            this.dataGridViewItems.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItem_CellValueChanged);
        }

        private void panelMain_SizeChanged(object sender, EventArgs e)
        {
            SetDataGridViewStyle();
        }
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataManage.docData.SaveDataDoc();
                MessageBox.Show("Successfuly saved.");
            }
            catch (Exception)
            {
            }
        }
    }
}
