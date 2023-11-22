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
    public class LMM01000Controller : ControllerBase, ILMM01000
    {
        private LoggerLMM01000 _LMM01000logger;

        public LMM01000Controller(ILogger<LoggerLMM01000> logger)
        {
            //Initial and Get Logger
            LoggerLMM01000.R_InitializeLogger(logger);
            _LMM01000logger = LoggerLMM01000.R_GetInstanceLogger();
        }

        private async IAsyncEnumerable<T> GetChargesUtilityListStream<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01000DTOPropety> GetProperty()
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start GetProperty");

            IAsyncEnumerable<LMM01000DTOPropety> loRtn = null;
            var loParameter = new LMM01000PropertyParameterDTO();

            try
            {
                var loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Set Param GetProperty");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _LMM01000logger.LogInfo("Call Back Method GetProperty");
                var loResult = loCls.GetProperty(loParameter);

                _LMM01000logger.LogInfo("Call Stream Method For Stream Data GetProperty");
                loRtn = GetChargesUtilityListStream<LMM01000DTOPropety>(loResult);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End GetProperty");

            return loRtn;
        }

        [HttpPost]
        public LMM01000UniversalDTO GetChargesType()
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start GetChargesType");

            LMM01000UniversalDTO loRtn = new LMM01000UniversalDTO();

            try
            {
                var loCls = new LMM01000Cls();

                var poParam = new LMM01000UniversalDTO();

                _LMM01000logger.LogInfo("Set Param GetChargesType");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_LANGUAGE = R_BackGlobalVar.CULTURE;

                _LMM01000logger.LogInfo("Call Back Method GetChargesType");
                var loResult = loCls.GetChargesType(poParam);

                loRtn = loResult;
            }
            catch (Exception ex)
            {
                _LMM01000logger.LogError(ex);
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End GetChargesType");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<LMM01002DTO> GetChargesUtilityList()
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start GetChargesUtilityList");

            IAsyncEnumerable<LMM01002DTO> loRtn = null;
            var loParameter = new LMM01002DTO();
            List<LMM01002DTO> loTempRtn = null;

            try
            {
                var loCls = new LMM01000Cls();
                loTempRtn = new List<LMM01002DTO>();

                //Set Param
                _LMM01000logger.LogInfo("Set Param GetChargesUtilityList");
                loParameter.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParameter.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                loParameter.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                loParameter.CUSER_ID = R_BackGlobalVar.USER_ID;

                _LMM01000logger.LogInfo("Call Back Method GetAllChargesUtility");
                loTempRtn = loCls.GetAllChargesUtility(loParameter);

                _LMM01000logger.LogInfo("Call Stream Method For Stream Data GetChargesUtilityList");
                loRtn = GetChargesUtilityListStream<LMM01002DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End GetChargesUtilityList");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM01000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start ServiceGetRecord LMM01000");

            R_ServiceGetRecordResultDTO<LMM01000DTO> loRtn = new R_ServiceGetRecordResultDTO<LMM01000DTO>();

            try
            {
                _LMM01000logger.LogInfo("Set Param Entity ServiceGetRecord LMM01000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Call Back Method R_GetRecord LMM01000Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End R_GetRecord LMM01000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM01000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start ServiceSave LMM01000");

            R_ServiceSaveResultDTO<LMM01000DTO> loRtn = new R_ServiceSaveResultDTO<LMM01000DTO>();

            try
            {
                _LMM01000logger.LogInfo("Set Param Entity ServiceSave LMM01000");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CCHARGES_TYPE = R_Utility.R_GetContext<string>(ContextConstant.CCHARGES_TYPE);
                    poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                    poParameter.Entity.CPROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.CPROPERTY_ID);
                }
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Call Back Method R_Save LMM01000Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End ServiceSave LMM01000");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM01000DTO> poParameter)
        {
            var loEx = new R_Exception();
            _LMM01000logger.LogInfo("Start ServiceDelete LMM01000");

            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();

            try
            {
                _LMM01000logger.LogInfo("Set Param Entity ServiceDelete LMM01000");
                poParameter.Entity.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.Entity.CUSER_ID = R_BackGlobalVar.USER_ID;

                var loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Call Back Method R_Delete LMM01000Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _LMM01000logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End ServiceDelete LMM01000");

            return loRtn;
        }

        [HttpPost]
        public LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam)
        {
            R_Exception loException = new R_Exception();
            _LMM01000logger.LogInfo("Start LMM01000ActiveInactive");

            LMM01000DTO loRtn = new LMM01000DTO();

            try
            {
                LMM01000Cls loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Set Param LMM01000ActiveInactive");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _LMM01000logger.LogInfo("Call Back Method LMM01000ChangeStatusSP");
                loCls.LMM01000ChangeStatusSP(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LMM01000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End LMM01000ActiveInactive");

            return loRtn;
        }

        [HttpPost]
        public LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam)
        {
            R_Exception loException = new R_Exception();
            _LMM01000logger.LogInfo("Start LMM01000CopyNewCharges");

            LMM01003DTO loRtn = new LMM01003DTO();

            try
            {
                LMM01000Cls loCls = new LMM01000Cls();

                _LMM01000logger.LogInfo("Set Param LMM01000CopyNewCharges");
                poParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParam.CUSER_ID = R_BackGlobalVar.USER_ID;

                _LMM01000logger.LogInfo("Set Param LMM01000CopySource");
                loCls.LMM01000CopySource(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _LMM01000logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _LMM01000logger.LogInfo("End LMM01000CopyNewCharges");

            return loRtn;
        }
    }
}