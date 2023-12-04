using BaseHeaderReportCOMMON;
using BaseHeaderReportCOMMON.Models;
using GLR00200BACK;
using GLR00200COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Cache;
using R_Common;
using R_CommonFrontBackAPI;
using R_ReportFastReportBack;
using System.Collections;
using System.Reflection;

namespace GLR00200SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLR00200Controller : ControllerBase, IGLR00200
    {
        private LoggerGLR00200 _Logger;
        public GLR00200Controller(ILogger<LoggerGLR00200> logger)
        {
            //Initial and Get Logger
            LoggerGLR00200.R_InitializeLogger(logger);
            _Logger = LoggerGLR00200.R_GetInstanceLogger();
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public GLR00200InitialDTO GetInitialVar()
        {
            var loEx = new R_Exception();
            GLR00200InitialDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                var poParam = new GLR00200InitialDTO();
                loRtn = new GLR00200InitialDTO();
                _Logger.LogInfo("Set Param GetInitialVar");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

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
        public IAsyncEnumerable<GLR00200UniversalDTO> GetPrintMethodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLR00200UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetPrintMethodList");

            try
            {
                var poParam = new GLR00200UniversalDTO();
                _Logger.LogInfo("Set Param GetPrintMethodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

                _Logger.LogInfo("Call Back Method GetAllPrintMethod");
                var loTempRtn = loCls.GetAllPrintMethod(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPrintMethodList");
                loRtn = GetStream<GLR00200UniversalDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPrintMethodList");

            return loRtn;
        }

        [HttpPost]
        public GLR00200GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLR00200GLSystemParamDTO loRtn = null;
            _Logger.LogInfo("Start GetSystemParam");

            try
            {
                var poParam = new GLR00200GLSystemParamDTO();
                loRtn = new GLR00200GLSystemParamDTO();
                _Logger.LogInfo("Set Param GetSystemParam");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLR00200Cls();

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
        public GLR00200Result<GLR00200GetMinMaxAccount> GetMinMaxAccount()
        {
            var loEx = new R_Exception();
            GLR00200Result<GLR00200GetMinMaxAccount> loRtn = new GLR00200Result<GLR00200GetMinMaxAccount>();
            _Logger.LogInfo("Start GetMinMaxAccount");

            try
            {
                var loCls = new GLR00200Cls();

                _Logger.LogInfo("Call Back Method GetMinMaxAccount");
                loRtn.Data = loCls.GetMinMaxAccount();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetMinMaxAccount");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLR00200PeriodDTO> GetPriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLR00200PeriodDTO> loRtn = null;
            _Logger.LogInfo("Start GetPriodList");

            try
            {
                var poParam = new GLR00200PeriodDTO();
                _Logger.LogInfo("Set Param GetPriodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CCYEAR = R_Utility.R_GetStreamingContext<string>(ContextConstant.CYEAR);

                var loCls = new GLR00200Cls();

                _Logger.LogInfo("Call Back Method GetAllPriod");
                var loTempRtn = loCls.GetAllPriod(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPriodList");
                loRtn = GetStream<GLR00200PeriodDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPriodList");

            return loRtn;
        }
    }
}