using LMM06500BACK;
using LMM06500COMMON;
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

namespace LMM06500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM06500UniversalController : ControllerBase, ILMM06500Universal
    {
        private LoggerLMM06500Universal _Logger;

        public LMM06500UniversalController(ILogger<LoggerLMM06500Universal> logger)
        {
            //Initial and Get Logger
            LoggerLMM06500Universal.R_InitializeLogger(logger);
            _Logger = LoggerLMM06500Universal.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06500UniversalDTO> GetPositionList(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetPositionList");

            try
            {
                var loCls = new LMM06500UniversalCls();

                _Logger.LogInfo("Set Param GetPositionList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllPosition");
                var loResult = loCls.GetAllPosition(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPositionList");
                loRtn = GetStream<LMM06500UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPositionList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06500UniversalDTO> GetGenderList(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetGenderList");

            try
            {
                var loCls = new LMM06500UniversalCls();

                _Logger.LogInfo("Set Param GetGenderList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllGender");
                var loResult = loCls.GetAllGender(poParam);

                _Logger.LogInfo("Call Stream Method Data GetGenderList");
                loRtn = GetStream<LMM06500UniversalDTO>(loResult);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGenderList");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
    }
}
