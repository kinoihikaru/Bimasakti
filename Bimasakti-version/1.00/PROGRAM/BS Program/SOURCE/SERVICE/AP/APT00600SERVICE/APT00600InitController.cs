using APT00600BACK;
using APT00600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace APT00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00600InitController : ControllerBase, IAPT00600Init
    {
        private LoggerAPT00600Init _Logger;
        private readonly ActivitySource _activitySource;
        public APT00600InitController(ILogger<LoggerAPT00600Init> logger)
        {
            //Initial and Get Logger
            LoggerAPT00600Init.R_InitializeLogger(logger);
            _Logger = LoggerAPT00600Init.R_GetInstanceLogger();
            _activitySource = APT00600ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(APT00600InitController));
        }
        [HttpPost]
        public APT00600SingleResult<APT00600InitTabListDTO> GetAllInitialProcessTabList()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabList");
            var loEx = new R_Exception();
            APT00600SingleResult<APT00600InitTabListDTO> loRtn = new APT00600SingleResult<APT00600InitTabListDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabList");

            try
            {
                var loAllData = new APT00600InitTabListDTO();
                var loCls = new APT00600InitCls();

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
        public APT00600SingleResult<APT00600InitTabEntryDTO> GetAllInitialProcessTabEntry()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessTabEntry");
            var loEx = new R_Exception();
            APT00600SingleResult<APT00600InitTabEntryDTO> loRtn = new APT00600SingleResult<APT00600InitTabEntryDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessTabEntry");

            try
            {
                var loAllData = new APT00600InitTabEntryDTO();
                var loCls = new APT00600InitCls();

                _Logger.LogInfo("Call Back Method");
                loAllData.VAR_TODAY = loCls.GetTodayDateDB();
                loAllData.VAR_PROPERTY_LIST = loCls.GetAllProperty();
                loAllData.VAR_ADJUSTMENT_TRANS_CODE = loCls.GetTransCodeInfoGS(new APT00600TransCodeInfoGSParamDTO { CTRANS_CODE = ContextConstant.VAR_TRANS_CODE });
                loAllData.VAR_GL_SYSTEM_PARAM = loCls.GetSystemParamGL();
                loAllData.VAR_GSM_COMPANY = loCls.GetCompanyInfoGS();
                loAllData.VAR_SOFT_PERIOD_START_DATE = loCls.GetPeriodDTInfoGS(new APT00600PeriodDTInfoGSParamDTO { CYEAR = loAllData.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_YY, CPERIOD_NO = loAllData.VAR_GL_SYSTEM_PARAM.CSOFT_PERIOD_MM });

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
        public APT00600SingleResult<APT00600InitPopupAllocDTO> GetAllInitialProcessPopupAlloc()
        {
            using Activity activity = _activitySource.StartActivity("GetAllInitialProcessPopupAlloc");
            var loEx = new R_Exception();
            APT00600SingleResult<APT00600InitPopupAllocDTO> loRtn = new APT00600SingleResult<APT00600InitPopupAllocDTO>();
            _Logger.LogInfo("Start GetAllInitialProcessPopupAlloc");

            try
            {
                var loAllData = new APT00600InitPopupAllocDTO();
                var loCls = new APT00600InitCls();

                _Logger.LogInfo("Call Back Method");
                loAllData.VAR_GSM_COMPANY = loCls.GetCompanyInfoGS();
                loAllData.VAR_ALLOCATION_TRANS_CODE = loCls.GetTransCodeInfoGS(new APT00600TransCodeInfoGSParamDTO { CTRANS_CODE = ContextConstant.VAR_TRANS_CODE_ALLOC });
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