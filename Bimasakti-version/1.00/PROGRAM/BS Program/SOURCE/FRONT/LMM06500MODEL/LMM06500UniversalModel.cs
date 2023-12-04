using LMM06500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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


        public IAsyncEnumerable<LMM06500DTOInitial> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM06500DTOInitial>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<LMM06500DTOInitial> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500DTOInitial>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetProperty),
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

        public IAsyncEnumerable<LMM06500UniversalDTO> GetGenderList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM06500UniversalDTO>> GetGenderListAsync()
        {
            var loEx = new R_Exception();
            List<LMM06500UniversalDTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetGenderList),
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

        public IAsyncEnumerable<LMM06500UniversalDTO> GetPositionList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM06500UniversalDTO>> GetPositionListAsync()
        {
            var loEx = new R_Exception();
            List<LMM06500UniversalDTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM06500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetPositionList),
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

        public LMM06500AllListUniversalDTO GetAllUniversalList()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500AllListUniversalDTO> GetAllUniversalListAsync()
        {
            var loEx = new R_Exception();
            LMM06500AllListUniversalDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500AllListUniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06500Universal.GetAllUniversalList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
    }
}
