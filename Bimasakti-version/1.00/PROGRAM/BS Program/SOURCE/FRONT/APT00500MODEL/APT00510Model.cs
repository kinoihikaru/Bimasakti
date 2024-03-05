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
    public class APT00510Model : R_BusinessObjectServiceClientBase<APT00510DTO>, IAPT00510
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00510";
        private const string DEFAULT_MODULE = "AP";

        public APT00510Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

       
        public async Task<List<APT00510DTO>> GetPurhcaseAdjustmentAllocStreamAsync(APT00510DTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00510DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID, poParam.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00510.GetPurhcaseAdjustmentAllocStream),
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

        #region Not Implment
        public IAsyncEnumerable<APT00510DTO> GetPurhcaseAdjustmentAllocStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
