using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;
using GLM00400COMMON;
using R_CommonFrontBackAPI;

namespace GLM00400MODEL
{
    public class GLM00410Model : R_BusinessObjectServiceClientBase<GLM00410DTO>, IGLM00410
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLM00410";
        private const string DEFAULT_MODULE = "GL";

        public GLM00410Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GLM00400List<GLM00411DTO> GetAllocationAccountList(GLM00411DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400List<GLM00411DTO>> GetAllocationAccountListAsync(GLM00411DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00411DTO> loResult = new GLM00400List<GLM00411DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00411DTO>, GLM00411DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationAccountList),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00400List<GLM00415DTO> GetAllocationPeriodByTargetCenterList(GLM00415DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400List<GLM00415DTO>> GetAllocationPeriodByTargetCenterListAsync(GLM00415DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00415DTO> loResult = new GLM00400List<GLM00415DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00415DTO>, GLM00415DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationPeriodByTargetCenterList),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00400List<GLM00414DTO> GetAllocationPeriodList(GLM00414DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400List<GLM00414DTO>> GetAllocationPeriodListAsync(GLM00414DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00414DTO> loResult = new GLM00400List<GLM00414DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00414DTO>, GLM00414DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationPeriodList),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00400List<GLM00413DTO> GetAllocationTargetCenterByPeriodList(GLM00413DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GLM00400List<GLM00413DTO>> GetAllocationTargetCenterByPeriodListAsync(GLM00413DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00413DTO> loResult = new GLM00400List<GLM00413DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00413DTO>, GLM00413DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationTargetCenterByPeriodList),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }

        public GLM00400List<GLM00412DTO> GetAllocationTargetCenterList(GLM00412DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400List<GLM00412DTO>> GetAllocationTargetCenterListAsync(GLM00412DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00412DTO> loResult = new GLM00400List<GLM00412DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00412DTO>, GLM00412DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00410.GetAllocationTargetCenterList),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);

                loResult = loTempResult;
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
