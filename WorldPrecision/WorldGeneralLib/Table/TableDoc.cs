using System;
using System.Collections.Generic;
using System.Linq;
using System.IO.Ports;
using System.ComponentModel;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace WorldGeneralLib.Table
{
    public class TableDoc
    {
        public List<TableData> listTableData;

        [XmlIgnore]
        public Dictionary<string, TableData> dicTableData;

        public TableDoc()
        {
            listTableData = new List<TableData>();
            dicTableData = new Dictionary<string, TableData>();
        }

        public static TableDoc LoadObj(ref bool bErr)
        {
            TableDoc pDoc;
            XmlSerializer xml = new XmlSerializer(typeof(TableDoc));
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(@".//Parameter/Table/TableDoc.xml");
                pDoc = (TableDoc)xml.Deserialize(fs);
                fs.Close();
                pDoc.dicTableData = pDoc.listTableData.ToDictionary(p => p.Name);
                foreach (TableData table in pDoc.listTableData)
                {
                    table.dicTableAxisItem = table.ListTableAxesItems.ToDictionary(p => p.Name);
                    table.dicTablePosItem = table.ListTablePosItems.ToDictionary(p => p.Name);
                }

                return pDoc;
            }
            catch// (Exception ex)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                pDoc = new TableDoc();
                //throw;
            }
            bErr = true;
            return pDoc;
        }

        public static TableDoc LoadObj(string strFullPath, ref bool bErr)
        {
            TableDoc pDoc;
            XmlSerializer xml = new XmlSerializer(typeof(TableDoc));
            FileStream fs = null;
            try
            {
                fs = File.OpenRead(strFullPath);
                pDoc = (TableDoc)xml.Deserialize(fs);
                fs.Close();
                pDoc.dicTableData = pDoc.listTableData.ToDictionary(p => p.Name);
                foreach (TableData table in pDoc.listTableData)
                {
                    table.dicTableAxisItem = table.ListTableAxesItems.ToDictionary(p => p.Name);
                    table.dicTablePosItem = table.ListTablePosItems.ToDictionary(p => p.Name);
                }

                TableManage.strConfigFile = strFullPath;
                return pDoc;
            }
            catch// (Exception ex)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                pDoc = new TableDoc();
                //throw;
            }
            bErr = true;
            return pDoc;
        }
        public bool SaveDoc()
        {
            FileStream fs = null;
            try
            {
                if (!Directory.Exists(@".//Parameter/Table/"))
                {
                    Directory.CreateDirectory(@".//Parameter/Table/");
                }

                fs = new FileStream(@".//Parameter/Table/TableDoc.xml", FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                XmlSerializer xml = new XmlSerializer(typeof(TableDoc));
                xml.Serialize(fs, this);
                fs.Close();

                return true;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                return false;
            }
        }
        public bool SaveDoc(string strFullPath)
        {
            FileStream fs = null;
            try
            {
                fs = new FileStream(strFullPath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
                XmlSerializer xml = new XmlSerializer(typeof(TableDoc));
                xml.Serialize(fs, this);
                fs.Close();

                return true;
            }
            catch (Exception)
            {
                if (null != fs)
                {
                    fs.Close();
                }
                return false;
            }
        }
    }
}
