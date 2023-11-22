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
using System.Reflection;

namespace LMM01000SERVICE
{
    public class LMM01020PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private LMM01020PrintParamDTO _AllRateWGParameter;
        private LoggerLMM01020Print _LoggerPrint;

        #region instantiate
        public LMM01020PrintController(ILogger<LoggerLMM01020Print> logger)
        {
            //Initial and Get Logger
            LoggerLMM01020Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerLMM01020Print.R_GetInstanceLogger();

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01020RateWG.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllRateWGParameter));
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
        public R_DownloadFileResultDTO AllRateWGReportPost(LMM01020PrintParamDTO poParameter)
        {
            _LoggerPrint.LogInfo("Start AllRateWGReportPost");
            R_Exception loException = new R_Exception();
            LMM01000PrintLogKeyDTO<LMM01020PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new LMM01000PrintLogKeyDTO<LMM01020PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllRateWGReportPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<LMM01000PrintLogKeyDTO<LMM01020PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllRateWGReportPost");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamRateWGReportGet(string pcGuid)
        {
            LMM01000PrintLogKeyDTO<LMM01020PrintParamDTO> loResultGUID = null;
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllStreamRateWGReportGet");
            FileStreamResult loRtn = null;
            try
            {

                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01000PrintLogKeyDTO<LMM01020PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllRateWGParameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamRateWGReportGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamRateWGReportGet");

            return loRtn;
        }

        #region Helper
        private LMM01020ResultWithBaseHeaderPrintDTO GenerateDataPrint(LMM01020PrintParamDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01020ResultWithBaseHeaderPrintDTO loRtn = new LMM01020ResultWithBaseHeaderPrintDTO();

            try
            {
                LMM01020ResultPrintDTO loData = new LMM01020ResultPrintDTO()
                {
                    Title = "Water and Gas",
                    Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME),
                };

                var loCls = new LMM01020Cls(_LoggerPrint);

                var loHeaderParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01020DTO>(poParam);
                var loDetailParam = R_Utility.R_ConvertObjectToObject<LMM01020PrintParamDTO, LMM01021DTO>(poParam);

                _LoggerPrint.LogInfo("Call Method GetHDReportRateWG And GetDetailReportRateWG Report");
                var loHeaderCollection = loCls.GetHDReportRateWG(loHeaderParam);
                var loDetailCollection = loCls.GetDetailReportRateWG(loDetailParam);

                loHeaderCollection.CRATE_WG_LIST = new List<LMM01021DTO>();
                loHeaderCollection.CRATE_WG_LIST.AddRange(loDetailCollection);

                loData.HeaderData = loHeaderCollection;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Water and Gas",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.RateWG = loData;

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