using LMM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM02500MODEL
{
    public class LMM02500Model : R_BusinessObjectServiceClientBase<LMM02500DTO>, ILMM02500
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM02500";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM02500DTO>> GetTenantGrpistAsync(string poPropertyID)
        {
            var loEx = new R_Exception();
            List<LMM02500DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTenantGrpist),
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
        public async Task<List<LMM02500TenantDTO>> GetTenantListAsync(string poPropertyID, string poTenantGrpId)
        {
            var loEx = new R_Exception();
            List<LMM02500TenantDTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTENANT_GROUP_ID, poTenantGrpId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500TenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500.GetTenantList),
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
        public IAsyncEnumerable<LMM02500DTO> GetTenantGrpist()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500TenantDTO> GetTenantList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
