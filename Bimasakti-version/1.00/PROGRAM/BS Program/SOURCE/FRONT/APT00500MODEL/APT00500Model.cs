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
    public class APT00500Model : R_BusinessObjectServiceClientBase<APT00500DTO>, IAPT00500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00500";
        private const string DEFAULT_MODULE = "AP";

        public APT00500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<APT00500DTO>> GetPurhcaseAdjustmentStreamAsync(APT00500ParamDTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00500DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, string.IsNullOrWhiteSpace(poParam.CDEPT_CODE) ? "" : poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFROM_PERIOD, poParam.CFROM_PERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTO_PERIOD, poParam.CTO_PERIOD);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00500DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500.GetPurhcaseAdjustmentStream),
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
        public async Task SubmitPurchaseAdjAsync(APT00500SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00500SingleResult<APT00500SubmitRedraftDTO>, APT00500SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500.SubmitPurchaseAdj),
                    poParam,
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
        public async Task RedraftPurchaseAdjAsync(APT00500SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00500SingleResult<APT00500SubmitRedraftDTO>, APT00500SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00500.RedraftPurchaseAdj),
                    poParam,
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
        #region Not Implment
        public IAsyncEnumerable<APT00500DTO> GetPurhcaseAdjustmentStream()
        {
            throw new NotImplementedException();
        }

        public APT00500SingleResult<APT00500SubmitRedraftDTO> SubmitPurchaseAdj(APT00500SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APT00500SingleResult<APT00500SubmitRedraftDTO> RedraftPurchaseAdj(APT00500SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
