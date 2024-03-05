using APT00600BACK;
using APT00600COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace APT00600SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00600Controller : ControllerBase, IAPT00600
    {
        private LoggerAPT00600 _Logger;
        private readonly ActivitySource _activitySource;
        public APT00600Controller(ILogger<LoggerAPT00600> logger)
        {
            //Initial and Get Logger
            LoggerAPT00600.R_InitializeLogger(logger);
            _Logger = LoggerAPT00600.R_GetInstanceLogger();
            _activitySource = APT00600ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00600Controller));
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
        [HttpPost]
        public IAsyncEnumerable<APT00600DTO> GetPurhcaseAdjustmentStream()
        {
            using Activity activity = _activitySource.StartActivity("GetPurhcaseAdjustmentStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00600DTO> loRtn = null;
            _Logger.LogInfo("Start GetAPSearchSupplierList");

            try
            {
                _Logger.LogInfo("Set Param GetPurhcaseAdjustmentStream");
                var poParam = new APT00600ParamDTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poParam.CSUPPLIER_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSUPPLIER_ID);
                poParam.CFROM_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CFROM_PERIOD);
                poParam.CTO_PERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTO_PERIOD);

                _Logger.LogInfo("Call Back Method GetAllSearchSupplier");
                var loCls = new APT00600Cls();
                var loTempRtn = loCls.GetAllPurchaseAdjustment(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPurhcaseAdjustmentStream");
                loRtn = GetStreamData<APT00600DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPurhcaseAdjustmentStream");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00600DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00600DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00600DTO> loRtn = new R_ServiceGetRecordResultDTO<APT00600DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APT00600");

            try
            {
                var loCls = new APT00600Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APT00600Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APT00600");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00600DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00600DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APT00600DTO> loRtn = new R_ServiceSaveResultDTO<APT00600DTO>();
            _Logger.LogInfo("Start ServiceSave APT00600");

            try
            {
                var loCls = new APT00600Cls();

                _Logger.LogInfo("Call Back Method R_Save APT00600Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APT00600");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00600DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APT00600");

            try
            {
                var loCls = new APT00600Cls();

                _Logger.LogInfo("Call Back Method R_Delete APT00600Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APT00600");

            return loRtn;
        }

        [HttpPost]
        public APT00600SingleResult<APT00600SubmitRedraftDTO> SubmitPurchaseAdj(APT00600SubmitRedraftDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("SubmitPurchaseAdj");
            var loEx = new R_Exception();
            APT00600SingleResult<APT00600SubmitRedraftDTO> loRtn = new APT00600SingleResult<APT00600SubmitRedraftDTO>();
            _Logger.LogInfo("Start SubmitPurchaseAdj");

            try
            {
                var loCls = new APT00600Cls();

                _Logger.LogInfo("Call Back Method SubmitPurchaseAdjustment APT00500Cls");
                loCls.SubmitPurchaseAdjustment(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SubmitPurchaseAdj");

            return loRtn;
        }

        [HttpPost]
        public APT00600SingleResult<APT00600SubmitRedraftDTO> RedraftPurchaseAdj(APT00600SubmitRedraftDTO poEntity)
        {
            using Activity activity = _activitySource.StartActivity("RedraftPurchaseAdj");
            var loEx = new R_Exception();
            APT00600SingleResult<APT00600SubmitRedraftDTO> loRtn = new APT00600SingleResult<APT00600SubmitRedraftDTO>();
            _Logger.LogInfo("Start RedraftPurchaseAdj");

            try
            {
                var loCls = new APT00600Cls();

                _Logger.LogInfo("Call Back Method RedraftPurchaseAdjustment APT00500Cls");
                loCls.RedraftPurchaseAdjustment(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End RedraftPurchaseAdj");

            return loRtn;
        }
    }
}