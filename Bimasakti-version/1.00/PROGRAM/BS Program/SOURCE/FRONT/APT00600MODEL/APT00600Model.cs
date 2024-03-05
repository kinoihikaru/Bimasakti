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
    public class APT00600Model : R_BusinessObjectServiceClientBase<APT00600DTO>, IAPT00600
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00600";
        private const string DEFAULT_MODULE = "AP";

        public APT00600Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<APT00600DTO>> GetPurhcaseAdjustmentStreamAsync(APT00600ParamDTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00600DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, string.IsNullOrWhiteSpace(poParam.CDEPT_CODE) ? "" : poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFROM_PERIOD, poParam.CFROM_PERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTO_PERIOD, poParam.CTO_PERIOD);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600.GetPurhcaseAdjustmentStream),
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
        public async Task SubmitPurchaseAdjAsync(APT00600SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00600SingleResult<APT00600SubmitRedraftDTO>, APT00600SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600.SubmitPurchaseAdj),
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
        public async Task RedraftPurchaseAdjAsync(APT00600SubmitRedraftDTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00600SingleResult<APT00600SubmitRedraftDTO>, APT00600SubmitRedraftDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00600.RedraftPurchaseAdj),
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
        public IAsyncEnumerable<APT00600DTO> GetPurhcaseAdjustmentStream()
        {
            throw new NotImplementedException();
        }

        public APT00600SingleResult<APT00600SubmitRedraftDTO> SubmitPurchaseAdj(APT00600SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APT00600SingleResult<APT00600SubmitRedraftDTO> RedraftPurchaseAdj(APT00600SubmitRedraftDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
