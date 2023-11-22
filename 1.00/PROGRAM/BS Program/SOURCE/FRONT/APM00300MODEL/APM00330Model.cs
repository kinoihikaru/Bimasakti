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
    public class APM00330Model : R_BusinessObjectServiceClientBase<APM00330DTO>, IAPM00330
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APM00330";
        private const string DEFAULT_MODULE = "AP";

        public APM00330Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }



        #region Supplier Bank List
        public IAsyncEnumerable<APM00330DTO> GetSupplierBankList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<APM00330DTO>> GetSupplierBankListAsync(string poRecSupplierID)
        {
            var loEx = new R_Exception();
            List<APM00330DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSUPPLIER_REC_ID, poRecSupplierID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00330DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00330.GetSupplierBankList),
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
