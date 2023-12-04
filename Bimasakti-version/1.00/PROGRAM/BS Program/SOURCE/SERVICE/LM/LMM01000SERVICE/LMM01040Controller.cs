using LMM01000BACK;
using LMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace LMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01040Controller : ControllerBase, ILMM01040
    {
        private LoggerLMM01040 _Logger;

        public LMM01040Controller(ILogger<LoggerLMM01040> logger)
        {
            //Initial and Get Logger
            LoggerLMM01040.R_InitializeLogger(logger);
            _Logger = LoggerLMM01040.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01040DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01040DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01040DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01040DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01040");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01040");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01040Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01040Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01040");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01040DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01040DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01040DTO> loRtn = new R_ServiceSaveResultDTO<LMM01040DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01040");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01040");

                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01040Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01040Cls");

                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01040");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01040DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        
    }
}