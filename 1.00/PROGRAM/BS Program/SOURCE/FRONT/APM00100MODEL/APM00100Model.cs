using APM00100COMMON;
using APM00100COMMON.DTOs.APM00100;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APM00100MODEL
{
    public class APM00100Model : R_BusinessObjectServiceClientBase<APM00100ParameterDTO>, IAPM00100
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlAP";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/APM00100";
        private const string DEFAULT_MODULE = "AP";

        public APM00100Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GetAPMSystemParamResultDTO GetAPMSystemParam()
        {
            throw new NotImplementedException();
        }

        public async Task<GetAPMSystemParamResultDTO> GetAPMSystemParamAsync()
        {
            R_Exception loEx = new R_Exception();
            GetAPMSystemParamResultDTO loRtn = null;
            
            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GetAPMSystemParamResultDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00100.GetAPMSystemParam),
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
