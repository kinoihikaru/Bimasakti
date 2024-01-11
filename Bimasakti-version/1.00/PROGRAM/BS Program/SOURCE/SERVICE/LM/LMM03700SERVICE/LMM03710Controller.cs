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
    public class LMM03710Controller : ControllerBase, ILMM03710
    {
        private LoggerLMM03710 _Logger;
        public LMM03710Controller(ILogger<LoggerLMM03710> logger)
        {
            //Initial and Get Logger
            LoggerLMM03710.R_InitializeLogger(logger);
            _Logger = LoggerLMM03710.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM03700Record<LMM03711AssignTenantDTO> AssignTenantClass(LMM03711AssignTenantDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM03700Record<LMM03711AssignTenantDTO> loRtn = new();
            _Logger.LogInfo("Start AssignTenantClass");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Call Back Method GetTenantCLassTenantList");
                loCls.AssignTenantClass(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End AssignTenantClass");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03711DTO> GetAssignTenantCLassList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM03711DTO> loRtn = null;
            _Logger.LogInfo("Start GetAssignTenantCLassList");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Set Param GetAssignTenantCLassList");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                var loTenantClassGrpId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID);
                var loTenantClassId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTENANT_CLASSIFICATION_ID);

                _Logger.LogInfo("Call Back Method GetAssignTenantCLassList");
                var loResult = loCls.GetAllAssignTenantClass(loPropertyId, loTenantClassGrpId, loTenantClassId);

                _Logger.LogInfo("Call Stream Method Data GetAssignTenantCLassList");
                loRtn = GetStream<LMM03711DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetAssignTenantCLassList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03710DTO> GetTenantClassList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM03710DTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantClassList");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Set Param GetTenantClassList");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                var loTenantClassGrpId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID);

                _Logger.LogInfo("Call Back Method GetTenantClassList");
                var loResult = loCls.GetAllTenantClass(loPropertyId, loTenantClassGrpId);

                _Logger.LogInfo("Call Stream Method Data GetTenantClassList");
                loRtn = GetStream<LMM03710DTO>(loResult);
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
        public IAsyncEnumerable<LMM03711DTO> GetTenantCLassTenantList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM03711DTO> loRtn = null;
            _Logger.LogInfo("Start GetTenantCLassTenantList");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Set Param GetTenantCLassTenantList");
                var loPropertyId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                var loTenantClassId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTENANT_CLASSIFICATION_ID);

                _Logger.LogInfo("Call Back Method GetTenantCLassTenantList");
                var loResult = loCls.GetAllTenantClassTenant(loPropertyId, loTenantClassId);

                _Logger.LogInfo("Call Stream Method Data GetTenantCLassTenantList");
                loRtn = GetStream<LMM03711DTO>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetTenantCLassTenantList");

            return loRtn;
        }

        [HttpPost]
        public LMM03700Record<LMM03711MoveTenantDTO> MoveTenantClass(LMM03711MoveTenantDTO poEntity)
        {
            var loEx = new R_Exception();
            LMM03700Record<LMM03711MoveTenantDTO> loRtn = new();
            _Logger.LogInfo("Start MoveTenantClass");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Call Back Method MoveTenantClass");
                loCls.MoveTenantClass(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End MoveTenantClass");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03710DTO> poParameter)
        {
            var loEx = new R_Exception();
            _Logger.LogInfo("Start ServiceDelete LMM03710");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Call Back Method R_Delete LMM03710Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceDelete LMM03710");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM03710DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03710DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM03710DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM03710DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM03710");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM03710Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM03710");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM03710DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03710DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM03710DTO> loRtn = new R_ServiceSaveResultDTO<LMM03710DTO>();
            _Logger.LogInfo("Start ServiceSave LMM03710");

            try
            {
                var loCls = new LMM03710Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM03710Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM03710");

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