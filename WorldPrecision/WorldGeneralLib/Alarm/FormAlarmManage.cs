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

namespace WorldGeneralLib.Alarm
{
    public partial class FormAlarmManage : Form
    {
        public FormAlarmManage()
        {
            InitializeComponent();
        }

        #region Load
        private void ShowItems()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (AlarmData item in MainModule.alarmManage.docAlarm.listAlarmItems)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                    DataGridViewComboBoxCell alarmSrcCell = new DataGridViewComboBoxCell();
                    DataGridViewTextBoxCell msgCell = new DataGridViewTextBoxCell();
                    DataGridViewTextBoxCell remarkCell = new DataGridViewTextBoxCell();

                    alarmSrcCell.Items.Clear();
                    alarmSrcCell.Items.Add("PC");
                    foreach (HardwareData data in HardwareManage.docHardware.listHardwareData)
                    {
                        alarmSrcCell.Items.Add(data.Name);
                    }
                    alarmSrcCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                    keyCell.Value = item.AlarmKey;
                    nameCell.Value = item.AlarmName;
                    alarmSrcCell.Value = alarmSrcCell.Items.Contains(item.AlarmSrc) ? item.AlarmSrc : "PC";
                    msgCell.Value = item.AlarmMsg;
                    remarkCell.Value = item.AlarmRemark;

