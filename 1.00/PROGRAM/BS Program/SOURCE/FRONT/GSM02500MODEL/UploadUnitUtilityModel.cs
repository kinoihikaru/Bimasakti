using GSM02500COMMON.DTOs.GSM02530;
using GSM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM02500COMMON.DTOs.GSM02531;
using GSM02500COMMON.DTOs.GSM02520;

namespace GSM02500MODEL
{
    public class UploadUnitUtilityModel : R_BusinessObjectServiceClientBase<UploadUnitUtilityDTO>, IUploadUnitUtility
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadUnitUtility";
        private const string DEFAULT_MODULE = "gs";

        public UploadUnitUtilityModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<GetAllUnitUtilityDTO> GetAllUnitUtilities()
        {
            throw new NotImplementedException();
        }
        public async Task<GetAllUnitUtilityResultDTO> GetAllUnitUtilitiesStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<GetAllUnitUtilityDTO> loResult = null;
            GetAllUnitUtilityResultDTO loRtn = new GetAllUnitUtilityResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GetAllUnitUtilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitUtility.GetAllUnitUtilities),
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

        public IAsyncEnumerable<UploadUnitUtilityErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadUnitUtilityErrorResultDTO> GetErrorProcessListAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitUtilityErrorDTO> loResult = null;
            UploadUnitUtilityErrorResultDTO loRtn = new UploadUnitUtilityErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitUtilityErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitUtility.GetErrorProcessList),
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


        public IAsyncEnumerable<UploadUnitUtilityDTO> GetUploadUnitUtilityList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadUnitUtilityResultDTO> GetUploadUnitUtilityListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<UploadUnitUtilityDTO> loResult = null;
            UploadUnitUtilityResultDTO loRtn = new UploadUnitUtilityResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadUnitUtilityDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadUnitUtility.GetUploadUnitUtilityList),
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
