using GLB00600BACK;
using GLB00600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLB00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB00600Controller : ControllerBase, IGLB00600
    {
        private LoggerGLB00600 _Logger;
        public GLB00600Controller(ILogger<LoggerGLB00600> logger)
        {
            //Initial and Get Logger
            LoggerGLB00600.R_InitializeLogger(logger);
            _Logger = LoggerGLB00600.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLB00600GSMTransactionCodeDTO GetInitialGSMTransactionCode(GLB00600GSMTransactionCodeDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600GSMTransactionCodeDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialGSMTransactionCode");

            try
            {
                _Logger.LogInfo("Set Param GetInitialGSMTransactionCode");
                loRtn = new GLB00600GSMTransactionCodeDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetGSMTransactionCode");
                loRtn = loCls.GetGSMTransactionCode(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGSMTransactionCode");

            return loRtn;
        }

        [HttpPost]
        public GLB00600SuspenseAmountDTO GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600SuspenseAmountDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialSupenseAmount");

            try
            {
                _Logger.LogInfo("Set Param GetInitialSupenseAmount");
                loRtn = new GLB00600SuspenseAmountDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetSuspenseAmount");
                loRtn = loCls.GetSuspenseAmount(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialSupenseAmount");

            return loRtn;
        }

        [HttpPost]
        public GLB00600InitialDTO GetInitialVar(GLB00600InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600InitialDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                loRtn = new GLB00600InitialDTO();

                _Logger.LogInfo("Set Param GetInitialVar");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetInitial");
                loRtn = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialVar");

            return loRtn;
        }

        [HttpPost]
        public GLB00600DTO GetResultClosingEntries(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600DTO loRtn = new GLB00600DTO();
            _Logger.LogInfo("Start GetResultClosingEntries");

            try
            {
                _Logger.LogInfo("Set Param GetResultClosingEntries");
                var loCls = new GLB00600Cls();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetClosingEntries");
                loCls.GetClosingEntries(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetResultClosingEntries");

            return loRtn;
        }

        [HttpPost]
        public GLB00600GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLB00600GLSystemParamDTO loRtn = null;
            _Logger.LogInfo("Start GetSystemParam");

            try
            {
                loRtn = new GLB00600GLSystemParamDTO();

                _Logger.LogInfo("Set Param GetSystemParam");
                var poParam = new GLB00600GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetSystemParam");
                loRtn = loCls.GetSystemParam(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSystemParam");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLB00600DTO> loRtn = null;
            _Logger.LogInfo("Start GetValidationClosingResult");

            try
            {
                _Logger.LogInfo("Set Param GetValidationClosingResult");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetResult");
                var loResult = loCls.GetResult(poParam);

                _Logger.LogInfo("Call Stream Method Data GetValidationClosingResult");
                loRtn = GetStream<GLB00600DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetValidationClosingResult");

            return loRtn;
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
    }
}
