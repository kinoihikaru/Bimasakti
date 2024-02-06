using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using LMM01000BACK;
using LMM01000COMMON;
using LMM01000COMMON.Print;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace LMM01000SERVICE
{
    public class LMM01040PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private LMM01040PrintParamDTO _AllRateGUParameter;
        private LoggerLMM01040Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public LMM01040PrintController(ILogger<LoggerLMM01040Print> logger)
        {
            //Initial and Get Logger
            LoggerLMM01040Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerLMM01040Print.R_GetInstanceLogger();
            _activitySource = LMM01040PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM01040PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01040RateGU.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllRateGUParameter));
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
        public R_DownloadFileResultDTO AllRateGUReportPost(LMM01040PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllRateGUReportPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllRateGUReportPost");
            LMM01000PrintLogKeyDTO<LMM01040PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new LMM01000PrintLogKeyDTO<LMM01040PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllRateGUReportPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<LMM01000PrintLogKeyDTO<LMM01040PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllRateECReportPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamRateGUReportGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamRateGUReportGet");
            R_Exception loException = new R_Exception();
            LMM01000PrintLogKeyDTO<LMM01040PrintParamDTO> loResultGUID = null;
            FileStreamResult loRtn = null;
            _LoggerPrint.LogInfo("Start AllStreamRateGUReportGet");
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01000PrintLogKeyDTO<LMM01040PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllRateGUParameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamRateGUReportGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamRateGUReportGet");

            return loRtn;
        }

        #region Helper
        private LMM01040ResultWithBaseHeaderPrintDTO GenerateDataPrint(LMM01040PrintParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            LMM01040ResultWithBaseHeaderPrintDTO loRtn = new LMM01040ResultWithBaseHeaderPrintDTO();

            try
            {
                LMM01040ResultPrintDTO loData = new LMM01040ResultPrintDTO()
                {
                    Title = "General Usage Rate",
                    Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME),
                };

                var loCls = new LMM01040Cls(_LoggerPrint);

                var loHeaderParam = R_Utility.R_ConvertObjectToObject<LMM01040PrintParamDTO, LMM01040DTO>(poParam);

                _LoggerPrint.LogInfo("Call Method GetReportRateGU Report");
                var loHeaderCollection = loCls.GetReportRateGU(loHeaderParam);

                loData.HeaderData = loHeaderCollection;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "General Usage",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.RateGU = loData;

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LoggerPrint.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
   
}