using GSM07000BACK;
using GSM07000COMMON;
using GSM07000COMMON.DTOs;
using GSM07000COMMON.Loggers;
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

namespace GSM07000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM07010Controller : ControllerBase, IGSM07010
    {
        private LoggerGSM07010 _logger;

        public GSM07010Controller(ILogger<GSM07010Controller> logger)
        {
            LoggerGSM07010.R_InitializeLogger(logger);
            _logger = LoggerGSM07010.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM07010DTO> GetActivityApprovalUserList()
        {
            _logger.LogInfo("Start || GetActivityApprovalUserList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM07010DTO> loRtn = null;
            GetActivityApprovalUserListParameterDTO loParam = new GetActivityApprovalUserListParameterDTO();
            GSM07010Cls loCls = new GSM07010Cls();
            List<GSM07010DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetActivityApprovalUserList(Controller)");
                loParam.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.LOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.SELECTED_APPROVAL_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.SELECTED_APPROVAL_CODE_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetActivityApprovalUserList(Cls) || GetActivityApprovalUserList(Controller)");
                loTempRtn = loCls.GetActivityApprovalUserList(loParam);

                _logger.LogInfo("Run GetActivityApprovalListStream(Controller) || GetActivityApprovalUserList(Controller)");
                loRtn = GetActivityApprovalListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetActivityApprovalUserList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM07010DTO> GetActivityApprovalListStream(List<GSM07010DTO> poParameter)
        {
            foreach (GSM07010DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public IAsyncEnumerable<GSM07010UserDTO> GetLookUpUserList()
        {
            _logger.LogInfo("Start || GetLookUpUserList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM07010UserDTO> loRtn = null;
            GSM07010UserParameterDTO loParam = new GSM07010UserParameterDTO();
            GSM07010Cls loCls = new GSM07010Cls();
            List<GSM07010UserDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetLookUpUserList(Controller)");
                loParam.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CSELECTED_APPROVAL_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.SELECTED_APPROVAL_CODE_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetLookUpUserList(Cls) || GetLookUpUserList(Controller)");
                loTempRtn = loCls.GetLookUpUserList(loParam);

                _logger.LogInfo("Run GetLookUpUserListStream(Controller) || GetLookUpUserList(Controller)");
                loRtn = GetLookUpUserListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetLookUpUserList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM07010UserDTO> GetLookUpUserListStream(List<GSM07010UserDTO> poParameter)
        {
            foreach (GSM07010UserDTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM07010ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceDelete(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            GSM07010ParameterDTO loParam = new GSM07010ParameterDTO();
            GSM07010Cls loCls = new GSM07010Cls();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceDelete(Controller)");
                loParam.Data = poParameter.Entity.Data;
                loParam.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.SELECTED_APPROVAL_CODE = poParameter.Entity.SELECTED_APPROVAL_CODE;
                loParam.LOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                //loParam.SELECTED_APPROVAL_CODE = R_Utility.R_GetContext<string>(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT);

                _logger.LogInfo("Run R_Delete(Cls) || R_ServiceDelete(Controller)");
                loCls.R_Delete(loParam);
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
        public R_ServiceGetRecordResultDTO<GSM07010ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM07010ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM07010ParameterDTO> loRtn = new R_ServiceGetRecordResultDTO<GSM07010ParameterDTO>();
            GSM07010ParameterDTO loParam = null;


            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM07010Cls loCls = new GSM07010Cls();
                loParam = new GSM07010ParameterDTO();

                loParam.Data = poParameter.Entity.Data;
                loParam.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                //loParam.SELECTED_APPROVAL_CODE = R_Utility.R_GetContext<string>(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT);
                loParam.LOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                loParam.SELECTED_APPROVAL_CODE = poParameter.Entity.SELECTED_APPROVAL_CODE;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loRtn.data = loCls.R_GetRecord(loParam);
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
        public R_ServiceSaveResultDTO<GSM07010ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM07010ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM07010ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM07010ParameterDTO>();
            GSM07010ParameterDTO loParam = null;
            GSM07010Cls loCls = new GSM07010Cls();

            try
            {
                _logger.LogInfo("Start || Set Parameter(Controller)");
                loParam = new GSM07010ParameterDTO();
                loParam.Data = poParameter.Entity.Data;
                loParam.COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.LOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                //loParam.SELECTED_APPROVAL_CODE = R_Utility.R_GetContext<string>(ContextConstant.SELECTED_APPROVAL_CODE_CONTEXT);
                loParam.SELECTED_APPROVAL_CODE = poParameter.Entity.SELECTED_APPROVAL_CODE;

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
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
