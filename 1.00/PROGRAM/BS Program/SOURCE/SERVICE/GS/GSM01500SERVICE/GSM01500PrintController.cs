using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GSM01500BACK;
using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using GSM01500COMMON.DTOs.GSM01500Print;
using GSM01500COMMON.Loggers;
using GSM01500SERVICE.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_CommonFrontBackAPI.Log;
using R_ReportFastReportBack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500SERVICE
{
    public class GSM01500PrintController : R_ReportControllerBase
    {
        private R_ReportFastReportBackClass _ReportCls;
        private GSM01500PrintCenterParameterDTO _AllCenterParameter;
        private LoggerGSM01500Print _logger;

        #region instantiate
        public GSM01500PrintController(ILogger<GSM01500PrintController> logger)
        {
            LoggerGSM01500Print.R_InitializeLogger(logger);
            _logger = LoggerGSM01500Print.R_GetInstanceLogger();
            _ReportCls = new R_ReportFastReportBackClass();
            _ReportCls.R_InstantiateMainReportWithFileName += _ReportCls_R_InstantiateMainReportWithFileName;
            _ReportCls.R_GetMainDataAndName += _ReportCls_R_GetMainDataAndName;
            _ReportCls.R_SetNumberAndDateFormat += _ReportCls_R_SetNumberAndDateFormat;
        }
        #endregion

        #region Event Handler
        private void _ReportCls_R_InstantiateMainReportWithFileName(ref string pcFileTemplate)
        {
            _logger.LogInfo("Start || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
            pcFileTemplate = System.IO.Path.Combine("Reports", "GSM01500PrintCenter.frx");
            _logger.LogInfo("End || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
        }

        private void _ReportCls_R_GetMainDataAndName(ref ArrayList poData, ref string pcDataSourceName)
        {
            _logger.LogInfo("Start || _ReportCls_R_GetMainDataAndName(Controller)");
            poData.Add(GenerateDataPrint(_AllCenterParameter));
            //GSM01500PrintCenterResultDTO loTemp = GSM01500COMMON.Models.GSM01500PrintCenterModelDummyData.DefaultDataCenter();
            //BaseHeaderDTO loParam = new BaseHeaderDTO()
            //{
            //    CCOMPANY_NAME = "PT Realta Chackradarma",
            //    CPRINT_CODE = "001",
            //    CPRINT_NAME = "Center",
            //    CUSER_ID = R_BackGlobalVar.USER_ID,
            //};
            //GSM01500PrintCenterResultWithBaseHeaderPrintDTO loResult = new GSM01500PrintCenterResultWithBaseHeaderPrintDTO()
            //{
            //    BaseHeaderData = loParam,
            //    CenterData = loTemp
            //};
            //poData.Add(loResult);
            pcDataSourceName = "ResponseDataModel";
            _logger.LogInfo("End || _ReportCls_R_InstantiateMainReportWithFileName(Controller)");
        }

        private void _ReportCls_R_SetNumberAndDateFormat(ref R_ReportFormatDTO poReportFormat)
        {
            _logger.LogInfo("Start || _ReportCls_R_SetNumberAndDateFormat(Controller)");
            poReportFormat.DecimalSeparator = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_SEPARATOR;
            poReportFormat.GroupSeparator = R_BackGlobalVar.REPORT_FORMAT_GROUP_SEPARATOR;
            poReportFormat.DecimalPlaces = R_BackGlobalVar.REPORT_FORMAT_DECIMAL_PLACES;
            poReportFormat.ShortDate = R_BackGlobalVar.REPORT_FORMAT_SHORT_DATE;
            poReportFormat.ShortTime = R_BackGlobalVar.REPORT_FORMAT_SHORT_TIME;
            _logger.LogInfo("End || _ReportCls_R_SetNumberAndDateFormat(Controller)");
        }
        #endregion

        [HttpPost]
        public R_DownloadFileResultDTO PrintCenterPost(GSM01500PrintCenterParameterDTO poParameter)
        {
            _logger.LogInfo("Start || PrintCenterPost(Controller)");
            R_Exception loException = new R_Exception();
            R_DownloadFileResultDTO loRtn = null;
            GSM01500PrintLogKeyDTO loCache = null;
            try
            {
                loRtn = new R_DownloadFileResultDTO();

                loCache = new GSM01500PrintLogKeyDTO
                {
                    poParam = poParameter,
                    poLogKey = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY)
                };

                _logger.LogInfo("Set GUID Param || PrintCenterPost(Controller)");
                R_DistributedCache.R_Set(loRtn.GuidResult, R_NetCoreUtility.R_SerializeObjectToByte<GSM01500PrintLogKeyDTO>(loCache));
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }
            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || PrintCenterPost(Controller)");
            return loRtn;
        }

        [HttpGet, AllowAnonymous]
        public FileStreamResult PrintCenterGet(string pcGuid)
        {
            _logger.LogInfo("Start || PrintCenterGet(Controller)");
            R_Exception loException = new R_Exception();
            GSM01500PrintLogKeyDTO loResultGUID = null;
            FileStreamResult loRtn = null;
            try
            {
                //Get Parameter
                loResultGUID = R_NetCoreUtility.R_DeserializeObjectFromByte<GSM01500PrintLogKeyDTO>(R_DistributedCache.Cache.Get(pcGuid));

                //Get Data and Set Log Key
                R_NetCoreLogUtility.R_SetNetCoreLogKey(loResultGUID.poLogKey);
                _AllCenterParameter = loResultGUID.poParam;

                _logger.LogInfo("Read File Report || PrintCenterGet(Controller)");
                loRtn = new FileStreamResult(_ReportCls.R_GetStreamReport(), R_ReportUtility.GetMimeType(R_FileType.PDF));
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || PrintCenterGet(Controller)");
            return loRtn;
        }

        #region Helper
        private GSM01500PrintCenterResultWithBaseHeaderPrintDTO GenerateDataPrint(GSM01500PrintCenterParameterDTO poParam)
        {
            _logger.LogInfo("Start || GenerateDataPrint(Controller)");
            R_Exception loException = new R_Exception();
            GSM01500PrintCenterResultWithBaseHeaderPrintDTO loRtn = new GSM01500PrintCenterResultWithBaseHeaderPrintDTO();

            try
            {
                GSM01500Cls loCls = new GSM01500Cls(_logger);

                _logger.LogInfo("Run GetCompanyBasedOnId(Cls) || GenerateDataPrint(Controller)");
                CompanyDTO loCompany = loCls.GetCompanyBasedOnId(poParam.CLOGIN_COMPANY_ID);

                //poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //poParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                List<GSM01500PrintCenterDTO> loCollection = loCls.GetPrintCenterList(poParam);

                _logger.LogInfo("Group Data || GenerateDataPrint(Controller)");
                List<GSM01500PrintCenterHeaderDTO> loTempData = loCollection
                .GroupBy(item => new { item.CCENTER_CODE, item.CCENTER_NAME, item.LACTIVE })
                .Select(header => new GSM01500PrintCenterHeaderDTO
                {
                    CCENTER_CODE = header.Key.CCENTER_CODE,
                    CCENTER_NAME = header.Key.CCENTER_NAME,
                    LACTIVE = header.Key.LACTIVE,
                    DetailData = header
                        .Select(detail => new GSM01500PrintCenterDetailDTO
                        {
                            CDEPT_CODE = detail.CDEPT_CODE,
                            CDEPT_NAME = detail.CDEPT_NAME
                        })
                        .ToList()
                })
                .ToList();

                // Set Base Header Data
                var loBaseHeader = (R_NetCoreLogKeyDTO)R_NetCoreLogAsyncStorage.GetData(R_NetCoreLogConstant.LOG_KEY);
                _logger.LogInfo("Set Base Header || GenerateDataPrint(Controller)");
                BaseHeaderDTO loParam = new BaseHeaderDTO()
                {
                    CCOMPANY_NAME = "PT Realta Chackradarma",
                    CPRINT_CODE = "001",
                    CPRINT_NAME = "CENTERS",
                    //CUSER_ID = R_BackGlobalVar.USER_ID,
                    CUSER_ID = loBaseHeader.USER_ID,
                };

                // get image
                //Assembly loAsm = Assembly.Load("BIMASAKTI_GS_API");
                //var lcResourceFile = "BIMASAKTI_GS_API.Image.CompanyLogo.png";
                //using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                //{

                //    var ms = new MemoryStream();
                //    resFilestream.CopyTo(ms);
                //    var bytes = ms.ToArray();

                //    loParam.BLOGO_COMPANY = bytes;
                //}

                _logger.LogInfo("Set Parameter || GenerateDataPrint(Controller)");
                GSM01500PrintCenterResultDTO loData = new GSM01500PrintCenterResultDTO()
                {
                    Title = "",
                    Header = loCompany.CCOMPANY_NAME,
                    Column = new GSM01500PrintCenterColumnDTO(),
                    Data = loTempData,
                    PrintDept = poParam.LPRINT_DEPT
                };

                //GetHeader Data
                //var loHeader = loCollection.FirstOrDefault();

                //loData.Title = "Center";
                //loData.Header = loCompany.CCOMPANY_NAME;
                //loData.Data = loTempData;

                loRtn.BaseHeaderData = loParam;
                loRtn.CenterData = loData;
            }
            catch (Exception ex)
            {
                _logger.LogError(loException);
                loException.Add(ex);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GenerateDataPrint(Controller)");
            return loRtn;
        }
        #endregion
    }
}