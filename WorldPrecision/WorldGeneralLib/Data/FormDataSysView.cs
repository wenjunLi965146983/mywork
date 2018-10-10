using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Data
{
    public partial class FormDataSysView : Form
    {
        private string _strCurrGroupName = "";
        public FormDataSysView()
        {
            InitializeComponent();
        }

        public void ShowDataItems(string strGroupName)
        {
            dataGridViewItem.Rows.Clear();
            if (!DataManage.docData.dicDataGroup.ContainsKey(strGroupName))
            {
                _strCurrGroupName = string.Empty;
                return;
            }       
            _strCurrGroupName = strGroupName;
            try
            {
                if (this.InvokeRequired)
                {
                    Action action = () =>
                     {
                         foreach (DataItem item in DataManage.docData.dicDataGroup[strGroupName].listDataItem)
                         {
                             DataGridViewRow rowAdd = new DataGridViewRow();

                             DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                             DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                             DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                             DataGridViewCheckBoxCell chkCell = new DataGridViewCheckBoxCell();
                             DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();

                             DataTypeCell.DataSource = Enum.GetNames(typeof(DataType));
                             DataTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                             NameCell.Value = item.strItemName;
                             DataTypeCell.Value = item.dataType.ToString();
                             ValueCell.Value = item.objValue;
                             chkCell.Value = item.bVisible;
                             RemarkCell.Value = item.strItemRemark;

                             rowAdd.Cells.Add(NameCell);
                             rowAdd.Cells.Add(DataTypeCell);
                             rowAdd.Cells.Add(ValueCell);
                             rowAdd.Cells.Add(chkCell);
                             rowAdd.Cells.Add(RemarkCell);
                             dataGridViewItem.Rows.Add(rowAdd);
                         }
                     };
                    this.Invoke(action);
                }
                else
                {
                    foreach (DataItem item in DataManage.docData.dicDataGroup[strGroupName].listDataItem)
                    {
                        DataGridViewRow rowAdd = new DataGridViewRow();

                        DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                        DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                        DataGridViewCheckBoxCell chkCell = new DataGridViewCheckBoxCell();
                        DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();

                        DataTypeCell.DataSource = Enum.GetNames(typeof(DataType));
                        DataTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                        NameCell.Value = item.strItemName;
                        DataTypeCell.Value = item.dataType.ToString();
                        ValueCell.Value = item.objValue;
                        chkCell.Value = item.bVisible;
                        RemarkCell.Value = item.strItemRemark;

                        rowAdd.Cells.Add(NameCell);
                        rowAdd.Cells.Add(DataTypeCell);
                        rowAdd.Cells.Add(ValueCell);
                        rowAdd.Cells.Add(chkCell);
                        rowAdd.Cells.Add(RemarkCell);
                        dataGridViewItem.Rows.Add(rowAdd);
                    }
                }
            }
            catch 
            {
            }
        }
        private string GetNewItemName(string strGroupName)
        {
            try
            {
                if (!DataManage.docData.dicDataGroup.ContainsKey(strGroupName))
                {
                    return string.Empty;
                }

                string strName = "Item_";
                int iTemp = 1;
                while(true)
                {
                    if (!DataManage.docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strName + iTemp.ToString()))
                    {
                        return strName + iTemp.ToString();
                    }
                    iTemp++;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        private void dataGridViewItem_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridViewItem.Columns[0].Width = dataGridViewItem.Width * 2 / 10;
                dataGridViewItem.Columns[1].Width = dataGridViewItem.Width * 1 / 10;
                dataGridViewItem.Columns[2].Width = dataGridViewItem.Width * 1 / 10;
                dataGridViewItem.Columns[3].Width = dataGridViewItem.Width * 1 / 10;
                dataGridViewItem.Columns[4].Width = dataGridViewItem.Width * 5 / 10 - 5;
            }
            catch (Exception)
            {
            }
        }

        private void toorBtnAdd_Click(object sender, EventArgs e)
        {
            if(!DataManage.docData.dicDataGroup.ContainsKey(_strCurrGroupName))
            {
                return;
            }

            try
            {
                string strNewItemName = GetNewItemName(_strCurrGroupName);
                if(string.IsNullOrEmpty(strNewItemName))
                { 
                    MessageBox.Show("添加失败", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                if (DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.ContainsKey(strNewItemName))
                {
                    MessageBox.Show("已添加相同的项", "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                DataItem data = new DataItem();
                data.strItemName = strNewItemName;
                DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Add(data.strItemName, data);
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem.Add(data);

                DataGridViewRow rowAdd = new DataGridViewRow();

                DataGridViewTextBoxCell NameCell = new DataGridViewTextBoxCell();
                NameCell.Value = data.strItemName;

                data.dataType = DataType.String;
                DataGridViewComboBoxCell DataTypeCell = new DataGridViewComboBoxCell();
                DataTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                DataTypeCell.DataSource = Enum.GetNames(typeof(DataType));
                DataTypeCell.ValueType = typeof(DataType);
                DataTypeCell.Value = data.dataType;

                data.objValue = "";
                DataGridViewTextBoxCell ValueCell = new DataGridViewTextBoxCell();
                ValueCell.Value = data.objValue.ToString();

                data.bVisible = true;
                DataGridViewCheckBoxCell chkVisible = new DataGridViewCheckBoxCell();
                chkVisible.Value = data.bVisible;

                DataGridViewTextBoxCell RemarkCell = new DataGridViewTextBoxCell();
                RemarkCell.Value = data.strItemRemark;

                rowAdd.Cells.Add(NameCell);
                rowAdd.Cells.Add(DataTypeCell);
                rowAdd.Cells.Add(ValueCell);
                rowAdd.Cells.Add(chkVisible);
                rowAdd.Cells.Add(RemarkCell);
                dataGridViewItem.Rows.Add(rowAdd);
            }
            catch
            {

            }
        }

        private void toolBarBtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewItem.CurrentCell.RowIndex >= 0)
                {
                    if(string.IsNullOrEmpty(_strCurrGroupName))
                    {
                        return;
                    }
                    if(!DataManage.docData.dicDataGroup.ContainsKey(_strCurrGroupName))
                    {
                        return;
                    }

                    DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Remove(dataGridViewItem.Rows[dataGridViewItem.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem.RemoveAt(dataGridViewItem.CurrentCell.RowIndex);
                    ShowDataItems(_strCurrGroupName);                    
                }
            }
            catch //(Exception)
            {
            }
        }

        private void FormDataItems_Load(object sender, EventArgs e)
        {
            dataGridViewItem.ClearSelection();
            this.dataGridViewItem.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItem_CellValueChanged);
        }

        private void dataGridViewItem_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridViewItem.EndEdit();
                if(e.RowIndex >= DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem.Count)
                {
                    return;
                }
                string strOldName = DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName;
                string strItemName = (string)dataGridViewItem.Rows[e.RowIndex].Cells[0].Value;
                if (string.IsNullOrEmpty(strItemName))
                {
                    MessageBox.Show("名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    dataGridViewItem.Rows[e.RowIndex].Cells[0].Value = DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName;
                    return;
                }

                if (strItemName.Contains(" "))
                {
                    MessageBox.Show("名称包含非法符号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    dataGridViewItem.Rows[e.RowIndex].Cells[0].Value = DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName;
                    return;
                }
                if (DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.ContainsKey(strItemName) && DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName != strItemName)
                {
                    MessageBox.Show("已存在名为 " + strItemName + " 的数据项！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    dataGridViewItem.Rows[e.RowIndex].Cells[0].Value = DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName;
                    return;
                }

                //Remove old item form dicDataItem
                DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Remove(strOldName);

                //Update values
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemName = strItemName;
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].dataType = (DataType)Enum.Parse(typeof(DataType), dataGridViewItem["DataItemType", e.RowIndex].Value.ToString().Trim(), false);
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].objValue = dataGridViewItem.Rows[e.RowIndex].Cells[2].Value.ToString();
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].bVisible = Convert.ToBoolean(dataGridViewItem.Rows[e.RowIndex].Cells[3].Value);
                DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex].strItemRemark = dataGridViewItem.Rows[e.RowIndex].Cells[4].Value.ToString();

                //Add new item to dicDataItem
                DataManage.docData.dicDataGroup[_strCurrGroupName].dicDataItem.Add(strItemName, DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem[e.RowIndex]);
                dataGridViewItem.Rows[e.RowIndex].Selected = true;
            }
            catch (Exception)
            {
                ShowDataItems(_strCurrGroupName);
            }
        }

        private void dataGridViewItem_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        #region Export to cs file
        private string GetContent(string strItemName)
        {
            string strContent = "";
            try
            {
                if (string.IsNullOrEmpty(strItemName))
                {
                    throw new Exception("名称不能为空!");
                }
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.Data.NameItems\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + strItemName + "\r\n";
                strContent += "   {\r\n";

                foreach (DataItem item in DataManage.docData.dicDataGroup[_strCurrGroupName].listDataItem)
                {
                    strContent += "      public static string ";
                    strContent += item.strItemName;
                    strContent += " = ";
                    strContent += "\"" + item.strItemName + "\"";
                    strContent += " ;\r\n";
                }

                strContent += "   }\r\n";
                strContent += "}\r\n";
                return strContent;
            }
            catch (Exception)
            {
            }

            return string.Empty;
        }
        private bool WriteToFileAsCS()
        {
            if(string.IsNullOrEmpty(_strCurrGroupName))
            {
                return false;
            }
            if(!DataManage.docData.dicDataGroup.ContainsKey(_strCurrGroupName))
            {
                return false;
            }
            string strContent = GetContent("DataItem"+ _strCurrGroupName);
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }

            try
            {
                string strPath = Application.StartupPath + "\\Export\\Data\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                File.WriteAllText(strPath + "DataItem" + _strCurrGroupName + ".cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        private void toolStripBtnExport_Click(object sender, EventArgs e)
        {
            if (WriteToFileAsCS())
            {
                string strFilePath = Application.StartupPath + "\\Export\\Data\\";
                DialogResult dRet;
                dRet = MessageBox.Show("导出成功！\r\n是否打开文件夹？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information , MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                if (DialogResult.Yes == dRet)
                {
                    if (Directory.Exists(strFilePath))
                    {
                        try
                        {
                            System.Diagnostics.Process.Start(strFilePath);
                        }
                        catch (Exception)
                        {
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("导出失败！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        #endregion

        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DataManage.docData.SaveDataDoc();
                MessageBox.Show("Successfuly saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
            catch (Exception)
            {
            }
        }
    }
}
