using GSM09300COMMON;
using GSM09300COMMON.DTOs.GSM09300;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09300MODEL
{
    public class GSM09300Model : R_BusinessObjectServiceClientBase<GSM09300DetailDTO>, IGSM09300
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09300";
        private const string DEFAULT_MODULE = "GS";

        public GSM09300Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
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
                    nameof(IGSM09300.GetPropertyList),
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

        public IAsyncEnumerable<GSM09300DTO> GetSupplierCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09300ResultDTO> GetSupplierCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09300DTO> loResult = null;
            GSM09300ResultDTO loRtn = new GSM09300ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09300.GetSupplierCategoryList),
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
                    nameof(IGSM09300.ValidateIsCategoryExist),
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
