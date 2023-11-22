using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03500Controller : ControllerBase, ILMM03500
    {
        private LoggerLMM03500 _logger;
        public LMM03500Controller(ILogger<LMM03500Controller> logger)
        {
            LoggerLMM03500.R_InitializeLogger(logger);
            _logger = LoggerLMM03500.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GetCurrencyDTO> GetCurrencyList()
        {
            _logger.LogInfo("Start || GetCurrencyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetCurrencyDTO> loRtn = null;
            LMM03500Cls loCls = new LMM03500Cls();
            List<GetCurrencyDTO> loTempRtn = null;
            GetCurrencyParameterDTO loParam = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetCurrencyList(Controller)");
                loParam = new GetCurrencyParameterDTO()
                {
                    CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID,
                    CLOGIN_USER_ID = R_BackGlobalVar.USER_ID
                };

                _logger.LogInfo("Run GetCurrencyList(Cls) || GetCurrencyList(Controller)");
                loTempRtn = loCls.GetCurrencyList(loParam);

                _logger.LogInfo("Run GetCurrencyStream(Controller) || GetCurrencyList(Controller)");
                loRtn = GetCurrencyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetCurrencyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetCurrencyDTO> GetCurrencyStream(List<GetCurrencyDTO> poParameter)
        {
            foreach (GetCurrencyDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            _logger.LogInfo("Start || GetPropertyList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetPropertyListDTO> loRtn = null;
            GetPropertyListParameterDTO loParam = new GetPropertyListParameterDTO();
            LMM03500Cls loCls = new LMM03500Cls();
            List<GetPropertyListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetPropertyList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetPropertyList(Cls) || GetPropertyList(Controller)");
                loTempRtn = loCls.GetPropertyList(loParam);

                _logger.LogInfo("Run GetPropertyStream(Controller) || GetPropertyList(Controller)");
                loRtn = GetPropertyStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetPropertyList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetPropertyListDTO> GetPropertyStream(List<GetPropertyListDTO> poParameter)
        {
            foreach (GetPropertyListDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public TenantResultDTO GetTenant(TenantParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetTenant(Controller)");
            R_Exception loException = new R_Exception();
            TenantResultDTO loRtn = new TenantResultDTO();
            LMM03500Cls loCls = new LMM03500Cls();

            try
            {
                _logger.LogInfo("Set Parameter || GetTenant(Controller)");
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03500_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03500_TENANT_ID_CONTEXT);
                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTenant(Cls) || GetTenant(Controller)");
                loRtn.Data = loCls.GetTenant(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTenant(Controller)");
            return loRtn;
        }

    }
}
