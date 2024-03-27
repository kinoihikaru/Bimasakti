using GLB00600COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GLB00600MODEL
{
    public class GLB00600Model : R_BusinessObjectServiceClientBase<GLB00600DTO>, IGLB00600
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLB00600";
        private const string DEFAULT_MODULE = "GL";

        public GLB00600Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<GLB00600GSMTransactionCodeDTO> GetInitialGSMTransactionCodeAsync()
        {
            var loEx = new R_Exception();
            GLB00600GSMTransactionCodeDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB00600Record<GLB00600GSMTransactionCodeDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetInitialGSMTransactionCode),
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

        public async Task<GLB00600SuspenseAmountDTO> GetInitialSupenseAmountAsync(GLB00600SuspenseAmountDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600SuspenseAmountDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB00600Record<GLB00600SuspenseAmountDTO>, GLB00600SuspenseAmountDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetInitialSupenseAmount),
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

        public async Task<GLB00600InitialDTO> GetInitialVarAsync(GLB00600InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB00600Record<GLB00600InitialDTO>, GLB00600InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetInitialVar),
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

        public async Task GetResultClosingEntriesAsync(GLB00600DTO poParam)
        {
            var loEx = new R_Exception();
            GLB00600DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB00600Record<GLB00600InitialDTO>, GLB00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetResultClosingEntries),
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

        public async Task<GLB00600GLSystemParamDTO> GetSystemParamAsync()
        {
            var loEx = new R_Exception();
            GLB00600GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB00600Record<GLB00600GLSystemParamDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetSystemParam),
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

        public async Task<List<GLB00600DTO>> GetValidationClosingResultAsync()
        {
            var loEx = new R_Exception();
            List<GLB00600DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GLB00600DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB00600.GetValidationClosingResult),
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

        public GLB00600Record<GLB00600GSMTransactionCodeDTO> GetInitialGSMTransactionCode()
        {
            throw new NotImplementedException();
        }
        public GLB00600Record<GLB00600DTO> GetResultClosingEntries()
        {
            throw new NotImplementedException();
        }
        public IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult()
        {
            throw new NotImplementedException();
        }
        public GLB00600Record<GLB00600GLSystemParamDTO> GetSystemParam()
        {
            throw new NotImplementedException();
        }
        public GLB00600Record<GLB00600InitialDTO> GetInitialVar(GLB00600InitialDTO poParam)
        {
            throw new NotImplementedException();
        }
        public GLB00600Record<GLB00600SuspenseAmountDTO> GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam)
        {
            throw new NotImplementedException();
        }
    }
}
