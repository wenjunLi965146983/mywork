using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
namespace WorldGeneralLib.PLC
{
    public partial class AlarmManagerForm : Form
    {
        private AlarmDate m_alarmData;
        public AlarmManagerForm()
        {
            InitializeComponent();
        }
        public AlarmManagerForm(AlarmDate alarmData)
        {
            InitializeComponent();
            m_alarmData = alarmData;
        }

        private void AlarmManagerForm_Load(object sender, EventArgs e)
        {
            if (m_alarmData == null)
                return;
            for (int i = 0; i < m_alarmData.listItem.Count; i++)
            {
                dataGridViewData.Rows.Add(new object[] { i, m_alarmData.listItem[i].strPlcName, m_alarmData.listItem[i].strAddress, m_alarmData.listItem[i].strMachine, m_alarmData.listItem[i].strAlarmMes });
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AlarmItem alarmItem = new AlarmItem();
            if (dataGridViewData.Rows.Count > 0)
            {
                alarmItem.strPlcName = dataGridViewData.Rows[dataGridViewData.Rows.Count - 1].Cells[1].Value.ToString();
                alarmItem.strAddress = dataGridViewData.Rows[dataGridViewData.Rows.Count - 1].Cells[2].Value.ToString();
                alarmItem.strMachine = dataGridViewData.Rows[dataGridViewData.Rows.Count - 1].Cells[3].Value.ToString();
                alarmItem.strAlarmMes = dataGridViewData.Rows[dataGridViewData.Rows.Count - 1].Cells[4].Value.ToString();
            }
            m_alarmData.listItem.Add(alarmItem);
            dataGridViewData.Rows.Add(new object[] { m_alarmData.listItem.Count - 1, alarmItem.strPlcName, alarmItem.strAddress, alarmItem.strMachine, alarmItem.strAlarmMes });
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int iSelectRow = 0;
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                return;
            }
            iSelectRow = dataGridViewData.SelectedRows[0].Index;
            AlarmItem alarmItem = new AlarmItem();
            alarmItem.strPlcName = dataGridViewData.Rows[iSelectRow].Cells[1].Value.ToString();
            alarmItem.strAddress = dataGridViewData.Rows[iSelectRow].Cells[2].Value.ToString();
            alarmItem.strMachine = dataGridViewData.Rows[iSelectRow].Cells[3].Value.ToString();
            alarmItem.strAlarmMes = dataGridViewData.Rows[iSelectRow].Cells[4].Value.ToString();
            m_alarmData.listItem.Insert(iSelectRow, alarmItem);
            dataGridViewData.Rows.Insert(iSelectRow, new object[] { iSelectRow, alarmItem.strPlcName, alarmItem.strAddress, alarmItem.strMachine, alarmItem.strAlarmMes });
            for (int i = iSelectRow; i < m_alarmData.listItem.Count; i++)
            {
                dataGridViewData.Rows[i].Cells[0].Value = i;
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int iSelectRow = 0;
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                return;
            }
            iSelectRow = dataGridViewData.SelectedRows[0].Index;
            AlarmItem alarmItem = new AlarmItem();
            m_alarmData.listItem.RemoveAt(iSelectRow);
            dataGridViewData.Rows.RemoveAt(iSelectRow);
            for (int i = iSelectRow; i < m_alarmData.listItem.Count; i++)
            {
                dataGridViewData.Rows[i].Cells[0].Value = i;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            m_alarmData.listItem.Clear();
            for (int i = 0; i < dataGridViewData.Rows.Count; i++)
            {
                AlarmItem alarmItem = new AlarmItem();
                if (dataGridViewData.Rows[i].Cells[1].Value == null)
                    dataGridViewData.Rows[i].Cells[1].Value = " ";
                alarmItem.strPlcName = dataGridViewData.Rows[i].Cells[1].Value.ToString();
                if (dataGridViewData.Rows[i].Cells[2].Value == null)
                    dataGridViewData.Rows[i].Cells[2].Value = " ";
                alarmItem.strAddress = dataGridViewData.Rows[i].Cells[2].Value.ToString();
                if (dataGridViewData.Rows[i].Cells[3].Value == null)
                    dataGridViewData.Rows[i].Cells[3].Value = " ";
                alarmItem.strMachine = dataGridViewData.Rows[i].Cells[3].Value.ToString();
                if (dataGridViewData.Rows[i].Cells[4].Value == null)
                    dataGridViewData.Rows[i].Cells[4].Value = " ";
                alarmItem.strAlarmMes = dataGridViewData.Rows[i].Cells[4].Value.ToString();
                m_alarmData.listItem.Add(alarmItem);
            }
            m_alarmData.SaveDoc();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int iSelectRow = 0;
            if (dataGridViewData.SelectedRows.Count < 1)
            {
                return;
            }
            iSelectRow = dataGridViewData.SelectedRows[0].Index;
            AlarmItem AlarmItem = new AlarmItem();
            int iIndex = 0;

            iIndex = dataGridViewData.SelectedRows[0].Index;
            AlarmItem.strPlcName = m_alarmData.listItem[iIndex].strPlcName;
            AlarmItem.strAddress = m_alarmData.listItem[iIndex].strAddress;
            AlarmItem.strAlarmMes = m_alarmData.listItem[iIndex].strAlarmMes;

            Clipboard.SetData("AlarmItem", AlarmItem);
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (dataGridViewData.SelectedRows.Count < 1)
            {
                return;
            }
            AlarmItem alarmItem = (AlarmItem)Clipboard.GetData("AlarmItem");
            if (alarmItem == null)
            {
                return;
            }
            int iSelectRow = dataGridViewData.SelectedRows[0].Index;
            m_alarmData.listItem.Insert(iSelectRow + 1, alarmItem);

            dataGridViewData.Rows.Insert(iSelectRow + 1, new object[] { iSelectRow + 1, alarmItem.strPlcName, alarmItem.strAddress, alarmItem.strAlarmMes });


            for (int i = 0; i < m_alarmData.listItem.Count; i++)
            {
                dataGridViewData.Rows[i].Cells[0].Value = i;
            }
            //Clipboard.Clear();

        }

        private void dataGridViewData_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            m_alarmData.listItem[e.RowIndex].strPlcName = dataGridViewData.Rows[e.RowIndex].Cells[1].Value.ToString();
            m_alarmData.listItem[e.RowIndex].strAddress = dataGridViewData.Rows[e.RowIndex].Cells[2].Value.ToString();
            m_alarmData.listItem[e.RowIndex].strAlarmMes = dataGridViewData.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void buttonLoadCSV_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDLG = new OpenFileDialog();
            fileDLG.Title = "Open Excel File";
            fileDLG.Filter = "CSV files (*.csv)|*.csv";
            if (fileDLG.ShowDialog() == DialogResult.OK)
            {
                string filename = System.IO.Path.GetFileName(fileDLG.FileName);
                string path = System.IO.Path.GetDirectoryName(fileDLG.FileName);
                string ExcelFile = @path + "\\" + filename;
                if (!File.Exists(ExcelFile))
                {
                    MessageBox.Show(String.Format("File {0} does not Exist", ExcelFile), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    return;
                }
                StreamReader reader = new StreamReader(ExcelFile, System.Text.Encoding.Default);
                dataGridViewData.Rows.Clear();
                string strLine = reader.ReadLine();
                while (strLine != null)
                {
                    strLine = reader.ReadLine();
                    if (strLine == null)
                        break;
                    string[] str = strLine.Split(',');
                    int a = dataGridViewData.Rows.Add(1);
                    if (str.Length == 5)
                    {
                        for (int i = 0; i < str.Length; i++)
                        {
                            dataGridViewData[i, a].Value = str[i].Trim();
                        }
                    }
                }
                reader.Close();
            }
        }

        private void buttonSaveCSV_Click(object sender, EventArgs e)
        {
            if (dataGridViewData.Rows.Count == 0)
            {
                MessageBox.Show("No data available!", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.FileName = null;
                saveFileDialog.Title = "Save path of the file to be exported";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Stream myStream = saveFileDialog.OpenFile();
                    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                    string strLine = "";
                    try
                    {
                        //Write in the headers of the columns.
                        for (int i = 0; i < dataGridViewData.ColumnCount; i++)
                        {
                            if (i > 0)
                                strLine += ",";
                            strLine += dataGridViewData.Columns[i].HeaderText;
                        }
                        strLine.Remove(strLine.Length - 1);
                        sw.WriteLine(strLine);
                        strLine = "";
                        //Write in the content of the columns.
                        for (int j = 0; j < dataGridViewData.Rows.Count; j++)
                        {
                            strLine = "";
                            for (int k = 0; k < dataGridViewData.Columns.Count; k++)
                            {
                                if (k > 0)
                                    strLine += ",";
                                if (dataGridViewData.Rows[j].Cells[k].Value == null)
                                    strLine += "";
                                else
                                {
                                    string m = dataGridViewData.Rows[j].Cells[k].Value.ToString().Trim();
                                    strLine += m.Replace(",", "，");
                                }
                            }
                            strLine.Remove(strLine.Length - 1);
                            sw.WriteLine(strLine);
                        }
                        sw.Close();
                        myStream.Close();
                        MessageBox.Show("Data has been exported to：" + saveFileDialog.FileName.ToString(), "Exporting Completed", MessageBoxButtons.OK, MessageBoxIcon.Information
                            ,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Exporting Error", MessageBoxButtons.OK, MessageBoxIcon.Information,MessageBoxDefaultButton.Button1,MessageBoxOptions.ServiceNotification);
                    }
                }
            }

        }

        private void dataGridViewData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }
}
