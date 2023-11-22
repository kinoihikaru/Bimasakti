using LMM07000COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM07000MODEL
{
    public class LMM07000Model: R_BusinessObjectServiceClientBase<LMM07000DTO>, ILMM07000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM07000";
        private const string DEFAULT_MODULE = "LM";

        public LMM07000Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region ListChargesDiscount
        public IAsyncEnumerable<LMM07000DTO> GetChargesDiscountList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<LMM07000DTO>> GetChargesDiscountListAsync(LMM07000DTO poParam)
        {
            var loEx = new R_Exception();
            List<LMM07000DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poParam.CCHARGES_TYPE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM07000DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000.GetChargesDiscountList),
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

        #region Property
        public IAsyncEnumerable<LMM07000DTOInitial> GetProperty()
        {
            throw new NotImplementedException();
        }

        public async Task<List<LMM07000DTOInitial>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<LMM07000DTOInitial> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<LMM07000DTOInitial>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000.GetProperty),
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

        #region ActiveChargesDiscount
        public LMM07000Record<LMM07000DTO> LMM07000ActiveInactive(LMM07000DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task LMM07000ActiveInactiveAsync(LMM07000DTO poParam)
        {
            var loEx = new R_Exception();
            LMM07000Record<LMM07000DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM07000Record<LMM07000DTO>, LMM07000DTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000.LMM07000ActiveInactive),
                    poParam,
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
        #endregion

        #region MaxInvoicePeriod
        public LMM07000Record<LMM07000PeriodDTO> GetMaxInvoicePeriodValue(LMM07000PeriodDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<LMM07000PeriodDTO> GetMaxInvoicePeriodValueAsync(LMM07000PeriodDTO poParam)
        {
            var loEx = new R_Exception();
            LMM07000PeriodDTO loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempRtn = await R_HTTPClientWrapper.R_APIRequestObject<LMM07000Record<LMM07000PeriodDTO>, LMM07000PeriodDTO>(
                    _RequestServiceEndPoint,
                    nameof(ILMM07000.GetMaxInvoicePeriodValue),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loRtn = loTempRtn.Data;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loRtn;
        }
        #endregion
    }
}
