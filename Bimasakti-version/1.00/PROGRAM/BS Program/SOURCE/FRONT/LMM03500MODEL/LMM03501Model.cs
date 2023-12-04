using LMM03500COMMON;
using LMM03500COMMON.DTOs.LMM03501;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LMM03500MODEL
{
    public class LMM03501Model : R_BusinessObjectServiceClientBase<LMM03501DTO>, ILMM03501
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrlLM";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/LMM03501";
        private const string DEFAULT_MODULE = "lm";

        public LMM03501Model(string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public TemplateTenantDTO DownloadTemplateTenant()
        {
            throw new NotImplementedException();
        }

        public async Task<TemplateTenantDTO> DownloadTemplateTenantAsync()
        {
            R_Exception loEx = new R_Exception();
            TemplateTenantDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<TemplateTenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03501.DownloadTemplateTenant),
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


        public IAsyncEnumerable<LMM03501DTO> GetTenantList()
        {
            throw new NotImplementedException();
        }
        public async Task<LMM03501ResultDTO> GetTenantListStreamAsync()
        {
            R_Exception loEx = new R_Exception();
            List<LMM03501DTO> loResult = null;
            LMM03501ResultDTO loRtn = new LMM03501ResultDTO();
            //R_ContextHeader loContextHeader = new R_ContextHeader();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03501DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03501.GetTenantList),
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
