using LMM03500BACK;
using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03505;
using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
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
    public class LMM03507Controller : ControllerBase, ILMM03507
    {
        private LoggerLMM03507 _logger;
        public LMM03507Controller(ILogger<LMM03507Controller> logger)
        {
            LoggerLMM03507.R_InitializeLogger(logger);
            _logger = LoggerLMM03507.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<LMM03507DTO> GetMembersList()
        {
            _logger.LogInfo("Start || GetMembersList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<LMM03507DTO> loRtn = null;
            GetMembersListParameterDTO loParam = new GetMembersListParameterDTO();
            LMM03507Cls loCls = new LMM03507Cls();
            List<LMM03507DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetMembersList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03507_PROPERTY_ID_STREAMING_CONTEXT);
                loParam.CSELECTED_TENANT_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.LMM03507_TENANT_ID_STREAMING_CONTEXT);
                loParam.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetMembersList(Cls) || GetMembersList(Controller)");
                loTempRtn = loCls.GetMembersList(loParam);

                _logger.LogInfo("Run GetMembersStream(Controller) || GetMembersList(Controller)");
                loRtn = GetMembersStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetMembersList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<LMM03507DTO> GetMembersStream(List<LMM03507DTO> poParameter)
        {
            foreach (LMM03507DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<LMM03507ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            LMM03507Cls loCls = new LMM03507Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
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
        public R_ServiceGetRecordResultDTO<LMM03507ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<LMM03507ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<LMM03507ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<LMM03507ParameterDTO>();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                var loCls = new LMM03507Cls();

                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
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
        public R_ServiceSaveResultDTO<LMM03507ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<LMM03507ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<LMM03507ParameterDTO> loRtn = new R_ServiceSaveResultDTO<LMM03507ParameterDTO>();
            LMM03507Cls loCls = new LMM03507Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                poParameter.Entity.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.CSELECTED_PROPERTY_ID = R_Utility.R_GetContext<string>(ContextConstant.LMM03507_PROPERTY_ID_CONTEXT);
                //loParam.CSELECTED_PROPERTY_ID = poParameter.Entity.CSELECTED_PROPERTY_ID;
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

        [HttpPost]
        public IAsyncEnumerable<GetIdTypeDTO> GetIdTypeList()
        {
            _logger.LogInfo("Start || GetIdTypeList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GetIdTypeDTO> loRtn = null;
            GetIdTypeParameterDTO loParam = new GetIdTypeParameterDTO();
            LMM03507Cls loCls = new LMM03507Cls();
            List<GetIdTypeDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetIdTypeList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLOGIN_LANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetIdTypeList(Cls) || GetIdTypeList(Controller)");
                loTempRtn = loCls.GetIdTypeList(loParam);

                _logger.LogInfo("Run GetIdTypeStream(Controller) || GetIdTypeList(Controller)");
                loRtn = GetIdTypeStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetIdTypeList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GetIdTypeDTO> GetIdTypeStream(List<GetIdTypeDTO> poParameter)
        {
            foreach (GetIdTypeDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
