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
    public class GSM07011Controller : ControllerBase, IGSM07011
    {
        private LoggerGSM07011 _logger;

        public GSM07011Controller(ILogger<GSM07011Controller> logger)
        {
            LoggerGSM07011.R_InitializeLogger(logger);
            _logger = LoggerGSM07011.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM07011DTO> GetMultipleUserAssignmentList()
        {
            _logger.LogInfo("Start || GetMultipleUserAssignmentList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM07011DTO> loRtn = null;
            GetMultipleUserAssignmentListParameterDTO loParam = new GetMultipleUserAssignmentListParameterDTO();
            GSM07011Cls loCls = new GSM07011Cls();
            List<GSM07011DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetMultipleUserAssignmentList(Controller)");
                loParam.LOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CAPPROVAL_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.GSM07011_CAPPROVAL_CODE_STREAMING_CONTEXT);

                _logger.LogInfo("Run GetMultipleUserAssignmentList(Cls) || GetMultipleUserAssignmentList(Controller)");
                loTempRtn = loCls.GetMultipleUserAssignmentList(loParam);

                _logger.LogInfo("Run GetMultipleUserAssignmentListStream(Controller) || GetMultipleUserAssignmentList(Controller)");
                loRtn = GetMultipleUserAssignmentListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetMultipleUserAssignmentList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM07011DTO> GetMultipleUserAssignmentListStream(List<GSM07011DTO> poParameter)
        {
            foreach (GSM07011DTO item in poParameter)
            {
                yield return item;
            }
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM07011ParameterDTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM07011ParameterDTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM07011ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM07011ParameterDTO> loRtn = null;
            GSM07011ParameterDTO loParam = new GSM07011ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                GSM07011Cls loCls = new GSM07011Cls();

                loParam.Data = poParameter.Entity.Data;
                loParam.LOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;

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
        public R_ServiceSaveResultDTO<GSM07011ParameterDTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM07011ParameterDTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM07011ParameterDTO> loRtn = new R_ServiceSaveResultDTO<GSM07011ParameterDTO>();
            GSM07011ParameterDTO loParam = new GSM07011ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                GSM07011Cls loCls = new GSM07011Cls();

                loParam.Data = poParameter.Entity.Data;
                loParam.LOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.LOGIN_USER_ID = R_BackGlobalVar.USER_ID;
                //loParam.SELECTED_APPROVAL_CODE = R_Utility.R_GetContext<string>(ContextConstant.GSM07011_SELECTED_APPROVAL_CODE_CONTEXT);
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

        [HttpPost]
        public SaveMultipleUserAssignmentDTO SaveMultipleUserAssignment(SaveMultipleUserAssignmentParameterDTO poParameter)
        {
            _logger.LogInfo("Start || SaveMultipleUserAssignment(Controller)");
            R_Exception loException = new R_Exception();
            SaveMultipleUserAssignmentDTO loRtn = new SaveMultipleUserAssignmentDTO();
            //SaveMultipleUserAssignmentParameterDTO loParam = new SaveMultipleUserAssignmentParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || SaveMultipleUserAssignment(Controller)");
                GSM07011Cls loCls = new GSM07011Cls();

                //loParam = poParameter;
                poParameter.CLOGIN_COMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                poParameter.CLOGIN_USER_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run SaveMultipleUserAssignment(Cls) || SaveMultipleUserAssignment(Controller)");
                loCls.SaveMultipleUserAssignment(poParameter);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || SaveMultipleUserAssignment(Controller)");
            return loRtn;
        }
    }
}
