using APT00300BACK;
using APT00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace APT00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00300UniversalController : ControllerBase, IAPT00300Universal
    {
        private LoggerAPT00300Universal _Logger;
        public APT00300UniversalController(ILogger<LoggerAPT00300Universal> logger)
        {
            //Initial and Get Logger
            LoggerAPT00300Universal.R_InitializeLogger(logger);
            _Logger = LoggerAPT00300Universal.R_GetInstanceLogger();
        }

        [HttpPost]
        public APT00300SingleResult<APT00300ALLInitialTab1Result> GetAllInitialProcessTab1()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300ALLInitialTab1Result> loRtn = new APT00300SingleResult<APT00300ALLInitialTab1Result>();
            _Logger.LogInfo("Start GetAllInitialProcess");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetAllInitialProcess");
                loRtn.Data = loCls.GetALLInitial();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcess");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300ALLInitialTab2Result> GetAllInitialProcessTab2()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300ALLInitialTab2Result> loRtn = new APT00300SingleResult<APT00300ALLInitialTab2Result>();
            _Logger.LogInfo("Start GetAllInitialProcess");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetAllInitialProcess");
                loRtn.Data = new APT00300ALLInitialTab2Result
                {
                    VAR_PROPERTY_LIST = loCls.GetProperty(),
                    VAR_CURRENCY_LIST = loCls.GetCurrencyList(),
                    VAR_AP_SYSTEM_PARAM = loCls.GetAPSystemParam(),
                    VAR_GSM_COMPANY = loCls.GetGSCompanyInfo(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetGSTransCode()
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitialProcess");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300APSystemParamDTO> GetInitialAPSystemParam()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300APSystemParamDTO> loRtn = new APT00300SingleResult<APT00300APSystemParamDTO>();
            _Logger.LogInfo("Start GetInitialAPSystemParam");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetInitialAPSystemParam");
                loRtn.Data = loCls.GetAPSystemParam();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialAPSystemParam");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APT00300CurrencyDTO> GetInitialCurrencyListStream()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00300CurrencyDTO> loRtn = null;
            _Logger.LogInfo("Start GetInitialCurrencyListStream");

            try
            {
                _Logger.LogInfo("Call Back Method GetInitialCurrencyListStream");
                var loCls = new APT00300UniversalCls();
                var loTempRtn = loCls.GetCurrencyList();

                _Logger.LogInfo("Call Stream Method Data GetInitialCurrencyListStream");
                loRtn = GetStreamData<APT00300CurrencyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialCurrencyListStream");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300GLSystemParamDTO> GetInitialGLSystemParam()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300GLSystemParamDTO> loRtn = new APT00300SingleResult<APT00300GLSystemParamDTO>();
            _Logger.LogInfo("Start GetInitialGLSystemParam");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetInitialGLSystemParam");
                loRtn.Data = loCls.GetGLSystemParam();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGLSystemParam");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APT00300GSBCodeDTO> GetInitialGSBCodeListStream()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00300GSBCodeDTO> loRtn = null;
            _Logger.LogInfo("Start GetInitialGSBCodeListStream");

            try
            {
                _Logger.LogInfo("Call Back Method GetInitialGSBCodeListStream");
                var loCls = new APT00300UniversalCls();
                var loTempRtn = loCls.GetGSBCodeList();

                _Logger.LogInfo("Call Stream Method Data GetInitialGSBCodeListStream");
                loRtn = GetStreamData<APT00300GSBCodeDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGSBCodeListStream");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300GSCompanyInfoDTO> GetInitialGSMCompanyInfo()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300GSCompanyInfoDTO> loRtn = new APT00300SingleResult<APT00300GSCompanyInfoDTO>();
            _Logger.LogInfo("Start GetInitialGSMCompanyInfo");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetInitialGSMCompanyInfo");
                loRtn.Data = loCls.GetGSCompanyInfo();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGSMCompanyInfo");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300PeriodYearRangeDTO> GetInitialGSMPeriodWithParameter(APT00300PeriodYearRangeDTO poEntity)
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300PeriodYearRangeDTO> loRtn = new APT00300SingleResult<APT00300PeriodYearRangeDTO>();
            _Logger.LogInfo("Start GetInitialGSMPeriodWithParameter");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetInitialGSMPeriodWithParameter");
                loRtn.Data = loCls.GetPeriodYearRange(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGSMPeriodWithParameter");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00300GSTransCodeInfoDTO> GetInitialGSMTransCode()
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00300GSTransCodeInfoDTO> loRtn = new APT00300SingleResult<APT00300GSTransCodeInfoDTO>();
            _Logger.LogInfo("Start GetInitialGSMTransCode");

            try
            {
                var loCls = new APT00300UniversalCls();

                _Logger.LogInfo("Call Back Method GetInitialGSMTransCode");
                loRtn.Data = loCls.GetGSTransCode();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialGSMTransCode");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APT00300PropertyDTO> GetInitialPropertyListStream()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00300PropertyDTO> loRtn = null;
            _Logger.LogInfo("Start GetInitialPropertyListStream");

            try
            {
                _Logger.LogInfo("Call Back Method GetInitialPropertyListStream");
                var loCls = new APT00300UniversalCls();
                var loTempRtn = loCls.GetProperty();

                _Logger.LogInfo("Call Stream Method Data GetInitialPropertyListStream");
                loRtn = GetStreamData<APT00300PropertyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInitialPropertyListStream");

            return loRtn;
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}