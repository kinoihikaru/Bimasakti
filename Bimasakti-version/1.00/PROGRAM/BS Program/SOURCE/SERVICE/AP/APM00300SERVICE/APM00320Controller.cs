using APM00300BACK;
using APM00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace APM00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APM00320Controller : ControllerBase, IAPM00320
    {
        private LoggerAPM00320 _Logger;
        public APM00320Controller(ILogger<LoggerAPM00320> logger)
        {
            //Initial and Get Logger
            LoggerAPM00320.R_InitializeLogger(logger);
            _Logger = LoggerAPM00320.R_GetInstanceLogger();
        }

        [HttpPost]
        public GLM00400SingleResult<APM00320DTO> GetSupplierInfo(APM00320DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00400SingleResult<APM00320DTO> loRtn = new GLM00400SingleResult<APM00320DTO>();
            APM00320Cls loCls = new APM00320Cls();
            _Logger.LogInfo("Start GetSupplierInfo");

            try
            {
                _Logger.LogInfo("Set Param GetSupplierInfo");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetSupplierInfo");
                loRtn.Data = loCls.GetSupplierInfo(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSupplierInfo");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APM00321DTO> GetSupplierSeqList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00321DTO> loRtn = null;
            APM00321PARAMDTO loParameter = null;
            _Logger.LogInfo("Start GetSupplierSeqList");

            try
            {
                var loCls = new APM00320Cls();
                loParameter = new APM00321PARAMDTO();

                _Logger.LogInfo("Set Param GetSupplierSeqList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                loParameter.CSUPPLIER_REC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSUPPLIER_REC_ID);

                _Logger.LogInfo("Call Back Method GetSupplierSeq");
                var loResult = loCls.GetSupplierSeq(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetSupplierSeqList");
                loRtn = GetStreamData<APM00321DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSupplierSeqList");

            return loRtn;
        }

        [HttpPost]
        public GLM00400SingleResult<APM00320DTO> SaveSupplierInfo(APM00320DTO poParam)
        {
            R_Exception loException = new R_Exception();
            GLM00400SingleResult<APM00320DTO> loRtn = new GLM00400SingleResult<APM00320DTO>();
            APM00320Cls loCls = new APM00320Cls();
            _Logger.LogInfo("Start SaveSupplierInfo");

            try
            {
                _Logger.LogInfo("Set Param SaveSupplierInfo");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method SaveSupplierInfo");
                loRtn.Data = loCls.SaveSupplierInfo(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveSupplierInfo");

            return loRtn;
        }

       
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

    }
}