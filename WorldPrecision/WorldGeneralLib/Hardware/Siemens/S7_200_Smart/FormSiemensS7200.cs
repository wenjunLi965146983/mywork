using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WorldGeneralLib.Functions;
using System.Net;
using OmronFins.Net;
using System.IO;

namespace WorldGeneralLib.Hardware.Siemens.S7_200_Smart
{
    public partial class FormSiemensS7200 : Form
    {
        private PlcSiemensS7200Data _plcData;
        private PlcSiemensS7200 _plcDriver;

        private Thread threadScan;
        public FormSiemensS7200()
        {
            InitializeComponent();
        }
        public FormSiemensS7200(PlcSiemensS7200Data plcData):this()
        {
            _plcData = plcData;
            _plcDriver = (PlcSiemensS7200)HardwareManage.dicHardwareDriver[_plcData.Name];

            ViewInit();
        }
        private void ShowItems()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (PlcScanItems item in _plcData.m_ScanDataList)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell addrTypeCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell addrCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell dataTypeCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell valueCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell indexCell = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell refreshchkCell = new DataGridViewCheckBoxCell();

                    nameCell.Value = item.strName;
                    addrTypeCell.Value = item.AddressType.ToString();
                    addrCell.Value = item.Address;
                    dataTypeCell.Value = item.DataType.ToString();
                    valueCell.Value = item.strValue;
                    indexCell.Value = item.Index.ToString();
                    refreshchkCell.Value = item.Refresh;

                    row.Cells.Add(nameCell);
                    row.Cells.Add(addrTypeCell);
                    row.Cells.Add(addrCell);
                    row.Cells.Add(dataTypeCell);
                    row.Cells.Add(valueCell);
                    row.Cells.Add(indexCell);
                    row.Cells.Add(refreshchkCell);

