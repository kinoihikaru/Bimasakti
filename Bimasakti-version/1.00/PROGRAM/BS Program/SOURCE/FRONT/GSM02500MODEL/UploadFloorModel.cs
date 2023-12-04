using GSM02500COMMON;
using GSM02500COMMON.DTOs.GSM02520;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM02500MODEL
{
    public class UploadFloorModel : R_BusinessObjectServiceClientBase<UploadFloorDTO>, IUploadFloor
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadFloor";
        private const string DEFAULT_MODULE = "gs";

        public UploadFloorModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public CheckIsFloorUsedResultDTO CheckIsFloorUsed()
        {
            throw new NotImplementedException();
        }
        public async Task<CheckIsFloorUsedResultDTO> CheckIsFloorUsedAsync()
        {
            R_Exception loEx = new R_Exception();
            CheckIsFloorUsedResultDTO loRtn = new CheckIsFloorUsedResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<CheckIsFloorUsedResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadFloor.CheckIsFloorUsed),
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

        public IAsyncEnumerable<UploadFloorErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadFloorErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadFloorErrorDTO> loResult = null;
            UploadFloorErrorResultDTO loRtn = new UploadFloorErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadFloorErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadFloor.GetErrorProcessList),
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

        public IAsyncEnumerable<UploadFloorDTO> GetUploadFloorList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadFloorResultDTO> GetUploadFloorListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadFloorDTO> loResult = null;
            UploadFloorResultDTO loRtn = new UploadFloorResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadFloorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadFloor.GetUploadFloorList),
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
