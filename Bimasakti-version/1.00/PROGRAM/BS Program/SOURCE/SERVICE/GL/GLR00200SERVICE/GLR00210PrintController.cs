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
    public class GLR00210PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GLR00200PrintParamDTO _AllGLR00200Parameter;
        private LoggerGLR00210Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public GLR00210PrintController(ILogger<LoggerGLR00210Print> logger)
        {
            //Initial and Get Logger
            LoggerGLR00200Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerGLR00210Print.R_GetInstanceLogger();
            _activitySource = GLR00210PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLR00210PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "GLR00210.frx");
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
        private GLR00210ResultWithBaseHeaderPrintDTO GenerateDataPrint(GLR00200PrintParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            GLR00210ResultWithBaseHeaderPrintDTO loRtn = new GLR00210ResultWithBaseHeaderPrintDTO();

            try
            {
                var loCls = new GLR00210Cls();

                _LoggerPrint.LogInfo("Call Method GetSummaryGLLedger And GetDetailGLLEdger Report");
                var loSummaryData = loCls.GetSummaryGLLedger(poParam);
                var loDetailData = loCls.GetDetailGLLEdger(poParam);

                //Set string empty if Duplicate
                var duplicates = loSummaryData.GroupBy(x => x.CGLACCOUNT_NO)
                                    .Where(group => group.Count() > 1)
                                    .SelectMany(group => group.Skip(1));
                foreach (var duplicate in duplicates)
                {
                    duplicate.CGLACCOUNT_NO = "";
                    duplicate.CGLACCOUNT_NAME = "";
                }

                GLR00210ResultPrintDTO loData = new GLR00210ResultPrintDTO();

                _LoggerPrint.LogInfo("Grouping Report");

                // Grouping logic
                var groupedData = loDetailData
                    .GroupBy(item =>
                        new { item.CGLACCOUNT_NO, item.CACCOUNT, item.CDBCR, item.CBSIS, item.NBEGIN_BALANCE, item.CGLACCOUNT_NAME })
                    .Select(group => new GLR00212DTO
                    {
                        CGLACCOUNT_NO = group.Key.CGLACCOUNT_NO,
                        CGLACCOUNT_NAME = group.Key.CGLACCOUNT_NAME,
                        CACCOUNT = group.Key.CACCOUNT,
                        CDBCR = group.Key.CDBCR,
                        CBSIS = group.Key.CBSIS,
                        AccountDetailData = group
                            .GroupBy(subItem => new { subItem.CCENTER_CODE, subItem.CCENTER_NAME, subItem.NBEGIN_BALANCE })
                            .Select(subGroup => new GLR00213DTO
                            {
                                CCENTER_CODE = subGroup.Key.CCENTER_CODE,
                                CCENTER_NAME = subGroup.Key.CCENTER_NAME,
                                NBEGIN_BALANCE = subGroup.Key.NBEGIN_BALANCE,
                                CGLACCOUNT_NO = subGroup.FirstOrDefault().CGLACCOUNT_NO,
                                CenterDetailData = subGroup.Select(subSubItem => new GLR00214DTO
                                {
                                    NEND_BALANCE = subSubItem.NEND_BALANCE,
                                    CREC_ID = subSubItem.CREC_ID,
                                    CREF_NO = subSubItem.CREF_NO,
                                    CREF_DATE = string.IsNullOrWhiteSpace(subSubItem.CREF_DATE) ? "" : DateTime.ParseExact(subSubItem.CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                                    CREF_PRD = subSubItem.CREF_PRD,
                                    NDEBIT_AMOUNT = subSubItem.NDEBIT_AMOUNT,
                                    NCREDIT_AMOUNT = subSubItem.NCREDIT_AMOUNT,
                                    CDETAIL_DESC = subSubItem.CDETAIL_DESC,
                                    CDOCUMENT_DATE = string.IsNullOrWhiteSpace(subSubItem.CDOCUMENT_DATE) ? "" : DateTime.ParseExact(subSubItem.CDOCUMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy"),
                                    CSOURCE_MODULE = subSubItem.CSOURCE_MODULE,
                                    CCUSTOMER_SUPPLIER = subSubItem.CCUSTOMER_SUPPLIER,
                                    CDOCUMENT_NO = subSubItem.CDOCUMENT_NO,
                                    CGLACCOUNT_NO = subSubItem.CGLACCOUNT_NO,
                                    CCENTER_CODE = subSubItem.CCENTER_CODE,
                                }).ToList()
                            }).ToList()
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
                loData.HeaderData = loSummaryData.FirstOrDefault();
                loData.SummaryData = loSummaryData;
                loData.DetailData = groupedData;

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