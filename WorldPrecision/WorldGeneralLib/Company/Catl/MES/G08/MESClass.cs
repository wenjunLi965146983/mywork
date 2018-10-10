using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CATL.WebReference;
using CATL.WebReferenc2;
using WorldGeneralLib.Data;
using WorldGeneralLib.Data.NameItems;
using System.Net;
using WorldGeneralLib.TaskBase;

namespace WorldGeneralLib.Company.Catl.MES.G08
{
    public class MESRes
    {
        public string strErrMsg;
        public bool bSuccess;

        public miCheckSFCstatusResponse miCheckResponse;
        public dataCollectForSfcExResponse dataCollectResponse;

        public MESRes()
        {
            strErrMsg = string.Empty;
            bSuccess = false;

            miCheckResponse = new miCheckSFCstatusResponse();
            dataCollectResponse = new dataCollectForSfcExResponse();
        }
    }

    //G08清洗喷淋参数
    public class SprayingInfo
    {
        /// <summary>
        /// 清洗喷淋压力
        /// </summary>
        public string strSprayingPressure;
        /// <summary>
        /// 清洗喷淋时间
        /// </summary>
        public string strSprayingTime;
        /// <summary>
        /// 干燥压力
        /// </summary>
        public string strDryingPressure;
        /// <summary>
        /// 吹残液时间
        /// </summary>
        public string strBlowingResidualFluidTime;
        /// <summary>
        /// 干燥时间
        /// </summary>
        public string strDryingTime;
        /// <summary>
        /// 油温
        /// </summary>
        public string strOilTemp;
    }
    public class MESClass
    {
        public string strUser;
        public string strPassword;

        //miCheckSfcStatus
        public MiCheckSFCStatusServiceService miCheckSFCStatusService;
        public miCheckSFCstatus miCheckSFCsta;
        public changeSFCStatusRequest changeStatusRequest;
        public miCheckSFCstatusResponse miCheckSFCstatusRes;
        miCommonResponse miCommon;

        //MachineIntegrationServiceService
        public MachineIntegrationServiceService machineIntegrationService;
        public dataCollectForSfcEx dataCollect;
        public sfcDcExRequest sfcDataCollectRequest;

        public sfcDcExResponse sfcRes;
        public dataCollectForSfcExResponse dataCollectForSfcExRes;
        public MESClass()
        {
            miCheckSFCStatusService = new MiCheckSFCStatusServiceService();
            miCheckSFCsta = new miCheckSFCstatus();
            changeStatusRequest = new changeSFCStatusRequest();
            miCheckSFCstatusRes = new miCheckSFCstatusResponse();
            miCommon = new miCommonResponse();

            machineIntegrationService = new MachineIntegrationServiceService();
            dataCollect = new dataCollectForSfcEx();
            sfcDataCollectRequest = new sfcDcExRequest();
            sfcRes = new sfcDcExResponse();
            dataCollectForSfcExRes = new dataCollectForSfcExResponse();
        }

        #region Cleaning maching interface
        /// <summary>
        /// 电芯进站校验
        /// </summary>
        /// <param name="strSFC">Barcode</param>
        /// <returns>OK - MES return =0 </returns>
        /// <returns>NG - MEW return >0 </returns>
        public MESRes CheckSfcStatus(string strSFC)
        {
            MESRes mesRes = new MESRes();
            string strStartTime = "";
            string strEndTime = "";
            HiPerfTimer hTimer = new HiPerfTimer();

            mesRes.bSuccess = false;
            mesRes.strErrMsg = "调用失败";
            try
            {
                hTimer.Start();
                strStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssfff");
                string strUser = DataManage.StrValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.User);
                string strPassword = DataManage.StrValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.Password);
                int iTimeout = DataManage.IntValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.TimeOut);
                miCheckSFCStatusService.Credentials = new NetworkCredential(strUser, strPassword);
                iTimeout = (iTimeout < 10 || iTimeout > 60000) ? 10000 : iTimeout;
                miCheckSFCStatusService.Timeout = iTimeout;

