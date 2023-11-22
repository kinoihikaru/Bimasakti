using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
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
    public class LMM03504Controller : ControllerBase, ILMM03504
    {
        private LoggerLMM03504 _logger;
        public LMM03504Controller(ILogger<LMM03504Controller> logger)
        {
            LoggerLMM03504.R_InitializeLogger(logger);
            _logger = LoggerLMM03504.R_GetInstanceLogger();
        }

        [HttpPost]
        public LMM03504ResultDTO GetBilling(GetBillingParameterDTO poParam)
        {
            _logger.LogInfo("Start || GetBilling(Controller)");
            R_Exception loException = new R_Exception();
            LMM03504ResultDTO loRtn = new LMM03504ResultDTO();

            try
            {
                _logger.LogInfo("Set Parameter || GetBilling(Controller)");
                LMM03504Cls loCls = new LMM03504Cls();

                poParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03504_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_TENANT_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03504_TENANT_ID_CONTEXT);
                poParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetBilling(Cls) || GetBilling(Controller)");
                loRtn.Data = loCls.GetBilling(poParam);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetBilling(Controller)");
            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03504ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<LMM03504ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03504ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM03504ParameterDTO>loRtn = new R_ServiceGetRecordResultDTO<LMM03504ParameterDTO>();


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                LMM03504Cls loCls = new LMM03504Cls();
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03504_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
                poParameter.Entity.CCUSTOMER_TYPE = "01";
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
        public R_ServiceSaveResultDTO<LMM03504ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03504ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM03504ParameterDTO> loRtn = new R_ServiceSaveResultDTO<LMM03504ParameterDTO>();
            LMM03504Cls loCls = new LMM03504Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03504_PROPERTY_ID_CONTEXT);
                //poParameter.Entity.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
                poParameter.Entity.CCUSTOMER_TYPE = "01";
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
