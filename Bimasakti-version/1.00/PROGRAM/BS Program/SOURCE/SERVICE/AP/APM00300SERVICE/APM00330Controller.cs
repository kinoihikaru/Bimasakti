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
    public class APM00330Controller : ControllerBase, IAPM00330
    {
        private LoggerAPM00330 _Logger;
        private readonly ActivitySource _activitySource;
        public APM00330Controller(ILogger<LoggerAPM00330> logger)
        {
            //Initial and Get Logger
            LoggerAPM00330.R_InitializeLogger(logger);
            _Logger = LoggerAPM00330.R_GetInstanceLogger();
            _activitySource = APM00330ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APM00330Controller));
        }

        [HttpPost]
        public IAsyncEnumerable<APM00330DTO> GetSupplierBankList()
        {
            using Activity activity = _activitySource.StartActivity("GetSupplierBankList");
            var loEx = new R_Exception();
            IAsyncEnumerable<APM00330DTO> loRtn = null;
            _Logger.LogInfo("Start GetSupplierBankList");

            try
            {
                var poParam = new APM00330DTO();
                _Logger.LogInfo("Set Param GetSupplierBankList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;
                poParam.CSUPPLIER_REC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSUPPLIER_REC_ID);

                var loCls = new APM00330Cls();
                _Logger.LogInfo("Call Back Method GetAllSupplierBank");
                var loTempRtn = loCls.GetAllSupplierBank(poParam);

                _Logger.LogInfo("Call Stream Method Data GetSupplierBankList");
                loRtn = GetStreamData<APM00330DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetSupplierBankList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APM00330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APM03300");

            try
            {
                var loCls = new APM00330Cls();

                _Logger.LogInfo("Set Param Entity ServiceGetRecord APM03300Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APM03300");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APM00330DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APM00330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APM00330DTO> loRtn = new R_ServiceGetRecordResultDTO<APM00330DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APM03300");

            try
            {
                //Set Context Global
                _Logger.LogInfo("Set Param Entity ServiceGetRecord APM03300");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00330Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APM03300Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APM03300");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APM00330DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APM00330DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APM00330DTO> loRtn = new R_ServiceSaveResultDTO<APM00330DTO>();
            _Logger.LogInfo("Start ServiceSave APM03300");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave APM03300");
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                var loCls = new APM00330Cls();

                _Logger.LogInfo("Call Back Method R_Save APM03300Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APM03300");

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