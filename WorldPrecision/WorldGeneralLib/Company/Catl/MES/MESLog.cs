using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Spire.Xls;
using CATL;
using CATL.WebReference;
using CATL.WebReferenc2;
using WorldGeneralLib.Company.Catl.MES.G08;

namespace WorldGeneralLib.Company.Catl.MES
{
    public static  class MESLog
    {
        #region G08
        public const string strG08MesLogFilePath1 = "D:\\MES\\MESLog\\miCheckSfcStatus\\";
        public const string strG08MesLogFilePath2 = "D:\\MES\\MESLog\\dataCollectForSfcEx\\";
        private static object _objG08FileOperationLock1 = new object();
        private static object _objG08FileOperationLock2 = new object();

        public static void CreateMiCheckSfcStatusLogFile(string strFileName)
        {
            lock(_objG08FileOperationLock1)
            {
                try
                {
                    Workbook wb = new Workbook();
                    wb.SaveToFile(strG08MesLogFilePath1 + strFileName, ExcelVersion.Version2007);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void CreateDataCollectForSfcExLogFile(string strFileName)
        {
            lock (_objG08FileOperationLock2)
            {
                try
                {
                    Workbook wb = new Workbook();
                    wb.SaveToFile(strG08MesLogFilePath2 + strFileName, ExcelVersion.Version2007);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void WriteMiCheckSfcStatusLog(miCheckSFCstatus data, MESRes mesRes,string strStartTime,string strEndTime, int iTime)
        {
            lock (_objG08FileOperationLock1)
            {
                try
                {
                    string strFilename = DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                    if (!Directory.Exists(strG08MesLogFilePath1))
                    {
                        Directory.CreateDirectory(strG08MesLogFilePath1);
                    }
                    if (!File.Exists(strG08MesLogFilePath1 + strFilename))
                    {
                        CreateMiCheckSfcStatusLogFile(strFilename);
                    }

                    Workbook wb = new Workbook();
                    wb.LoadFromFile(strG08MesLogFilePath1 + strFilename);
                    Worksheet sheet1 = wb.Worksheets[0];

                    int rowIndex = sheet1.Range.RowCount;
                    if(rowIndex > 1)
                    {
                        rowIndex++;
                        sheet1.Range["A" + rowIndex.ToString()].Text = " ";
                        sheet1.Range["B" + rowIndex.ToString()].Text = " ";
                        rowIndex++;
                    }

                    string strParam = "{";
                    strParam += "\"site\":" + "\"" + data.ChangeSFCStatusRequest.site + "\"" + ",";
                    strParam += "\"sfc\":" + "\"" + data.ChangeSFCStatusRequest.sfc + "\"" + ",";
                    strParam += "\"operation\":" + "\"" + data.ChangeSFCStatusRequest.operation + "\"" + ",";
                    strParam += "\"operationRevision\":" + "\"" + data.ChangeSFCStatusRequest.operationRevision + "\"}";

                    sheet1.Range["A" + rowIndex.ToString()].Text = "SFC";
                    sheet1.Range["B" + rowIndex.ToString()].Text = data.ChangeSFCStatusRequest.sfc;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用开始时间";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strStartTime;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用传参";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strParam;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用返回时间";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strEndTime;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "耗时(ms)";
                    sheet1.Range["B" + rowIndex.ToString()].Text = iTime.ToString();

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "返回代码";
                    sheet1.Range["B" + rowIndex.ToString()].Text = mesRes.miCheckResponse.@return.code.ToString() ;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "返回信息";
                    sheet1.Range["B" + rowIndex.ToString()].Text = mesRes.miCheckResponse.@return.message.Replace(",", ", ").Replace("\r\n", " ");

                    wb.SaveToFile(strG08MesLogFilePath1+strFilename);
                }
                catch (Exception)
                {
                }
            }
        }

        public static void WriteDataCollectForSfcExLog(dataCollectForSfcEx data, MESRes mesRes, string strStartTime, string strEndTime, int iTime)
        {
            lock (_objG08FileOperationLock2)
            {
                try
                {
                    string strFilename = DateTime.Now.ToString("yyyy-MM-dd") + ".xlsx";
                    if (!Directory.Exists(strG08MesLogFilePath2))
                    {
                        Directory.CreateDirectory(strG08MesLogFilePath2);
                    }
                    if (!File.Exists(strG08MesLogFilePath2 + strFilename))
                    {
                        CreateDataCollectForSfcExLogFile(strFilename);
                    }

                    Workbook wb = new Workbook();
                    wb.LoadFromFile(strG08MesLogFilePath2 + strFilename);
                    Worksheet sheet1 = wb.Worksheets[0];

                    int rowIndex = sheet1.Range.RowCount;
                    if (rowIndex > 1)
                    {
                        rowIndex++;
                        sheet1.Range["A" + rowIndex.ToString()].Text = " ";
                        sheet1.Range["B" + rowIndex.ToString()].Text = " ";
                        rowIndex++;
                    }
                    string strParam = "{";
                    strParam += "\"site\":" + "\"" + data.SfcDcExRequest.site + "\",";
                    strParam += "\"sfc\":" + "\"" + data.SfcDcExRequest.sfc + "\",";
                    strParam += "\"user\":" + "\"" + data.SfcDcExRequest.user + "\",";
                    strParam += "\"operation\":" + "\"" + data.SfcDcExRequest.operation + "\",";
                    strParam += "\"operationRevision\":" + "\"" + data.SfcDcExRequest.operationRevision + "\",";
                    strParam += "\"resource\":" + "\"" + data.SfcDcExRequest.resource + "\",";
                    strParam += "\"activityId\":" + "\"" + data.SfcDcExRequest.activityId + "\",";
                    strParam += "\"dcGroup\":" + "\"" + data.SfcDcExRequest.dcGroup + "\",";
                    strParam += "\"dcGroupRevision\":" + "\"" + data.SfcDcExRequest.dcGroupRevision + "\",";
                    strParam += "\"modeProcessSf\":" + "\"" + data.SfcDcExRequest.modeProcessSfc.ToString() + "\",";
                    strParam += "\"parametricDataArray\":[";

                    for(int index=0; index<data.SfcDcExRequest.parametricDataArray.Length;index++)
                    {
                        strParam += "{\"name\":" + "\"" + data.SfcDcExRequest.parametricDataArray[index].name + "\",";
                        strParam += "\"value\":" + "\"" + data.SfcDcExRequest.parametricDataArray[index].value + "\",";
                        strParam += "\"dataType\":" + "\"" + data.SfcDcExRequest.parametricDataArray[index].dataType.ToString() + "\"}";

                        if (index < data.SfcDcExRequest.parametricDataArray.Length - 1)
                            strParam += ",";
                    }

                    strParam += "]";
                   

                    sheet1.Range["A" + rowIndex.ToString()].Text = "SFC";
                    sheet1.Range["B" + rowIndex.ToString()].Text = data.SfcDcExRequest.sfc;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用开始时间";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strStartTime;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用传参";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strParam;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "接口调用返回时间";
                    sheet1.Range["B" + rowIndex.ToString()].Text = strEndTime;

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "耗时(ms)";
                    sheet1.Range["B" + rowIndex.ToString()].Text = iTime.ToString();

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "返回代码";
                    sheet1.Range["B" + rowIndex.ToString()].Text = mesRes.dataCollectResponse.@return.code.ToString();

                    rowIndex++;
                    sheet1.Range["A" + rowIndex.ToString()].Text = "返回信息";
                    sheet1.Range["B" + rowIndex.ToString()].Text = mesRes.dataCollectResponse.@return.message.Replace(",", "，").Replace("\r\n", " ");

                    wb.SaveToFile(strG08MesLogFilePath2 + strFilename);
                }
                catch (Exception)
                {
                }
            }
        }

        #endregion
    }
}
