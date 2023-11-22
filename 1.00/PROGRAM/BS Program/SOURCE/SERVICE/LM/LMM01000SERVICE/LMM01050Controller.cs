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
    public class LMM01050Controller : ControllerBase, ILMM01050
    {
        private LoggerLMM01050 _Logger;
        public LMM01050Controller(ILogger<LoggerLMM01050> logger)
        {
            //Initial and Get Logger
            LoggerLMM01050.R_InitializeLogger(logger);
            _Logger = LoggerLMM01050.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01051DTO> GetRateOTList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01051DTO> loRtn = null;
            List<LMM01051DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateOTList");

            try
            {
                var loCls = new LMM01050Cls();
                loTempRtn = new List<LMM01051DTO>();
                var poParam = new LMM01051DTO();

                _Logger.LogInfo("Set Param GetRateOTList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CDAY_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDAY_TYPE);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateOTList");
                loTempRtn = loCls.GetAllRateOTList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateOTList");
                loRtn = GetRateOTListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateOTList");

            return loRtn;
        }

        private async IAsyncEnumerable<LMM01051DTO> GetRateOTListStream(List<LMM01051DTO> poParameter)
        {
            foreach (LMM01051DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01050DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01050DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01050DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01050DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01050");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01050");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01050Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01050Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01050");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01050DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01050DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01050DTO> loRtn = new R_ServiceSaveResultDTO<LMM01050DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01050");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01050");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01050Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01050Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01050");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01050DTO> poParameter)
        {
            throw new NotImplementedException();
        }

    }
}