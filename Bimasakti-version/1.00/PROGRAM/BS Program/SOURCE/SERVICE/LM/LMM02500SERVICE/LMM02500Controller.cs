using LMM02500BACK;
using LMM02500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM02500Controller : ControllerBase, ILMM02500
    {
        private LoggerLMM02500 _Logger;
        public LMM02500Controller(ILogger<LoggerLMM02500> logger)
        {
            //Initial and Get Logger
            LoggerLMM02500.R_InitializeLogger(logger);
            _Logger = LoggerLMM02500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500DTO> GetTenantGrpist()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500DTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantGrpist");

            try
            {
                var loCls = new LMM02500Cls();

                _Logger.LogInfo("Set Param GetTenantGrpist");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetTenantGrpist");
                var loResult = loCls.GetAllTenantGroup(loPropertyId);

                _Logger.LogInfo("Call Stream Method Data GetTenantGrpist");
                loRtn = GetStream<LMM02500DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTenantGrpist");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500TenantDTO> GetTenantList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500TenantDTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantList");

            try
            {
                var loCls = new LMM02500Cls();

                _Logger.LogInfo("Set Param GetTenantList");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                var loTenantGrpId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTENANT_GROUP_ID);

                _Logger.LogInfo("Call Back Method GetTenantList");
                var loResult = loCls.GetAllTenant(loPropertyId, loTenantGrpId);

                _Logger.LogInfo("Call Stream Method Data GetTenantList");
                loRtn = GetStream<LMM02500TenantDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTenantList");

            return loRtn;
        }

        #region Get Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
        #endregion
    }
}