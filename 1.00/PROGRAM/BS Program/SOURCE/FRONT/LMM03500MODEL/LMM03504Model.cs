using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03503;
using LMM03500COMMON.DTOs.LMM03504;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03504Model : R_BusinessObjectServiceClientBase<LMM03504ParameterDTO>, ILMM03504
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03504";
        private const string DEFAULT_MODULE = "lm";

        public LMM03504Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM03504ResultDTO GetBilling(GetBillingParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<LMM03504ResultDTO> GetBillingAsync(GetBillingParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            LMM03504ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM03504ResultDTO, GetBillingParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(ILMM03504.GetBilling),
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
