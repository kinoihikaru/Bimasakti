using LMM02500BACK;
using LMM02500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace LMM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM02500UniversalController : ControllerBase, ILMM02500Universal
    {
        private LoggerLMM02500Universal _Logger;
        public LMM02500UniversalController(ILogger<LoggerLMM02500Universal> logger)
        {
            //Initial and Get Logger
            LoggerLMM02500Universal.R_InitializeLogger(logger);
            _Logger = LoggerLMM02500Universal.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM02500Record<LMM02510AllUniversalDataDTO> GetALlUniversalData()
        {
            R_Exception loException = new R_Exception();
            _Logger.LogInfo("Start GetALlUniversalData");

            LMM02500Record<LMM02510AllUniversalDataDTO> loRtn = new LMM02500Record<LMM02510AllUniversalDataDTO>();

            try
            {
                LMM02500UniversalCls loCls = new LMM02500UniversalCls();

                _Logger.LogInfo("Call Back Method GetALlUniversalData");
                loRtn.Data = new LMM02510AllUniversalDataDTO 
                { 
                    ID_TYPE_LIST = loCls.GetAllIDType(),
                    TAX_CODE_LIST = loCls.GetAllTaxCode(),
                    TAX_TYPE_LIST = loCls.GetAllTaxType()
                };
                
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetALlUniversalData");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500UniversalDTO> GetIDTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetIDTypeist");

            try
            {
                var loCls = new LMM02500UniversalCls();

                _Logger.LogInfo("Call Back Method GetIDTypeist");
                var loResult = loCls.GetAllIDType();

                _Logger.LogInfo("Call Stream Method Data GetIDTypeist");
                loRtn = GetStream<LMM02500UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetIDTypeist");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500PropertyDTO> GetPropertyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetPropertyist");

            try
            {
                var loCls = new LMM02500UniversalCls();

                _Logger.LogInfo("Call Back Method GetPropertyist");
                var loResult = loCls.GetProperty();

                _Logger.LogInfo("Call Stream Method Data GetPropertyist");
                loRtn = GetStream<LMM02500PropertyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPropertyist");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500UniversalDTO> GetTaxCodeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetTaxCodeist");

            try
            {
                var loCls = new LMM02500UniversalCls();

                _Logger.LogInfo("Call Back Method GetTaxCodeist");
                var loResult = loCls.GetAllTaxCode();

                _Logger.LogInfo("Call Stream Method Data GetTaxCodeist");
                loRtn = GetStream<LMM02500UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxCodeist");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM02500UniversalDTO> GetTaxTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM02500UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetTaxTypeist");

            try
            {
                var loCls = new LMM02500UniversalCls();

                _Logger.LogInfo("Call Back Method GetTaxTypeist");
                var loResult = loCls.GetAllTaxType();

                _Logger.LogInfo("Call Stream Method Data GetTaxTypeist");
                loRtn = GetStream<LMM02500UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxTypeist");

            return loRtn;
        }

        #region Get Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
        #endregion
    }
}