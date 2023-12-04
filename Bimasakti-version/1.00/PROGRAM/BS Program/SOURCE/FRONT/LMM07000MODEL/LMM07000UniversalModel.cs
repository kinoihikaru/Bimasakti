using LMM07000COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM07000MODEL
{
    public class LMM07000UniversalModel: R_BusinessObjectServiceClientBase<LMM07000DTOUniversal>, ILMM07000Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM07000Universal";
        private const string DEFAULT_MODULE = "LM";

        public LMM07000UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region ChargesType
        public IAsyncEnumerable<LMM07000DTOUniversal> GetChargesTypeList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM07000DTOUniversal>> GetChargesTypeListAsync()
        {
            var loEx = new R_Exception();
            List<LMM07000DTOUniversal> loResult = new List<LMM07000DTOUniversal>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM07000DTOUniversal>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000Universal.GetChargesTypeList),
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

        #region Discount Type
        public IAsyncEnumerable<LMM07000DTOUniversal> GetDiscountTypeList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM07000DTOUniversal>> GetDiscountTypeListAsync()
        {
            var loEx = new R_Exception();
            List<LMM07000DTOUniversal> loResult = new List<LMM07000DTOUniversal>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM07000DTOUniversal>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000Universal.GetDiscountTypeList),
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
        #region Invoice Period
        public IAsyncEnumerable<LMM07000PeriodDTO> GetInvPeriodList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM07000PeriodDTO>> GetInvPeriodListAsync()
        {
            var loEx = new R_Exception();
            List<LMM07000PeriodDTO> loResult = new List<LMM07000PeriodDTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM07000PeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000Universal.GetInvPeriodList),
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
    }
}
