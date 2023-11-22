using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03505;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03505Model : R_BusinessObjectServiceClientBase<LMM03505ParameterDTO>, ILMM03505
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03505";
        private const string DEFAULT_MODULE = "lm";

        public LMM03505Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<LMM03505DTO> GetBankInfoList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM03505ResultDTO> GetBankInfoListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<LMM03505DTO> loResult = null;
            LMM03505ResultDTO loRtn = new LMM03505ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03505DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03505.GetBankInfoList),
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
