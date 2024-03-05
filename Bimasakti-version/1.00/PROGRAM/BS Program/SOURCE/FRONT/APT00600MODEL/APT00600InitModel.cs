using APT00600COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APT00600MODEL
{
    public class APT00600InitModel : R_BusinessObjectServiceClientBase<APT00600InitTabListDTO>, IAPT00600Init
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00600Init";
        private const string DEFAULT_MODULE = "AP";

        public APT00600InitModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APT00600InitTabListDTO> GetAllInitialProcessTabListAsync()
        {
            var loEx = new R_Exception();
            APT00600InitTabListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00600SingleResult<APT00600InitTabListDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600Init.GetAllInitialProcessTabList),
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
        public async Task<APT00600InitTabEntryDTO> GetAllInitialProcessTabEntryAsync()
        {
            var loEx = new R_Exception();
            APT00600InitTabEntryDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00600SingleResult<APT00600InitTabEntryDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600Init.GetAllInitialProcessTabEntry),
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
        public async Task<APT00600InitPopupAllocDTO> GetAllInitialProcessPopupAllocAsync()
        {
            var loEx = new R_Exception();
            APT00600InitPopupAllocDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00600SingleResult<APT00600InitPopupAllocDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600Init.GetAllInitialProcessPopupAlloc),
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
        public APT00600SingleResult<APT00600InitPopupAllocDTO> GetAllInitialProcessPopupAlloc()
        {
            throw new NotImplementedException();
        }
        public APT00600SingleResult<APT00600InitTabEntryDTO> GetAllInitialProcessTabEntry()
        {
            throw new NotImplementedException();
        }
        public APT00600SingleResult<APT00600InitTabListDTO> GetAllInitialProcessTabList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
