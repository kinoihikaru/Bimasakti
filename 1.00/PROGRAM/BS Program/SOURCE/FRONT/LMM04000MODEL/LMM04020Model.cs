using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using LMM04000COMMON.DTOs.LMM04020;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL
{
    public class LMM04020Model : R_BusinessObjectServiceClientBase<ProfileTaxDTO>, ILMM04020
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM04020";
        private const string DEFAULT_MODULE = "lm";

        public LMM04020Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        public LMM04020ResultDTO GetTaxInfo(LMM04020ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM04020ResultDTO> GetTaxInfoAsync(LMM04020ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LMM04020ResultDTO loRtn = new LMM04020ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM04020ResultDTO, LMM04020ParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04020.GetTaxInfo),
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

        public IAsyncEnumerable<GetLMM04020ListDTO> GetIdTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM04020ListResultDTO> GetIdTypeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetLMM04020ListDTO> loResult = null;
            GetLMM04020ListResultDTO loRtn = new GetLMM04020ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM04020ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04020.GetIdTypeList),
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

        public IAsyncEnumerable<GetLMM04020ListDTO> GetTaxCodeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM04020ListResultDTO> GetTaxCodeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetLMM04020ListDTO> loResult = null;
            GetLMM04020ListResultDTO loRtn = new GetLMM04020ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM04020ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04020.GetTaxCodeList),
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

        public IAsyncEnumerable<GetLMM04020ListDTO> GetTaxTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLMM04020ListResultDTO> GetTaxTypeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetLMM04020ListDTO> loResult = null;
            GetLMM04020ListResultDTO loRtn = new GetLMM04020ListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLMM04020ListDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04020.GetTaxTypeList),
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