                    row.Cells.Add(keyCell);
                    row.Cells.Add(nameCell);
                    row.Cells.Add(alarmSrcCell);
                    row.Cells.Add(msgCell);
                    row.Cells.Add(remarkCell);
                    dataGridView1.Rows.Add(row);
                }
            }
            catch (Exception)
            {
            }
        }
        private void SetDataGridViewStyle()
        {
            int iWidth = panelConfig.Width;

            dataGridView1.Columns[0].Width = iWidth / 10;
            dataGridView1.Columns[1].Width = iWidth / 10;
            dataGridView1.Columns[2].Width = iWidth / 10;
            dataGridView1.Columns[3].Width = iWidth * 4/ 10;
            dataGridView1.Columns[4].Width = iWidth * 3/ 10 - 10;
        }
        private void FormAlarmManage_Load(object sender, EventArgs e)
        {
            ShowItems();
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
        }
        #endregion

        #region Alarm items edit
        private string GetNewKey()
        {
            try
            {
                int iTemp = 1;
                bool bFlag = false;
                while (iTemp <= 999)
                {
                    if (!MainModule.alarmManage.docAlarm.dicAlarmItems.ContainsKey(iTemp.ToString()))
                    {
                        bFlag = true;
                        foreach(AlarmData item in MainModule.alarmManage.docAlarm.listAlarmItems)
                        {
                            if(item.AlarmName.Equals("Alarm_"+iTemp.ToString()))
                            {
                                bFlag = false;
                                break;
                            }
                        }
                        if(bFlag)
                        {
                            return iTemp.ToString();
                        }   
                    }
                        
                    iTemp++;
                }
            }
            catch (Exception)
            {
            }
            return null ;

        }
        private bool ItemRemove()
        {
            try
            {
                if (dataGridView1.CurrentCell.RowIndex < 0)
                    return false;
                MainModule.alarmManage.docAlarm.dicAlarmItems.Remove(MainModule.alarmManage.docAlarm.listAlarmItems[dataGridView1.CurrentCell.RowIndex].AlarmName);
                MainModule.alarmManage.docAlarm.listAlarmItems.RemoveAt(dataGridView1.CurrentCell.RowIndex);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        private bool ItemAdd()
        {
            try
            {
                string strKey = GetNewKey();
                if(string.IsNullOrEmpty(strKey))
                {
                    return false;
                }

                AlarmData alarmData = new AlarmData();
                DataGridViewRow row = new DataGridViewRow();
                alarmData.AlarmKey = strKey;
                alarmData.AlarmName = "Alarm_" + strKey;
                alarmData.AlarmSrc = "PC";
                alarmData.AlarmMsg = " ";
                alarmData.AlarmRemark = " ";

                DataGridViewTextBoxCell keyCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell nameCell = new DataGridViewTextBoxCell();
                DataGridViewComboBoxCell alarmSrcCell = new DataGridViewComboBoxCell();
                DataGridViewTextBoxCell msgCell = new DataGridViewTextBoxCell();
                DataGridViewTextBoxCell remarkCell = new DataGridViewTextBoxCell();

                alarmSrcCell.Items.Clear();
                alarmSrcCell.Items.Add("PC");
                foreach(HardwareData item in HardwareManage.docHardware.listHardwareData)
                {
                    alarmSrcCell.Items.Add(item.Name);
                }
                alarmSrcCell.DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;

                keyCell.Value = alarmData.AlarmKey;
                nameCell.Value = alarmData.AlarmName;
                alarmSrcCell.Value = alarmData.AlarmSrc;
                msgCell.Value = alarmData.AlarmMsg;
                remarkCell.Value = alarmData.AlarmRemark;

                row.Cells.Add(keyCell);
                row.Cells.Add(nameCell);
                row.Cells.Add(alarmSrcCell);
                row.Cells.Add(msgCell);
                row.Cells.Add(remarkCell);
                dataGridView1.Rows.Add(row);

                MainModule.alarmManage.docAlarm.dicAlarmItems.Add(alarmData.AlarmKey, alarmData);
                MainModule.alarmManage.docAlarm.listAlarmItems.Add(alarmData);

                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        private bool ItemSave()
        {
            if(null != MainModule.alarmManage.docAlarm)
            {
                if(MainModule.alarmManage.docAlarm.SaveDoc())
                {
                    MessageBox.Show("Saved successful !", "", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                }
                else
                {
                    MessageBox.Show("Saved unsuccessful !", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return false;
                }
            }
            return true;
        }

        #region Export as cs file
        private string GetContent()
        {
            string strContent = "";
            try
            {
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.Alarm\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + "AlarmKeys" + "\r\n";
                strContent += "   {\r\n";

                foreach (AlarmData item in MainModule.alarmManage.docAlarm.listAlarmItems)
                {
                    strContent += "      public static string ";
                    strContent += item.AlarmName;
                    strContent += " = ";
                    strContent += "\"" + item.AlarmKey + "\"";
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
            string strContent = GetContent();
            if (string.IsNullOrEmpty(strContent))
            {
                return false;
            }

            try
            {
                string strPath = Application.StartupPath + "\\Export\\Alarm Items\\";
                if (!Directory.Exists(strPath))
                {
                    Directory.CreateDirectory(strPath);
                }
                File.WriteAllText(strPath + "AlarmKeys.cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        private void ExportAsCsFile()
        {
            if (WriteToFileAsCS())
            {
                string strFilePath = Application.StartupPath + "\\Export\\Alarm Items\\";
                DialogResult dRet;
                 dRet = MessageBox.Show("Exported successful！\r\nDo you want to open the floder？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
                MessageBox.Show("Exported unsuccessful！", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        #endregion
        #region Import xml file
        private bool ImportXmlFile()
        {
            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }
            return true;
        }
        #endregion
        #endregion

        #region Events
        private void toolBarBtnSave_Click(object sender, EventArgs e)
        {
            ItemSave();
        }

        private void toorBtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ItemAdd())
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
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportAsCsFile();
        }
        private void toolStripMenuItemExprotCsFile_Click(object sender, EventArgs e)
        {
            ExportAsCsFile();
        }
        private void panelConfig_SizeChanged(object sender, EventArgs e)
        {
            dataGridView1.Width = panelConfig.Width - 16;
            dataGridView1.Height = panelConfig.Height - 63 - 20;
            dataGridView1.Location = new Point(8, 63);
        }

        private void dataGridView1_SizeChanged(object sender, EventArgs e)
        {
            SetDataGridViewStyle();
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                if (e.RowIndex >= MainModule.alarmManage.docAlarm.listAlarmItems.Count)
                {
                    return;
                }
                string strOldName = MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmName;
                string strOldKey = MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmKey;
                string strName = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string strKey = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                string strAlarmSrc = dataGridView1["ColumnAlarmSrc", e.RowIndex].Value.ToString().Trim();

                if(string.IsNullOrEmpty(strKey) || string.IsNullOrEmpty(strName))
                {
                    
                    throw new Exception();
                }

                if ((MainModule.alarmManage.docAlarm.dicAlarmItems.ContainsKey(strKey) && e.ColumnIndex == 0))
                {
                    throw new Exception();
                }
                if(1 == e.ColumnIndex)
                {
                    foreach(AlarmData item in MainModule.alarmManage.docAlarm.listAlarmItems)
                    {
                        if(item.AlarmName.Equals(strName))
                        {
                            throw new Exception();
                        }
                    }
                }

                MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmKey = strKey;
                MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmName = strName;
                MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmSrc = strAlarmSrc;
                MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmMsg = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex].AlarmRemark = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

                MainModule.alarmManage.docAlarm.dicAlarmItems.Remove(strOldKey);
                MainModule.alarmManage.docAlarm.dicAlarmItems.Add(strKey, MainModule.alarmManage.docAlarm.listAlarmItems[e.RowIndex]);
            }
            catch (Exception)
            {
                MessageBox.Show("Modefine unsuccessful.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                ShowItems();
            }
        }
        #endregion

        private void toolStripBtnImport_Click(object sender, EventArgs e)
        {

        }
    }
}
