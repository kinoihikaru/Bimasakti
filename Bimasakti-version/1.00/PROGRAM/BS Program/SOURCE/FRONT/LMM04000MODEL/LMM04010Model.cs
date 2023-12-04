using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL
{
    public class LMM04010Model : R_BusinessObjectServiceClientBase<ProfileTaxParameterDTO>, ILMM04010
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM04010";
        private const string DEFAULT_MODULE = "lm";

        public LMM04010Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetCurrencyDTO> GetCurrencyList()
        {
            throw new NotImplementedException();
        }

        public async Task<GetCurrencyResultDTO> GetCurrencyListAsync()
        {
            var loEx = new R_Exception();
            List<GetCurrencyDTO> loResult = null;
            GetCurrencyResultDTO loRtn = new GetCurrencyResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetCurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetCurrencyList),
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

        public LMM04010ResultDTO GetContractorProfile(LMM04010ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM04010ResultDTO> GetContractorProfileAsync(LMM04010ParameterDTO poParam)
        {
            var loEx = new R_Exception();
            LMM04010ResultDTO loRtn = new LMM04010ResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM04010ResultDTO, LMM04010ParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetContractorProfile),
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

        public IAsyncEnumerable<GetJournalGroupDTO> GetJournalGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetJournalGroupResultDTO> GetJournalGroupListAsync()
        {
            var loEx = new R_Exception();
            List<GetJournalGroupDTO> loResult = null;
            GetJournalGroupResultDTO loRtn = new GetJournalGroupResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetJournalGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetJournalGroupList),
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

        public IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLineOfBusinessResultDTO> GetLineOfBusinessListAsync()
        {
            var loEx = new R_Exception();
            List<GetLineOfBusinessDTO> loResult = null;
            GetLineOfBusinessResultDTO loRtn = new GetLineOfBusinessResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLineOfBusinessDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetLineOfBusinessList),
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

        public IAsyncEnumerable<GetPaymentTermDTO> GetPaymentTermList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetPaymentTermResultDTO> GetPaymentTermListAsync()
        {
            var loEx = new R_Exception();
            List<GetPaymentTermDTO> loResult = null;
            GetPaymentTermResultDTO loRtn = new GetPaymentTermResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPaymentTermDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetPaymentTermList),
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

        public IAsyncEnumerable<GetContractorCategoryDTO> GetContractorCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetContractorCategoryResultDTO> GetContractorCategoryListAsync()
        {
            var loEx = new R_Exception();
            List<GetContractorCategoryDTO> loResult = null;
            GetContractorCategoryResultDTO loRtn = new GetContractorCategoryResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetContractorCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetContractorCategoryList),
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

        public IAsyncEnumerable<GetContractorGroupDTO> GetContractorGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetContractorGroupResultDTO> GetContractorGroupListAsync()
        {
            var loEx = new R_Exception();
            List<GetContractorGroupDTO> loResult = null;
            GetContractorGroupResultDTO loRtn = new GetContractorGroupResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetContractorGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM04010.GetContractorGroupList),
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
