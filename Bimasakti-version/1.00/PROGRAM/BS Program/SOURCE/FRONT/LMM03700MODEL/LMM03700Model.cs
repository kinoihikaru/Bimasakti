using LMM03700COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM03700MODEL
{
    public class LMM03700Model : R_BusinessObjectServiceClientBase<LMM03700DTO>, ILMM03700
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM03700";
        private const string DEFAULT_MODULE = "LM";

        public LMM03700Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM03700DTO>> GetTenantClassGrpListAsync(string poPropertyID)
        {
            var loEx = new R_Exception();
            List<LMM03700DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03700DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03700.GetTenantClassGrpList),
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
        public async Task<List<LMM03700PropetyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<LMM03700PropetyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03700PropetyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03700.GetPropertyList),
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
        public IAsyncEnumerable<LMM03700PropetyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<LMM03700DTO> GetTenantClassGrpList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
