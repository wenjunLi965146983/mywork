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
using OmronFins.Net;
using WorldGeneralLib.Functions;
using System.Net;
using System.IO;

namespace WorldGeneralLib.Hardware.Omron.TypeNJ
{
    public partial class FormOmronTypeNJ : Form
    {
        private PlcOmronTypeNJData _plcData;
        private PlcOmronTypeNJ _plcDriver;

        private Thread threadScan;
        public FormOmronTypeNJ()
        {
            InitializeComponent();
        }
        public FormOmronTypeNJ(PlcOmronTypeNJData plcData):this()
        {
            _plcData = plcData;
            _plcDriver = (PlcOmronTypeNJ)HardwareManage.dicHardwareDriver[_plcData.Name];
            this.panelConfig.Text = plcData.Name;

            ViewInit();
        }
        private void ShowItems()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (PlcScanItems item in _plcData.listScanItems)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                    DataGridViewComboBoxCell addrTypeCell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell addrCell = new DataGridViewTextBoxCell();
                    DataGridViewComboBoxCell dataTypeCell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell valueCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell indexCell = new DataGridViewTextBoxCell();
                    DataGridViewCheckBoxCell refreshchkCell = new DataGridViewCheckBoxCell();

                    addrTypeCell.DataSource = Enum.GetNames(typeof(PlcMemory));
                    dataTypeCell.DataSource = Enum.GetNames(typeof(DataType));

