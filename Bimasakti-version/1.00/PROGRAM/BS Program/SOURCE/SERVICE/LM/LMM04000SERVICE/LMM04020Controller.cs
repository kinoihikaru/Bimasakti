using LMM04000BACK;
using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04020;
using LMM04000COMMON.Loggers;
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

namespace LMM04000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM04020Controller : ControllerBase, ILMM04020
    {
        private LoggerLMM04020 _logger;
        public LMM04020Controller(ILogger<LMM04020Controller> logger)
        {
            LoggerLMM04020.R_InitializeLogger(logger);
            _logger = LoggerLMM04020.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM04020ResultDTO GetTaxInfo(LMM04020ParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetTaxInfo(Controller)");
            R_Exception loException = new R_Exception();
            LMM04020DTO loTempRtn = new LMM04020DTO();
            LMM04020ResultDTO loRtn = new LMM04020ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxInfo(Controller)");
                LMM04020Cls loCls = new LMM04020Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04020_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM04020_TENANT_ID_CONTEXT);
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
        public IAsyncEnumerable<GetLMM04020ListDTO> GetIdTypeList()
        {
            _logger.LogInfo("Start || GetIdTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM04020ListDTO> loRtn = null;
            GetLMM04020ListParameterDTO loParam = new GetLMM04020ListParameterDTO();
            LMM04020Cls loCls = new LMM04020Cls();
            List<GetLMM04020ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetIdTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetIdTypeList(Cls) || GetIdTypeList(Controller)");
                loTempRtn = loCls.GetIdTypeList(loParam);

                _logger.LogInfo("Run GetLMM04020ListStream(Controller) || GetIdTypeList(Controller)");
                loRtn = GetLMM04020ListStream(loTempRtn);
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
        public IAsyncEnumerable<GetLMM04020ListDTO> GetTaxCodeList()
        {
            _logger.LogInfo("Start || GetTaxCodeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM04020ListDTO> loRtn = null;
            GetLMM04020ListParameterDTO loParam = new GetLMM04020ListParameterDTO();
            LMM04020Cls loCls = new LMM04020Cls();
            List<GetLMM04020ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxCodeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTaxCodeList(Cls) || GetTaxCodeList(Controller)");
                loTempRtn = loCls.GetTaxCodeList(loParam);

                _logger.LogInfo("Run GetLMM04020ListStream(Controller) || GetTaxCodeList(Controller)");
                loRtn = GetLMM04020ListStream(loTempRtn);
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
        public IAsyncEnumerable<GetLMM04020ListDTO> GetTaxTypeList()
        {
            _logger.LogInfo("Start || GetTaxTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetLMM04020ListDTO> loRtn = null;
            GetLMM04020ListParameterDTO loParam = new GetLMM04020ListParameterDTO();
            LMM04020Cls loCls = new LMM04020Cls();
            List<GetLMM04020ListDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetTaxTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetTaxTypeList(Cls) || GetTaxTypeList(Controller)");
                loTempRtn = loCls.GetTaxTypeList(loParam);

                _logger.LogInfo("Run GetLMM04020ListStream(Controller) || GetTaxTypeList(Controller)");
                loRtn = GetLMM04020ListStream(loTempRtn);
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

        private async IAsyncEnumerable<GetLMM04020ListDTO> GetLMM04020ListStream(List<GetLMM04020ListDTO> poParameter)
        {
            foreach (GetLMM04020ListDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
