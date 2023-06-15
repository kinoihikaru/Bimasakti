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
    public class LMM01050Model : R_BusinessObjectServiceClientBase<LMM01050DTO>, ILMM01050
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01050";
        private const string DEFAULT_MODULE = "LM";

        public LMM01050Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM01000List<LMM01051DTO> GetRateOTList(LMM01051DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM01000List<LMM01051DTO>> GetRateOTListAsync(LMM01051DTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01051DTO> loRtn = new LMM01000List<LMM01051DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01051DTO>, LMM01051DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01050.GetRateOTList),
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
