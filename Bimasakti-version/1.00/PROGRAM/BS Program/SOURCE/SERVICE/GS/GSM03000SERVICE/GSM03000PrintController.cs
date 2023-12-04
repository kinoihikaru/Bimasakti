using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM03000Back;
using GSM03000Common.DTOs;
using GSM03000Common.Loggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GSM03000SERVICE
{
    public class GSM03000PrintController : R_ReportControllerBase
    {
        private LoggerGSM03000Print _Gsm03000loggerPrint;
        private R_ReportFastReportBackClass _ReportCls;
        private GSM03000PrintParamDTO _AllOtherChargesParameter;

        #region instantiate
        public GSM03000PrintController(ILogger<LoggerGSM03000Print> logger)
        {
            //Initial and Get Logger
            LoggerGSM03000Print.R_InitializeLogger(logger);
            _Gsm03000loggerPrint = LoggerGSM03000Print.R_GetInstanceLogger();

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports","GSM03000.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllOtherChargesParameter));
            pcDataSourceName = "ResponseDataModel";
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO AllOtherChargesPost(GSM03000PrintParamDTO poParameter)
        {
            R_Exception loException = new R_Exception();
            _Gsm03000loggerPrint.LogInfo("Start AllOtherChargesPost");
            GSM03000PrintLogKeyDTO loCache =null;
            R_DownloadFileResultDTO loRtn = null;

            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GSM03000PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _Gsm03000loggerPrint.LogInfo("Set GUID Param AllOtherChargesPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GSM03000PrintLogKeyDTO>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Gsm03000loggerPrint.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Gsm03000loggerPrint.LogInfo("End AllOtherChargesPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamOtherChargesGet(string pcGuid)
        {
            R_Exception loException = new R_Exception();
            GSM03000PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            _Gsm03000loggerPrint.LogInfo("Start AllStreamOtherChargesGet");

            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GSM03000PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllOtherChargesParameter = loResultGUID.poParam;

                _Gsm03000loggerPrint.LogInfo("Read File Report AllStreamOtherChargesGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Gsm03000loggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _Gsm03000loggerPrint.LogInfo("End AllStreamOtherChargesGet");

            return loRtn;
        }

        #region Helper
        private GSM03000ResultWithBaseHeaderPrintDTO GenerateDataPrint(GSM03000PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            GSM03000ResultWithBaseHeaderPrintDTO loRtn = new GSM03000ResultWithBaseHeaderPrintDTO();

            try
            {
                var loCls = new GSM03000Cls(_Gsm03000loggerPrint);

                _Gsm03000loggerPrint.LogInfo("Call Method GetPrintDataResult Report");
                var loCollection = loCls.GetPrintDataResult(poParam);

                // Set Base Header Data
                _Gsm03000loggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Other Charges",
                    CUSER_ID = poParam.CUSER_LOGIN_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                var loData = new GSM03000ResultPrintDTO();

                loData.Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME);
                loData.Data = loCollection;

                _Gsm03000loggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.OtherCharges = loData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Gsm03000loggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
}