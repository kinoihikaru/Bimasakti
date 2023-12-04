using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03503Model : R_BusinessObjectServiceClientBase<ProfileTaxDTO>, ILMM03503
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03503";
        private const string DEFAULT_MODULE = "lm";

        public LMM03503Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public LMM03503ResultDTO GetTaxInfo(LMM03503ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM03503ResultDTO> GetTaxInfoAsync(LMM03503ParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            LMM03503ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM03503ResultDTO, LMM03503ParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(ILMM03503.GetTaxInfo),
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

        public IAsyncEnumerable<GetLMM03503ListDTO> GetIdTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM03503ListResultDTO> GetIdTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            GetLMM03503ListResultDTO loRtn = new GetLMM03503ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM03503ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03503.GetIdTypeList),
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

        public IAsyncEnumerable<GetLMM03503ListDTO> GetTaxCodeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM03503ListResultDTO> GetTaxCodeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            GetLMM03503ListResultDTO loRtn = new GetLMM03503ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM03503ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03503.GetTaxCodeList),
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

        public IAsyncEnumerable<GetLMM03503ListDTO> GetTaxTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM03503ListResultDTO> GetTaxTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetLMM03503ListDTO> loResult = null;
            GetLMM03503ListResultDTO loRtn = new GetLMM03503ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM03503ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03503.GetTaxTypeList),
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
