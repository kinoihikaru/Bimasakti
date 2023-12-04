using GSM09200COMMON;
using GSM09200COMMON.DTOs.GSM09200;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09200MODEL
{
    public class GSM09200Model : R_BusinessObjectServiceClientBase<GSM09200DetailDTO>, IGSM09200
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09200";
        private const string DEFAULT_MODULE = "GS";

        public GSM09200Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
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

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetPropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09200.GetPropertyList),
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

        public IAsyncEnumerable<GSM09200DTO> GetExpenditureCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09200ResultDTO> GetExpenditureCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09200DTO> loResult = null;
            GSM09200ResultDTO loRtn = new GSM09200ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09200.GetExpenditureCategoryList),
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

        public ValidateIsCategoryExistResultDTO ValidateIsCategoryExist(ValidateIsCategoryExistParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task ValidateIsCategoryExistAsync(ValidateIsCategoryExistParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            ValidateIsCategoryExistResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<ValidateIsCategoryExistResultDTO, ValidateIsCategoryExistParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09200.ValidateIsCategoryExist),
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