                    addrTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    dataTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

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
                ipAddressControl1.Text = _plcData.IP;
            }
            catch (Exception)
            {
            }
        }
        private void SetDataGridViewStyle()
        {
            int iWidth = panelConfig.Width;

            dataGridView1.Columns[0].Width = iWidth / 7;
            dataGridView1.Columns[1].Width = iWidth / 7 + 15;
            dataGridView1.Columns[2].Width = iWidth / 7;
            dataGridView1.Columns[3].Width = iWidth / 7;
            dataGridView1.Columns[4].Width = iWidth / 7;
            dataGridView1.Columns[5].Width = iWidth / 7 - 10;
            dataGridView1.Columns[6].Width = iWidth / 7 - 20;
        }
        private void ViewInit()
        {
            //cmbAddrType.DataSource = Enum.GetNames(typeof(PlcMemory));
            //cmbDataType.DataSource = Enum.GetNames(typeof(DataType));

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
                if (_plcDriver.omronFinsAPI.bConnectOmronPLC)
                {
                    _plcDriver.omronFinsAPI.DisconnectToOmronPLC();
                }
                else
                {
                    if (!_plcDriver.omronFinsAPI.ConnectToOmronPLC(_plcData.IP, 9600,null))
                    {
                        throw new Exception();
                    }
                }
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
        private bool UniqueCheck(int cmdindex, string strName, string strAddr, PlcMemory memory, DataType dataType)
        {
            //string strName,SoftElemType elementtype,int startaddress
            if (string.IsNullOrEmpty(strName))
            {
                MessageBox.Show("Name is empty", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return false;
            }
            foreach (PlcScanItems item in _plcData.listScanItems)
            {
                if (cmdindex == 0)
                {
                    if (strName.Equals(item.strName) && (memory == item.AddressType) && (strAddr == item.Address) && (dataType == item.DataType))
                    {
                        MessageBox.Show("Name/Address has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                    if (!JudgeNumber.isPositiveUINT1632(strAddr) && dataType != DataType.BIT)
                    {
                        MessageBox.Show("Number should be unsigned interger/bitaddress should be 0.00", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                }
                else if (cmdindex == 1)
                {
                    if (strName.Equals(item.strName))
                    {
                        MessageBox.Show("Name has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                    if (!JudgeNumber.isPositiveUINT1632(strAddr) && dataType != DataType.BIT)
                    {
                        MessageBox.Show("Number should be unsigned interger/bitaddress should be 0.00", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }

                    if ((memory == item.AddressType) && (strAddr) == item.Address)
                    {
                        MessageBox.Show("Address has duplicate item", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                        return false;
                    }
                }
            }
            return true;
        }
        private string GetNewName()
        {
            string strNewName = "ScanItem_";
            int iNum = 0;
            try
            {
                while (true)
                {
                    if (!_plcData.dicScanItems.ContainsKey(strNewName + iNum.ToString()))
                        break;
                    iNum++;
                }
            }
            catch (Exception)
            {
            }
            return strNewName + iNum.ToString() ;
        }
        private string GetNewAddr()
        {
            int iNum = 1;
            bool bFlag = true;
            try
            {
                while (true)
                {
                    bFlag = true;
                    foreach(PlcScanItems item in _plcData.listScanItems)
                    {
                        if(item.Address.Equals(iNum.ToString()))
                        {
                            bFlag = false;
                            break;
                        }
                    }
                    if (bFlag)
                        break;
                    iNum++;
                }
            }
            catch (Exception)
            {
            }
            return iNum.ToString();
        }
        private bool ItemAdd(string strName, string strAddr, PlcMemory memory, DataType dataType)
        {
            if(!UniqueCheck(1,strName, strAddr, memory, dataType))
            {
                return false;
            }

            try
            {
                PlcScanItems item = new PlcScanItems();
                DataGridViewRow row = new DataGridViewRow();
                item.strName = strName;
                item.Address = strAddr;
                item.AddressType = memory ;
                item.DataType = dataType;

                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewComboBoxCell addrTypeCell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell addrCell = new DataGridViewTextBoxCell();
                DataGridViewComboBoxCell dataTypeCell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell valueCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell indexCell = new DataGridViewTextBoxCell();
                DataGridViewCheckBoxCell refreshchkCell = new DataGridViewCheckBoxCell();

                addrTypeCell.DataSource = Enum.GetNames(typeof(PlcMemory));
                dataTypeCell.DataSource = Enum.GetNames(typeof(DataType));

                addrTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                dataTypeCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                nameCell.Value = item.strName;
                addrTypeCell.Value = item.AddressType.ToString();
                addrCell.Value = item.Address;
                dataTypeCell.Value = item.DataType.ToString();
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

                _plcData.dicScanItems.Add(item.strName, item);
                _plcData.listScanItems.Add(item);

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
                if (dataGridView1.CurrentCell.RowIndex < 0)
                    return false;
                _plcData.dicScanItems.Remove(_plcData.listScanItems[dataGridView1.CurrentCell.RowIndex].strName);
                _plcData.listScanItems.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private void toorBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ItemAdd(GetNewName(), GetNewAddr(), PlcMemory.DM, DataType.INT16))
                    throw new Exception();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Add new item unsuccessful！\r\n" + ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void toolBarBtnRemove_Click(object sender, EventArgs e)
        {
            ItemRemove();
            ShowItems();
        }
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            if (null != HardwareManage.docHardware)
            {
                HardwareManage.docHardware.SaveDoc();
                MessageBox.Show("Successfuly saved.", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                    return;

                dataGridView1.EndEdit();
                if (e.RowIndex >= _plcData.listScanItems.Count)
                {
                    return;
                }
                string strName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string strOldName = _plcData.listScanItems[e.RowIndex].strName;

                string strAddrType = dataGridView1["ColumnAddressType", e.RowIndex].Value.ToString().Trim();
                string strDataType = dataGridView1["ColumnDataType", e.RowIndex].Value.ToString().Trim();

                if(e.ColumnIndex < 4 && !UniqueCheck(0,strName, dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), (PlcMemory)Enum.Parse(typeof(PlcMemory), strAddrType, false), (DataType)Enum.Parse(typeof(DataType), strDataType, false)))
                {
                    ShowItems();
                    return;
                }
                if(_plcData.dicScanItems.ContainsKey(strName) && e.ColumnIndex == 0)
                {
                    throw new Exception();
                }

                _plcData.listScanItems[e.RowIndex].strName = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                _plcData.listScanItems[e.RowIndex].Address = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                _plcData.listScanItems[e.RowIndex].Index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                _plcData.listScanItems[e.RowIndex].Refresh = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[6].Value);

                _plcData.listScanItems[e.RowIndex].AddressType = (PlcMemory)Enum.Parse(typeof(PlcMemory), strAddrType, false);
                _plcData.listScanItems[e.RowIndex].DataType = (DataType)Enum.Parse(typeof(DataType), strDataType, false);

                _plcData.dicScanItems.Remove(strOldName);
                _plcData.dicScanItems.Add(strName, _plcData.listScanItems[e.RowIndex]);
            }
            catch (Exception)
            {
                MessageBox.Show("Modefine unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                ShowItems();
            }
        }
        #endregion

        #region Monitor
        private void ScanItems()
        {
            try
            {
                for (int index = 0; index < _plcData.listScanItems.Count; index++)
                {
                    Action action = () =>
                    {
                        if (dataGridView1.Rows.Count != 0)
                        {
                            if (dataGridView1.Rows.Count == index)
                            {
                                dataGridView1.Rows[index - 1].Cells[4].Value = _plcData.listScanItems[index].strValue;
                            }
                            else
                            {
                                dataGridView1.Rows[index].Cells[4].Value = _plcData.listScanItems[index].strValue;
                            }
                        }
                    };
                    this.Invoke(action);
                }

#if false
                string strValue = string.Empty;
                object objTemp = new object();
                for (int index = 0; index < _plcData.listScanItems.Count; index++)
                {
                    strValue = string.Empty;
                    switch(_plcData.listScanItems[index].DataType)
                    {
                        case DataType.BIT:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
                            if ((bool)objTemp)
                                strValue = "1";
                            else
                                strValue = "0";
                            break;
                        case DataType.INT16:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
                            strValue = ((Int16)objTemp).ToString();
                            break;
                        case DataType.UINT16:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
                            strValue = ((UInt16)objTemp).ToString();
                            break;
                        case DataType.UINT32:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
                            strValue = ((UInt32)objTemp).ToString();
                            break;
                        case DataType.INT32:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
                            strValue = ((Int32)objTemp).ToString();
                            break;
                        case DataType.REAL:
                            _plcDriver.omronFinsAPI.ReadSingleElement(_plcData.listScanItems[index], ref objTemp);
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
                    if(!_plcDriver.omronFinsAPI.bConnectOmronPLC)
                    {
                        Action action1 = () =>
                         {
                             labelConnSta.Text = "与PLC连接断开";
                             labelConnSta.ForeColor = Color.DarkRed;
                             btnConn.Text = "Connect";
                         };
                        this.Invoke(action1);

                        ////Auto connect
                        //times++;
                        //MainModule.formMain.formOutput.InsertMessage("与PLC连接断开，正进行第" + times.ToString() + "次重新连接...");
                        //_plcDriver.omronFinsAPI.DisconnectToOmronPLC();
                        //Thread.Sleep(500);
                        //_plcDriver.omronFinsAPI.ConnectToOmronPLC(_plcData.IP,9600);
                        //if(_plcDriver.omronFinsAPI.bConnectOmronPLC)
                        //{
                        //    times = 0;
                        //    MainModule.formMain.formOutput.InsertMessage("连接恢复正常。");
                        //}
                        //else
                        //{
                        //    MainModule.formMain.formOutput.InsertMessage("连接未成功。");
                        //    Thread.Sleep(2000);
                        //}

                        continue;
                    }
                    else
                    {
                        Action action2 = () =>
                        {
                            labelConnSta.Text = "与PLC连接成功";
                            labelConnSta.ForeColor = Color.LightSeaGreen;
                            btnConn.Text = "Disconnect";
                        };
                        this.Invoke(action2);
                        
                        if(chkMonitor.Checked)
                        {
                            ScanItems();
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

#endregion

        #region Export
        private string CreatePlcnameCSFile()
        {
            string strreturn = string.Empty;
            if (_plcData.listScanItems.Count < 1)
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

            foreach (PlcScanItems item in _plcData.listScanItems)
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
        private void toolStripBtnExport_Click(object sender, EventArgs e)
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
            MessageBox.Show("Successfully Exported,", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
        }
        #endregion


        private void panelConfig_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Width = panelConfig.Width - 16;
            dataGridView1.Height = panelConfig.Height - 63 - 20;
            dataGridView1.Location = new Point(8,63);
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 6)
            {
                this.dataGridView1_CellValueChanged(sender, e);
            }
        }
        private void ipAddressControl1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (null == _plcData)
                    return;
                _plcData.IP = ipAddressControl1.Text;
            }
            catch 
            {
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex != 4 || e.Button != MouseButtons.Right)
                {
                    return;
                }
                string strItemName = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(); ;
                FormSetValue formSetValue = new FormSetValue(_plcDriver, _plcData, strItemName);
                formSetValue.StartPosition = FormStartPosition.Manual;
                formSetValue.Location = new Point(e.Location.X + 20, e.Location.Y + 5);
                formSetValue.ShowDialog();
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
