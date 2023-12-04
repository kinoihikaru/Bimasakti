using GSM02500COMMON.DTOs.GSM02520;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02530;

namespace GSM02500MODEL
{
    public class UploadUnitModel : R_BusinessObjectServiceClientBase<UploadUnitDTO>, IUploadUnit
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnit";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<UploadUnitErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitErrorDTO> loResult = null;
            UploadUnitErrorResultDTO loRtn = new UploadUnitErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnit.GetErrorProcessList),
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

        public IAsyncEnumerable<UploadUnitDTO> GetUploadUnitList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitResultDTO> GetUploadUnitListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitDTO> loResult = null;
            UploadUnitResultDTO loRtn = new UploadUnitResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnit.GetUploadUnitList),
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
