using GSM02500COMMON.DTOs.GSM02502;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02503;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL
{
    public class UploadUnitTypeModel : R_BusinessObjectServiceClientBase<UploadUnitTypeDTO>, IUploadUnitType
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnitType";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitTypeModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<UploadUnitTypeErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitTypeErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitTypeErrorDTO> loResult = null;
            UploadUnitTypeErrorResultDTO loRtn = new UploadUnitTypeErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitTypeErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitType.GetErrorProcessList),
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

        public CheckIsUnitTypeUsedResultDTO CheckIsUnitTypeUsed()
        {
            throw new NotImplementedException();
        }
        public async Task<CheckIsUnitTypeUsedResultDTO> CheckIsUnitTypeUsedAsync()
        {
            R_Exception loEx = new R_Exception();
            CheckIsUnitTypeUsedResultDTO loRtn = new CheckIsUnitTypeUsedResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<CheckIsUnitTypeUsedResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitType.CheckIsUnitTypeUsed),
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

        public IAsyncEnumerable<UploadUnitTypeDTO> GetUploadUnitTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitTypeResultDTO> GetUploadUnitTypeListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitTypeDTO> loResult = null;
            UploadUnitTypeResultDTO loRtn = new UploadUnitTypeResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitTypeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitType.GetUploadUnitTypeList),
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