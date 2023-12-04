using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04030;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL
{
    public class LMM04030Model : R_BusinessObjectServiceClientBase<LMM04030ParameterDTO>, ILMM04030
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM04030";
        private const string DEFAULT_MODULE = "lm";

        public LMM04030Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TenantResultDTO GetTenant(TenantParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<TenantResultDTO> GetTenantAsync(TenantParameterDTO poParam)
        {
            var loEx = new R_Exception();
            TenantResultDTO loRtn = new TenantResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<TenantResultDTO, TenantParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04030.GetTenant),
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

        public IAsyncEnumerable<GetBankCodeDTO> GetBankCodeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetBankCodeResultDTO> GetBankCodeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GetBankCodeDTO> loResult = null;
            GetBankCodeResultDTO loRtn = new GetBankCodeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetBankCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04030.GetBankCodeList),
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

        public IAsyncEnumerable<LMM04030DTO> GetBankInfoList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM04030ResultDTO> GetBankInfoListStreamAsync()
        {
            var loEx = new R_Exception();
            List<LMM04030DTO> loResult = null;
            LMM04030ResultDTO loRtn = new LMM04030ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM04030DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04030.GetBankInfoList),
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
