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
    public class LMM01030Controller : ControllerBase, ILMM01030
    {
        private LoggerLMM01030 _Logger;

        public LMM01030Controller(ILogger<LoggerLMM01030> logger)
        {
            //Initial and Get Logger
            LoggerLMM01030.R_InitializeLogger(logger);
            _Logger = LoggerLMM01030.R_GetInstanceLogger();
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01030DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01030DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01030DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01030DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01030DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01030");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01030");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01030Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01030Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01030");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01030DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01030DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01030DTO> loRtn = new R_ServiceSaveResultDTO<LMM01030DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01030");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01030");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01030Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01030Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01030");

            return loRtn;
        }
    }
}