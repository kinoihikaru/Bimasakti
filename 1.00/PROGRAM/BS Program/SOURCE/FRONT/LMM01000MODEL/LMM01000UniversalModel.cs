using LMM01000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM01000MODEL
{
    public class LMM01000UniversalModel : R_BusinessObjectServiceClientBase<LMM01000DTO>, ILMM01000Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01000Universal";
        private const string DEFAULT_MODULE = "LM";

        public LMM01000UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetChargesTypeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data= await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetChargesTypeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetStatusListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetStatusList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetTaxExemptionCodeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetTaxExemptionCodeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetWithholdingTaxTypeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetWithholdingTaxTypeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetRateTypeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetRateTypeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetUsageRateModeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetUsageRateModeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetAdminFeeTypeListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetAdminFeeTypeList),
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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetAccrualMethodListAsync()
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn.Data = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetAccrualMethodList),
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

        public IAsyncEnumerable<LMM01000UniversalDTO> GetChargesTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetStatusList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetTaxExemptionCodeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetWithholdingTaxTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetUsageRateModeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetRateTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetAdminFeeTypeList()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<LMM01000UniversalDTO> GetAccrualMethodList()
        {
            throw new NotImplementedException();
        }
    }
}