                changeStatusRequest.site = DataManage.StrValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.site);   //设备所在站点
                changeStatusRequest.operation = DataManage.StrValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.operation);   //工位
                changeStatusRequest.operationRevision = DataManage.StrValue(DataGroups.DataGroupMESCheckSfcParam, DataItemMESCheckSfcParam.operationRevision);  //工位版本
                changeStatusRequest.sfc = strSFC;                              //电芯号

                miCheckSFCsta.ChangeSFCStatusRequest = changeStatusRequest;
                miCheckSFCstatusRes = miCheckSFCStatusService.miCheckSFCstatus(miCheckSFCsta);
                mesRes.miCheckResponse = miCheckSFCstatusRes;
                mesRes.bSuccess = true;
            }
            catch (Exception ex)
            {
                mesRes.bSuccess = false;
                mesRes.strErrMsg = ex.ToString();
            }
            strEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssfff");
            int iTime = (int)(hTimer.Duration * 1000);

            MESLog.WriteMiCheckSfcStatusLog(miCheckSFCsta,mesRes,strStartTime,strEndTime,iTime);
            return mesRes;
        }

        /// <summary>
        /// 电芯出站，向MES传输需求的参数值
        /// </summary>
        /// <param name="strSFC">Barcode</param>
        /// <param name="sprayingInfo">清洗参数</param>
        /// <returns></returns>
        public MESRes DataCollectForSfcEx(string strSFC, SprayingInfo sprayingInfo)
        {
            string strStartTime = "";
            string strEndTime = "";
            HiPerfTimer hTimer = new HiPerfTimer();
            MESRes mesRes = new MESRes();

            try
            {
                hTimer.Start();
                strStartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssfff");
                string strUser = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.User);
                string strPassword = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.Password);
                int iTimeout = DataManage.IntValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.TimeOut);
                machineIntegrationService.Credentials = new NetworkCredential(strUser, strPassword);
                iTimeout = (iTimeout < 10 || iTimeout > 60000) ? 10000 : iTimeout;
                machineIntegrationService.Timeout = iTimeout;

                sfcDataCollectRequest.site = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.site);
                sfcDataCollectRequest.user = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.user);
                sfcDataCollectRequest.operation = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.operation);
                sfcDataCollectRequest.operationRevision = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.operationRevision); ;
                sfcDataCollectRequest.resource = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.resource);
                sfcDataCollectRequest.activityId = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.activityId);
                sfcDataCollectRequest.dcGroup = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.dcGroup); ;
                sfcDataCollectRequest.dcGroupRevision = DataManage.StrValue(DataGroups.DataGroupMESDataCollectParam, DataItemMESDataCollectParam.dcGroupRevision); ;
                sfcDataCollectRequest.modeProcessSfc = ModeProcessSfc.MODE_PASS_SFC_POST_DC;
                sfcDataCollectRequest.sfc = strSFC;

                int index = 0;
                int iArrayCount = 0;
                foreach(DataItem item in DataManage.docData.dicDataGroup[DataGroups.DataGroupParametricDataArray].listDataItem)
                {
                   iArrayCount++;
                }

                sfcDataCollectRequest.parametricDataArray = new machineIntegrationParametricData[iArrayCount];
                foreach (DataItem item in DataManage.docData.dicDataGroup[DataGroups.DataGroupParametricDataArray].listDataItem)
                {
                    sfcDataCollectRequest.parametricDataArray[index] = new machineIntegrationParametricData();
                    sfcDataCollectRequest.parametricDataArray[index].dataType = ParameterDataType.NUMBER;
                    sfcDataCollectRequest.parametricDataArray[index].name = item.strItemName;
                    sfcDataCollectRequest.parametricDataArray[index].value = item.objValue.ToString();

                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQXPRES"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strSprayingPressure;
                    }
                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQXTIME"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strSprayingTime;
                    }
                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQXGZYL"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strDryingPressure;
                    }
                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQXCCYSJ"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strBlowingResidualFluidTime;
                    }
                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQCGZSJ"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strDryingTime;
                    }
                    if (sfcDataCollectRequest.parametricDataArray[index].name.Equals("DXQXYW"))
                    {
                        sfcDataCollectRequest.parametricDataArray[index].value = sprayingInfo.strOilTemp;
                    }
                    index++;
                }

                sfcDataCollectRequest.ncCodeArray = new nonConfirmCodeArray[1];  //?
                sfcDataCollectRequest.sfc = strSFC;

                dataCollect.SfcDcExRequest = sfcDataCollectRequest;
                dataCollectForSfcExRes = machineIntegrationService.dataCollectForSfcEx(dataCollect);
                mesRes.dataCollectResponse = dataCollectForSfcExRes;

                mesRes.bSuccess = true;
                //strArrayMsg[4] = dataCollectForSfcExRes.@return.message.Replace(",", "，").Replace("\r\n", " ");
            }
            catch (Exception ex)
            {
                mesRes.bSuccess = false;
                mesRes.strErrMsg = ex.ToString();
            }

            strEndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ssfff");
            int iTime = (int)(hTimer.Duration * 1000);

            MESLog.WriteDataCollectForSfcExLog(dataCollect, mesRes, strStartTime, strEndTime, iTime);
            return mesRes;
        }

        #endregion
    }
}
