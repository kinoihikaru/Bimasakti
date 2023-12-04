using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02502;

namespace GSM02500MODEL
{
    public class UploadUnitTypeCategoryModel : R_BusinessObjectServiceClientBase<UploadUnitTypeCategoryDTO>, IUploadUnitTypeCategory
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnitTypeCategory";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitTypeCategoryModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<UploadUnitTypeCategoryErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitTypeCategoryErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitTypeCategoryErrorDTO> loResult = null;
            UploadUnitTypeCategoryErrorResultDTO loRtn = new UploadUnitTypeCategoryErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitTypeCategoryErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitTypeCategory.GetErrorProcessList),
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

        public CheckIsUnitTypeCategoryUsedResultDTO CheckIsUnitTypeCategoryUsed()
        {
            throw new NotImplementedException();
        }
        public async Task<CheckIsUnitTypeCategoryUsedResultDTO> CheckIsUnitTypeCategoryUsedAsync()
        {
            R_Exception loEx = new R_Exception();
            CheckIsUnitTypeCategoryUsedResultDTO loRtn = new CheckIsUnitTypeCategoryUsedResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<CheckIsUnitTypeCategoryUsedResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitTypeCategory.CheckIsUnitTypeCategoryUsed),
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

        public IAsyncEnumerable<UploadUnitTypeCategoryDTO> GetUploadUnitTypeCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitTypeCategoryResultDTO> GetUploadUnitTypeCategoryListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitTypeCategoryDTO> loResult = null;
            UploadUnitTypeCategoryResultDTO loRtn = new UploadUnitTypeCategoryResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitTypeCategoryDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitTypeCategory.GetUploadUnitTypeCategoryList),
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