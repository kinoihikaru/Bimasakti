using GSM04500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04500MODEL
{
    public class GSM04500Model : R_BusinessObjectServiceClientBase<GSM04500DTO>, IGSM04500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04500";
        private const string DEFAULT_MODULE = "GS";

        public GSM04500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM04500DTO>> GetJournalGroupListAsync(string pcPropertyID, string pcJournalGroupType)
        {
            var loEx = new R_Exception();
            List<GSM04500DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, string.IsNullOrWhiteSpace(pcPropertyID) ? "" : pcPropertyID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, string.IsNullOrWhiteSpace(pcJournalGroupType) ? "" : pcJournalGroupType);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetJournalGroupList),
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

        public async Task<GSM04500InitDTO> GetInitDataAsync()
        {
            var loEx = new R_Exception();
            GSM04500InitDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500Record<GSM04500InitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetInitData),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
            return loRtn;
        }

        public async Task<GSM04500FileByteDTO> GetTemplateJournalGroupExcelAsync()
        {
            var loEx = new R_Exception();
            GSM04500FileByteDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM04500FileByteDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04500.GetTemplateJournalGroupExcel),
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

        #region Not Implement
        public GSM04500Record<GSM04500InitDTO> GetInitData()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GSM04500DTO> GetJournalGroupList()
        {
            throw new NotImplementedException();
        }

        public GSM04500FileByteDTO GetTemplateJournalGroupExcel()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
