using Lookup_LMBACK;
using Lookup_LMCOMMON;
using Lookup_LMCOMMON.DTOs;
using Lookup_LMCOMMON.Logs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lookup_LMSERVICES
{
    [ApiController]
    [Route("api/[controller]/[action]"), AllowAnonymous]
    public class PublicLookupLMGetRecordController : ControllerBase, IGetRecordLookupLM
    {
        private LoggerLookupLM _loggerLookup;
        private readonly ActivitySource _activitySource;

        public PublicLookupLMGetRecordController(ILogger<LoggerLookupLM> logger)
        {
            //Initial and Get Logger
            LoggerLookupLM.R_InitializeLogger(logger);
            _loggerLookup = LoggerLookupLM.R_GetInstanceLogger();
            _activitySource = LookupLMActivity.R_InitializeAndGetActivitySource(nameof(PublicLookupLMGetRecordController));
        }
        [HttpPost]
        public LMLGenericRecord<LML00100DTO> LML00100GetSalesTax(LML00100ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00100GetSalesTax);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00100DTO> loReturn= new();
            LML00100ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00100ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _loggerLookup.LogInfo("Call method GetAllSalesTax");
                var loTempList = loCls.GetAllSalesTax(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Sales Tax");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CTAX_ID == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch ( Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00200DTO> LML00200UnitCharges(LML00200ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00200UnitCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00200DTO> loReturn = new();
            LML00200ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00200ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGE_TYPE_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);

                _loggerLookup.LogInfo("Call method GetUnitCharges");
                var loTempList = loCls.GetAllUnitCharges(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Unit Charges");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CCHARGES_ID == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00300DTO> LML00300Supervisor(LML00300ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00300Supervisor);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00300DTO> loReturn = new();
            LML00300ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00300ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
       
                _loggerLookup.LogInfo("Call method GetSupervisor");
                var loTempList = loCls.GetAllSupervisor(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Supervisor");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CSUPERVISOR == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00400DTO> LML00400UtilityCharges(LML00400ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00400UtilityCharges);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00400DTO> loReturn = new();
            LML00400ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00400ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo("Call method GetUtilityCharges");
                var loTempList = loCls.GetAllUtilityCharges(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Utility Charges");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CCHARGES_ID == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00500DTO> LML00500Salesman(LML00500ParameterDTO poParam)
        {

            string lcMethodName = nameof(LML00500Salesman);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00500DTO> loReturn = new();
            LML00500ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter = new LML00500ParameterDTO();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);

                _loggerLookup.LogInfo("Call method GetSalesman");
                var loTempList = loCls.GetAllSalesman(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Salesman");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CSALESMAN_ID == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00600DTO> LML00600Tenant(LML00600ParameterDTO poParam)
        {

            string lcMethodName = nameof(LML00600Tenant);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00600DTO> loReturn = new();
            LML00600ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCUSTOMER_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCUSTOMER_TYPE);

                _loggerLookup.LogInfo("Call method GetAllTenant");
                var loTempList = loCls.GetTenant(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Tenant");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CTENANT_ID == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
        [HttpPost]
        public LMLGenericRecord<LML00700DTO> LML00700Discount(LML00700ParameterDTO poParam)
        {
            string lcMethodName = nameof(LML00700Discount);
            using Activity activity = _activitySource.StartActivity(lcMethodName);
            _loggerLookup.LogInfo(string.Format("START process method {0} on Controller", lcMethodName));

            var loEx = new R_Exception();
            LMLGenericRecord<LML00700DTO> loReturn = new();
            LML00700ParameterDTO poParameter = null;
            try
            {
                var loCls = new PublicLookupLMCls();
                poParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CUSER_ID = R_BackGlobalVar.USER_ID; 
                poParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CPROPERTY_ID);
                poParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstantPublicLookup.CCHARGE_TYPE_ID);

                _loggerLookup.LogInfo("Call method GetAllDiscount");
                var loTempList = loCls.GetDiscount(poParameter);

                _loggerLookup.LogInfo("Filter Search by text Discount");
#pragma warning disable CS8601 // Possible null reference assignment.
                loReturn.Data = loTempList.Find(x => x.CDISCOUNT_CODE == poParam.CSEARCH_TEXT.Trim());
#pragma warning restore CS8601 // Possible null reference assignment.

            }
            catch (Exception ex)
            {

                loEx.Add(ex);
                _loggerLookup.LogError(loEx);
            }
            loEx.ThrowExceptionIfErrors();
            _loggerLookup.LogInfo(string.Format("END process method {0} on Controller", lcMethodName));
            return loReturn;
        }
    }
}
