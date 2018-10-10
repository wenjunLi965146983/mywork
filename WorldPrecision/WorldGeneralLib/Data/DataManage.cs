using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorldGeneralLib.Data
{
    public class DataManage
    {
        public static DataDoc docData;
        public static FormDataSysView formDataSysView;
        public static FormDataUserView formDataUserView;

        public static void LoadData()
        {
            docData = DataDoc.LoadObj();
        }

        static public string StrValue(string strGroupName, string strItemName)
        {
            string strReturn = "";
            try
            {
                strReturn = docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue.ToString();
            }
            catch
            {
                if (docData.dicDataGroup.ContainsKey(strGroupName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("不存在组名字为:" + strGroupName + "的参数据");
                    DataGroup newGroup = new DataGroup();
                    docData.dicDataGroup.Add(strGroupName, newGroup);
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    DataItem newItem = new DataItem();
                    docData.dicDataGroup[strGroupName].dicDataItem.Add(strItemName, newItem);
                   newItem.strItemName = strItemName;
                   newItem.dataType = DataType.String;
                    newItem.objValue = "";
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType != DataType.String)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是字符");
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType = DataType.String;
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue = "";
                }
            }
            return strReturn;
        }
        static public int IntValue(string strGroupName, string strItemName)
        {
            int iReturn = 0;
            try
            {
                iReturn = int.Parse(docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue.ToString());
            }
            catch
            {
                if (docData.dicDataGroup.ContainsKey(strGroupName) == false)
                {

                    MainModule.alarmManage.InsertAlarm("不存在组名字为:" + strGroupName + "的参数据");
                    DataGroup newGroup = new DataGroup();
                    docData.dicDataGroup.Add(strGroupName, newGroup);
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    DataItem newItem = new DataItem();
                    docData.dicDataGroup[strGroupName].dicDataItem.Add(strItemName, newItem);
                    newItem.strItemName = strItemName;
                    newItem.dataType = DataType.Int;
                    newItem.objValue = 0;
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType != DataType.Int)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是整形");
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType = DataType.Int;
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue = 0;
                }
            }
            return iReturn;
        }
        static public short ShortValue(string strGroupName, string strItemName)
        {
            short sReturn = 0;
            try
            {
                sReturn = short.Parse(docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue.ToString());
            }
            catch
            {
                if (docData.dicDataGroup.ContainsKey(strGroupName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("不存在组名字为:" + strGroupName + "的参数据");
                    DataGroup newGroup = new DataGroup();
                    docData.dicDataGroup.Add(strGroupName, newGroup);
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    DataItem newItem = new DataItem();
                    docData.dicDataGroup[strGroupName].dicDataItem.Add(strItemName, newItem);
                    newItem.strItemName = strItemName;
                    newItem.dataType = DataType.Short;
                    newItem.objValue = 0;
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType != DataType.Short)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是Short");
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType = DataType.Short;
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue = 0;
                }
            }
            return sReturn;
        }
        static public double DoubleValue(string strGroupName, string strItemName)
        {
            double dReturn = 0.0;
            try
            {
                dReturn = double.Parse(docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue.ToString());
            }
            catch
            {

                if (docData.dicDataGroup.ContainsKey(strGroupName) == false)
                {

                    MainModule.alarmManage.InsertAlarm("不存在组名字为:" + strGroupName + "的参数据");
                    DataGroup newGroup = new DataGroup();
                    docData.dicDataGroup.Add(strGroupName, newGroup);
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    DataItem newItem = new DataItem();
                    docData.dicDataGroup[strGroupName].dicDataItem.Add(strItemName, newItem);
                    newItem.strItemName = strItemName;
                    newItem.dataType = DataType.Double;
                    newItem.objValue = 0.0;
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType != DataType.Double)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + ":" + strItemName + "的参数" + "浮点型的");
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType = DataType.Double;
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue = 0.0;
                }
            }
            return dReturn;
        }
        static public bool BOOLValue(string strGroupName, string strItemName)
        {
            bool bReturn = false;
            try
            {
                bReturn = bool.Parse(docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue.ToString());
            }
            catch
            {
                if (docData.dicDataGroup.ContainsKey(strGroupName) == false)
                {

                    MainModule.alarmManage.InsertAlarm("不存在组名字为:" + strGroupName + "的参数据");
                    DataGroup newGroup = new DataGroup();
                    docData.dicDataGroup.Add(strGroupName, newGroup);
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName) == false)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + "不存在名字为:" + strItemName + "的参数");
                    DataItem newItem = new DataItem();
                    docData.dicDataGroup[strGroupName].dicDataItem.Add(strItemName, newItem);
                    newItem.strItemName = strItemName;
                    newItem.dataType = DataType.Bool;
                    newItem.objValue = false;
                }
                if (docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType != DataType.Bool)
                {
                    MainModule.alarmManage.InsertAlarm("参数组：" + strGroupName + ":" + strItemName + "的参数" + "不是布尔型");
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].dataType = DataType.Bool;
                    docData.dicDataGroup[strGroupName].dicDataItem[strItemName].objValue = false;
                }
            }
            return bReturn;
        }

        /// <summary>
        /// 查看参数是否存在
        /// </summary>
        /// <param name="strGroupName"></param>
        /// <param name="strItemName"></param>
        /// <returns></returns>
        public static bool CheckItemExist(string strGroupName, string strItemName)
        {
            try
            {
                if (null == docData)
                    return false;
                if (!docData.dicDataGroup.ContainsKey(strGroupName))
                    return false;
                if (!docData.dicDataGroup[strGroupName].dicDataItem.ContainsKey(strItemName))
                    return false;
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }

        #region Add/Remove/Rename data group
        public static string GetNewGroupName(string strPrefix)
        {
            string strTemp = strPrefix;
            int index = 1;

            if (DataManage.docData == null)
                return strTemp + index.ToString();

            while (true)
            {
                if (!DataManage.docData.dicDataGroup.ContainsKey(strTemp + index.ToString()))
                    return strTemp + index.ToString();
                index++;
            }
        }
        public static string AddNewDataGroup()
        {
            try
            {
                if (null == DataManage.docData)
                {
                    throw new Exception("系统异常，对象未实例化。");
                }

                string strName = GetNewGroupName("NewGroup_");
                DataGroup newGroup = new DataGroup();
                newGroup.strGroupName = strName;
                DataManage.docData.dicDataGroup.Add(strName, newGroup);
                DataManage.docData.listDataGroup.Add(newGroup);
                return strName;
            }
            catch 
            {
                return null;
            }
        }
        public static bool RemoveDataGroup(string strGroupName)
        {
            try
            {
                if(!docData.dicDataGroup.ContainsKey(strGroupName))
                {
                    return true;
                }
                docData.dicDataGroup.Remove(strGroupName);
                docData.listDataGroup.Clear();
                foreach(KeyValuePair<string, DataGroup> item in docData.dicDataGroup)
                {
                    docData.listDataGroup.Add(item.Value);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool RenameDataGroup(string strOldName, string strNewName)
        {
            if(docData == null)
            {
                return false;
            }
            try
            {
                if (!docData.dicDataGroup.ContainsKey(strOldName))
                {
                    return false;
                }
                foreach(DataGroup item in docData.listDataGroup)
                {
                    if(item.strGroupName.Equals(strOldName))
                    {
                        item.strGroupName = strNewName;
                        docData.dicDataGroup.Remove(strOldName);
                        docData.dicDataGroup.Add(strNewName, item);
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
        #region Export
        public static string GetContent(string strGroupName)
        {
            string strContent = "";
            try
            {
                if (string.IsNullOrEmpty(strGroupName))
                {
                    throw new Exception("名称不能为空!");
                }
                strContent += "using System.Text;\r\n";
                strContent += "using System.Threading.Tasks;\r\n";
                strContent += "\r\n";
                strContent += "namespace WorldGeneralLib.Data.NameItems\r\n";
                strContent += "{\r\n";
                strContent += "   public static class " + strGroupName + "\r\n";
                strContent += "   {\r\n";

                foreach (DataGroup item in DataManage.docData.listDataGroup)
                {
                    strContent += "      public static string ";
                    strContent += "DataGroup" + item.strGroupName;
                    strContent += " = ";
                    strContent += "\"" + item.strGroupName + "\"";
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
        public static  bool WriteToFileAsCS()
        {
            string strContent = GetContent("DataGroups");
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
                File.WriteAllText(strPath + "DataGroups.cs", strContent);
                return true;
            }
            catch (Exception)
            {
            }
            return false;
        }
        public static void DataGroupExport()
        {
            if (WriteToFileAsCS())
            {
                string strFilePath = Application.StartupPath + "\\Export\\Data\\";
                DialogResult dRet;
                dRet = MessageBox.Show("导出成功！\r\n是否打开文件夹？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
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
    }
}
