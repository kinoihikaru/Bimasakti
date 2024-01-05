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
    public class LMM02500UniversalModel : R_BusinessObjectServiceClientBase<LMM02500UniversalDTO>, ILMM02500Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM02500Universal";
        private const string DEFAULT_MODULE = "LM";

        public LMM02500UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<LMM02500PropertyDTO>> GetPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<LMM02500PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Universal.GetPropertyList),
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
        public async Task<List<LMM02500UniversalDTO>> GetIDTypeListAsync()
        {
            var loEx = new R_Exception();
            List<LMM02500UniversalDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Universal.GetIDTypeList),
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
        public async Task<List<LMM02500UniversalDTO>> GetTaxCodeListAsync()
        {
            var loEx = new R_Exception();
            List<LMM02500UniversalDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Universal.GetTaxCodeList),
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
        public async Task<List<LMM02500UniversalDTO>> GetTaxTypeListAsync()
        {
            var loEx = new R_Exception();
            List<LMM02500UniversalDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM02500UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Universal.GetTaxTypeList),
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
        public async Task<LMM02510AllUniversalDataDTO> GetALlUniversalDataAsync()
        {
            var loEx = new R_Exception();
            LMM02510AllUniversalDataDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<LMM02500Record<LMM02510AllUniversalDataDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ILMM02500Universal.GetALlUniversalData),
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        #region Not Implement
        public LMM02500Record<LMM02510AllUniversalDataDTO> GetALlUniversalData()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500UniversalDTO> GetIDTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500PropertyDTO> GetPropertyList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500UniversalDTO> GetTaxCodeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM02500UniversalDTO> GetTaxTypeList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
