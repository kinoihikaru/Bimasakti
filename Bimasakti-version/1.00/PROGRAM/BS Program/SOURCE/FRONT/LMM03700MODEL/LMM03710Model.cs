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
    public class LMM03710Model : R_BusinessObjectServiceClientBase<LMM03710DTO>, ILMM03710
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM03710";
        private const string DEFAULT_MODULE = "LM";

        public LMM03710Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM03710DTO>> GetTenantClassListAsync(LMM03710DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM03710DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? "" : poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, string.IsNullOrWhiteSpace(poEntity.CTENANT_CLASSIFICATION_GROUP_ID) ? "" : poEntity.CTENANT_CLASSIFICATION_GROUP_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03710DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetTenantClassList),
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

        public async Task<List<LMM03711DTO>> GetTenantCLassTenantListAsync(LMM03711DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM03711DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? "" : poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTENANT_CLASSIFICATION_ID, string.IsNullOrWhiteSpace(poEntity.CTENANT_CLASSIFICATION_ID) ? "" : poEntity.CTENANT_CLASSIFICATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03711DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetTenantCLassTenantList),
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

        public async Task<List<LMM03711DTO>> GetAssignTenantCLassListAsync(LMM03711DTO poEntity)
        {
            var loEx = new R_Exception();
            List<LMM03711DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, string.IsNullOrWhiteSpace(poEntity.CPROPERTY_ID) ? "" : poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTENANT_CLASSIFICATION_GROUP_ID, string.IsNullOrWhiteSpace(poEntity.CTENANT_CLASSIFICATION_GROUP_ID) ? "" : poEntity.CTENANT_CLASSIFICATION_GROUP_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTENANT_CLASSIFICATION_ID, string.IsNullOrWhiteSpace(poEntity.CTENANT_CLASSIFICATION_ID) ? "" : poEntity.CTENANT_CLASSIFICATION_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM03711DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.GetAssignTenantCLassList),
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
        public async Task AssignTenantClassAsync(LMM03711AssignTenantDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM03700Record<LMM03711AssignTenantDTO>, LMM03711AssignTenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.AssignTenantClass),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        public async Task MoveTenantClassAsync(LMM03711MoveTenantDTO poEntity)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM03700Record<LMM03711MoveTenantDTO>, LMM03711MoveTenantDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM03710.MoveTenantClass),
                    poEntity,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();
        }
        #region Not Implement
        public IAsyncEnumerable<LMM03710DTO> GetTenantClassList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM03711DTO> GetTenantCLassTenantList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM03711DTO> GetAssignTenantCLassList()
        {
            throw new NotImplementedException();
        }

        public LMM03700Record<LMM03711AssignTenantDTO> AssignTenantClass(LMM03711AssignTenantDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public LMM03700Record<LMM03711MoveTenantDTO> MoveTenantClass(LMM03711MoveTenantDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
