using R_BusinessObjectFront;
using LMM06500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;

namespace LMM06500MODEL
{
    public class LMM06500UniversalModel : R_BusinessObjectServiceClientBase<LMM06500DTO>, ILMM06500Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06500Universal";
        private const string DEFAULT_MODULE = "LM";

        public LMM06500UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM06500List<LMM06500UniversalDTO> GetGenderList(LMM06500UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500List<LMM06500UniversalDTO>> GetGenderListAsync(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500UniversalDTO> loRtn = new LMM06500List<LMM06500UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06500UniversalDTO>, LMM06500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetGenderList),
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

        public LMM06500List<LMM06500UniversalDTO> GetPositionList(LMM06500UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500List<LMM06500UniversalDTO>> GetPositionListAsync(LMM06500UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500UniversalDTO> loRtn = new LMM06500List<LMM06500UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06500UniversalDTO>, LMM06500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetPositionList),
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
