using GLB00600BACK;
using GLB00600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly ActivitySource _activitySource;
        public GLB00600Controller(ILogger<LoggerGLB00600> logger)
        {
            //Initial and Get Logger
            LoggerGLB00600.R_InitializeLogger(logger);
            _Logger = LoggerGLB00600.R_GetInstanceLogger();
            _activitySource = GLB00600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLB00600Controller));
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GLB00600Record<GLB00600GSMTransactionCodeDTO> GetInitialGSMTransactionCode()
        {
            using Activity activity = _activitySource.StartActivity("GetInitialGSMTransactionCode");
            var loEx = new R_Exception();
            GLB00600Record<GLB00600GSMTransactionCodeDTO> loRtn = new();
            _Logger.LogInfo("Start GetInitialGSMTransactionCode");

            try
            {
                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetGSMTransactionCode");
                var loTempRtn = loCls.GetGSMTransactionCode();
                loRtn.Data = loTempRtn;
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
        public IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult()
        {
            using Activity activity = _activitySource.StartActivity("GetValidationClosingResult");
            var loEx = new R_Exception();
            IAsyncEnumerable<GLB00600DTO> loRtn = null;
            _Logger.LogInfo("Start GetValidationClosingResult");

            try
            {
                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetResult");
                var loResult = loCls.GetResult();

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

        [HttpPost]
        public GLB00600Record<GLB00600DTO> GetResultClosingEntries()
        {
            using Activity activity = _activitySource.StartActivity("GetResultClosingEntries");
            var loEx = new R_Exception();
            GLB00600Record<GLB00600DTO> loRtn = new GLB00600Record<GLB00600DTO>();
            _Logger.LogInfo("Start GetResultClosingEntries");

            try
            {
                _Logger.LogInfo("Call Back Method GetClosingEntries");
                var loCls = new GLB00600Cls();
                loCls.GetClosingEntries();
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

        public GLB00600Record<GLB00600GLSystemParamDTO> GetSystemParam()
        {
            using Activity activity = _activitySource.StartActivity("GetSystemParam");
            var loEx = new R_Exception();
            GLB00600Record<GLB00600GLSystemParamDTO> loRtn = new();
            _Logger.LogInfo("Start GetSystemParam");

            try
            {
                _Logger.LogInfo("Call Back Method GetSystemParam");
                var loCls = new GLB00600Cls();
                var loTempRtn = loCls.GetSystemParam();

                loRtn.Data = loTempRtn;
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

        public GLB00600Record<GLB00600InitialDTO> GetInitialVar(GLB00600InitialDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetInitialVar");
            var loEx = new R_Exception();
            GLB00600Record<GLB00600InitialDTO> loRtn = new();
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                _Logger.LogInfo("Set Param GetInitialVar");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetInitial");
                var loTempRtn = loCls.GetInitial(poParam);

                loRtn.Data = loTempRtn;
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

        public GLB00600Record<GLB00600SuspenseAmountDTO> GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetInitialSupenseAmount");
            var loEx = new R_Exception();
            GLB00600Record<GLB00600SuspenseAmountDTO> loRtn = new();
            _Logger.LogInfo("Start GetInitialSupenseAmount");

            try
            {
                var loCls = new GLB00600Cls();

                _Logger.LogInfo("Call Back Method GetSuspenseAmount");
                var loTempRtn = loCls.GetSuspenseAmount(poParam);
                loRtn.Data = loTempRtn;
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
    }
}
