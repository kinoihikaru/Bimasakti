using GSM01500COMMON;
using GSM01500COMMON.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GSM01500MODEL
{
    public class UploadCenterModel : R_BusinessObjectServiceClientBase<UploadCenterParameterDTO>, IUploadCenter
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/UploadCenter";
        private const string DEFAULT_MODULE = "gs";

        public UploadCenterModel(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public CompanyResultDTO GetCompany()
        {
            throw new NotImplementedException();
        }
        public async Task<CompanyResultDTO> GetCompanyAsync()
        {
            var loEx = new R_Exception();
            CompanyResultDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<CompanyResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IUploadCenter.GetCompany),
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
    }
}
