using GLT00100BACK;
using GLT00100COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_Common;

namespace GLT00100SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class GLT00100Controller : ControllerBase, IGLT00100
    {
        private LoggerGLT00100 _Logger;
        public GLT00100Controller(ILogger<LoggerGLT00100> logger)
        {
            //Initial and Get Logger
            LoggerGLT00100.R_InitializeLogger(logger);
            _Logger = LoggerGLT00100.R_GetInstanceLogger();
        }

        [HttpPost]
        public IAsyncEnumerable<GLT00101DTO> GetJournalDetailList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00101DTO> loRtn = null;
            _Logger.LogInfo("Start GetJournalDetailList");

            try
            {
                _Logger.LogInfo("Set Param GetJournalDetailList");
                var loRecId = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);

                var loCls = new GLT00100Cls();

                _Logger.LogInfo("Call Back Method GetJournalDetailList");
                var loTempRtn = loCls.GetJournalDetailList(loRecId);

                _Logger.LogInfo("Call Stream Method Data GetJournalDetailList");
                loRtn = StreamListData<GLT00101DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalDetailList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<GLT00100DTO> GetJournalList()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<GLT00100DTO> loRtn = null;
            _Logger.LogInfo("Start GetJournalDetailList");

            try
            {
                _Logger.LogInfo("Set Param GetJournalDetailList");
                var loParam = new GLT00100ParamDTO();
                loParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                loParam.CPERIOD = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPERIOD);
                loParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSTATUS);
                loParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(ContextConstant.CSEARCH_TEXT);

                var loCls = new GLT00100Cls();

                _Logger.LogInfo("Call Back Method GetJournalDetailList");
                var loTempRtn = loCls.GetJournalList(loParam);

                _Logger.LogInfo("Call Stream Method Data GetJournalDetailList");
                loRtn = StreamListData<GLT00100DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetJournalDetailList");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100UpdateStatusDTO> UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100UpdateStatusDTO> loRtn = new GLT00100RecordResult<GLT00100UpdateStatusDTO>();
            _Logger.LogInfo("Start UpdateJournalStatus");

            try
            {
                var loCls = new GLT00100Cls();

                _Logger.LogInfo("Call Back Method UpdateJournalStatus");
                loCls.UpdateJournalStatus(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End UpdateJournalStatus");

            return loRtn;
        }

        [HttpPost]
        public GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> ValidationRapidApproval(GLT00100RapidApprovalValidationDTO poEntity)
        {
            var loEx = new R_Exception();
            GLT00100RecordResult<GLT00100RapidApprovalValidationDTO> loRtn = new GLT00100RecordResult<GLT00100RapidApprovalValidationDTO>();
            _Logger.LogInfo("Start ValidationRapidApproval");

            try
            {
                var loCls = new GLT00100Cls();

                _Logger.LogInfo("Call Back Method ValidationRapidApproval");
                loRtn.Data = loCls.ValidationRapidAppro(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ValidationRapidApproval");

            return loRtn;
        }

        #region Stream List Data
        private async IAsyncEnumerable<T> StreamListData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}