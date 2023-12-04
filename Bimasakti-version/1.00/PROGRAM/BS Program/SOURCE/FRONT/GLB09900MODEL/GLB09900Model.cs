using GLB09900COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace GLB09900MODEL
{
    public class GLB09900Model : R_BusinessObjectServiceClientBase<GLB09900DTO>, IGLB09900
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLB09900";
        private const string DEFAULT_MODULE = "GL";

        public GLB09900Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GLB09900InitialDTO GetInitialVar()
        {
            throw new NotImplementedException();
        }

        public async Task<GLB09900InitialDTO> GetInitialVarAsync()
        {
            var loEx = new R_Exception();
            GLB09900InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB09900InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB09900.GetInitialVar),
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

        public GLB09900GLSystemParamDTO GetSystemParam()
        {
            throw new NotImplementedException();
        }

        public async Task<GLB09900GLSystemParamDTO> GetSystemParamAsync()
        {
            var loEx = new R_Exception();
            GLB09900GLSystemParamDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB09900GLSystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB09900.GetSystemParam),
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

        public GLB09900DTO GetResultCloseGlPeriod(GLB09900DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLB09900DTO> GetResultCloseGlPeriodAsync(GLB09900DTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB09900DTO, GLB09900DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB09900.GetResultCloseGlPeriod),
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

            return loResult;
        }

        public GLB09900ValidateDTO GetValidateResultCloseGlPeriod(GLB09900ValidateDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GLB09900ValidateDTO> GetValidateResultCloseGlPeriodAsync(GLB09900ValidateDTO poParam)
        {
            var loEx = new R_Exception();
            GLB09900ValidateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLB09900ValidateDTO, GLB09900ValidateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLB09900.GetValidateResultCloseGlPeriod),
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

            return loResult;
        }
    }
}
