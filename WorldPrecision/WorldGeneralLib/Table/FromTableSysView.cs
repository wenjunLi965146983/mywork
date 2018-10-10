using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.Table
{
    public partial class FormTableSysView : Form
    {
        private string _strCurrTable = string.Empty;

        public FormTableSysView()
        {
            InitializeComponent();
        }

        #region Load
        private void ShowPosItems()
        {
            if (this.InvokeRequired)
            {
                Action action = () =>
                {
                    try
                    {
                        dataGridView1.Rows.Clear();
                        foreach (TablePosItem item in TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                            DataGridViewComboBoxCell elmentCell = new DataGridViewComboBoxCell();
                            DataGridViewTextBoxCell posCellX = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellY = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellZ = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellU = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellA = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellB = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellC = new DataGridViewTextBoxCell();
                            DataGridViewTextBoxCell posCellD = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell activeCellX = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellY = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellZ = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellU = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellA = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellB = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellC = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell activeCellD = new DataGridViewCheckBoxCell();
                            DataGridViewCheckBoxCell moveRelCell = new DataGridViewCheckBoxCell();
                            DataGridViewComboBoxCell moveModeCell = new DataGridViewComboBoxCell();
                            DataGridViewTextBoxCell remarkCell = new DataGridViewTextBoxCell();

                            nameCell.Value = item.Name;
                            isUseCell.Value = item.IsUse;
                            posCellX.Value = item.PosX.ToString("0.000"); 
                            posCellY.Value = item.PosY.ToString("0.000"); 
                            posCellZ.Value = item.PosZ.ToString("0.000"); 
                            posCellU.Value = item.PosU.ToString("0.000"); 
                            posCellA.Value = item.PosA.ToString("0.000"); 
                            posCellB.Value = item.PosB.ToString("0.000"); 
                            posCellC.Value = item.PosC.ToString("0.000"); 
                            posCellD.Value = item.PosD.ToString("0.000"); 
                            activeCellX.Value = item.ActiveX;
                            activeCellY.Value = item.ActiveY;
                            activeCellZ.Value = item.ActiveZ;
                            activeCellU.Value = item.ActiveU;
                            activeCellA.Value = item.ActiveA;
                            activeCellB.Value = item.ActiveB;
                            activeCellC.Value = item.ActiveC;
                            activeCellD.Value = item.ActiveD;
                            moveRelCell.Value = item.MoveRel;
                            remarkCell.Value = item.Remarks;

                            moveModeCell.DataSource = Enum.GetNames(typeof(PointMoveMode));
                            moveModeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            moveModeCell.Value = item.MoveMode.ToString();

                            elmentCell.DataSource = Enum.GetNames(typeof(Element));
                            elmentCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            elmentCell.Value = item.TrgElement.ToString();

                            row.Cells.Add(nameCell);
                            row.Cells.Add(isUseCell);
                            row.Cells.Add(elmentCell);
                            row.Cells.Add(posCellX);
                            row.Cells.Add(posCellY);
                            row.Cells.Add(posCellZ);
                            row.Cells.Add(posCellU);
                            row.Cells.Add(posCellA);
                            row.Cells.Add(posCellB);
                            row.Cells.Add(posCellC);
                            row.Cells.Add(posCellD);

                            row.Cells.Add(activeCellX);
                            row.Cells.Add(activeCellY);
                            row.Cells.Add(activeCellZ);
                            row.Cells.Add(activeCellU);
                            row.Cells.Add(activeCellA);
                            row.Cells.Add(activeCellB);
                            row.Cells.Add(activeCellC);
                            row.Cells.Add(activeCellD);
                            row.Cells.Add(moveRelCell);
                            row.Cells.Add(moveModeCell);
                            row.Cells.Add(remarkCell);
                            dataGridView1.Rows.Add(row);
                        }
                    }
                    catch (Exception)
                    {
                    }
                };
                this.Invoke(action);
            }
            else
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    foreach (TablePosItem item in TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                        DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                        DataGridViewComboBoxCell elmentCell = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell posCellX = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellY = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellZ = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellU = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellA = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellB = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellC = new DataGridViewTextBoxCell();
                        DataGridViewTextBoxCell posCellD = new DataGridViewTextBoxCell();
                        DataGridViewCheckBoxCell activeCellX = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellY = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellZ = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellU = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellA = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellB = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellC = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell activeCellD = new DataGridViewCheckBoxCell();
                        DataGridViewCheckBoxCell moveRelCell = new DataGridViewCheckBoxCell();
                        DataGridViewComboBoxCell moveModeCell = new DataGridViewComboBoxCell();
                        DataGridViewTextBoxCell remarkCell = new DataGridViewTextBoxCell();

                        nameCell.Value = item.Name;
                        isUseCell.Value = item.IsUse;
                        posCellX.Value = item.PosX.ToString("0.000");
                        posCellY.Value = item.PosY.ToString("0.000");
                        posCellZ.Value = item.PosZ.ToString("0.000");
                        posCellU.Value = item.PosU.ToString("0.000");
                        posCellA.Value = item.PosA.ToString("0.000");
                        posCellB.Value = item.PosB.ToString("0.000");
                        posCellC.Value = item.PosC.ToString("0.000");
                        posCellD.Value = item.PosD.ToString("0.000");
                        activeCellX.Value = item.ActiveX;
                        activeCellY.Value = item.ActiveY;
                        activeCellZ.Value = item.ActiveZ;
                        activeCellU.Value = item.ActiveU;
                        activeCellA.Value = item.ActiveA;
                        activeCellB.Value = item.ActiveB;
                        activeCellC.Value = item.ActiveC;
                        activeCellD.Value = item.ActiveD;
                        moveRelCell.Value = item.MoveRel;
                        remarkCell.Value = item.Remarks;

                        moveModeCell.DataSource = Enum.GetNames(typeof(PointMoveMode));
                        moveModeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        moveModeCell.Value = item.MoveMode.ToString();

                        elmentCell.DataSource = Enum.GetNames(typeof(Element));
                        elmentCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        elmentCell.Value = item.TrgElement.ToString();

                        row.Cells.Add(nameCell);
                        row.Cells.Add(isUseCell);
                        row.Cells.Add(elmentCell);
                        row.Cells.Add(posCellX);
                        row.Cells.Add(posCellY);
                        row.Cells.Add(posCellZ);
                        row.Cells.Add(posCellU);
                        row.Cells.Add(posCellA);
                        row.Cells.Add(posCellB);
                        row.Cells.Add(posCellC);
                        row.Cells.Add(posCellD);

                        row.Cells.Add(activeCellX);
                        row.Cells.Add(activeCellY);
                        row.Cells.Add(activeCellZ);
                        row.Cells.Add(activeCellU);
                        row.Cells.Add(activeCellA);
                        row.Cells.Add(activeCellB);
                        row.Cells.Add(activeCellC);
                        row.Cells.Add(activeCellD);
                        row.Cells.Add(moveRelCell);
                        row.Cells.Add(moveModeCell);
                        row.Cells.Add(remarkCell);
                        dataGridView1.Rows.Add(row);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        private void Init()
        {
            try
            {
                cmbMotionCard.Items.Clear();
                foreach (HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    if (item.Type == HardwareType.MotionCard)
                    {
                        cmbMotionCard.Items.Add(item.Name);
                    }
                }

                checkedListBox1.Items.Clear();
                foreach (string axis in Enum.GetNames(typeof(DefaultAxis)))
                {
                    checkedListBox1.Items.Add("Axis " + axis);
                }

                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                    return;
                checkBox1.Checked = TableManage.docTable.dicTableData[_strCurrTable].AllowUserAddPoints;
                checkBox2.Checked = TableManage.docTable.dicTableData[_strCurrTable].AllowUserRemovePoints;
                chkDisplayOnManualPage.Checked = TableManage.docTable.dicTableData[_strCurrTable].ShowOnManualPage;
                //ShowTable(" ");
            }
            catch (Exception)
            {

            }
        }
        private void FormTableSysView_Load(object sender, EventArgs e)
        {
            Init();

            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
        }
        #endregion

        #region Event
        private void SelectedValueChangedHandler()
        {
            try
            {
                bool bFlag = false;
                if (checkedListBox1.SelectedIndex < 0)
                {
                    checkedListBox1.SelectedIndex = 0;
                }
                for (int index = 0; index < checkedListBox1.Items.Count; index++)
                {
                    if (checkedListBox1.GetItemCheckState(index) == CheckState.Checked)
                    {
                        if (TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                        {
                            TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active = true;
                        }
                        dataGridView1.Columns[3 + index].Visible = true;
                        dataGridView1.Columns[11 + index].Visible = true;
                        bFlag = true;
                    }
                    else
                    {
                        if (TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                        {
                            TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active = false;
                        }
                        dataGridView1.Columns[3 + index].Visible = false;
                        dataGridView1.Columns[11 + index].Visible = false;
                    }
                }
                dataGridView1.Columns[19].Visible = bFlag;
                dataGridView1.Columns[20].Visible = bFlag;
                SetDvColumnWidth();

                panelAxisSetup.Enabled = checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex);
                panelAxisSetup.Text = "Axis Setup [ " + checkedListBox1.SelectedItem.ToString() + " ]";
                ShowAxisData(checkedListBox1.SelectedIndex);
            }
            catch (Exception)
            {
            }
        }
        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            SelectedValueChangedHandler();
        }
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            TableDataSave();
        }
        private void panelConfig_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Width = panelConfig.Width - 26;
                dataGridView1.Height = panelConfig.Height - 80;


            }
            catch (Exception)
            {
            }
        }
        private void Textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private void cmbMotionCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbMotionCard.Focused)
            {
                SendKeys.Send("{Tab}");
            }
        }
        private void toorBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AddNewPoint())
                    throw new Exception();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add new point unsuccessful！\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void toolBarBtnRemove_Click(object sender, EventArgs e)
        {
            PointRemove();
            ShowPosItems();
        }
        private void toolStripBtnExport_Click(object sender, EventArgs e)
        {
            if (WriteToFileAsCS())
            {
                string strFilePath = Application.StartupPath + "\\Export\\Tables\\";
                DialogResult dRet;
                dRet = MessageBox.Show("导出成功！\r\n是否打开文件夹？", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
        //示教
        private void toolStripTeach_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                if(index < 0 || index > TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems.Count)
                {
                    throw new Exception();
                }
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosX = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentX;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosY = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentY;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosZ = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentZ;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosU = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentU;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosA = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentA;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosB = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentB;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosC = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentC;
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[index].PosD = TableManage.tableDrivers.dicDrivers[_strCurrTable].CurrentD;
                
                MessageBox.Show("Success.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                ShowPosItems();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        #region Datagridview
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                if (e.RowIndex >= TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems.Count)
                {
                    return;
                }
                string strName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string strOldName = TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[e.RowIndex].Name;

                string strMoveMode = dataGridView1["ColumnMoveMode", e.RowIndex].Value.ToString().Trim();

                if (e.ColumnIndex == 0 && !UniqueCheck(1, strName))
                {
                    throw new Exception();
                }

                TablePosItem posItem = TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[e.RowIndex];

                double dTemp = 0.00;
                string strTemp = "";
                List<double> listPos = new List<double>();
                for (int index = 0; index < 8; index++)
                {
                    strTemp = dataGridView1.Rows[e.RowIndex].Cells[index + 3].Value.ToString();
                    if (!double.TryParse(strTemp, out dTemp))
                    {
                        MessageBox.Show("The value type should be double type !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        throw new Exception();
                    }
                    listPos.Add(dTemp);
                }
                posItem.Name = strName;
                posItem.IsUse = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[1].EditedFormattedValue);
                posItem.TrgElement = (Element)Enum.Parse(typeof(Element), dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), false);
                posItem.PosX = listPos[0];
                posItem.PosY = listPos[1];
                posItem.PosZ = listPos[2];
                posItem.PosU = listPos[3];
                posItem.PosA = listPos[4];
                posItem.PosB = listPos[5];
                posItem.PosC = listPos[6];
                posItem.PosD = listPos[7];
                posItem.ActiveX = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[11].EditedFormattedValue);
                posItem.ActiveY = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[12].EditedFormattedValue);
                posItem.ActiveZ = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[13].EditedFormattedValue);
                posItem.ActiveU = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[14].EditedFormattedValue);
                posItem.ActiveA = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[15].EditedFormattedValue);
                posItem.ActiveB = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[16].EditedFormattedValue);
                posItem.ActiveC = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[17].EditedFormattedValue);
                posItem.ActiveD = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[18].EditedFormattedValue);
                posItem.MoveRel = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[19].EditedFormattedValue);
                posItem.MoveMode = (PointMoveMode)Enum.Parse(typeof(PointMoveMode), dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString(), false);
                posItem.Remarks = dataGridView1.Rows[e.RowIndex].Cells[21].Value.ToString();

                TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.Remove(strOldName);
                TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.Add(strName, TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[e.RowIndex]);
            }
            catch (Exception)
            {
                MessageBox.Show("Modefine unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                ShowPosItems();
            }
        }
        #endregion
        #region Set Value
        private void tbIndex_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Index = Convert.ToInt16(tbIndex.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbPulseToMM_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].PulseToMM = Convert.ToDouble(tbPulseToMM.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbAcc_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if(double.TryParse(tbAcc.Text,out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Acc = dTemp;
                }
                else
                {
                    tbAcc.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Acc.ToString("0.0");
                }
            }
            catch (Exception)
            {
            }

        }

        private void tbDec_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if (double.TryParse(tbDec.Text, out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Dec = dTemp;
                }
                else
                {
                    tbDec.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Dec.ToString("0.0");
                }
            }
            catch (Exception)
            {
            }

        }

        private void tbSearchHomeSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if (double.TryParse(tbSearchHomeSpeed.Text, out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchHomeSpeed = dTemp;
                }
                else
                {
                    tbSearchHomeSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchHomeSpeed.ToString("0.0");
                }
            }
            catch (Exception)
            {
            }

        }

        private void tbSearchLimitSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if (double.TryParse(tbSearchLimitSpeed.Text, out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchLimitSpeed = dTemp;
                }
                else
                {
                    tbSearchLimitSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchLimitSpeed.ToString("0.0");
                }
            }
            catch (Exception)
            {
            }

        }

        private void tbCorNo_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].CorNo = Convert.ToInt16(tbCorNo.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbJogSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if (double.TryParse(tbJogSpeed.Text, out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].JogSpeed = dTemp;
                }
                else
                {
                    tbJogSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].JogSpeed.ToString("0.0");
                }

            }
            catch (Exception)
            {
            }

        }

        private void tbRunSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = 0;
                if (double.TryParse(tbRunSpeed.Text, out dTemp))
                {
                    TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].RunSpeed = dTemp;
                }
                else
                {
                    tbRunSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].RunSpeed.ToString("0.0");
                }
            }
            catch (Exception)
            {
            }

        }

        private void tbLimitSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].MaxSpeed = Convert.ToDouble(tbLimitSpeed.Text.Trim());
            }
            catch (Exception)
            {
            }
        }

        private void tbSoftLimitPos_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SoftLimitPos = Convert.ToDouble(tbSoftLimitPos.Text.Trim());
            }
            catch (Exception)
            {
            }
        }

        private void tbSoftLimitNeg_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SoftLimitNeg = Convert.ToDouble(tbSoftLimitNeg.Text.Trim());
            }
            catch (Exception)
            {
            }
        }

        private void cmbMotionCard_Validated(object sender, EventArgs e)
        {
            try
            {
                TableManage.docTable.dicTableData[_strCurrTable].MotionCardName = cmbMotionCard.SelectedItem.ToString();
                if (cmbMotionCard.SelectedIndex < 0)
                {
                    groupBoxAxesSelect.Enabled = false;
                    panelConfig.Enabled = false;
                    panelAxisSetup.Enabled = false;
                    return;
                }
                else
                {
                    groupBoxAxesSelect.Enabled = true;
                    panelConfig.Enabled = true;
                    panelAxisSetup.Enabled = true;
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbPulseMode_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].AxisPulseMode = (PulseMode)cmbPulseMode.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void cmbHomeMode_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].AxisHomeMode = (HomeMode)cmbHomeMode.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void cmbOrgLogic_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].OrgLogic = (SenserLogic)cmbOrgLogic.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void cmbLimitLogic_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].LimitLogic = (SenserLogic)cmbLimitLogic.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }

        private void cmbAlarmLogic_Validated(object sender, EventArgs e)
        {
            try
            {
                int index = checkedListBox1.SelectedIndex;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].AlarmLogic = (SenserLogic)cmbAlarmLogic.SelectedIndex;
            }
            catch (Exception)
            {
            }
        }
        private void chkDisplayOnManualPage_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].ShowOnManualPage = chkDisplayOnManualPage.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].AllowUserAddPoints = checkBox1.Checked;
            }
            catch (Exception)
            {
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                TableManage.docTable.dicTableData[_strCurrTable].AllowUserRemovePoints = checkBox2.Checked;
            }
            catch (Exception)
            {
            }
        }
        #endregion
        #endregion

        #region Methods
        public void ShowTable(string strTableName)
        {
            try
            {
                _strCurrTable = strTableName;
                panelConfig.Text = "Table [ " + _strCurrTable + " ]";
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    panelMain.Enabled = false;
                    return;
                }
                panelMain.Enabled = true;

                checkedListBox1.Items.Clear();
                foreach (string axis in Enum.GetNames(typeof(DefaultAxis)))
                {
                    checkedListBox1.Items.Add("Axis " + axis);
                }

                cmbMotionCard.Items.Clear();
                foreach (HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    if (item.Type == HardwareType.MotionCard || item.Type == HardwareType.Robot)
                    {
                        cmbMotionCard.Items.Add(item.Name);
                    }
                }

                if (cmbMotionCard.Items.Contains(TableManage.docTable.dicTableData[_strCurrTable].MotionCardName))
                {
                    cmbMotionCard.SelectedItem = TableManage.docTable.dicTableData[_strCurrTable].MotionCardName;
                }
                else
                {
                    cmbMotionCard.SelectedIndex = -1;
                }

                TableData table = TableManage.docTable.dicTableData[_strCurrTable];
                checkBox1.Checked = table.AllowUserAddPoints;
                checkBox2.Checked = table.AllowUserRemovePoints;
                chkDisplayOnManualPage.Checked = table.ShowOnManualPage;
                foreach (DefaultAxis axis in Enum.GetValues(typeof(DefaultAxis)))
                {
                    checkedListBox1.SetItemChecked((int)axis, table.ListTableAxesItems[(int)axis].Active);
                }
                SelectedValueChangedHandler();

                if (cmbMotionCard.SelectedIndex < 0)
                {
                    groupBoxAxesSelect.Enabled = false;
                    panelConfig.Enabled = false;
                    panelAxisSetup.Enabled = false;
                }
                else
                {
                    groupBoxAxesSelect.Enabled = true;
                    panelConfig.Enabled = true;
                    panelAxisSetup.Enabled = true;
                }
                ShowPosItems();
            }
            catch (Exception)
            {
            }
        }
        public void ShowAxisData(int index)
        {
            if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
            {
                MessageBox.Show("Error : Table inexistence.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            if (index > (int)DefaultAxis.D)
            {
                MessageBox.Show("Error : wrong axis index.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }

            try
            {
                TableAxisData axisData = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index];
                cmbPulseMode.SelectedIndex = (int)axisData.AxisPulseMode;
                cmbHomeMode.SelectedIndex = (int)axisData.AxisHomeMode;
                cmbOrgLogic.SelectedIndex = (int)axisData.OrgLogic;
                cmbLimitLogic.SelectedIndex = (int)axisData.LimitLogic;
                cmbAlarmLogic.SelectedIndex = (int)axisData.AlarmLogic;

                tbIndex.Text = axisData.Index.ToString();
                tbPulseToMM.Text = axisData.PulseToMM.ToString();
                tbAcc.Text = axisData.Acc.ToString("0.0");
                tbDec.Text = axisData.Dec.ToString("0.0");
                tbSearchHomeSpeed.Text = axisData.SearchHomeSpeed.ToString("0.00");
                tbSearchLimitSpeed.Text = axisData.SearchLimitSpeed.ToString("0.00");
                tbCorNo.Text = axisData.CorNo.ToString();
                tbJogSpeed.Text = axisData.JogSpeed.ToString("0.00");
                tbRunSpeed.Text = axisData.RunSpeed.ToString("0.00");
                tbLimitSpeed.Text = axisData.MaxSpeed.ToString("0.00");
                tbSoftLimitPos.Text = axisData.SoftLimitPos.ToString("0.00");
                tbSoftLimitNeg.Text = axisData.SoftLimitNeg.ToString("0.00");

            }
            catch (Exception)
            {
            }
        }
        public void SetDvColumnWidth()
        {
            try
            {
                int iWidth = 0;
                for (int index =0;index<dataGridView1.ColumnCount; index++)
                {
                    if(dataGridView1.Columns[index].Visible)
                    {
                        dataGridView1.Columns[index].Width = dataGridView1.Columns[index].MinimumWidth;
                        iWidth += dataGridView1.Columns[index].Width;
                    }
                }
                if(dataGridView1.Width > iWidth)
                {
                    dataGridView1.Columns["ColumnRemarks"].Width += (dataGridView1.Width - iWidth) - 8;
                }
            }
            catch (Exception)
            {
            }
        }
        private string GetContent(string strTableName)
        {
            string strContent = "";
            try
            {
                if (string.IsNullOrEmpty(strTableName))
                {
                    throw new Exception("名称不能为空!");
                }
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.Table.NameItems\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + strTableName + "\r\n";
                strContent += "   {\r\n";

                foreach (TablePosItem item in TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems)
                {
                    strContent += "      public static string ";
                    strContent += item.Name;
                    strContent += " = ";
                    strContent += "\"" + item.Name + "\"";
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
            if (string.IsNullOrEmpty(_strCurrTable))
            {
                return false;
            }
            if(!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
            {
                return false;
            }
            string strContent = GetContent("Postion" + _strCurrTable);
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }

            try
            {
                string strPath = Application.StartupPath + "\\Export\\Tables\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                File.WriteAllText(strPath + "Postion" + _strCurrTable + ".cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        protected internal void TableDataSave()
        {
            if (null != TableManage.docTable)
            {
                if (TableManage.docTable.SaveDoc())
                {
                    MessageBox.Show("Successfuly saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
            }
            MessageBox.Show("Save unsuccessful!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
        protected internal bool GetNewPointName(ref string strName)
        {
            int index = 0;
            if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                return false;
            while (true)
            {
                if (!TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.ContainsKey("Point_" + index.ToString()))
                {
                    strName = "Point_" + index.ToString();
                    return true;
                }
                index++;
            }
        }
        protected internal bool AddNewPoint()
        {
            try
            {
                string strNewName = "";
                if (!GetNewPointName(ref strNewName) || !TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    throw new Exception();
                }
                TablePosItem posItem = new TablePosItem(strNewName);
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                DataGridViewTextBoxCell posCellX = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellY = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellZ = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellU = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellA = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellB = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellC = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell posCellD = new DataGridViewTextBoxCell();
                DataGridViewCheckBoxCell activeCellX = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellY = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellZ = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellU = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellA = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellB = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellC = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell activeCellD = new DataGridViewCheckBoxCell();
                DataGridViewCheckBoxCell moveRelCell = new DataGridViewCheckBoxCell();
                DataGridViewComboBoxCell moveModeCell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell remarkCell = new DataGridViewTextBoxCell();

                nameCell.Value = strNewName;
                isUseCell.Value = posItem.IsUse;
                posCellX.Value = posItem.PosX;
                posCellY.Value = posItem.PosY;
                posCellZ.Value = posItem.PosZ;
                posCellU.Value = posItem.PosU;
                posCellA.Value = posItem.PosA;
                posCellB.Value = posItem.PosB;
                posCellC.Value = posItem.PosC;
                posCellD.Value = posItem.PosD;
                activeCellX.Value = posItem.ActiveX;
                activeCellY.Value = posItem.ActiveY;
                activeCellZ.Value = posItem.ActiveA;
                activeCellU.Value = posItem.ActiveU;
                activeCellA.Value = posItem.ActiveA;
                activeCellB.Value = posItem.ActiveB;
                activeCellC.Value = posItem.ActiveC;
                activeCellD.Value = posItem.ActiveD;
                moveRelCell.Value = posItem.MoveRel;
                remarkCell.Value = posItem.Remarks;

                moveModeCell.DataSource = Enum.GetNames(typeof(PointMoveMode));
                moveModeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                moveModeCell.Value = posItem.MoveMode.ToString();

                row.Cells.Add(nameCell);
                row.Cells.Add(isUseCell);
                row.Cells.Add(posCellX);
                row.Cells.Add(posCellY);
                row.Cells.Add(posCellZ);
                row.Cells.Add(posCellU);
                row.Cells.Add(posCellA);
                row.Cells.Add(posCellB);
                row.Cells.Add(posCellC);
                row.Cells.Add(posCellD);

                row.Cells.Add(activeCellX);
                row.Cells.Add(activeCellY);
                row.Cells.Add(activeCellZ);
                row.Cells.Add(activeCellU);
                row.Cells.Add(activeCellA);
                row.Cells.Add(activeCellB);
                row.Cells.Add(activeCellC);
                row.Cells.Add(activeCellD);
                row.Cells.Add(moveRelCell);
                row.Cells.Add(moveModeCell);
                row.Cells.Add(remarkCell);
                dataGridView1.Rows.Add(row);

                TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.Add(posItem.Name, posItem);
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems.Add(posItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add new item unsuccessful！\r\n"+ ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return false;
            }
            return true;
        }
        protected internal bool PointRemove()
        {
            try
            {
                if (dataGridView1.CurrentCell.RowIndex < 0)
                    return false;
                TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.Remove(TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems[dataGridView1.CurrentCell.RowIndex].Name);
                TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        protected internal bool UniqueCheck(int iCmdIndex, string strName)
        {
            try
            {
                if (0 == iCmdIndex)
                {
                    //New
                    return true;
                }
                else if (1 == iCmdIndex)
                {
                    //Modefine
                    return TableManage.docTable.dicTableData[_strCurrTable].dicTablePosItem.ContainsKey(strName) ? false : true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        #endregion
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1 || (e.ColumnIndex > 10 && e.ColumnIndex < 20))
            {
                this.dataGridView1_CellValueChanged(sender, e);
                //MessageBox.Show(this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].EditedFormattedValue.ToString());
            }
        }
    }
}
