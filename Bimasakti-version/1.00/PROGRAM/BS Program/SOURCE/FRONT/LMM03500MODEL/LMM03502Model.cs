using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03502Model : R_BusinessObjectServiceClientBase<ProfileTaxParameterDTO>, ILMM03502
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03502";
        private const string DEFAULT_MODULE = "lm";

        public LMM03502Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM03502ResultDTO GetTenantProfile(LMM03502ParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM03502ResultDTO> GetTenantProfileAsync(LMM03502ParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            LMM03502ResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM03502ResultDTO, LMM03502ParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(ILMM03502.GetTenantProfile),
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

        public IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetLineOfBusinessResultDTO> GetLineOfBusinessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetLineOfBusinessDTO> loResult = null;
            GetLineOfBusinessResultDTO loRtn = new GetLineOfBusinessResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetLineOfBusinessDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03502.GetLineOfBusinessList),
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

        public IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTenantCategoryResultDTO> GetTenantCategoryListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetTenantCategoryDTO> loResult = null;
            GetTenantCategoryResultDTO loRtn = new GetTenantCategoryResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTenantCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03502.GetTenantCategoryList),
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

        public IAsyncEnumerable<GetTenantGroupDTO> GetTenantGroupList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTenantGroupResultDTO> GetTenantGroupListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetTenantGroupDTO> loResult = null;
            GetTenantGroupResultDTO loRtn = new GetTenantGroupResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTenantGroupDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03502.GetTenantGroupList),
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


        public IAsyncEnumerable<GetTenantTypeDTO> GetTenantTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTenantTypeResultDTO> GetTenantTypeListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetTenantTypeDTO> loResult = null;
            GetTenantTypeResultDTO loRtn = new GetTenantTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTenantTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03502.GetTenantTypeList),
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
