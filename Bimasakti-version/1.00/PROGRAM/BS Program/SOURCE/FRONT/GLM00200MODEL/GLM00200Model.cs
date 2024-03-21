﻿using GLM00200Common;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLM00200Model
{
    public class GLM00200Model : R_BusinessObjectServiceClientBase<JournalParamDTO>, IGLM00200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlGL";
        private const string DEFAULT_CHECKPOINT_NAME = "api/GLM00200";
        private const string DEFAULT_MODULE = "GL";
        public GLM00200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_CHECKPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true
            ) : base(
                pcHttpClientName,
                pcRequestServiceEndPoint,
                DEFAULT_MODULE,
                plSendWithContext,
                plSendWithToken)
        {
        }

        //FUNCTION
        #region real function
        public async Task<InitALLDTO> GetInitDataAsync()
        {
            var loEx = new R_Exception();
            InitALLDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<InitALLDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetInitData),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<JournalDTO> GetJournalDataAsync(JournalDTO poEntity)
        {
            var loEx = new R_Exception();
            JournalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<JournalDTO>, JournalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetJournalData),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<JournalDTO> SaveJournalDataAsync(ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO> poEntity)
        {
            var loEx = new R_Exception();
            JournalDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<JournalDTO>, ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.SaveJournalData),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<CurrencyRateResult> GetLastCurrencyAsync(CurrencyRateResult poEntity)
        {
            var loEx = new R_Exception();
            CurrencyRateResult loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<CurrencyRateResult>, CurrencyRateResult>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetLastCurrency),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task UpdateStatusJournalDataAsync(GLM00200UpdateStatusDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<RecordResultDTO<GLM00200UpdateStatusDTO>, GLM00200UpdateStatusDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.UpdateStatusJournalData),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task<List<JournalDTO>> GetAllRecurringListAsync(RecurringJournalListParamDTO poParam)
        {
            var loEx = new R_Exception();
            List<JournalDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CPERIOD_YYYYMM, poParam.CPERIOD_YYYYMM);
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CSTATUS, string.IsNullOrWhiteSpace(poParam.CSTATUS) ? "" : poParam.CSTATUS);
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CSEARCH_TEXT, string.IsNullOrWhiteSpace(poParam.CSEARCH_TEXT) ? "" : poParam.CSEARCH_TEXT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<JournalDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetAllRecurringList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<JournalDetailActualGridDTO>> GetAllActualJournalDetailListAsync(RecurringJournalListParamDTO poParam)
        {
            var loEx = new R_Exception();
            List<JournalDetailActualGridDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CDEPT_CODE, string.IsNullOrWhiteSpace(poParam.CDEPT_CODE) ? "" : poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CREF_NO, string.IsNullOrWhiteSpace(poParam.CREF_NO) ? "" : poParam.CREF_NO);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<JournalDetailActualGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetAllActualJournalDetailList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<JournalDetailGridDTO>> GetAllJournalDetailListAsync(JournalDTO poParam)
        {
            var loEx = new R_Exception();
            List<JournalDetailGridDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(RecurringJournalContext.CREC_ID, poParam.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<JournalDetailGridDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.GetAllJournalDetailList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<UploadByte> DownloadTemplateAsync()
        {
            var loEx = new R_Exception();
            UploadByte loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<UploadByte>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00200.DownloadTemplate),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion real function

        #region for implement only
        public RecordResultDTO<InitALLDTO> GetInitData()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<JournalDTO> GetAllRecurringList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<JournalDetailGridDTO> GetAllJournalDetailList()
        {
            throw new NotImplementedException();
        }
        public RecordResultDTO<JournalDTO> GetJournalData(JournalDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public RecordResultDTO<JournalDTO> SaveJournalData(ParemeterRecordWithCRUDModeResultDTO<JournalParamDTO> poEntity)
        {
            throw new NotImplementedException();
        }
        public RecordResultDTO<GLM00200UpdateStatusDTO> UpdateStatusJournalData(GLM00200UpdateStatusDTO poEntity)
        {
            throw new NotImplementedException();
        }
        public RecordResultDTO<CurrencyRateResult> GetLastCurrency(CurrencyRateResult poEntity)
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<JournalDetailActualGridDTO> GetAllActualJournalDetailList()
        {
            throw new NotImplementedException();
        }
        public UploadByte DownloadTemplate()
        {
            throw new NotImplementedException();
        }
        #endregion for implement only

    }
}
