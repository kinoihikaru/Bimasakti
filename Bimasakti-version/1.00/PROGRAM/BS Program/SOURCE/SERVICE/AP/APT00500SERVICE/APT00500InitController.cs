using APT00500BACK;
using APT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System.Diagnostics;

namespace APT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00500InitController : ControllerBase, IAPT00500Init
    {
        private LoggerAPT00500Init _Logger;
        private readonly ActivitySource _activitySource;
        public APT00500InitController(ILogger<LoggerAPT00500Init> logger)
        {
            //Initial and Get Logger
            LoggerAPT00500Init.R_InitializeLogger(logger);
            _Logger = LoggerAPT00500Init.R_GetInstanceLogger();
            _activitySource = APT00500ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(APT00500InitController));
        }

        [HttpPost]
        public APT00500SingleResult<APT00500InitTabListDTO> GetAllInitialProcessTabList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabList");
            var loEx = new R_Exception();
            APT00500SingleResult<APT00500InitTabListDTO> loRtn = new APT00500SingleResult<APT00500InitTabListDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabList");

            try
            {
                var loAllData = new APT00500InitTabListDTO();
                var loCls = new APT00500InitCls();

                _Logger.LogInfo("Call Back Method");
                loAllData.VAR_TODAY = loCls.GetTodayDateDB();
                loAllData.VAR_GSM_PERIOD = loCls.GetPeriodYearRangeGS();
                loAllData.VAR_PROPERTY_LIST = loCls.GetAllProperty();

                loRtn.Data = loAllData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcessTabList");

            return loRtn;
        }

        [HttpPost]
        public APT00500SingleResult<APT00500InitTabEntryDTO> GetAllInitialProcessTabEntry()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabEntry");
            var loEx = new R_Exception();
            APT00500SingleResult<APT00500InitTabEntryDTO> loRtn = new APT00500SingleResult<APT00500InitTabEntryDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabEntry");

            try
            {
                var loAllData = new APT00500InitTabEntryDTO();
                var loCls = new APT00500InitCls();

                _Logger.LogInfo("Call Back Method");
                loAllData.VAR_TODAY = loCls.GetTodayDateDB();
                loAllData.VAR_PROPERTY_LIST = loCls.GetAllProperty();
                loAllData.VAR_ADJUSTMENT_TRANS_CODE = loCls.GetTransCodeInfoGS(new APT00500TransCodeInfoGSParamDTO { CTRANS_CODE = ContextConstant.VAR_TRANS_CODE });
                loAllData.VAR_GL_SYSTEM_PARAM = loCls.GetSystemParamGL();
                loAllData.VAR_GSM_COMPANY = loCls.GetCompanyInfoGS();
                loAllData.VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoGS(new APT00500PeriodDTInfoGSParamDTO { CYEAR = loAllData.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_YY, CPERIOD_NO = loAllData.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_MM });

                loRtn.Data = loAllData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcessTabEntry");

            return loRtn;
        }

        [HttpPost]
        public APT00500SingleResult<APT00500InitPopupAllocDTO> GetAllInitialProcessPopupAlloc()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessPopupAlloc");
            var loEx = new R_Exception();
            APT00500SingleResult<APT00500InitPopupAllocDTO> loRtn = new APT00500SingleResult<APT00500InitPopupAllocDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessPopupAlloc");

            try
            {
                var loAllData = new APT00500InitPopupAllocDTO();
                var loCls = new APT00500InitCls();

                _Logger.LogInfo("Call Back Method");
                loAllData.VAR_GSM_COMPANY = loCls.GetCompanyInfoGS();
                loAllData.VAR_ALLOCATION_TRANS_CODE = loCls.GetTransCodeInfoGS(new APT00500TransCodeInfoGSParamDTO { CTRANS_CODE = ContextConstant.VAR_TRANS_CODE_ALLOC });
                loAllData.VAR_ALLOC_TRX_TYPE_LIST = loCls.GetAllAllocTrxTypeAP();

                loRtn.Data = loAllData;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcessPopupAlloc");

            return loRtn;
        }
    }
}