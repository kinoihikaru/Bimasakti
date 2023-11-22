using GLB09900BACK;
using GLB09900COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GLB09900SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLB09900Controller : ControllerBase, IGLB09900
    {
        private LoggerGLB09900 _Logger;
        public GLB09900Controller(ILogger<LoggerGLB09900> logger)
        {
            //Initial and Get Logger
            LoggerGLB09900.R_InitializeLogger(logger);
            _Logger = LoggerGLB09900.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLB09900InitialDTO GetInitialVar()
        {
            var loEx = new R_Exception();
            GLB09900InitialDTO loRtn = null;
            _Logger.LogInfo("Start GetInitialVar");

            try
            {
                loRtn = new GLB09900InitialDTO();

                _Logger.LogInfo("Set Param GetInitialVar");
                GLB09900InitialDTO poParam = new GLB09900InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB09900Cls();

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
        public GLB09900GLSystemParamDTO GetSystemParam()
        {
            var loEx = new R_Exception();
            GLB09900GLSystemParamDTO loRtn = null;
            _Logger.LogInfo("Start GetSystemParam");

            try
            {
                GLB09900GLSystemParamDTO poParam = new GLB09900GLSystemParamDTO();

                _Logger.LogInfo("Set Param GetSystemParam");
                loRtn = new GLB09900GLSystemParamDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                var loCls = new GLB09900Cls();

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
        public GLB09900DTO GetResultCloseGlPeriod(GLB09900DTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900DTO loRtn = null;
            _Logger.LogInfo("Start GetResultCloseGlPeriod");

            try
            {
                _Logger.LogInfo("Set Param GetResultCloseGlPeriod");
                loRtn = new GLB09900DTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new GLB09900Cls();

                _Logger.LogInfo("Call Back Method GetResultClosePeriod");
                loRtn = loCls.GetResultClosePeriod(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetResultCloseGlPeriod");

            return loRtn;
        }

        [HttpPost]
        public GLB09900ValidateDTO GetValidateResultCloseGlPeriod(GLB09900ValidateDTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900ValidateDTO loRtn = null;
            _Logger.LogInfo("Start GetValidateResultCloseGlPeriod");

            try
            {
                _Logger.LogInfo("Set Param GetValidateResultCloseGlPeriod");
                loRtn = new GLB09900ValidateDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                var loCls = new GLB09900Cls();

                _Logger.LogInfo("Call Back Method GetValidateResultClosePeriod");
                loRtn = loCls.GetValidateResultClosePeriod(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetValidateResultCloseGlPeriod");

            return loRtn;
        }
    }
}
