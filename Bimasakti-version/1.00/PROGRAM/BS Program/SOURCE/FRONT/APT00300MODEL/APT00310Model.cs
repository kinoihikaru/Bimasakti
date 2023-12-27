using APT00300COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APT00300MODEL
{
    public class APT00310Model : R_BusinessObjectServiceClientBase<APT00310DTO>, IAPT00310
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00310";
        private const string DEFAULT_MODULE = "AP";

        public APT00310Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APT00310LastCurrencyDTO> GetLastCurrencyRateAsync(APT00310LastCurrencyParameterDTO poParam)
        {
            var loEx = new R_Exception();
            APT00310LastCurrencyDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00310LastCurrencyDTO>, APT00310LastCurrencyParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00310.GetLastCurrencyRate),
                    poParam,
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

        public async Task<APT00311DTO> GetDebitNoteDTAsync(APT00311DTO poParam)
        {
            var loEx = new R_Exception();
            APT00311DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00311DTO>, APT00311DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00310.GetDebitNoteDT),
                    poParam,
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
        public async Task GenerateWHTaxDeducationDTAsync(APT00311DTO poParam)
        {
            var loEx = new R_Exception();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00311DTO>, APT00311DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00310.GenerateWHTaxDeducationDT),
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
        public async Task<List<APT00311DTO>> GetDebitNoteDTListStreamAsync(APT00311DTO poParam)
        {
            var loEx = new R_Exception();
            List<APT00311DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANS_CODE, poParam.CTRANS_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREF_NO, poParam.CREF_NO);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CREC_ID, poParam.CREC_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00311DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00310.GetDebitNoteDTListStream),
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
        public async Task<APT00310DTO> SaveSummaryDebitNoteAsync(APT00310DTO poParam)
        {
            var loEx = new R_Exception();
            APT00310DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00310DTO>, APT00310DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00310.SaveSummaryDebitNote),
                    poParam,
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
        public APT00300SingleResult<APT00310LastCurrencyDTO> GetLastCurrencyRate(APT00310LastCurrencyParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APT00311DTO> GetDebitNoteDTListStream()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00311DTO> GetDebitNoteDT(APT00311DTO poParam)
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00311DTO> GenerateWHTaxDeducationDT(APT00311DTO poParam)
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00310DTO> SaveSummaryDebitNote(APT00310DTO poParam)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
