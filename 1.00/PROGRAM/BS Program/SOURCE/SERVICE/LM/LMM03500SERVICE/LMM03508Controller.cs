using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.DTOs.LMM03508;
using LMM03500COMMON.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03508Controller : ControllerBase, ILMM03508
    {
        private LoggerLMM03508 _logger;
        public LMM03508Controller(ILogger<LMM03508Controller> logger)
        {
            LoggerLMM03508.R_InitializeLogger(logger);
            _logger = LoggerLMM03508.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03508DTO> GetFixVAList()
        {
            _logger.LogInfo("Start || GetFixVAList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM03508DTO> loRtn = null;
            LMM03508ParameterDTO loParam = new LMM03508ParameterDTO();
            LMM03508Cls loCls = new LMM03508Cls();
            List<LMM03508DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetFixVAList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03508_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03508_TENANT_ID_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetFixVAList(Cls) || GetFixVAList(Controller)");
                loTempRtn = loCls.GetFixVAList(loParam);

                _logger.LogInfo("Run GetFixVAStream(Controller) || GetFixVAList(Controller)");
                loRtn = GetFixVAStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetFixVAList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<LMM03508DTO> GetFixVAStream(List<LMM03508DTO> poParameter)
        {
            foreach (LMM03508DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
