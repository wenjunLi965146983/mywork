using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Hardware;

namespace WorldGeneralLib.Table
{
    public partial class FormTableUserView : Form
    {
        private string _strCurrTable = string.Empty;

        public FormTableUserView()
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
                        bool bFlag = false;
                        dataGridView1.Rows.Clear();
                        foreach (TablePosItem item in TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems)
                        {
                            DataGridViewRow row = new DataGridViewRow();
                            DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                            DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                            DataGridViewComboBoxCell elementCell = new DataGridViewComboBoxCell();
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
                            posCellX.Value = item.PosX;
                            posCellY.Value = item.PosY;
                            posCellZ.Value = item.PosZ;
                            posCellU.Value = item.PosU;
                            posCellA.Value = item.PosA;
                            posCellB.Value = item.PosB;
                            posCellC.Value = item.PosC;
                            posCellD.Value = item.PosD;
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

                            elementCell.DataSource = Enum.GetNames(typeof(Element));
                            elementCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                            elementCell.Value = item.TrgElement.ToString();

                            row.Cells.Add(nameCell);
                            row.Cells.Add(isUseCell);
                            row.Cells.Add(elementCell);
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
                        for(int index=0; index < TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems.Count; index++)
                        {
                            dataGridView1.Columns[3 + index].Visible = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active;
                            dataGridView1.Columns[11 + index].Visible = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active;
                            if(TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active)
                            {
                                bFlag = true;
                            }
                        }
                        dataGridView1.Columns[19].Visible = bFlag;
                        dataGridView1.Columns[20].Visible = bFlag;
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
                    bool bFlag = false;
                    dataGridView1.Rows.Clear();
                    foreach (TablePosItem item in TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems)
                    {
                        DataGridViewRow row = new DataGridViewRow();
                        DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                        DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                        DataGridViewComboBoxCell elementCell = new DataGridViewComboBoxCell();
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
                        posCellY.Value = item.PosY.ToString("0.000"); ;
                        posCellZ.Value = item.PosZ.ToString("0.000"); ;
                        posCellU.Value = item.PosU.ToString("0.000"); ;
                        posCellA.Value = item.PosA.ToString("0.000"); ;
                        posCellB.Value = item.PosB.ToString("0.000"); ;
                        posCellC.Value = item.PosC.ToString("0.000"); ;
                        posCellD.Value = item.PosD.ToString("0.000"); ;
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

                        elementCell.DataSource = Enum.GetNames(typeof(Element));
                        elementCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                        elementCell.Value = item.TrgElement.ToString();

                        row.Cells.Add(nameCell);
                        row.Cells.Add(isUseCell);
                        row.Cells.Add(elementCell);
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
                    for (int index = 0; index < TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems.Count; index++)
                    {
                        dataGridView1.Columns[3 + index].Visible = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active;
                        dataGridView1.Columns[11 + index].Visible = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active;
                        if (TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Active)
                        {
                            bFlag = true;
                        }
                    }
                    dataGridView1.Columns[19].Visible = bFlag;
                    dataGridView1.Columns[20].Visible = bFlag;
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
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    groupBoxAxisSpeedAndSafe.Enabled = false;
                    return;
                }
                    
                cmbAxisSelect.Items.Clear();
                foreach (KeyValuePair<string, TableAxisData> item  in TableManage.docTable.dicTableData[_strCurrTable].dicTableAxisItem)
                {
                    if (item.Value.Active)
                    {
                        cmbAxisSelect.Items.Add(item.Key);
                    }
                }
                if(cmbAxisSelect.Items.Count > 0)
                {
                    cmbAxisSelect.SelectedIndex = 0;
                }
                groupBoxAxisSpeedAndSafe.Enabled = cmbAxisSelect.SelectedIndex >= 0 ? true : false;

                toorBtnAdd.Visible = TableManage.docTable.dicTableData[_strCurrTable].AllowUserAddPoints;
                toolBarBtnRemove.Visible = TableManage.docTable.dicTableData[_strCurrTable].AllowUserRemovePoints;
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                DefaultAxis axis = (DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString());
                ShowAxisData((int)axis);
            }
            catch (Exception)
            {
            }
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
        //示教
        private void toolStripTeach_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView1.CurrentCell.RowIndex;
                if (index < 0 || index > TableManage.docTable.dicTableData[_strCurrTable].ListTablePosItems.Count)
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Acc = Convert.ToDouble(tbAcc.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbDec_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].Dec = Convert.ToDouble(tbDec.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbSearchHomeSpeed_Validated(object sender, EventArgs e)
        {
            int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = Convert.ToDouble(tbSearchHomeSpeed.Text.Trim());
                if (dTemp > TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].MaxSpeed)
                {
                    MessageBox.Show("速度超过所设定的上限!", "参数无效", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    throw new Exception();
                }
                
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchHomeSpeed = dTemp;
            }
            catch (Exception)
            {
                tbSearchHomeSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].JogSpeed.ToString("0.00");

            }

        }

