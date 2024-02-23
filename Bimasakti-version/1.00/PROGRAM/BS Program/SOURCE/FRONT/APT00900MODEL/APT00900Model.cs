using APT00900COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APT00900MODEL
{
    public class APT00900Model : R_BusinessObjectServiceClientBase<APT00900DisplayDTO>, IAPT00900
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00900";
        private const string DEFAULT_MODULE = "AP";

        public APT00900Model(
           string pcHttpClientName = DEFAULT_HTTP,
           string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
           bool plSendWithContext = true,
           bool plSendWithToken = true) :
           base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APT00900UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            APT00900UploadFileDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00900UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00900.DownloadTemplateFile),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        public async Task<List<APT00900PropertyDTO>> GetGSPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<APT00900PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00900PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00900.GetGSPropertyList),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Not Implement
        public APT00900UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<APT00900PropertyDTO> GetGSPropertyList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
