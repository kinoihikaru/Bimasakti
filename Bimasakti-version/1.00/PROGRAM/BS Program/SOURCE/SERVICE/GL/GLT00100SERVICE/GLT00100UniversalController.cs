using GLT00100BACK;
using GLT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;

namespace GLT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00100UniversalController : ControllerBase, IGLT00100Universal
    {
        private LoggerGLT00100Universal _Logger;
        public GLT00100UniversalController(ILogger<LoggerGLT00100Universal> logger)
        {
            //Initial and Get Logger
            LoggerGLT00100Universal.R_InitializeLogger(logger);
            _Logger = LoggerGLT00100Universal.R_GetInstanceLogger();
        }

        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public IAsyncEnumerable<GLT00100GSCenterDTO> GetCenterList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSCenterDTO> loRtn = null;
            _Logger.LogInfo("Start GetCenterList");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetCenterList");
                var loTempRtn = loCls.GetCenterList();

                _Logger.LogInfo("Call Stream Method Data GetCenterList");
                loRtn = StreamListData<GLT00100GSCenterDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetCenterList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLT00100GSCurrencyDTO> GetCurrencyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSCurrencyDTO> loRtn = null;
            _Logger.LogInfo("Start GetCurrencyList");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetCurrencyList");
                var loTempRtn = loCls.GetCurrencyList();

                _Logger.LogInfo("Call Stream Method Data GetCurrencyList");
                loRtn = StreamListData<GLT00100GSCurrencyDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetCurrencyList");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GLSystemParamDTO> GetGLSystemParam()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GLSystemParamDTO> loRtn = new GLT00100RecordResult<GLT00100GLSystemParamDTO>();
            _Logger.LogInfo("Start GetGLSystemParam");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGLSystemParam");
                loRtn.Data = loCls.GetGLSystemParamRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGLSystemParam");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLT00100GSGSBCodeDTO> GetGSBCodeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100GSGSBCodeDTO> loRtn = null;
            _Logger.LogInfo("Start GetGSBCodeList");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSBCodeList");
                var loTempRtn = loCls.GetGSBCodeList();

                _Logger.LogInfo("Call Stream Method Data GetGSBCodeList");
                loRtn = StreamListData<GLT00100GSGSBCodeDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSBCodeList");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GSCompanyInfoDTO> GetGSCompanyInfo()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSCompanyInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSCompanyInfoDTO>();
            _Logger.LogInfo("Start GetGSCompanyInfo");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSCompanyInfo");
                loRtn.Data = loCls.GetCompanyInfoRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSCompanyInfo");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> GetGSPeriodDTInfo(GLT00100ParamGSPeriodDTInfoDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSPeriodDTInfoDTO>();
            _Logger.LogInfo("Start GetGSPeriodDTInfo");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSPeriodDTInfo");
                loRtn.Data = loCls.GetPeriodDTInfoRecord(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPeriodDTInfo");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> GetGSPeriodYearRange()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO> loRtn = new GLT00100RecordResult<GLT00100GSPeriodYearRangeDTO>();
            _Logger.LogInfo("Start GetGSPeriodYearRange");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSPeriodYearRange");
                loRtn.Data = loCls.GetPeriodYearRangeRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSPeriodYearRange");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> GetGSSystemEnableOptionInfo()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GLSystemEnableOptionInfoDTO>();
            _Logger.LogInfo("Start GetGSSystemEnableOptionInfo");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSSystemEnableOptionInfo");
                loRtn.Data = loCls.GetGLSystemEnableOptionRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSSystemEnableOptionInfo");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100GSTransInfoDTO> GetGSTransCodeInfo()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100GSTransInfoDTO> loRtn = new GLT00100RecordResult<GLT00100GSTransInfoDTO>();
            _Logger.LogInfo("Start GetGSTransCodeInfo");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetGSTransCodeInfo");
                loRtn.Data = loCls.GetTransCodeInfoRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetGSTransCodeInfo");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100UniversalDTO> GetTabJournalListUniversalVar()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100UniversalDTO> loRtn = new GLT00100RecordResult<GLT00100UniversalDTO>();
            _Logger.LogInfo("Start GetTabJournalListUniversalVar");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetTabJournalListUniversalVar");
                loRtn.Data = new()
                {
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_IUNDO_COMMIT_JRN = loCls.GetGLSystemEnableOptionRecord(),
                    VAR_GSB_CODE_LIST = loCls.GetGSBCodeList(),
                    VAR_GSM_PERIOD = loCls.GetPeriodYearRangeRecord(),
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTabJournalListUniversalVar");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100TodayDateDTO> GetTodayDate()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100TodayDateDTO> loRtn = new GLT00100RecordResult<GLT00100TodayDateDTO>();
            _Logger.LogInfo("Start GetTodayDate");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetTodayDate");
                loRtn.Data = loCls.GetTodayDateRecord();
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTodayDate");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00110UniversalDTO> GetTabJournalEntryUniversalVar()
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00110UniversalDTO> loRtn = new GLT00100RecordResult<GLT00110UniversalDTO>();
            _Logger.LogInfo("Start GetTabJournalEntryUniversalVar");

            try
            {
                var loCls = new GLT00100UniversalCls();

                _Logger.LogInfo("Call Back Method GetTabJournalEntryUniversalVar");
                loRtn.Data = new()
                {
                    VAR_GSM_TRANSACTION_CODE = loCls.GetTransCodeInfoRecord(),
                    VAR_GSM_COMPANY = loCls.GetCompanyInfoRecord(),
                    VAR_TODAY = loCls.GetTodayDateRecord(),
                    VAR_GL_SYSTEM_PARAM = loCls.GetGLSystemParamRecord(),
                    VAR_CENTER_LIST = loCls.GetCenterList(),
                    VAR_CURRENCY_LIST = loCls.GetCurrencyList(),
                };
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTabJournalEntryUniversalVar");

            return loRtn;
        }
    }
}