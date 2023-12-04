using GSM09300COMMON;
using GSM09300COMMON.DTOs.GSM09310;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM09300MODEL
{
    public class GSM09310Model : R_BusinessObjectServiceClientBase<GSM09310DTO>, IGSM09310
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM09310";
        private const string DEFAULT_MODULE = "GS";

        public GSM09310Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetSupplierCategoryResultDTO GetSupplierCategory()
        {
            throw new NotImplementedException();
        }
        public async Task<GetSupplierCategoryResultDTO> GetSupplierCategoryAsync()
        {
            R_Exception loEx = new R_Exception();
            GetSupplierCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetSupplierCategoryResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09310.GetSupplierCategory),
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

        public IAsyncEnumerable<GSM09310DTO> GetSupplierList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09310ResultDTO> GetSupplierListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GSM09310DTO> loResult = null;
            GSM09310ResultDTO loRtn = new GSM09310ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09310DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09310.GetSupplierList),
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

        public MoveSupplierCategoryResultDTO MoveSupplierCategory(MoveSupplierCategoryParameterDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task MoveSupplierCategoryAsync(MoveSupplierCategoryParameterDTO poParam)
        {
            R_Exception loEx = new R_Exception();
            MoveSupplierCategoryResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<MoveSupplierCategoryResultDTO, MoveSupplierCategoryParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09310.MoveSupplierCategory),
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
