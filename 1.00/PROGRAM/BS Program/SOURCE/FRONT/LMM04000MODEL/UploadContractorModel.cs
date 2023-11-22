using LMM04000COMMON;
using LMM04000COMMON.DTOs.LMM04010;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM04000MODEL
{
    public class UploadContractorModel : R_BusinessObjectServiceClientBase<UploadContractorDTO>, IUploadContractor
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadContractor";
        private const string DEFAULT_MODULE = "lm";

        public UploadContractorModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public IAsyncEnumerable<UploadContractorErrorDTO> GetErrorProcessList()
        {
            throw new NotImplementedException();
        }

        public async Task<UploadContractorErrorResultDTO> GetErrorProcessListAsync()
        {
            var loEx = new R_Exception();
            List<UploadContractorErrorDTO> loResult = null;
            UploadContractorErrorResultDTO loRtn = new UploadContractorErrorResultDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadContractorErrorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadContractor.GetErrorProcessList),
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

        public IAsyncEnumerable<UploadContractorDTO> GetUploadContractorList()
        {
            throw new NotImplementedException();
        }
        public async Task<UploadContractorResultDTO> GetUploadContractorListStreamAsync()
        {
            var loEx = new R_Exception();
            List<UploadContractorDTO> loResult = null;
            UploadContractorResultDTO loRtn = new UploadContractorResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<UploadContractorDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadContractor.GetUploadContractorList),
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
