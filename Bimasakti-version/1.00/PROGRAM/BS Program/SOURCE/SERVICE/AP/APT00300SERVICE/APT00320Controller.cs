using APT00300BACK;
using APT00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace APT00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00320Controller : ControllerBase, IAPT00320
    {
        private LoggerAPT00320 _Logger;
        public APT00320Controller(ILogger<LoggerAPT00320> logger)
        {
            //Initial and Get Logger
            LoggerAPT00320.R_InitializeLogger(logger);
            _Logger = LoggerAPT00320.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00311DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APT00320");

            try
            {
                var loCls = new APT00320Cls();

                _Logger.LogInfo("Call Back Method R_Delete APT00320Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APT00320");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00311DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00311DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00311DTO> loRtn = new R_ServiceGetRecordResultDTO<APT00311DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APT00320");

            try
            {
                var loCls = new APT00320Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APT00320Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APT00320");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00311DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00311DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APT00311DTO> loRtn = new R_ServiceSaveResultDTO<APT00311DTO>();
            _Logger.LogInfo("Start ServiceSave APT00320");

            try
            {
                var loCls = new APT00320Cls();

                _Logger.LogInfo("Call Back Method R_Save APT00320Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APT00320");

            return loRtn;
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
    }
}