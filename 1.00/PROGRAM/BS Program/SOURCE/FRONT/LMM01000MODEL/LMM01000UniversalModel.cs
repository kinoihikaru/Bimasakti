using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;
using LMM01000COMMON;

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

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetChargesTypeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetChargesTypeList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetStatusListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetStatusList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetTaxExemptionCodeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetTaxExemptionCodeList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetWithholdingTaxTypeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetWithholdingTaxTypeList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetRateTypeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetRateTypeList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetUsageRateModeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetUsageRateModeList),
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

            return loRtn;
        }

        public async Task<LMM01000List<LMM01000UniversalDTO>> GetAdminFeeTypeListAsync(LMM01000UniversalDTO poParam)
        {
            var loEx = new R_Exception();
            LMM01000List<LMM01000UniversalDTO> loRtn = new LMM01000List<LMM01000UniversalDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM01000List<LMM01000UniversalDTO>, LMM01000UniversalDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM01000Universal.GetAdminFeeTypeList),
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

            return loRtn;
        }


        public LMM01000List<LMM01000UniversalDTO> GetChargesTypeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetStatusList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetTaxExemptionCodeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetWithholdingTaxTypeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetUsageRateModeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetRateTypeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }

        public LMM01000List<LMM01000UniversalDTO> GetAdminFeeTypeList(LMM01000UniversalDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
