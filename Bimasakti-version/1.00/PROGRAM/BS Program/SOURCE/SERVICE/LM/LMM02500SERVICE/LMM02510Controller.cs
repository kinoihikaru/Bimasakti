using LMM02500BACK;
using LMM02500COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM02500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM02510Controller : ControllerBase, ILMM02510
    {
        private LoggerLMM02510 _Logger;
        public LMM02510Controller(ILogger<LoggerLMM02510> logger)
        {
            //Initial and Get Logger
            LoggerLMM02510.R_InitializeLogger(logger);
            _Logger = LoggerLMM02510.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM02510DTO> poParameter)
        {
            var loEx = new R_Exception();
            _Logger.LogInfo("Start ServiceDelete LMM02510");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new LMM02510Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM02510Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM02510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM02510DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM02510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM02510DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM02510DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM02510");

            try
            {
                var loCls = new LMM02510Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM02510Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM02510");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM02510DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM02510DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM02510DTO> loRtn = new R_ServiceSaveResultDTO<LMM02510DTO>();
            _Logger.LogInfo("Start ServiceSave LMM02500");

            try
            {
                var loCls = new LMM02510Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM02510Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM02500");

            return loRtn;
        }
       
        #region Get Stream Data
        private async IAsyncEnumerable<T> GetStream<T>(List<T> poList)
        {
            foreach (var item in poList)
            {
                yield return item;
            }
        }
        #endregion
    }
}