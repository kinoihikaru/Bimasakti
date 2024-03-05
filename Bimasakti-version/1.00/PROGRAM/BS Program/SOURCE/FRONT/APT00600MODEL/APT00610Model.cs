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
    public class APT00610Model : R_BusinessObjectServiceClientBase<APT00610DTO>, IAPT00610
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00610";
        private const string DEFAULT_MODULE = "AP";

        public APT00610Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

       
        public async Task<List<APT00610DTO>> GetPurhcaseAdjustmentAllocStreamAsync(APT00610DTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00610DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID, poParam.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00610DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00610.GetPurhcaseAdjustmentAllocStream),
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
        public IAsyncEnumerable<APT00610DTO> GetPurhcaseAdjustmentAllocStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
