using APT00300BACK;
using APT00300COMMON;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;

namespace APT00300SERVICE
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class APT00310Controller : ControllerBase, IAPT00310
    {
        private LoggerAPT00310 _Logger;
        public APT00310Controller(ILogger<LoggerAPT00310> logger)
        {
            //Initial and Get Logger
            LoggerAPT00310.R_InitializeLogger(logger);
            _Logger = LoggerAPT00310.R_GetInstanceLogger();
        }

        [HttpPost]
        public APT00300SingleResult<APT00311DTO> GenerateWHTaxDeducationDT(APT00311DTO poParam)
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00311DTO> loRtn = new APT00300SingleResult<APT00311DTO>();
            _Logger.LogInfo("Start GenerateWHTaxDeducationDT");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method GenerateWHTaxDeducationDT");
                loCls.GenerateWHTaxDeducation(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GenerateWHTaxDeducationDT");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00311DTO> GetDebitNoteDT(APT00311DTO poParam)
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00311DTO> loRtn = new APT00300SingleResult<APT00311DTO>();
            _Logger.LogInfo("Start GetDebitNoteDT");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method GetDebitNoteDT");
                loRtn.Data = loCls.GetDebitNoteDT(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetDebitNoteDT");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<APT00311DTO> GetDebitNoteDTListStream()
        {
            var loEx = new R_Exception();
            IAsyncEnumerable<APT00311DTO> loRtn = null;
            _Logger.LogInfo("Start GetDebitNoteDTListStream");

            try
            {
                _Logger.LogInfo("Set Param GetDebitNoteListStream");
                var poParam = new APT00311DTO();
                poParam.CPROPERTY_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CPROPERTY_ID);
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CDEPT_CODE);
                poParam.CTRANS_CODE = R_Utility.R_GetStreamingContext<string>(ContextConstant.CTRANS_CODE);
                poParam.CREF_NO = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREF_NO);
                poParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(ContextConstant.CREC_ID);

                _Logger.LogInfo("Call Back Method GetDebitNoteDTListStream");
                var loCls = new APT00310Cls();
                var loTempRtn = loCls.GetAllDebitNoteDT(poParam);

                _Logger.LogInfo("Call Stream Method Data GetDebitNoteDTListStream");
                loRtn = GetStreamData<APT00311DTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetDebitNoteDTListStream");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00310LastCurrencyDTO> GetLastCurrencyRate(APT00310LastCurrencyParameterDTO poParam)
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00310LastCurrencyDTO> loRtn = new APT00300SingleResult<APT00310LastCurrencyDTO>();
            _Logger.LogInfo("Start GetLastCurrencyRate");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method GetLastCurrencyRate");
                loRtn.Data = loCls.GetLastCurrencyRate(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End GetLastCurrencyRate");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceDeleteResultDTO R_ServiceDelete(R_ServiceDeleteParameterDTO<APT00310DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceDeleteResultDTO loRtn = new R_ServiceDeleteResultDTO();
            _Logger.LogInfo("Start R_ServiceDelete APT00310");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method R_Delete APT00310Cls");
                loCls.R_Delete(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_ServiceDelete APT00310");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceGetRecordResultDTO<APT00310DTO> R_ServiceGetRecord(R_ServiceGetRecordParameterDTO<APT00310DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceGetRecordResultDTO<APT00310DTO> loRtn = new R_ServiceGetRecordResultDTO<APT00310DTO>();
            _Logger.LogInfo("Start ServiceGetRecord APT00310");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method R_GetRecord APT00310Cls");
                loRtn.data = loCls.R_GetRecord(poParameter.Entity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End R_GetRecord APT00310");

            return loRtn;
        }

        [HttpPost]
        public R_ServiceSaveResultDTO<APT00310DTO> R_ServiceSave(R_ServiceSaveParameterDTO<APT00310DTO> poParameter)
        {
            var loEx = new R_Exception();
            R_ServiceSaveResultDTO<APT00310DTO> loRtn = new R_ServiceSaveResultDTO<APT00310DTO>();
            _Logger.LogInfo("Start ServiceSave APT00310");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method R_Save APT00310Cls");
                loRtn.data = loCls.R_Save(poParameter.Entity, poParameter.CRUDMode);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End ServiceSave APT00310");

            return loRtn;
        }

        [HttpPost]
        public APT00300SingleResult<APT00310DTO> SaveSummaryDebitNote(APT00310DTO poParam)
        {
            var loEx = new R_Exception();
            APT00300SingleResult<APT00310DTO> loRtn = new APT00300SingleResult<APT00310DTO>();
            _Logger.LogInfo("Start SaveSummaryDebitNote");

            try
            {
                var loCls = new APT00310Cls();

                _Logger.LogInfo("Call Back Method SaveSummaryDebitNote");
                loRtn.Data = loCls.SaveSummaryDebit(poParam);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                _Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            _Logger.LogInfo("End SaveSummaryDebitNote");

            return loRtn;
        }

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion
    }
}