using GSM02200COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM02200MODEL
{
    public class GSM02200Model : R_BusinessObjectServiceClientBase<GSM02200DTO>, IGSM02200
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM02200";
        private const string DEFAULT_MODULE = "GS";

        public GSM02200Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region DownloadUrlTemplate
        public GSM02200UploadFileDTO DownloadTemplateFile()
        {
            throw new NotImplementedException();
        }

        public async Task<GSM02200UploadFileDTO> DownloadTemplateFileAsync()
        {
            var loEx = new R_Exception();
            GSM02200UploadFileDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM02200UploadFileDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02200.DownloadTemplateFile),
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
        #endregion

        #region Geography List
        public IAsyncEnumerable<GSM02200DTO> GetGeographyList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<GSM02200DTO>> GetGeographyAsync()
        {
            var loEx = new R_Exception();
            List<GSM02200DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM02200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02200.GetGeographyList),
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
        #endregion

        #region Change Status Geography
        public GSM02200SingleResult<GSM02200DTO> GSM02200GeographyChangeStatus(GSM02200DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task GSM02200GeographyChangeStatusAsync(GSM02200DTO poParam)
        {
            var loEx = new R_Exception();
            GSM02200SingleResult<GSM02200DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM02200SingleResult<GSM02200DTO>, GSM02200DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM02200.GSM02200GeographyChangeStatus),
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
        #endregion
    }
}
