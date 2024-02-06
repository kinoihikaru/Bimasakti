using APM00300BACK;
using APM00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace APM00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APM00310Controller : ControllerBase, IAPM00310
    {
        private LoggerAPM00310 _Logger;
        private readonly ActivitySource _activitySource;
        public APM00310Controller(ILogger<LoggerAPM00310> logger)
        {
            //Initial and Get Logger
            LoggerAPM00310.R_InitializeLogger(logger);
            _Logger = LoggerAPM00310.R_GetInstanceLogger();
            _activitySource = APM00310ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APM00310Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<APM00300CurrencyDTO> GetCurrencyList()
        {
            using Activity activity = _activitySource.StartActivity("GetCurrencyList");
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300CurrencyDTO> loRtn = null;
            _Logger.LogInfo("Start GetCurrencyList");

            try
            {
                _Logger.LogInfo("Set Param GetCurrencyList");
                var poParam = new APM00300CurrencyDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;


                var loCls = new APM00310Cls();
                _Logger.LogInfo("Call Back Method GetAllCurrency");
                var loTempRtn = loCls.GetAllCurrency(poParam);

                _Logger.LogInfo("Call Stream Method Data GetCurrencyList");
                loRtn = GetStreamData<APM00300CurrencyDTO>(loTempRtn);
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
        public IAsyncEnumerable<APM00300PayTermDTO> GetPayTermList()
        {
            using Activity activity = _activitySource.StartActivity("GetPayTermList");
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300PayTermDTO> loRtn = null;
            _Logger.LogInfo("Start GetPayTermList");

            try
            {
                _Logger.LogInfo("Set Param GetPayTermList");
                var poParam = new APM00300PayTermDTO();
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new APM00310Cls();
                _Logger.LogInfo("Call Back Method GetAllPayTerm");
                var loTempRtn = loCls.GetAllPayTerm(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPayTermList");
                loRtn = GetStreamData<APM00300PayTermDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPayTermList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APM00300TaxTypeDTO> GetTaxTypeList()
        {
            using Activity activity = _activitySource.StartActivity("GetTaxTypeList");
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00300TaxTypeDTO> loRtn = null;
            _Logger.LogInfo("Start GetTaxTypeList");

            try
            {
                var poParam = new APM00300TaxTypeDTO();
                _Logger.LogInfo("Set Param GetTaxTypeList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUANGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00310Cls();
                _Logger.LogInfo("Call Back Method GetAllTaxType");
                var loTempRtn = loCls.GetAllTaxType(poParam);

                _Logger.LogInfo("Call Stream Method Data GetTaxTypeList");
                loRtn = GetStreamData<APM00300TaxTypeDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTaxTypeList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APM00310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APM00300");

            try
            {
                var loCls = new APM00310Cls();

                _Logger.LogInfo("Call Back Method R_Delete APM00300Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APM00300");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APM00310DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APM00310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APM00310DTO> loRtn = new R_ServiceGetRecordResultDTO<APM00310DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APM00300");

            try
            {
                //Set Context Global
                _Logger.LogInfo("Set Param Entity ServiceGetRecord APM00300");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00310Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APM00300Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APM00300");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APM00310DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APM00310DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APM00310DTO> loRtn = new R_ServiceSaveResultDTO<APM00310DTO>();
            _Logger.LogInfo("Start ServiceSave APM00300");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave APM00300");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00310Cls();

                _Logger.LogInfo("Call Back Method R_Save APM00300Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APM00300");

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