using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorldPrecision
{
    public class MESLocalData
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string strBarcode;
        /// <summary>
        /// 清洗时间
        /// </summary>
        public string strTime;
        /// <summary>
        /// 设备资源号
        /// </summary>
        public string strResourceID;
        /// <summary>
        /// MES结果
        /// </summary>
        public string strMESResult;
        /// <summary>
        /// 清洗结果
        /// </summary>
        public string strCleanResult;

        /// <summary>
        /// 清洗喷淋压力
        /// </summary>
        public string strSprPre;
        /// <summary>
        /// 清洗喷淋压力设定上限值
        /// </summary>
        public string strSprPreMaxSettingVal;
        /// <summary>
        /// 清洗喷淋压力设定下限值
        /// </summary>
        public string strSprPreMinSettingVal;

        /// <summary>
        /// 清洗喷淋时间
        /// </summary>
        public string strSprTime;
        /// <summary>
        /// 清洗喷淋时间设定值
        /// </summary>
        public string strSprTimeSettingVal;

        /// <summary>
        /// 干燥压力
        /// </summary>
        public string strDryPre;
        /// <summary>
        /// 干燥压力设定上限值
        /// </summary>
        public string strDryPreMaxSettingVal;
        /// <summary>
        /// 干燥压力设定下限值
        /// </summary>
        public string strDryPreMinSettingVal;

        /// <summary>
        /// 干燥时间
        /// </summary>
        public string strDryTime;
        /// <summary>
        /// 干燥时间设定值
        /// </summary>
        public string strDryTimeSettingVal;

        /// <summary>
        /// 吹残液时间
        /// </summary>
        public string strBlowingTime;
        /// <summary>
        /// 吹残液时间设定值
        /// </summary>
        public string strBlowingTimeSettingVal;

        /// <summary>
        /// 油温
        /// </summary>
        public string strOilTemp;
        /// <summary>
        /// 油温设定值
        /// </summary>
        public string strOilTempSettingVal;
    }
    public class WriteMesFile
    {
        public static object objLock = new object();
        public static string strFilePath = "C:\\MES\\MESData\\SFC\\";
        public static string strTempFilePath = "C:\\temp\\";

        /// <summary>
        /// 清洗结果写出到CSV文件
        /// </summary>
        /// <param name="data">MES 本地数据内容</param>
        public static void WriteToCsvFile(MESLocalData data)
        {
            lock(objLock)
            {
                try
                {
                    string strPath = strFilePath + System.DateTime.Now.ToString("yyyyMMdd") + "\\";
                    if(!Directory.Exists(strPath))
                    {
                        Directory.CreateDirectory(strPath);
                    }

                    if (!Directory.Exists(strTempFilePath))
                    {
                        Directory.CreateDirectory(strTempFilePath);
                    }

                    data.strBarcode = data.strBarcode.Replace('\0',' ').Trim();

                    //文件标头 19
                    string strProInfoFileHead = "电芯条码,生产时间,设备ID,MES,清洗结果,清洗喷淋压力(mPa),清洗喷淋压力设定上限(mPa),清洗喷淋压力设定下限(mPa),干燥压力(kPa),干燥压力设定上限(kPa),干燥压力设定下限(kPa),清洗喷淋时间(s),清洗喷淋时间设定值(s),吹残液时间(s),吹残液时间设定值(s),干燥时间(s),干燥时间设定值(s),油温(℃),油温设定值(℃)\r\n";
                    string strData = data.strBarcode + "," +
                                     data.strTime + "," +
                                     data.strResourceID + "," +
                                     data.strMESResult + "," +
                                     data.strCleanResult + "," +
                                     data.strSprPre + "," +
                                     data.strSprPreMaxSettingVal + "," +
                                     data.strSprPreMinSettingVal + "," +
                                     data.strDryPre + "," +
                                     data.strDryPreMaxSettingVal + "," +
                                     data.strDryPreMinSettingVal + "," +
                                     data.strSprTime + "," +
                                     data.strSprTimeSettingVal + "," +
                                     data.strBlowingTime + "," +
                                     data.strBlowingTimeSettingVal + "," +
                                     data.strDryTime + "," +
                                     data.strDryTimeSettingVal + "," +
                                     data.strOilTemp + "," +
                                     data.strOilTempSettingVal + "\r\n";

                    string strFileName = data.strBarcode  +"_"+ System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".CSV";
                    strData = strProInfoFileHead + strData;

                    StreamWriter sw = new StreamWriter(strTempFilePath + strFileName, true, UnicodeEncoding.GetEncoding("GB2312"));
                    sw.Write(strData);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();

                    if (File.Exists(strFilePath + strFileName))
                    {
                        File.Delete(strFilePath + strFileName);
                    }
                    File.Move(strTempFilePath + strFileName, strPath + strFileName);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
