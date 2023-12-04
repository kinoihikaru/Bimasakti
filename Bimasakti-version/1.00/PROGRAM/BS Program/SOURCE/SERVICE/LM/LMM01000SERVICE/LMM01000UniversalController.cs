using LMM01000BACK;
using LMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01000UniversalController : ControllerBase, ILMM01000Universal
    {
        private LoggerLMM01000Universal _Logger;
        public LMM01000UniversalController(ILogger<LoggerLMM01000Universal> logger)
        {
            //Initial and Get Logger
            LoggerLMM01000Universal.R_InitializeLogger(logger);
            _Logger = LoggerLMM01000Universal.R_GetInstanceLogger();
        }

        private async IAsyncEnumerable<T> GetStream<T>(List<T> poParam)
        {
            foreach (var item in poParam)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetChargesTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetChargesTypeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetChargesTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllChargesType");
                var loResult = loCls.GetAllChargesType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetChargesTypeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
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
        public IAsyncEnumerable<LMM01000UniversalDTO> GetStatusList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetStatusList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetStatusList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllStatus");
                var loResult = loCls.GetAllStatus(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetStatusList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetStatusList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetTaxExemptionCodeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetTaxExemptionCodeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetTaxExemptionCodeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllTaxExemptionCode");
                var loResult = loCls.GetAllTaxExemptionCode(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetTaxExemptionCodeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxExemptionCodeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetWithholdingTaxTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetWithholdingTaxTypeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetWithholdingTaxTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllWithholdingTaxType");
                var loResult = loCls.GetAllWithholdingTaxType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetWithholdingTaxTypeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetWithholdingTaxTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetUsageRateModeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetUsageRateModeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetUsageRateModeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllUsageRateMode");
                var loResult = loCls.GetAllUsageRateMode(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetUsageRateModeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetUsageRateModeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetRateTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetRateTypeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetRateTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllRateType");
                var loResult = loCls.GetAllRateType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateTypeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetAdminFeeTypeList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAdminFeeTypeList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetAdminFeeTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAdminFeeType");
                var loResult = loCls.GetAdminFeeType(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetAdminFeeTypeList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAdminFeeTypeList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000UniversalDTO> GetAccrualMethodList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01000UniversalDTO> loRtn = null;
            _Logger.LogInfo("Start GetAccrualMethodList");

            try
            {
                var loCls = new LMM01000UniversalCls();

                var poParam = new LMM01000UniversalDTO();

                _Logger.LogInfo("Set Param GetAccrualMethodList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetAllAccrualMethod");
                var loResult = loCls.GetAllAccrualMethod(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetAccrualMethodList");
                loRtn = GetStream<LMM01000UniversalDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAccrualMethodList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000DTOPropety> GetProperty()
        {
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GetProperty");

            IAsyncEnumerable<LMM01000DTOPropety> loRtn = null;
            var loParameter = new LMM01000PropertyParameterDTO();

            try
            {
                var loCls = new LMM01000UniversalCls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method For Stream Data GetProperty");
                loRtn = GetStream<LMM01000DTOPropety>(loResult);
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
        public LMM01000AllResultInit GetAllInitLMM01000List()
        {
            var loEx = new R_Exception();
            _Logger.LogInfo("Start GetAllInitLMM01000List");

            LMM01000AllResultInit loRtn = new LMM01000AllResultInit();
            var loPropertyParameter = new LMM01000PropertyParameterDTO();
            var loUniversalParam = new LMM01000UniversalDTO();

            try
            {
                var loCls = new LMM01000UniversalCls();

                _Logger.LogInfo("Set Param");
                loPropertyParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loPropertyParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loUniversalParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loUniversalParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _Logger.LogInfo("Call Back Method GetProperty");
                loRtn.PropertyList = loCls.GetProperty(loPropertyParameter);
                loRtn.TaxExemptionCodeList = loCls.GetAllTaxExemptionCode(loUniversalParam);
                loRtn.WithholdingTaxTypeList = loCls.GetAllWithholdingTaxType(loUniversalParam);
                loRtn.AccrualMethodTypeList = loCls.GetAllAccrualMethod(loUniversalParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAllInitLMM01000List");

            return loRtn;
        }
    }
}