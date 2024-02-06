using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLR00200BACK;
using GLR00200COMMON;
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
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Xml.Linq;

namespace GLR00200SERVICE
{
    public class GLR00200PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLR00200PrintParamDTO _AllGLR00200Parameter;
        private LoggerGLR00200Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public GLR00200PrintController(ILogger<LoggerGLR00200Print> logger)
        {
            //Initial and Get Logger
            LoggerGLR00200Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerGLR00200Print.R_GetInstanceLogger();
            _activitySource = GLR00200PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLR00200PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLR00200.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllGLR00200Parameter));
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
        public R_DownloadFileResultDTO AllGLAccountLedgerPost(GLR00200PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllGLAccountLedgerPost");
            _LoggerPrint.LogInfo("Start AllGLAccountLedgerPost");
            R_Exception loException = new R_Exception();
            GLR00200PrintLogKeyDTO<GLR00200PrintParamDTO> loCache = null;
            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new GLR00200PrintLogKeyDTO<GLR00200PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllGLAccountLedgerPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GLR00200PrintLogKeyDTO<GLR00200PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            _LoggerPrint.LogInfo("End AllGLAccountLedgerPost");
            loException.ThrowExceptionIfErrors();
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamGLAccountLedgersGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamGLAccountLedgersGet");
            R_Exception loException = new R_Exception();
            GLR00200PrintLogKeyDTO<GLR00200PrintParamDTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamGLAccountLedgersGet");
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GLR00200PrintLogKeyDTO<GLR00200PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllGLR00200Parameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamGLAccountLedgersGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamGLAccountLedgersGet");

            return loRtn;
        }

        #region Helper
        private GLR00200ResultWithBaseHeaderPrintDTO GenerateDataPrint(GLR00200PrintParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            GLR00200ResultWithBaseHeaderPrintDTO loRtn = new GLR00200ResultWithBaseHeaderPrintDTO();

            try
            {
                var loCls = new GLR00200Cls(_LoggerPrint);

                _LoggerPrint.LogInfo("Call Method GetSummaryGLLedger And GetDetailGLLEdger Report");
                var loSummaryData = loCls.GetSummaryGLLedger(poParam);
                var loDetailData = loCls.GetDetailGLLEdger(poParam);

                GLR00200ResultPrintDTO loData = new GLR00200ResultPrintDTO();

                _LoggerPrint.LogInfo("Grouping Report");

                // Grouping logic
                var loTempResultDetail = loDetailData.GroupBy(g =>
                  new
                  {
                      g.CACCOUNT,
                      g.CDBCR,
                      g.CBSIS,
                      g.NBEGIN_BALANCE,
                      g.CGLACCOUNT_NO,
                      g.CGLACCOUNT_NAME
                  },
                  g => new GLR00204DTO()
                  {
                      CGLACCOUNT_NO = g.CGLACCOUNT_NO,

                      CREF_DATE = DateTime.ParseExact(g.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                      CSOURCE_MODULE = g.CSOURCE_MODULE,
                      CREF_NO = g.CREF_NO,
                      CDETAIL_DESC = g.CDETAIL_DESC,
                      CCENTER_CODE = g.CCENTER_CODE,
                      CDOCUMENT_NO = g.CDOCUMENT_NO,
                      CCUSTOMER_SUPPLIER = g.CCUSTOMER_SUPPLIER,
                      NLDEBIT_AMOUNT = g.NLDEBIT_AMOUNT,
                      NCREDIT_AMOUNT = g.NCREDIT_AMOUNT,
                      NEND_BALANCE = g.NEND_BALANCE,
                      CDOCUMENT_DATE = !string.IsNullOrWhiteSpace(g.CDOCUMENT_DATE)
                                      ? DateTime.ParseExact(g.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy")
                                      : "",

                      CREC_ID = g.CREC_ID,
                      CREF_PRD = g.CREF_PRD,
                      NDEBIT_AMOUNT = g.NDEBIT_AMOUNT,
                      NBDEBIT_AMOUNT = g.NBDEBIT_AMOUNT,
                      NLCREDIT_AMOUNT = g.NLCREDIT_AMOUNT,
                      NBCREDIT_AMOUNT = g.NBCREDIT_AMOUNT,
                      CCURRENCY = g.CCURRENCY,
                      CFROM_ACCOUNT_NO = g.CFROM_ACCOUNT_NO,
                      CTO_ACCOUNT_NO = g.CTO_ACCOUNT_NO,
                      CFROM_CENTER_CODE = g.CFROM_CENTER_CODE,
                      CTO_CENTER_CODE = g.CTO_CENTER_CODE,
                      CPRINT_METHOD_NAME = g.CPRINT_METHOD_NAME,
                      CINCLUDE_AUDIT_JOURNAL = g.CINCLUDE_AUDIT_JOURNAL
                  }).Select(s => new GLR00202DTO
                  {
                      CACCOUNT = s.Key.CACCOUNT,
                      CDBCR = s.Key.CDBCR,
                      CBSIS = s.Key.CBSIS,
                      CGLACCOUNT_NO = s.Key.CGLACCOUNT_NO,
                      CGLACCOUNT_NAME = s.Key.CGLACCOUNT_NAME,
                      NBEGIN_BALANCE = s.Key.NBEGIN_BALANCE,
                      NDEBIT_AMOUNT_SUM = s.Sum(item => item.NDEBIT_AMOUNT),
                      NCREDIT_AMOUNT_SUM = s.Sum(item => item.NCREDIT_AMOUNT),
                      PeriodDetailData = s.ToList()
                  }).ToList();


                _LoggerPrint.LogInfo("Set BaseHeader Report");
                // set Base Header Common
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "GL Account Ledger",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                // Set ResponseModelData
                loData.SummaryData = loSummaryData;
                loData.DetailData = loTempResultDetail;

                _LoggerPrint.LogInfo("Set Data Report");
                // Set All Return Data
                loRtn.BaseHeaderData = loParam;
                loRtn.GLR = loData;
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