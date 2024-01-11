using LMM03700BACK;
using LMM03700COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM03700SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03700Controller : ControllerBase, ILMM03700
    {
        private LoggerLMM03700 _Logger;
        public LMM03700Controller(ILogger<LoggerLMM03700> logger)
        {
            //Initial and Get Logger
            LoggerLMM03700.R_InitializeLogger(logger);
            _Logger = LoggerLMM03700.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03700PropetyDTO> GetPropertyList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM03700PropetyDTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantClassList");

            try
            {
                var loCls = new LMM03700Cls();

                _Logger.LogInfo("Call Back Method GetTenantClassList");
                var loResult = loCls.GetAllProperty();

                _Logger.LogInfo("Call Stream Method Data GetTenantClassList");
                loRtn = GetStream<LMM03700PropetyDTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTenantClassList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03700DTO> GetTenantClassGrpList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM03700DTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantClassList");

            try
            {
                var loCls = new LMM03700Cls();

                _Logger.LogInfo("Set Param GetTenantClassList");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);

                _Logger.LogInfo("Call Back Method GetTenantClassList");
                var loResult = loCls.GetAllTenantClassGrp(loPropertyId);

                _Logger.LogInfo("Call Stream Method Data GetTenantClassList");
                loRtn = GetStream<LMM03700DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTenantClassList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03700DTO> poParameter)
        {
            var loEx = new R_Exception();
            _Logger.LogInfo("Start ServiceDelete LMM03700");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new LMM03700Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM03700Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM03700");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM03700DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03700DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM03700DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM03700DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM03700");

            try
            {
                var loCls = new LMM03700Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM03700Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM03700");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM03700DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03700DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM03700DTO> loRtn = new R_ServiceSaveResultDTO<LMM03700DTO>();
            _Logger.LogInfo("Start ServiceSave LMM03700");

            try
            {
                var loCls = new LMM03700Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM03700Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM03700");

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