using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using LMM01000BACK;
using LMM01000COMMON;
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
using System.Reflection;

namespace LMM01000SERVICE
{
    public class LMM01000PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private LMM01000PrintParamDTO _AllUtilityParameter;
        private LoggerLMM01000Print _LoggerPrint;
        private readonly ActivitySource _activitySource;

        #region instantiate
        public LMM01000PrintController(ILogger<LoggerLMM01000Print> logger)
        {
            //Initial and Get Logger
            LoggerLMM01000Print.R_InitializeLogger(logger);
            _LoggerPrint = LoggerLMM01000Print.R_GetInstanceLogger();
            _activitySource = LMM01000PrintActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM01000PrintController));

            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            pcFileTemplate = System.IO.Path.Combine("Reports", "LMM01000UtilityCharges.frx");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            poData.Add(GenerateDataPrint(_AllUtilityParameter));
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
        public R_DownloadFileResultDTO AllUtilityChargesPost(LMM01000PrintParamDTO poParameter)
        {
            using Activity activity = _activitySource.StartActivity("AllUtilityChargesPost");
            R_Exception loException = new R_Exception();
            _LoggerPrint.LogInfo("Start AllUtilityChargesPost");
            LMM01000PrintLogKeyDTO<LMM01000PrintParamDTO> loCache = null;

            R_DownloadFileResultDTO loRtn = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();
                loCache = new LMM01000PrintLogKeyDTO<LMM01000PrintParamDTO>
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                // Set Guid Param 
                _LoggerPrint.LogInfo("Set GUID Param AllUtilityChargesPost");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<LMM01000PrintLogKeyDTO<LMM01000PrintParamDTO>>(loCache));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllUtilityChargesPost");

            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult AllStreamUtilityChargesGet(string pcGuid)
        {
            using Activity activity = _activitySource.StartActivity("AllStreamUtilityChargesGet");
            R_Exception loException = new R_Exception();
            FileStreamResult loRtn = null;
            LMM01000PrintLogKeyDTO<LMM01000PrintParamDTO> loResultGUID = null;
            _LoggerPrint.LogInfo("Start AllStreamOtherChargesGet");

            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<LMM01000PrintLogKeyDTO<LMM01000PrintParamDTO>>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllUtilityParameter = loResultGUID.poParam;

                _LoggerPrint.LogInfo("Read File Report AllStreamOtherChargesGet");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(peExport: R_FileType.PDF), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LoggerPrint.LogError(loException);
            }
            loException.ThrowExceptionIfErrors();
            _LoggerPrint.LogInfo("End AllStreamOtherChargesGet");

            return loRtn;
        }

        #region Helper
        private LMM01000ResultWithBaseHeaderPrintDTO GenerateDataPrint(LMM01000PrintParamDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GenerateDataPrint");
            var loEx = new R_Exception();
            LMM01000ResultWithBaseHeaderPrintDTO loRtn = new LMM01000ResultWithBaseHeaderPrintDTO();

            try
            {
                LMM01000ResultPrintDTO loData = new LMM01000ResultPrintDTO()
                {
                    Title = "Utility Charges",
                    Header = string.Format("{0} - {1}", poParam.CPROPERTY_ID, poParam.CPROPERTY_NAME),
                    Column = new LMM01000ColumnPrintDTO()
                };

                var loCls = new LMM01000Cls(_LoggerPrint);

                _LoggerPrint.LogInfo("Call Method GetListDataPrint Report");
                var loCollection = loCls.GetListDataPrint(poParam);

                
                _LoggerPrint.LogInfo("Grouping Report");
                var loTempData = loCollection
                .GroupBy(item => new { item.CCHARGES_TYPE_DESCR, item.CCHARGES_TYPE })
                .Select(group => new LMM01000TopPrintDTO
                {
                    CCHARGES_TYPE_DESCR = group.Key.CCHARGES_TYPE_DESCR,
                    DataCharges = group
                        .GroupBy(headerGroup => new
                        {
                            headerGroup.CCHARGES_TYPE,
                            headerGroup.CCHARGES_ID,
                            headerGroup.CCHARGES_NAME,
                            headerGroup.LACTIVE,
                            headerGroup.LACCRUAL,
                            headerGroup.CUTILITY_JRNGRP_CODE,
                            headerGroup.CUTILITY_JRNGRP_NAME,
                            headerGroup.LTAX_EXEMPTION,
                            headerGroup.LOTHER_TAX,
                            headerGroup.CTAX_EXEMPTION_CODE,
                            headerGroup.NTAX_EXEMPTION_PCT,
                            headerGroup.COTHER_TAX_ID,
                            headerGroup.LWITHHOLDING_TAX,
                            headerGroup.CWITHHOLDING_TAX_TYPE,
                            headerGroup.CWITHHOLDING_TAX_ID
                        })
                        .Select(headerGroup => new LMM01000HeaderPrintDTO
                        {
                            CCHARGES_TYPE = headerGroup.Key.CCHARGES_TYPE,
                            CCHARGES_ID = headerGroup.Key.CCHARGES_ID,
                            CCHARGES_NAME = headerGroup.Key.CCHARGES_NAME,
                            LACTIVE = headerGroup.Key.LACTIVE,
                            LACCRUAL = headerGroup.Key.LACCRUAL,
                            CUTILITY_JRNGRP_CODE = headerGroup.Key.CUTILITY_JRNGRP_CODE,
                            CUTILITY_JRNGRP_NAME = headerGroup.Key.CUTILITY_JRNGRP_NAME,
                            LTAX_EXEMPTION = headerGroup.Key.LTAX_EXEMPTION,
                            LOTHER_TAX = headerGroup.Key.LOTHER_TAX,
                            NTAX_EXEMPTION_PCT = headerGroup.Key.NTAX_EXEMPTION_PCT,
                            LWITHHOLDING_TAX = headerGroup.Key.LWITHHOLDING_TAX,
                            CTAX_EXEMPTION_CODE = headerGroup.Key.CTAX_EXEMPTION_CODE,
                            COTHER_TAX_ID = headerGroup.Key.COTHER_TAX_ID,
                            CWITHHOLDING_TAX_TYPE = headerGroup.Key.CWITHHOLDING_TAX_TYPE,
                            CWITHHOLDING_TAX_ID = headerGroup.Key.CWITHHOLDING_TAX_ID,
                            DetailCharges = headerGroup
                                .Select(detail => new LMM01000DetailPrintDTO
                                {
                                    CGOA_CODE = detail.CGOA_CODE,
                                    CGOA_NAME = detail.CGOA_NAME,
                                    LDEPARTMENT_MODE = detail.LDEPARTMENT_MODE,
                                    CGLACCOUNT_NO = detail.CGLACCOUNT_NO,
                                    CGLACCOUNT_NAME = detail.CGLACCOUNT_NAME
                                })
                                .ToList()
                        })
                        .ToList()
                })
                .ToList();

                loData.Data = loTempData;

                _LoggerPrint.LogInfo("Set BaseHeader Report");
                var loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = poParam.CCOMPANY_ID.ToUpper(),
                    CPRINT_NAME = "Utility Charges",
                    CUSER_ID = poParam.CUSER_ID.ToUpper(),
                };
                var loBaseHeader = loCls.GetBaseHeaderLogoCompany(poParam);
                loParam.BLOGO_COMPANY = loBaseHeader.CLOGO;

                _LoggerPrint.LogInfo("Set Data Report");
                loRtn.BaseHeaderData = loParam;
                loRtn.UtilitiesData = loData;
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