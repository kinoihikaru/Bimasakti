using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LMM03505Controller : ControllerBase, ILMM03505
    {
        private LoggerLMM03505 _logger;
        public LMM03505Controller(ILogger<LMM03505Controller> logger)
        {
            LoggerLMM03505.R_InitializeLogger(logger);
            _logger = LoggerLMM03505.R_GetInstanceLogger();
        }
        [HttpPost]
        public IAsyncEnumerable<LMM03505DTO> GetBankInfoList()
        {
            _logger.LogInfo("Start || GetBankInfoList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM03505DTO> loRtn = null;
            GetBankInfoParameterDTO loParam = new GetBankInfoParameterDTO();
            LMM03505Cls loCls = new LMM03505Cls();
            List<LMM03505DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetBankInfoList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03505_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03505_TENANT_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetBankInfoList(Cls) || GetBankInfoList(Controller)");
                loTempRtn = loCls.GetBankInfoList(loParam);

                _logger.LogInfo("Run GetBankInfoStream(Controller) || GetBankInfoList(Controller)");
                loRtn = GetBankInfoStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBankInfoList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<LMM03505DTO> GetBankInfoStream(List<LMM03505DTO> poParameter)
        {
            foreach (LMM03505DTO item in poParameter)
            {
                yield return item;
            }
        }


        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03505ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            LMM03505Cls loCls = new LMM03505Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_TENANT_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
                //loParam.CSELECTED_TENANT_ID = poParameter.Entity.CSELECTED_TENANT_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                poParameter.Entity.CACTION = "DELETE";

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceDelete(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM03505ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03505ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM03505ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<LMM03505ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                LMM03505Cls loCls = new LMM03505Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_TENANT_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
                //loParam.CSELECTED_TENANT_ID = poParameter.Entity.CSELECTED_TENANT_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceGetRecord(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<LMM03505ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03505ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM03505ParameterDTO> loRtn = new R_ServiceSaveResultDTO<LMM03505ParameterDTO>();
            LMM03505Cls loCls = new LMM03505Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03505_TENANT_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
                //loParam.CSELECTED_TENANT_ID = poParameter.Entity.CSELECTED_TENANT_ID;
                poParameter.Entity.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Set Action Based On Mode || R_ServiceSave(Controller)");
                if (poParameter.CRUDMode == eCRUDMode.AddMode)
                {
                    poParameter.Entity.CACTION = "ADD";
                }
                else if (poParameter.CRUDMode == eCRUDMode.EditMode)
                {
                    poParameter.Entity.CACTION = "EDIT";
                }

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || R_ServiceSave(Controller)");
            return loRtn;
        }
    }
}
