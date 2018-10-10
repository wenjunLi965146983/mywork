using System;
using System.Collections.Generic;
using NLog;
using WorldGeneralLib.DataBaseApplication;
using System.Data;
using System.Data.OleDb;
//using VisionzMaster;
using System.Windows.Forms;

namespace WorldGeneralLib.Functions
{
    //public delegate void delegateFunc(ref bool complete, ref short result,ref short exresult);
    public delegate void delegateFunc(short index);
    public static class ToolSet
    {
        public static Logger PLCLog = LogManager.GetLogger("PLCLog");
        public static Logger SECSGEMLog = LogManager.GetLogger("SECSGEMLog");
        public static Logger ScannerLog = LogManager.GetLogger("ScannerLog");
        public static Logger VisionLog = LogManager.GetLogger("VisionLog");

        //public static VisionzMasterMainForm mVisionForm;

        public static string iniFilePath = ".//Parameter/Configuration.ini";
        public static IniHelper inihelper = new IniHelper(iniFilePath);

        public static DataBaseHelper databasehelper;
        public static string timeformart = "yyyy/MM/dd HH:mm:ss:fff";

        public static bool debugmode = true;

        public static List<bool> GetBitList(short value)
        {
            var list = new List<bool>(16);
            for (short i = 0; i <= 15; i++)
            {
                short val = (short)(1 << i);
                list.Add((value & val) == val);
            }
            return list;
        }

        public static bool GetBitValue(int value, ushort index)
        {
            if (index > 15) return false;
            short val = (short)(1 << index);
            return (value & val) == val;
        }

        public static short SetBitValue(short value, ushort index, bool bitValue)
        {
            if (index > 15) return 0;
            short val = (short)(1 << index);
            return bitValue ? (short)(value | val) : (short)(value & ~val);
        }

        public static DataTable ExcelToDS(string Path)
        {
            DataTable dt = new DataTable();
            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + Path + ";" + "Extended Properties=Excel 8.0;";
                OleDbConnection conn = new OleDbConnection(strConn);
                string strExcel = "select * from [Event$]";
                OleDbCommand cmd = new OleDbCommand(strExcel, conn);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, "table1");
                dt = ds.Tables["table1"];
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                return dt;
            }
        }
    }
}
