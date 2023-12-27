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
    public class APT00300UniversalModel : R_BusinessObjectServiceClientBase<APT00300DTO>, IAPT00300Universal
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APT00300Universal";
        private const string DEFAULT_MODULE = "AP";

        public APT00300UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<APT00300ALLInitialTab1Result> GetAllInitialVarTab1Async()
        {
            var loEx = new R_Exception();
            APT00300ALLInitialTab1Result loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300ALLInitialTab1Result>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetAllInitialProcessTab1),
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
        public async Task<APT00300PeriodYearRangeDTO> GetInitialGSMPeriodWithParameterAsync(APT00300PeriodYearRangeDTO poParam)
        {
            var loEx = new R_Exception();
            APT00300PeriodYearRangeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300PeriodYearRangeDTO>, APT00300PeriodYearRangeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialGSMPeriodWithParameter),
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
        public async Task<APT00300ALLInitialTab2Result> GetAllInitialVarTab2Async()
        {
            var loEx = new R_Exception();
            APT00300ALLInitialTab2Result loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300ALLInitialTab2Result>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetAllInitialProcessTab2),
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
        public async Task<APT00300APSystemParamDTO> GetInitialAPSystemParamAsync()
        {
            var loEx = new R_Exception();
            APT00300APSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300APSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialAPSystemParam),
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
        public async Task<APT00300GSCompanyInfoDTO> GetInitialGSMCompanyInfoAsync()
        {
            var loEx = new R_Exception();
            APT00300GSCompanyInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300GSCompanyInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialGSMCompanyInfo),
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
        public async Task<APT00300GLSystemParamDTO> GetInitialGLSystemParamAsync()
        {
            var loEx = new R_Exception();
            APT00300GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialGLSystemParam),
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
        public async Task<APT00300GSTransCodeInfoDTO> GetInitialGSMTransCodeAsync()
        {
            var loEx = new R_Exception();
            APT00300GSTransCodeInfoDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<APT00300SingleResult<APT00300GSTransCodeInfoDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialGSMTransCode),
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
        public async Task<List<APT00300PropertyDTO>> GetInitialPropertyListStreamAsync()
        {
            var loEx = new R_Exception();
            List<APT00300PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00300PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialPropertyListStream),
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
        public async Task<List<APT00300CurrencyDTO>> GetInitialCurrencyListStreamAsync()
        {
            var loEx = new R_Exception();
            List<APT00300CurrencyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00300CurrencyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialCurrencyListStream),
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
        public async Task<List<APT00300GSBCodeDTO>> GetInitialGSBCodeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<APT00300GSBCodeDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APT00300GSBCodeDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPT00300Universal.GetInitialGSBCodeListStream),
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
        public APT00300SingleResult<APT00300ALLInitialTab1Result> GetAllInitialProcessTab1()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300ALLInitialTab2Result> GetAllInitialProcessTab2()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300PeriodYearRangeDTO> GetInitialGSMPeriodWithParameter(APT00300PeriodYearRangeDTO poEntity)
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300APSystemParamDTO> GetInitialAPSystemParam()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300GSCompanyInfoDTO> GetInitialGSMCompanyInfo()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300GLSystemParamDTO> GetInitialGLSystemParam()
        {
            throw new NotImplementedException();
        }

        public APT00300SingleResult<APT00300GSTransCodeInfoDTO> GetInitialGSMTransCode()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APT00300PropertyDTO> GetInitialPropertyListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APT00300CurrencyDTO> GetInitialCurrencyListStream()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<APT00300GSBCodeDTO> GetInitialGSBCodeListStream()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
