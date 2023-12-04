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
        public IAsyncEnumerable<LMM06500DTOInitial> GetProperty()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500DTOInitial> loRtn = null;
            var loParameter = new LMM06500DTOInitial();
            _Logger.LogInfo("Start GetProperty");

            try
            {
                var loCls = new LMM06500UniversalCls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProperty");
                loRtn = GetStream<LMM06500DTOInitial>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM06500UniversalDTO> GetPositionList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetPositionList");

            try
            {
                var loCls = new LMM06500UniversalCls();

                var poParam = new LMM06500UniversalDTO();
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
        public IAsyncEnumerable<LMM06500UniversalDTO> GetGenderList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM06500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetGenderList");

            try
            {
                var loCls = new LMM06500UniversalCls();

                var poParam = new LMM06500UniversalDTO();
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

        [HttpPost]
        public LMM06500AllListUniversalDTO GetAllUniversalList()
        {
            var loEx = new R_Exception();
            LMM06500AllListUniversalDTO loRtn = new LMM06500AllListUniversalDTO();
            var loPropertyParameter = new LMM06500DTOInitial();
            var loUniversalParam = new LMM06500UniversalDTO();
            _Logger.LogInfo("Start GetAllUniversalList");

            try
            {
                var loCls = new LMM06500UniversalCls();

                _Logger.LogInfo("Set Param");
                loPropertyParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loPropertyParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loUniversalParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loUniversalParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetProperty");
                loRtn.PropertyList = loCls.GetProperty(loPropertyParameter);
                _Logger.LogInfo("Call Back Method GetAllPosition");
                loRtn.PositionList = loCls.GetAllPosition(loUniversalParam);
                _Logger.LogInfo("Call Back Method GetAllGender");
                loRtn.GenderList = loCls.GetAllGender(loUniversalParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllUniversalList");

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
