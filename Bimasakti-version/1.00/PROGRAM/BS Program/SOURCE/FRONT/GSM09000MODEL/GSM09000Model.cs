using GSM09000COMMON;
using GSM09000COMMON.DTOs.GSM09000;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09000MODEL
{
    public class GSM09000Model : R_BusinessObjectServiceClientBase<GSM09000DetailDTO>, IGSM09000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09000";
        private const string DEFAULT_MODULE = "GS";

        public GSM09000Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetParentPositionIfEmptyResultDTO GetParentPositionIfEmpty(GetParentPositionIfEmptyParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GetParentPositionIfEmptyResultDTO> GetParentPositionIfEmptyAsync(GetParentPositionIfEmptyParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            GetParentPositionIfEmptyResultDTO loRtn = null;
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetParentPositionIfEmptyResultDTO, GetParentPositionIfEmptyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09000.GetParentPositionIfEmpty),
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

        public IAsyncEnumerable<GetPropertyListDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
        public async Task<GetPropertyListResultDTO> GetPropertyListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetPropertyListDTO> loResult = null;
            GetPropertyListResultDTO loRtn = new GetPropertyListResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09000.GetPropertyList),
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

        public IAsyncEnumerable<GSM09000DTO> GetTenantCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09000ResultDTO> GetTenantCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09000DTO> loResult = null;
            GSM09000ResultDTO loRtn = new GSM09000ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09000.GetTenantCategoryList),
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

        public InsertNewRootIfListEmptyResultDTO InsertNewRootIfListEmpty(InsertNewRootIfListEmptyParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task InsertNewRootIfListEmptyAsync(InsertNewRootIfListEmptyParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            InsertNewRootIfListEmptyResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<InsertNewRootIfListEmptyResultDTO, InsertNewRootIfListEmptyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09000.InsertNewRootIfListEmpty),
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
