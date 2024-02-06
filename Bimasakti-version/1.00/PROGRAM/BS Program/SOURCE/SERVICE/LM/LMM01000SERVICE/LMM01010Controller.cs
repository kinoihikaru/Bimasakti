using LMM01000BACK;
using LMM01000COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;

namespace LMM01000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM01010Controller : ControllerBase, ILMM01010
    {
        private LoggerLMM01010 _Logger;
        private readonly ActivitySource _activitySource;

        public LMM01010Controller(ILogger<LoggerLMM01010> logger)
        {
            //Initial and Get Logger
            LoggerLMM01010.R_InitializeLogger(logger);
            _Logger = LoggerLMM01010.R_GetInstanceLogger();
            _activitySource = LMM01010ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(LMM01010Controller));
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01010DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01010DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceGetRecord");
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01010DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01010DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01010");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01010");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01010Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01010");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01010DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01010DTO> poParameter)
        {
            using Activity activity = _activitySource.StartActivity("R_ServiceSave");
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01010DTO> loRtn = new R_ServiceSaveResultDTO<LMM01010DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01010");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01010");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01010Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01010Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01010");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01010DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01011DTO> GetRateECList()
        {
            using Activity activity = _activitySource.StartActivity("GetRateECList");
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01011DTO> loRtn = null;
            List<LMM01011DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateECList");

            try
            {
                var loCls = new LMM01010Cls();
                loTempRtn = new List<LMM01011DTO>();
                var poParam = new LMM01011DTO();

                _Logger.LogInfo("Set Param GetRateECList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateECList");
                loTempRtn = loCls.GetAllRateECList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateECList");
                loRtn = GetRateECListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateECList");

            return loRtn;
        }

        private async IAsyncEnumerable<LMM01011DTO> GetRateECListStream(List<LMM01011DTO> poParameter)
        {
            foreach (LMM01011DTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}