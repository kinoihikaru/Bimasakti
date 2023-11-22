using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03501;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
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
    public class LMM03503Controller : ControllerBase, ILMM03503
    {
        private LoggerLMM03503 _logger;
        public LMM03503Controller(ILogger<LMM03503Controller> logger)
        {
            LoggerLMM03503.R_InitializeLogger(logger);
            _logger = LoggerLMM03503.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM03503ResultDTO GetTaxInfo(LMM03503ParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetTaxInfo(Controller)");
            R_Exception loException = new R_Exception();
            LMM03503ResultDTO loRtn = new LMM03503ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxInfo(Controller)");
                LMM03503Cls loCls = new LMM03503Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03503_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03503_TENANT_ID_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetTaxInfo(Cls) || GetTaxInfo(Controller)");
                loRtn.Data = loCls.GetTaxInfo(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTaxInfo(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetLMM03503ListDTO> GetIdTypeList()
        {
            _logger.LogInfo("Start || GetIdTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM03503ListDTO> loRtn = null;
            GetLMM03503ListParameterDTO loParam = new GetLMM03503ListParameterDTO();
            LMM03503Cls loCls = new LMM03503Cls();
            List<GetLMM03503ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetIdTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetIdTypeList(Cls) || GetIdTypeList(Controller)");
                loTempRtn = loCls.GetIdTypeList(loParam);

                _logger.LogInfo("Run GetLMM03503ListStream(Controller) || GetIdTypeList(Controller)");
                loRtn = GetLMM03503ListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetIdTypeList(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetLMM03503ListDTO> GetTaxCodeList()
        {
            _logger.LogInfo("Start || GetTaxCodeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM03503ListDTO> loRtn = null;
            GetLMM03503ListParameterDTO loParam = new GetLMM03503ListParameterDTO();
            LMM03503Cls loCls = new LMM03503Cls();
            List<GetLMM03503ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxCodeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTaxCodeList(Cls) || GetTaxCodeList(Controller)");
                loTempRtn = loCls.GetTaxCodeList(loParam);

                _logger.LogInfo("Run GetLMM03503ListStream(Controller) || GetTaxCodeList(Controller)");
                loRtn = GetLMM03503ListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTaxCodeList(Controller)");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GetLMM03503ListDTO> GetTaxTypeList()
        {
            _logger.LogInfo("Start || GetTaxTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM03503ListDTO> loRtn = null;
            GetLMM03503ListParameterDTO loParam = new GetLMM03503ListParameterDTO();
            LMM03503Cls loCls = new LMM03503Cls();
            List<GetLMM03503ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTaxTypeList(Cls) || GetTaxTypeList(Controller)");
                loTempRtn = loCls.GetTaxTypeList(loParam);

                _logger.LogInfo("Run GetLMM03503ListStream(Controller) || GetTaxTypeList(Controller)");
                loRtn = GetLMM03503ListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetTaxTypeList(Controller)");
            return loRtn;
        }

        private async IAsyncEnumerable<GetLMM03503ListDTO> GetLMM03503ListStream(List<GetLMM03503ListDTO> poParameter)
        {
            foreach (GetLMM03503ListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
