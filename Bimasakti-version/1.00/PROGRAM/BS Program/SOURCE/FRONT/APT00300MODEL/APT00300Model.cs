using APT00300COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APT00300MODEL
{
    public class APT00300Model : R_BusinessObjectServiceClientBase<APT00300DTO>, IAPT00300
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00300";
        private const string DEFAULT_MODULE = "AP";

        public APT00300Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<APT00300DTO>> GetDebitNoteListStreamAsync(APT00300ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00300DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSUPPLIER_ID, poParam.CSUPPLIER_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CFROM_PERIOD, poParam.CFROM_PERIOD);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTO_PERIOD, poParam.CTO_PERIOD);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300.GetDebitNoteListStream),
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
        public IAsyncEnumerable<APT00300DTO> GetDebitNoteListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
