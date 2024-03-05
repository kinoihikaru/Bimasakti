using APT00500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APT00500MODEL
{
    public class APT00500InitModel : R_BusinessObjectServiceClientBase<APT00500InitTabListDTO>, IAPT00500Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00500Init";
        private const string DEFAULT_MODULE = "AP";

        public APT00500InitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APT00500InitTabListDTO> GetAllInitialProcessTabListAsync()
        {
            var loEx = new R_Exception();
            APT00500InitTabListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00500SingleResult<APT00500InitTabListDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500Init.GetAllInitialProcessTabList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<APT00500InitTabEntryDTO> GetAllInitialProcessTabEntryAsync()
        {
            var loEx = new R_Exception();
            APT00500InitTabEntryDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00500SingleResult<APT00500InitTabEntryDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500Init.GetAllInitialProcessTabEntry),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<APT00500InitPopupAllocDTO> GetAllInitialProcessPopupAllocAsync()
        {
            var loEx = new R_Exception();
            APT00500InitPopupAllocDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00500SingleResult<APT00500InitPopupAllocDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500Init.GetAllInitialProcessPopupAlloc),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Not Implment
        public APT00500SingleResult<APT00500InitPopupAllocDTO> GetAllInitialProcessPopupAlloc()
        {
            throw new NotImplementedException();
        }
        public APT00500SingleResult<APT00500InitTabEntryDTO> GetAllInitialProcessTabEntry()
        {
            throw new NotImplementedException();
        }
        public APT00500SingleResult<APT00500InitTabListDTO> GetAllInitialProcessTabList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
