using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03507;
using LMM03500COMMON.DTOs.LMM03508;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03508Model : R_BusinessObjectServiceClientBase<LMM03508DTO>, ILMM03508
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03508";
        private const string DEFAULT_MODULE = "lm";

        public LMM03508Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM03508DTO> GetFixVAList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM03508ResultDTO> GetFixVAListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<LMM03508DTO> loResult = null;
            LMM03508ResultDTO loRtn = new LMM03508ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03508DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03508.GetFixVAList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn.Data = loResult;
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
