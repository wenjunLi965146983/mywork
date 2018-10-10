using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGeneralLib.Forms.TipsForm;

namespace WorldGeneralLib.Table
{
    public class TableManage
    {
        public static string strConfigFile = string.Empty;
        public static TableDoc docTable;
        public static TableDrivers tableDrivers;

        public static FormTableSysView formTableSysView;
        public static FormTableUserView formTableUserView;

        public static void LoadDoc()
        {
            bool bErr = false;
            docTable = TableDoc.LoadObj(ref bErr);
        }

        public static void InitTables()
        {
            tableDrivers = new TableDrivers();
            foreach (KeyValuePair<string, TableData> item in docTable.dicTableData)
            {
                TableDriver driver = new TableDriver();
                tableDrivers.dicDrivers.Add(item.Value.Name, driver);
            }
            foreach (KeyValuePair<string, TableDriver> item in tableDrivers.dicDrivers)
            {
                item.Value.Init(docTable.dicTableData[item.Key]);
            }

        }

        #region Add/Remove/Rename Table
        public static string GetNewTableName()
        {
            string strTemp = "Table_";
            int index = 1;

            if (TableManage.docTable == null)
                return strTemp + index.ToString();

            while (true)
            {
                if (!TableManage.docTable.dicTableData.ContainsKey(strTemp + index.ToString()))
                    return strTemp + index.ToString();
                index++;
            }
        }
        public static string AddNewTable()
        {
            try
            {
                TableData tableData = new TableData(true);
                tableData.Name = GetNewTableName();
                tableData.Text = tableData.Name;
                docTable.listTableData.Add(tableData);
                docTable.dicTableData.Add(tableData.Name, tableData);

                TableDriver driver = new TableDriver();
                tableDrivers.dicDrivers.Add(tableData.Name, driver);
                driver.Init(tableData);

                return tableData.Name;
            }
            catch
            {
                return null;
            }

        }
        public static bool TableRename(string strOldName, string strNewName)
        {
            try
            {
                if (string.IsNullOrEmpty(strOldName) || string.IsNullOrEmpty(strNewName) || docTable == null)
                {
                    return false;
                }
                if (!docTable.dicTableData.ContainsKey(strOldName) || docTable.dicTableData.ContainsKey(strNewName))
                {
                    return false;
                }
                foreach (TableData item in docTable.listTableData)
                {
                    if (item.Name.Equals(strOldName))
                    {
                        item.Name = strNewName;
                        item.Text = strNewName;
                        docTable.dicTableData.Remove(strOldName);
                        docTable.dicTableData.Add(strNewName, item);

                        TableDriver temp = tableDrivers.dicDrivers[strOldName];
                        tableDrivers.dicDrivers.Remove(strOldName);
                        temp.strDriverName = strNewName;
                        tableDrivers.dicDrivers.Add(strNewName, temp);
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool RemoveTable(string strTableName)
        {
            if (string.IsNullOrEmpty(strTableName))
                return false;

            FormTips formTips = new FormTips(-1, true);
            formTips.SetTipsText("确定要移除平台" + strTableName + "吗？\r\n此操作不可撤销。");
            if (DialogResult.Yes != formTips.ShowDialog())
            {
                return false;
            }

            try
            {
                TableManage.docTable.dicTableData.Remove(strTableName);
                TableManage.docTable.listTableData.Clear();
                foreach (KeyValuePair<string, TableData> item in TableManage.docTable.dicTableData)
                {
                    TableManage.docTable.listTableData.Add(item.Value);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
