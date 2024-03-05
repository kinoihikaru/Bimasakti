using APT00500BACK;
using APT00500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace APT00500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00510Controller : ControllerBase, IAPT00510
    {
        private LoggerAPT00510 _Logger;
        private readonly ActivitySource _activitySource;
        public APT00510Controller(ILogger<LoggerAPT00510> logger)
        {
            //Initial and Get Logger
            LoggerAPT00510.R_InitializeLogger(logger);
            _Logger = LoggerAPT00510.R_GetInstanceLogger();
            _activitySource = APT00510ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(APT00510Controller));
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
        public IAsyncEnumerable<APT00510DTO> GetPurhcaseAdjustmentAllocStream()
        {
            using Activity activity = _activitySource.StartActivity("GetPurhcaseAdjustmentAllocStream");
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00510DTO> loRtn = null;
            _Logger.LogInfo("Start GetPurhcaseAdjustmentAllocStream");

            try
            {
                _Logger.LogInfo("Set Param GetPurhcaseAdjustmentAllocStream");
                var poParam = new APT00510DTO();
                poParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);

                _Logger.LogInfo("Call Back Method GetAllPurchaseAdjustmentAlloc");
                var loCls = new APT00510Cls();
                var loTempRtn = loCls.GetAllPurchaseAdjustmentAlloc(poParam);

                _Logger.LogInfo("Call Stream Method Data GetPurhcaseAdjustmentAllocStream");
                loRtn = GetStreamData<APT00510DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetPurhcaseAdjustmentAllocStream");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00510DTO> loRtn = new R_ServiceGetRecordResultDTO<APT00510DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APT00510");

            try
            {
                var loCls = new APT00510Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APT00510Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APT00510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APT00510DTO> loRtn = new R_ServiceSaveResultDTO<APT00510DTO>();
            _Logger.LogInfo("Start ServiceSave APT00510");

            try
            {
                var loCls = new APT00510Cls();

                _Logger.LogInfo("Call Back Method R_Save APT00510Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APT00510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00510DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceDelete");
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APT00510");

            try
            {
                var loCls = new APT00510Cls();

                _Logger.LogInfo("Call Back Method R_Delete APT00510Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APT00510");

            return loRtn;
        }
    }
}