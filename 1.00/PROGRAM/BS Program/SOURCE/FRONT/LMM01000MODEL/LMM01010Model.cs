using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;
using LMM01000COMMON;
using R_CommonFrontBackAPI;

namespace LMM01000MODEL
{
    public class LMM01010Model : R_BusinessObjectServiceClientBase<LMM01010DTO>, ILMM01010
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01010";
        private const string DEFAULT_MODULE = "LM";

        public LMM01010Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM01000List<LMM01011DTO> GetRateECList(LMM01011DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01000List<LMM01011DTO>> GetRateECListAsync(LMM01011DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01011DTO> loRtn = new LMM01000List<LMM01011DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01011DTO>, LMM01011DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01010.GetRateECList),
                    poParam,
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

    }
}
