using LMM07000BACK;
using LMM07000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM07000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM07000UniversalController : ControllerBase, ILMM07000Universal
    {
        private LoggerLMM07000Universal _Logger;

        public LMM07000UniversalController(ILogger<LoggerLMM07000Universal> logger)
        {
            //Initial and Get Logger
            LoggerLMM07000Universal.R_InitializeLogger(logger);
            _Logger = LoggerLMM07000Universal.R_GetInstanceLogger();
        }

        // Method Stream
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTOUniversal> GetChargesTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOUniversal> loRtn = null;
            var loParameter = new LMM07000DTOUniversal();
            _Logger.LogInfo("Start GetChargesTypeList");

            try
            {
                var loCls = new LMM07000UniversalCls();

                _Logger.LogInfo("Set Param GetChargesTypeList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllChargesType");
                var loResult = loCls.GetAllChargesType(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetChargesTypeList");
                loRtn = GetStream<LMM07000DTOUniversal>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetChargesTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTOUniversal> GetDiscountTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOUniversal> loRtn = null;
            var loParameter = new LMM07000DTOUniversal();
            _Logger.LogInfo("Start GetDiscountTypeList");

            try
            {
                var loCls = new LMM07000UniversalCls();

                _Logger.LogInfo("Set Param GetDiscountTypeList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllDiscountType");
                var loResult = loCls.GetAllDiscountType(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetDiscountTypeList");
                loRtn = GetStream<LMM07000DTOUniversal>(loResult);
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
        public IAsyncEnumerable<LMM07000PeriodDTO> GetInvPeriodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000PeriodDTO> loRtn = null;
            var loParameter = new LMM07000PeriodDTO();
            _Logger.LogInfo("Start GetInvPeriodList");

            try
            {
                var loCls = new LMM07000UniversalCls();

                _Logger.LogInfo("Set Param GetInvPeriodList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method GetAllInvoicePeriod");
                var loResult = loCls.GetAllInvoicePeriod(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetInvPeriodList");
                loRtn = GetStream<LMM07000PeriodDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetInvPeriodList");

            return loRtn;
        }
    }
}