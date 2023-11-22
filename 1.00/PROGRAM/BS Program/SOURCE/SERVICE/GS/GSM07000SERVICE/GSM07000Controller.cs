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
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GSM07000SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GSM07000Controller : ControllerBase, IGSM07000
    {
        private LoggerGSM07000 _logger;

        public GSM07000Controller(ILogger<GSM07000Controller> logger)
        {
            LoggerGSM07000.R_InitializeLogger(logger);
            _logger = LoggerGSM07000.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GSM07000DTO> GetActivityApprovalList()
        {
            _logger.LogInfo("Start || GetActivityApprovalList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<GSM07000DTO> loRtn = null;
            GetActivityApprovalListParameterDTO loParam = new GetActivityApprovalListParameterDTO();
            GSM07000Cls loCls = new GSM07000Cls();
            List<GSM07000DTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetActivityApprovalList(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CUSER_LOGIN_ID = R_BackGlobalVar.USER_ID;

                _logger.LogInfo("Run GetActivityApprovalList(Cls) || GetActivityApprovalList(Controller)");
                loTempRtn = loCls.GetActivityApprovalList(loParam);

                _logger.LogInfo("Run GetActivityApprovalListStream(Controller) || GetActivityApprovalList(Controller)");
                loRtn = GetActivityApprovalListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetActivityApprovalList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<GSM07000DTO> GetActivityApprovalListStream(List<GSM07000DTO> poParameter)
        {
            foreach (GSM07000DTO item in poParameter)
            {
                yield return item;
            }
        }
        
        [HttpPost]
        public R_ServiceGetRecordResultDTO<GSM07000DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<GSM07000DTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceGetRecord(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceGetRecordResultDTO<GSM07000DTO> loRtn = new R_ServiceGetRecordResultDTO<GSM07000DTO>();
            GSM07000ParameterDTO loParam = new GSM07000ParameterDTO();
            R_ServiceSaveResultDTO<GSM07000ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM07000ParameterDTO>();


            try
            {
                GSM07000Cls loCls = new GSM07000Cls();

                _logger.LogInfo("Set Parameter || R_ServiceGetRecord(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.Data.CUPDATE_BY = R_BackGlobalVar.USER_ID;
                loParam.Data.DUPDATE_DATE = DateTime.Now;

                _logger.LogInfo("Run R_GetRecord(Cls) || R_ServiceGetRecord(Controller)");
                loTempRtn.data = loCls.R_GetRecord(loParam);

                loRtn.data = loTempRtn.data.Data;
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
        public R_ServiceSaveResultDTO<GSM07000DTO> R_ServiceSave(R_ServiceSaveParameterDTO<GSM07000DTO> poParameter)
        {
            _logger.LogInfo("Start || R_ServiceSave(Controller)");
            R_Exception loException = new R_Exception();
            R_ServiceSaveResultDTO<GSM07000DTO> loRtn = new R_ServiceSaveResultDTO<GSM07000DTO>();
            R_ServiceSaveResultDTO<GSM07000ParameterDTO> loTempRtn = new R_ServiceSaveResultDTO<GSM07000ParameterDTO>();
            GSM07000Cls loCls = new GSM07000Cls();
            GSM07000ParameterDTO loParam = new GSM07000ParameterDTO();

            try
            {
                _logger.LogInfo("Set Parameter || R_ServiceSave(Controller)");
                loParam.Data = poParameter.Entity;
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.Data.CUPDATE_BY = R_BackGlobalVar.USER_ID;
                loParam.Data.DUPDATE_DATE = DateTime.Now;

                _logger.LogInfo("Run R_Save(Cls) || R_ServiceSave(Controller)");
                loTempRtn.data = loCls.R_Save(loParam, poParameter.CRUDMode);
                loRtn.data = loTempRtn.data.Data;
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
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<GSM07000DTO> poParameter)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public IAsyncEnumerable<ApprovalDTO> GetApprovalList()
        {
            _logger.LogInfo("Start || GetApprovalList(Controller)");
            R_Exception loException = new R_Exception();
            IAsyncEnumerable<ApprovalDTO> loRtn = null;
            GetApprovalListParameterDTO loParam = new GetApprovalListParameterDTO();
            GSM07000Cls loCls = new GSM07000Cls();
            List<ApprovalDTO> loTempRtn = null;

            try
            {
                _logger.LogInfo("Set Parameter || GetApprovalList(Controller)");
                loParam.CCOMPANY_ID = R_BackGlobalVar.COMPANY_ID;
                loParam.CLANGUAGE_ID = R_BackGlobalVar.CULTURE;

                _logger.LogInfo("Run GetApprovalList(Cls) || GetApprovalList(Controller)");
                loTempRtn = loCls.GetApprovalList(loParam);

                _logger.LogInfo("Run GetApprovalListStream(Controller) || GetApprovalList(Controller)");
                loRtn = GetApprovalListStream(loTempRtn);
            }
            catch (Exception ex)
            {
                loException.Add(ex);
                _logger.LogError(loException);
            }

            loException.ThrowExceptionIfErrors();
            _logger.LogInfo("End || GetApprovalList(Controller)");
            return loRtn;
        }
        private async IAsyncEnumerable<ApprovalDTO> GetApprovalListStream(List<ApprovalDTO> poParameter)
        {
            foreach (ApprovalDTO item in poParameter)
            {
                yield return item;
            }
        }
    }
}