                    dataGridView1.Rows.Add(row);
                }
            }
            catch (Exception)
            {
            }
        }
        private void SetDataGridViewStyle()
        {
            int iWidth = panel1.Width;

            dataGridView1.Columns[0].Width = iWidth / 7;
            dataGridView1.Columns[1].Width = iWidth / 7 + 15;
            dataGridView1.Columns[2].Width = iWidth / 7;
            dataGridView1.Columns[3].Width = iWidth / 7;
            dataGridView1.Columns[4].Width = iWidth / 7;
            dataGridView1.Columns[5].Width = iWidth / 7 - 10;
            dataGridView1.Columns[6].Width = iWidth / 7 - 8;
        }
        private void ViewInit()
        {
            cmbDataType.Items.Clear();
            cmbAddrType.Items.Clear();

            cmbAddrType.DataSource = Enum.GetNames(typeof(PlcMemory));
            cmbDataType.DataSource = Enum.GetNames(typeof(DataType));

            ShowItems();
            StartThread();

            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
        }
        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            SetDataGridViewStyle();
        }
        private void FormOmronTypeNJ_Load(object sender, EventArgs e)
        {
            SetDataGridViewStyle();

            _plcDriver.times = 0;
        }
        private void btnConn_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show("Failure to connected to PLC " + _plcData.Name, "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }

        }

        #region ScanItem Edit
        //true : 名字,地址没有重复项
        //false: 名字，地址有重复项或名字为空
        //cmdindex 0: modify  1:add
        private bool UniqueCheck(int cmdindex)
        {
            //string strName,SoftElemType elementtype,int startaddress
            if (string.IsNullOrEmpty(tbItemName.Text))
            {
                MessageBox.Show("Name is empty", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return false;
            }
            foreach (PlcScanItems item in _plcData.m_ScanDataList)
            {
                if (cmdindex == 0)
                {
                    if (tbItemName.Text.Equals(item.strName) && (((PlcMemory)cmbAddrType.SelectedItem).Equals(item.AddressType) && (tbAddr.Text) == item.Address))
                    {
                        MessageBox.Show("Name/Address has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                    if (!JudgeNumber.isPositiveUINT1632(tbAddr.Text) && (DataType)cmbDataType.SelectedItem != DataType.BIT)
                    {
                        MessageBox.Show("Number should be unsigned interger/bitaddress should be 0.00", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                }
                else if (cmdindex == 1)
                {
                    if (tbItemName.Text.Equals(item.strName))
                    {
                        MessageBox.Show("Name has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                    if (!JudgeNumber.isPositiveUINT1632(tbAddr.Text) && (DataType)cmbDataType.SelectedIndex != DataType.BIT)
                    {
                        MessageBox.Show("Number should be unsigned interger/bitaddress should be 0.00", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }

                    if (((PlcMemory)cmbAddrType.SelectedIndex).Equals(item.AddressType) && (tbAddr.Text) == item.Address)
                    {
                        MessageBox.Show("Address has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                }
            }
            return true;
        }
        private bool ItemAdd()
        {
            if(!UniqueCheck(1))
            {
                return false;
            }

            try
            {
                PlcScanItems item = new PlcScanItems();
                DataGridViewRow row = new DataGridViewRow();
                item.strName = tbItemName.Text;
                item.Address = (tbAddr.Text);
                item.AddressType = (PlcMemory)cmbAddrType.SelectedIndex;
                item.DataType = (DataType)cmbDataType.SelectedIndex;

                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell addrTypeCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell addrCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell dataTypeCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell valueCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell indexCell = new DataGridViewTextBoxCell();
                DataGridViewCheckBoxCell refreshChkCell = new DataGridViewCheckBoxCell();

                nameCell.Value = item.strName;
                addrTypeCell.ValueType = typeof(PlcMemory);
                addrTypeCell.Value = item.AddressType;
                addrCell.Value = item.Address;
                dataTypeCell.ValueType = typeof(DataType);
                dataTypeCell.Value = item.DataType;
                indexCell.Value = item.Index.ToString();
                refreshChkCell.Value = item.Refresh;

                row.Cells.Add(nameCell);
                row.Cells.Add(addrTypeCell);
                row.Cells.Add(addrCell);
                row.Cells.Add(dataTypeCell);
                row.Cells.Add(valueCell);
                row.Cells.Add(indexCell);
                row.Cells.Add(refreshChkCell);
                dataGridView1.Rows.Add(row);

                _plcData.m_scanDictionary.Add(item.strName, item);
                _plcData.m_ScanDataList.Add(item);

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool ItemRemove()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 1)
                    return false;
                _plcData.m_scanDictionary.Remove(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                _plcData.m_ScanDataList.RemoveAt(dataGridView1.SelectedRows[0].Index);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ItemAdd();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            ItemRemove();
            ShowItems();
        }

        #endregion

        #region Monitor
        private void ScanItems()
        {
            try
            {
                for (int index = 0; index < _plcData.m_ScanDataList.Count; index++)
                {
                    Action action = () =>
                    {
                        if (dataGridView1.Rows.Count != 0)
                        {
                            if (dataGridView1.Rows.Count == index)
                            {
                                dataGridView1.Rows[index - 1].Cells[4].Value = _plcData.m_ScanDataList[index].strValue;
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[4].Value = _plcData.m_ScanDataList[index].strValue;
                            }
                        }
                    };
                    this.Invoke(action);
                }

#if false
                string strValue = string.Empty;
                object objTemp = new object();
                for (int index = 0; index < _plcData.m_ScanDataList.Count; index++)
                {
                    strValue = string.Empty;
                    switch(_plcData.m_ScanDataList[index].DataType)
                    {
                        case DataType.BIT:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            if ((bool)objTemp)
                                strValue = "1";
                            else
                                strValue = "0";
                            break;
                        case DataType.INT16:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            strValue = ((Int16)objTemp).ToString();
                            break;
                        case DataType.UINT16:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            strValue = ((UInt16)objTemp).ToString();
                            break;
                        case DataType.UINT32:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            strValue = ((UInt32)objTemp).ToString();
                            break;
                        case DataType.INT32:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            strValue = ((Int32)objTemp).ToString();
                            break;
                        case DataType.REAL:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.m_ScanDataList[index], ref objTemp);
                            strValue = ((float)objTemp).ToString();
                            break;
                        default:
                            strValue = string.Empty;
                            break;
                    }
                    Action action = () =>
                     {
                         if (dataGridView1.Rows.Count != 0)
                         {
                             if (dataGridView1.Rows.Count == index)
                             {
                                 dataGridView1.Rows[index - 1].Cells[4].Value = strValue.ToString();
                             }
                             else
                             {
                                 dataGridView1.Rows[index].Cells[4].Value = strValue.ToString();
                             }
                         }
                    };
                    this.Invoke(action);
                }
#endif
            }
            catch (Exception)
            {

            }
        }
        private void StartThread()
        {
            threadScan = new Thread(ThreadScanHandler);
            threadScan.IsBackground = true;
            threadScan.Start();
        }

        private void ThreadScanHandler()
        {
            while(!MainModule.formMain.bExit)
            {
                Thread.Sleep(100);
                try
                {

                }
                catch (Exception)
                {
                }
            }
        }

#endregion

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex >= _plcData.m_ScanDataList.Count)
                {
                    return;
                }
                string strName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string strOldName = _plcData.m_ScanDataList[e.RowIndex].strName;

                _plcData.m_ScanDataList[e.RowIndex].strName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _plcData.m_ScanDataList[e.RowIndex].Address = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                _plcData.m_ScanDataList[e.RowIndex].Index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                _plcData.m_ScanDataList[e.RowIndex].Refresh = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value);

                _plcData.m_scanDictionary.Remove(strOldName);
                _plcData.m_scanDictionary.Add(strName, _plcData.m_ScanDataList[e.RowIndex]);
            }
            catch (Exception)
            {
                MessageBox.Show("Modefine unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                ShowItems();
            }
     }

        #region Export
        private string CreatePlcnameCSFile()
        {
            string strreturn = string.Empty;
            if (_plcData.m_ScanDataList.Count < 1)
                return strreturn;
            string classname = _plcData.Name;

            string strTempHeader = "using System;\r\n";
            strTempHeader += "using System.Collections.Generic;\r\n";
            strTempHeader += "using System.Linq;\r\n";
            strTempHeader += "using System.Text;\r\n";
            strTempHeader += "using System.Threading.Tasks;\r\n";
            strTempHeader += "\r\n";
            strTempHeader += "namespace WorldGeneralLib.OmronPLC\r\n";
            strTempHeader += "{\r\n";
            strTempHeader += "   public static class " + classname + "\r\n";
            strTempHeader += "   {\r\n";
            strreturn = strTempHeader;

            string strItemHeader = "      public static string ";
            string strItemName = "";
            string strItemOperation = " = ";
            string strValue = "";
            string strItemEnd = "  ;\r\n";

            foreach (PlcScanItems item in _plcData.m_ScanDataList)
            {
                strItemName = item.strName;
                strValue = "\"" + strItemName + "\"";
                strreturn += strItemHeader + strItemName + strItemOperation + strValue + strItemEnd;
            }

            string strTempEnd = "  }\r\n";
            strTempEnd += "}\r\n";
            strreturn += strTempEnd;
            return strreturn;
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            string filename = _plcData.Name + ".cs";
            string strTempPath = @".//TempFile/";
            string strTempName = filename;
            if (!Directory.Exists(strTempPath))
            {
                Directory.CreateDirectory(strTempPath);
            }
            string strcontent = CreatePlcnameCSFile();
            File.WriteAllText((strTempPath + strTempName), strcontent, Encoding.UTF8);
            MessageBox.Show("Successfully Exported.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
        #endregion
    }
}
