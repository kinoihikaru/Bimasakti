﻿using LMM07000BACK;
using LMM07000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace LMM07000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM07000Controller : ControllerBase, ILMM07000
    {
        private readonly ActivitySource _activitySource;

        private LoggerLMM07000 _Logger;

        public LMM07000Controller(ILogger<LoggerLMM07000> logger)
        {
            var loEx = new R_Exception();
            try
            {
                //Initial and Get Logger
                LoggerLMM07000.R_InitializeLogger(logger);
                _Logger = LoggerLMM07000.R_GetInstanceLogger();
                _activitySource = LMM07000ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM07000Controller));

                _Logger.LogInfo("Success Contructor");
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }
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
        public IAsyncEnumerable<LMM07000DTO> GetChargesDiscountList()
        {
            using Activity activity = _activitySource.StartActivity("GetChargesDiscountList");
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTO> loRtn = null;
            var loParameter = new LMM07000DTO();
            _Logger.LogInfo("Start GetChargesDiscountList");

            try
            {
                var loCls = new PMM07000Cls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);

                _Logger.LogInfo("Call Back Method GetAllChargesDiscount");
                var loResult = loCls.GetAllChargesDiscount(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetChargesDiscountList");
                loRtn = GetStream<LMM07000DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetChargesDiscountList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM07000DTOInitial> GetProperty()
        {
            using Activity activity = _activitySource.StartActivity("GetProperty");
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM07000DTOInitial> loRtn = null;
            var loParameter = new LMM07000DTOInitial();
            _Logger.LogInfo("Start GetProperty");

            try
            {
                var loCls = new PMM07000Cls();

                _Logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _Logger.LogInfo("Call Stream Method Data GetProperty");
                loRtn = GetStream<LMM07000DTOInitial>(loResult);
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM07000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start ServiceDelete LMM07000");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceDelete LMM07000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                var loCls = new PMM07000Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM07000Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM07000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM07000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM07000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM07000DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM07000DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM07000");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM07000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM07000Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM07000Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM07000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM07000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM07000DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM07000DTO> loRtn = new R_ServiceSaveResultDTO<LMM07000DTO>();
            _Logger.LogInfo("Start ServiceSave LMM07000");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM07000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new PMM07000Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM07000Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM07000");

            return loRtn;
        }

        [HttpPost]
        public LMM07000Record<LMM07000DTO> LMM07000ActiveInactive(LMM07000DTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("LMM07000ActiveInactive");
            R_Exception loException = new R_Exception();
            LMM07000Record<LMM07000DTO> loRtn = new LMM07000Record<LMM07000DTO>();
            _Logger.LogInfo("Start LMM07000ActiveInactive");

            try
            {
                PMM07000Cls loCls = new PMM07000Cls();

                _Logger.LogInfo("Set Param LMM07000ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method LMM00700ActiveInactiveSP");
                loCls.LMM00700ActiveInactiveSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End LMM07000ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public LMM07000Record<LMM07000PeriodDTO> GetMaxInvoicePeriodValue(LMM07000PeriodDTO poParam)
        {
            using Activity activity = _activitySource.StartActivity("GetMaxInvoicePeriodValue");
            R_Exception loException = new R_Exception();
            LMM07000Record<LMM07000PeriodDTO> loRtn = null;
            _Logger.LogInfo("Start GetMaxInvoicePeriodValue");

            try
            {
                loRtn = new LMM07000Record<LMM07000PeriodDTO> ();
                PMM07000Cls loCls = new PMM07000Cls();

                //set global var
                _Logger.LogInfo("Set Param GetMaxInvoicePeriodValue");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;

                _Logger.LogInfo("Call Back Method GetMaxInvoicePeriod");
                loRtn.Data = loCls.GetMaxInvoicePeriod(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _Logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetMaxInvoicePeriodValue");

            return loRtn;
        }
    }
}