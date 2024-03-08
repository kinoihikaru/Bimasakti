using CBM00100BACK;
using CBM00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using System.Diagnostics;

namespace CBM00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CBM00100Controller : ControllerBase, ICBM00100
    {
        private LoggerCBM00100 _Logger;
        private readonly ActivitySource _activitySource;
        public CBM00100Controller(ILogger<LoggerCBM00100> logger)
        {
            //Initial and Get Logger
            LoggerCBM00100.R_InitializeLogger(logger);
            _Logger = LoggerCBM00100.R_GetInstanceLogger();
            _activitySource = CBM00100ActivityInitSourceBase.R_InitializeAndGetActivitySource(nameof(CBM00100Controller));
        }

        [HttpPost]
        public CBM00100SingleResult<CBM00100ValidateInitDTO> GetInitValidate()
        {
            using Activity activity = _activitySource.StartActivity("GetInitValidate");
            var loEx = new R_Exception();
            CBM00100SingleResult<CBM00100ValidateInitDTO> loRtn = new CBM00100SingleResult<CBM00100ValidateInitDTO>();
            _Logger.LogInfo("Start GetInitValidate");

            try
            {
                var loCls = new CBM00100Cls();

                _Logger.LogInfo("Call Back Method GetInitValidate");
                loRtn.Data = loCls.GetInitValidate();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitValidate");

            return loRtn;
        }

        [HttpPost]
        public CBM00100SingleResult<CBM00100DTO> GetSystemParamCB()
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParamCB");
            var loEx = new R_Exception();
            CBM00100SingleResult<CBM00100DTO> loRtn = new CBM00100SingleResult<CBM00100DTO>();
            _Logger.LogInfo("Start GetSystemParamCB");

            try
            {
                var loCls = new CBM00100Cls();

                _Logger.LogInfo("Call Back Method GetSystemParamCB");
                loRtn.Data = loCls.GetSystemParamCB();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSystemParamCB");

            return loRtn;
        }

        [HttpPost]
        public CBM00100SingleResult<CBM00100DTO> SaveSystemParamCB(CBM00100SaveParameterDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SaveSystemParamCB");
            var loEx = new R_Exception();
            CBM00100SingleResult<CBM00100DTO> loRtn = new CBM00100SingleResult<CBM00100DTO>();
            _Logger.LogInfo("Start SaveSystemParamCB");

            try
            {
                var loCls = new CBM00100Cls();

                _Logger.LogInfo("Call Back Method SaveSystemParamCB");
                loRtn.Data = loCls.SaveSystemParamCB(poEntity.Entity, poEntity.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveSystemParamCB");

            return loRtn;
        }
    }
}