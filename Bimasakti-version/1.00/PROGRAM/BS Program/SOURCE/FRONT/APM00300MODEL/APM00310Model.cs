using APM00300COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APM00300MODEL
{
    public class APM00310Model : R_BusinessObjectServiceClientBase<APM00310DTO>, IAPM00310
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APM00310";
        private const string DEFAULT_MODULE = "AP";

        public APM00310Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region Pay Term
        public IAsyncEnumerable<APM00300PayTermDTO> GetPayTermList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APM00300PayTermDTO>> GetPayTermListAsync(string poPropertyIdParam)
        {
            var loEx = new R_Exception();
            List<APM00300PayTermDTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyIdParam);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300PayTermDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00310.GetPayTermList),
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
        #endregion

        #region Currency
        public IAsyncEnumerable<APM00300CurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APM00300CurrencyDTO>> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<APM00300CurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00310.GetCurrencyList),
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

        #endregion

        #region Tax Type
        public IAsyncEnumerable<APM00300TaxTypeDTO> GetTaxTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<APM00300TaxTypeDTO>> GetTaxTypeListAsync()
        {
            var loEx = new R_Exception();
            List<APM00300TaxTypeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300TaxTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00310.GetTaxTypeList),
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
        #endregion
    }
}
