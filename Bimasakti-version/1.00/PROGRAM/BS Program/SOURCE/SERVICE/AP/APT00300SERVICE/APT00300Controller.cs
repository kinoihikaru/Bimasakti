using APT00300BACK;
using APT00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace APT00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00300Controller : ControllerBase, IAPT00300
    {
        private LoggerAPT00300 _Logger;
        public APT00300Controller(ILogger<LoggerAPT00300> logger)
        {
            //Initial and Get Logger
            LoggerAPT00300.R_InitializeLogger(logger);
            _Logger = LoggerAPT00300.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<APT00300DTO> GetDebitNoteListStream()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00300DTO> loRtn = null;
            _Logger.LogInfo("Start GetDebitNoteListStream");

            try
            {
                _Logger.LogInfo("Set Param GetDebitNoteListStream");
                var poParam = new APT00300ParameterDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poParam.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSUPPLIER_ID);
                poParam.CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFROM_PERIOD);
                poParam.CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTO_PERIOD);

                _Logger.LogInfo("Call Back Method GetAllSearchSupplier");
                var loCls = new APT00300Cls();
                var loTempRtn = loCls.GetAllPurcahseDebit(poParam);

                _Logger.LogInfo("Call Stream Method Data GetDebitNoteListStream");
                loRtn = GetStreamData<APT00300DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetDebitNoteListStream");

            return loRtn;
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}