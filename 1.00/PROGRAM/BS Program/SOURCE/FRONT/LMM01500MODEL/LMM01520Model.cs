using R_BusinessObjectFront;
using LMM01500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;

namespace LMM01500MODEL
{
    public class LMM01520Model : R_BusinessObjectServiceClientBase<LMM01520DTO>, ILMM01520
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01520";
        private const string DEFAULT_MODULE = "LM";

        public LMM01520Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM01500List<LMM01522DTO> GetAdditionalIdLookup()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01500List<LMM01522DTO>> GetAdditionalIdLookupAsync()
        {
            var loEx = new R_Exception();
            LMM01500List<LMM01522DTO> loResult = new LMM01500List<LMM01522DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM01500List<LMM01522DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01520.GetAdditionalIdLookup),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
    }
}
