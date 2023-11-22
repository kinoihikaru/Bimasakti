using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09001;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000MODEL
{
    public class GSM09001Model : R_BusinessObjectServiceClientBase<GSM09001DTO>, IGSM09001
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09001";
        private const string DEFAULT_MODULE = "GS";

        public GSM09001Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetTenantCategoryResultDTO GetTenantCategory()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTenantCategoryResultDTO> GetTenantCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            GetTenantCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetTenantCategoryResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09001.GetTenantCategory),
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

        public IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetTenantCategoryListResultDTO> GetTenantCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetTenantCategoryDTO> loResult = null;
            GetTenantCategoryListResultDTO loRtn = new GetTenantCategoryListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetTenantCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09001.GetTenantCategoryList),
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

        public IAsyncEnumerable<GSM09001DTO> GetTenantList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09001ResultDTO> GetTenantListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09001DTO> loResult = null;
            GSM09001ResultDTO loRtn = new GSM09001ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09001DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09001.GetTenantList),
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

        public SaveMoveTenantCategoryResultDTO SaveMoveTenantCategory(SaveMoveTenantCategoryParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task SaveMoveTenantCategoryAsync(SaveMoveTenantCategoryParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            SaveMoveTenantCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<SaveMoveTenantCategoryResultDTO, SaveMoveTenantCategoryParameterDTO> (
                    _RequestServiceEndPoint,
                    nameof(IGSM09001.SaveMoveTenantCategory),
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
        }
    }
}