        private void tbSearchLimitSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SearchLimitSpeed = Convert.ToDouble(tbSearchLimitSpeed.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbCorNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].CorNo = Convert.ToInt16(tbCorNo.Text.Trim());
            }
            catch (Exception)
            {
            }

        }

        private void tbJogSpeed_Validated(object sender, EventArgs e)
        {
            int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = Convert.ToDouble(tbJogSpeed.Text.Trim());
                if (dTemp > TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].MaxSpeed)
                {
                    MessageBox.Show("速度超过所设定的上限!", "参数无效", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    throw new Exception();
                }
                
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].JogSpeed = Convert.ToDouble(tbJogSpeed.Text.Trim());
            }
            catch (Exception)
            {
                tbJogSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].JogSpeed.ToString("0.00");
            }

        }

        private void tbRunSpeed_Validated(object sender, EventArgs e)
        {
            int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }
                double dTemp = Convert.ToDouble(tbRunSpeed.Text.Trim());
                if(dTemp > TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].MaxSpeed)
                {
                    MessageBox.Show("速度超过所设定的上限!", "参数无效", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    throw new Exception();
                }
                
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].RunSpeed = Convert.ToDouble(tbRunSpeed.Text.Trim());
            }
            catch (Exception)
            {
                tbRunSpeed.Text = TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].RunSpeed.ToString("0.00");
            }

        }

        private void tbLimitSpeed_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].SoftLimitNeg = Convert.ToDouble(tbSoftLimitNeg.Text.Trim());
            }
            catch (Exception)
            {
            }
        }
        private void cmbPulseMode_Validated(object sender, EventArgs e)
        {
            try
            {
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
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
                if (cmbAxisSelect.SelectedIndex < 0)
                    return;
                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                {
                    return;
                }

                int index = (int)((DefaultAxis)Enum.Parse(typeof(DefaultAxis), cmbAxisSelect.SelectedItem.ToString()));
                TableManage.docTable.dicTableData[_strCurrTable].ListTableAxesItems[index].AlarmLogic = (SenserLogic)cmbAlarmLogic.SelectedIndex;
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

                if (!TableManage.docTable.dicTableData.ContainsKey(_strCurrTable))
                    return;

                Init();
                ShowPosItems();
                SetDvColumnWidth();
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
                for (int index = 0; index < dataGridView1.ColumnCount; index++)
                {
                    if (dataGridView1.Columns[index].Visible)
                    {
                        dataGridView1.Columns[index].Width = dataGridView1.Columns[index].MinimumWidth;
                        iWidth += dataGridView1.Columns[index].Width;
                    }
                }
                if (dataGridView1.Width > iWidth)
                {
                    dataGridView1.Columns["ColumnRemarks"].Width += (dataGridView1.Width - iWidth) - 8;
                }
            }
            catch (Exception)
            {
            }
        }
        protected internal void TableDataSave()
        {
            if (null != TableManage.docTable)
            {
                if (TableManage.docTable.SaveDoc())
                {
                    MessageBox.Show("Successfuly saved!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
                posItem.ActiveX = true;
                posItem.ActiveY = true;
                posItem.ActiveZ = true;
                posItem.ActiveU = true;
                posItem.ActiveA = true;
                posItem.ActiveB = true;
                posItem.ActiveC = true;
                posItem.ActiveD = true;

                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewCheckBoxCell isUseCell = new DataGridViewCheckBoxCell();
                DataGridViewComboBoxCell elementCell = new DataGridViewComboBoxCell();
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

                elementCell.DataSource = Enum.GetNames(typeof(Element));
                elementCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                elementCell.Value = posItem.TrgElement.ToString();

                row.Cells.Add(nameCell);
                row.Cells.Add(isUseCell);
                row.Cells.Add(elementCell);
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
                MessageBox.Show("Add new item unsuccessful！\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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

        private void cmbAxisSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedValueChangedHandler();
        }
    }
}
