using APM00300BACK;
using APM00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace APM00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APM00300Controller : ControllerBase, IAPM00300
    {
        private LoggerAPM00300 _Logger;
        public APM00300Controller(ILogger<LoggerAPM00300> logger)
        {
            //Initial and Get Logger
            LoggerAPM00300.R_InitializeLogger(logger);
            _Logger = LoggerAPM00300.R_GetInstanceLogger();
        }

        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<APM00300DTO> GetAPSearchSupplierList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300DTO> loRtn = null;
            _Logger.LogInfo("Start GetAPSearchSupplierList");

            try
            {
                _Logger.LogInfo("Set Param GetAPSearchSupplierList");
                var poParam = new APM00300DTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CLOB_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CLOB_CODE);
                poParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                _Logger.LogInfo("Call Back Method GetAllSearchSupplier");
                var loCls = new APM00300Cls();
                var loTempRtn = loCls.GetAllSearchSupplier(poParam);

                _Logger.LogInfo("Call Stream Method Data GetAPSearchSupplierList");
                loRtn = GetStreamData<APM00300DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAPSearchSupplierList");

            return loRtn;
        }

        [HttpPost]
        public GLM00400SingleResult<APM00300InitialDTO> GetInitial()
        {
            var loEx = new R_Exception();
            GLM00400SingleResult<APM00300InitialDTO> loRtn = new GLM00400SingleResult<APM00300InitialDTO>();
            _Logger.LogInfo("Start GetInitial");

            try
            {
                _Logger.LogInfo("Set Param GetInitial");
                APM00300InitialDTO poParam = new APM00300InitialDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00300Cls();

                _Logger.LogInfo("Call Back Method GetInitial");
                loRtn.Data = loCls.GetInitial(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitial");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APM00300PropertyDTO> GetGSPropertyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300PropertyDTO> loRtn = null;
            var loParameter = new APM00300PropertyDTO();
            _Logger.LogInfo("Start GetGSPropertyList");

            try
            {
                var loCls = new APM00300Cls();

                _Logger.LogInfo("Set Param GetGSPropertyList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);
                _Logger.LogInfo("Call Stream Method Data GetGSPropertyList");
                loRtn = GetStreamData<APM00300PropertyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            _Logger.LogInfo("End GetGSPropertyList");
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APM00300LOBDTO> GetLOBList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300LOBDTO> loRtn = null;
            _Logger.LogInfo("Start GetLOBList");

            try
            {
                var loCls = new APM00300Cls();
                _Logger.LogInfo("Call Back Method GetAllLOB");
                var loResult = loCls.GetAllLOB();

                _Logger.LogInfo("Call Stream Method Data GetLOBList");
                loRtn = GetStreamData<APM00300LOBDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            _Logger.LogInfo("End GetLOBList");
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}