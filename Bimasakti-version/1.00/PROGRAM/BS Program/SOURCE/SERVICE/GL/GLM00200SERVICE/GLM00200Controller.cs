﻿using GLM00200Back;
using GLM00200Common;
using Microsoft.AspNetCore.Mvc;
using R_BackEnd;
using R_Common;
using R_CommonFrontBackAPI;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace GLM00200Service
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GLM00200Controller : ControllerBase, IGLM00200
    {
        //private LoggerGLM00200 _Logger;
        //private readonly ActivitySource _activitySource;
        //public GLM00200Controller(ILogger<LoggerGLM00200> logger)
        //{
        //    ////Initial and Get Logger
        //    //LoggerGLM00200.R_InitializeLogger(logger);
        //    //_Logger = LoggerGLM00200.R_GetInstanceLogger();
        //    //_activitySource = GLM00200ActivitySourceBase.R_InitializeAndGetActivitySource(nameof(GLM00200Controller));
        //}

        #region Stream Data
        private async IAsyncEnumerable<T> GetStreamData<T>(List<T> poParameter)
        {
            foreach (var item in poParameter)
            {
                yield return item;
            }
        }
        #endregion

        [HttpPost]
        public RecordResultDTO<InitALLDTO> GetInitData()
        {
            //using Activity activity = _activitySource.StartActivity("GetInitData");
            var loEx = new R_Exception();
            RecordResultDTO<InitALLDTO> loRtn = new();
            //_Logger.LogInfo("Start GetInitData");

            try
            {
                InitALLDTO loTempRtn = new InitALLDTO();
                var loCls = new GLM00200Cls();
                //_Logger.LogInfo("Call All Back Method For Init Data");
                //Company Info
                loTempRtn.COMPANY_INFO = loCls.GetVAR_GSM_COMPANY();

                //System Param
                var loSystemParam = loCls.GetVAR_GL_SYSTEM_PARAM();
                loTempRtn.GL_SYSTEM_PARAM = loSystemParam;

                //Current
                VAR_PERIOD_DT_INFO_DTO loParam = new VAR_PERIOD_DT_INFO_DTO { CCYEAR = loSystemParam.CCURRENT_PERIOD_YY, CPERIOD_NO = loSystemParam.CCURRENT_PERIOD_MM };
                loTempRtn.CURRENT_PERIOD_START_DATE = loCls.GetPERIOD_DT_INFO(loParam);

                //Soft
                loParam.CCYEAR = loSystemParam.CSOFT_PERIOD_YY;
                loParam.CPERIOD_NO = loSystemParam.CSOFT_PERIOD_MM;
                loTempRtn.SOFT_PERIOD_START_DATE = loCls.GetPERIOD_DT_INFO(loParam);

                //Option
                loTempRtn.IUNDO_COMMIT_JRN = loCls.GetIUNDO_COMMIT_JRN();

                //Transaction
                loTempRtn.GSM_TRANSACTION_CODE = loCls.GetGSM_TRANSACTION_CODE();

                //Period Range
                loTempRtn.PERIOD_YEAR = loCls.GetGSM_PERIOD();

                //List Currency
                loTempRtn.CURRENCY_LIST = loCls.GetCurrency();

                //List Status
                loTempRtn.STATUS_LIST = loCls.GetSTATUS_DTO();

                loRtn.data = loTempRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetPurhcaseAdjustmentStream");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<JournalDTO> GetAllRecurringList()
        {
            //using Activity activity = _activitySource.StartActivity("GetAllRecurringList");
            var loEx = new R_Exception();
            IAsyncEnumerable<JournalDTO> loRtn = null;
            //_Logger.LogInfo("Start GetAllRecurringList");

            try
            {
                //_Logger.LogInfo("Set Param GetAllRecurringList");
                var poParam = new RecurringJournalListParamDTO();
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CDEPT_CODE);
                poParam.CPERIOD_YYYYMM = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CPERIOD_YYYYMM);
                poParam.CSTATUS = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CSTATUS);
                poParam.CSEARCH_TEXT = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CSEARCH_TEXT);

                //_Logger.LogInfo("Call Back Method GetJournalList");
                var loCls = new GLM00200Cls();
                var loTempRtn = loCls.GetJournalList(poParam);

                //_Logger.LogInfo("Call Stream Method Data GetAllRecurringList");
                loRtn = GetStreamData<JournalDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetAllRecurringList");

            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<JournalDetailGridDTO> GetAllJournalDetailList()
        {
            //using Activity activity = _activitySource.StartActivity("GetAllJournalDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<JournalDetailGridDTO> loRtn = null;
            //_Logger.LogInfo("Start GetAllJournalDetailList");

            try
            {
                //_Logger.LogInfo("Set Param GetAllJournalDetailList");
                var poParam = new RecurringJournalListParamDTO();
                poParam.CREC_ID = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CREC_ID);

                //_Logger.LogInfo("Call Back Method GetJournalDetailList");
                var loCls = new GLM00200Cls();
                var loTempRtn = loCls.GetJournalDetailList(poParam);

                //_Logger.LogInfo("Call Stream Method Data GetAllJournalDetailList");
                loRtn = GetStreamData<JournalDetailGridDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetAllJournalDetailList");

            return loRtn;
        }

        [HttpPost]
        public RecordResultDTO<JournalDTO> GetJournalData(JournalDTO poEntity)
        {
            //using Activity activity = _activitySource.StartActivity("GetInitData");
            var loEx = new R_Exception();
            RecordResultDTO<JournalDTO> loRtn = new();
            //_Logger.LogInfo("Start GetInitData");

            try
            {
                var loCls = new GLM00200Cls();
                var loTempRtn = loCls.GLM00200Display(poEntity);

                loRtn.data = loTempRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetPurhcaseAdjustmentStream");

            return loRtn;
        }

        [HttpPost]
        public RecordResultDTO<JournalDTO> SaveJournalData(ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO> poEntity)
        {
            //using Activity activity = _activitySource.StartActivity("GetInitData");
            var loEx = new R_Exception();
            RecordResultDTO<JournalDTO> loRtn = new();
            //_Logger.LogInfo("Start GetInitData");

            try
            {
                var loCls = new GLM00200Cls();

                var loTempRtn = loCls.GLM00200Saving(poEntity.data, poEntity.eCRUDMode);

                loRtn.data = loTempRtn;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetPurhcaseAdjustmentStream");

            return loRtn;
        }

        [HttpPost]
        public RecordResultDTO<GLM00200UpdateStatusDTO> UpdateStatusJournalData(GLM00200UpdateStatusDTO poEntity)
        {
            //using Activity activity = _activitySource.StartActivity("GetInitData");
            var loEx = new R_Exception();
            RecordResultDTO<GLM00200UpdateStatusDTO> loRtn = new();
            //_Logger.LogInfo("Start GetInitData");

            try
            {
                var loCls = new GLM00200Cls();

                loCls.UpdateJournalStatus(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetPurhcaseAdjustmentStream");

            return loRtn;
        }

        [HttpPost]
        public RecordResultDTO<CurrencyRateResult> GetLastCurrency(CurrencyRateResult poEntity)
        {
            //using Activity activity = _activitySource.StartActivity("GetLastCurrency");
            var loEx = new R_Exception();
            RecordResultDTO<CurrencyRateResult> loRtn = new RecordResultDTO<CurrencyRateResult>();
            //_Logger.LogInfo("Start GetLastCurrency");

            try
            {
                var loCls = new GLM00200Cls();

                loRtn.data = loCls.GetLastCurrency(poEntity);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();

            //_Logger.LogInfo("End GetLastCurrency");
            return loRtn;
        }

        [HttpPost]
        public IAsyncEnumerable<JournalDetailActualGridDTO> GetAllActualJournalDetailList()
        {
            //using Activity activity = _activitySource.StartActivity("GetAllActualJournalDetailList");
            var loEx = new R_Exception();
            IAsyncEnumerable<JournalDetailActualGridDTO> loRtn = null;
            //_Logger.LogInfo("Start GetAllActualJournalDetailList");

            try
            {
                //_Logger.LogInfo("Set Param GetAllActualJournalDetailList");
                var poParam = new RecurringJournalListParamDTO();
                poParam.CDEPT_CODE = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CDEPT_CODE);
                poParam.CREF_NO = R_Utility.R_GetStreamingContext<string>(RecurringJournalContext.CREF_NO);

                //_Logger.LogInfo("Call Back Method GetActualJournalList");
                var loCls = new GLM00200Cls();
                var loTempRtn = loCls.GetActualJournalList(poParam);

                //_Logger.LogInfo("Call Stream Method Data GetAllActualJournalDetailList");
                loRtn = GetStreamData<JournalDetailActualGridDTO>(loTempRtn);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End GetAllActualJournalDetailList");

            return loRtn;
        }

        [HttpPost]
        public UploadByte DownloadTemplate()
        {
            //using Activity activity = _activitySource.StartActivity("DownloadTemplate");
            var loEx = new R_Exception();
            var loRtn = new UploadByte();
            //_Logger.LogInfo("Start DownloadTemplate");

            try
            {
                Assembly loAsm = Assembly.Load("BIMASAKTI_GL_API");

                //_Logger.LogInfo("Load File Template From DownloadTemplateFile");
                var lcResourceFile = "BIMASAKTI_GL_API.Template.GL_RECURRING_JOURNAL_UPLOAD.xlsx";
                using (Stream resFilestream = loAsm.GetManifestResourceStream(lcResourceFile))
                {
                    var ms = new MemoryStream();
                    resFilestream.CopyTo(ms);
                    var bytes = ms.ToArray();

                    loRtn.data = bytes;
                }
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
                //_Logger.LogError(loEx);
            }

            loEx.ThrowExceptionIfErrors();
            //_Logger.LogInfo("End DownloadTemplateFile");

            return loRtn;
        }
    }
}