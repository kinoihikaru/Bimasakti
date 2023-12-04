using LMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01020Model : R_BusinessObjectServiceClientBase<LMM01020DTO>, ILMM01020
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01020";
        private const string DEFAULT_MODULE = "LM";

        public LMM01020Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMM01000List<LMM01021DTO>> GetRateWGListAsync(LMM01021DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01021DTO> loRtn = new LMM01000List<LMM01021DTO>();

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_ID, poParam.CCHARGES_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01021DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01020.GetRateWGList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }

        public IAsyncEnumerable<LMM01021DTO> GetRateWGList()
        {
            throw new NotImplementedException();
        }
    }
}
