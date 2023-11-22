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
    public class LMM01020Controller : ControllerBase, ILMM01020
    {
        private LoggerLMM01020 _Logger;
        public LMM01020Controller(ILogger<LoggerLMM01020> logger)
        {
            //Initial and Get Logger
            LoggerLMM01020.R_InitializeLogger(logger);
            _Logger = LoggerLMM01020.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01021DTO> GetRateWGList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<LMM01021DTO> loRtn = null;
            List<LMM01021DTO> loTempRtn = null;
            _Logger.LogInfo("Start GetRateWGList");

            try
            {
                var loCls = new LMM01020Cls();
                loTempRtn = new List<LMM01021DTO>();
                var poParam = new LMM01021DTO();

                _Logger.LogInfo("Set Param GetRateWGList");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CCHARGES_TYPE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_TYPE);
                poParam.CCHARGES_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CCHARGES_ID);
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _Logger.LogInfo("Call Back Method GetAllRateWGList");
                loTempRtn = loCls.GetAllRateWGList(poParam);

                _Logger.LogInfo("Call Stream Method For Stream Data GetRateWGList");
                loRtn = GetRateWGListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetRateWGList");

            return loRtn;
        }

        private async IAsyncEnumerable<LMM01021DTO> GetRateWGListStream(List<LMM01021DTO> poParameter)
        {
            foreach (LMM01021DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01020DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01020DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM01020DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01020DTO>();
            _Logger.LogInfo("Start ServiceGetRecord LMM01020");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceGetRecord LMM01020");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01020Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord LMM01020Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);

            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord LMM01020");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01020DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01020DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<LMM01020DTO> loRtn = new R_ServiceSaveResultDTO<LMM01020DTO>();
            _Logger.LogInfo("Start ServiceSave LMM01020");

            try
            {
                _Logger.LogInfo("Set Param Entity ServiceSave LMM01020");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }

                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01020Cls();

                _Logger.LogInfo("Call Back Method R_Save LMM01020Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave LMM01020");

            return loRtn;
        }
        
        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01020DTO> poParameter)
        {
            throw new NotImplementedException();
        }

       

    }
}