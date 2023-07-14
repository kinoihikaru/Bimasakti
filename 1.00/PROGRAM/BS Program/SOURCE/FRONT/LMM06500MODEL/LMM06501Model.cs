using R_BusinessObjectFront;
using LMM06500COMMON;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;

namespace LMM06500MODEL
{
    public class LMM06501Model : R_BusinessObjectServiceClientBase<LMM06502DTO>, ILMM06501
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM06501";
        private const string DEFAULT_MODULE = "LM";

        public LMM06501Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public LMM06500UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        public async Task<LMM06500UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            LMM06500UploadFileDTO loResult = new LMM06500UploadFileDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06501.DownloadTemplateFile),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public LMM06500List<LMM06500DTO> GetStaffUploadList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM06500List<LMM06500DTO>> GetStaffUploadListAsync()
        {
            var loEx = new R_Exception();
            LMM06500List<LMM06500DTO> loResult = new LMM06500List<LMM06500DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM06500List<LMM06500DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM06501.GetStaffUploadList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

    }
}
