using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using System.Data;
using GLM00400COMMON;

namespace GLM00400MODEL
{
    public class GLM00400Model : R_BusinessObjectServiceClientBase<GLM00400DTO>, IGLM00400
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlGL";
        private const string DEFAULT_ENDPOINT = "api/GLM00400";
        private const string DEFAULT_MODULE = "GL";

        public GLM00400Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public GLM00400List<GLM00400DTO> GetAllocationJournalHDList(GLM00400DTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400List<GLM00400DTO>> GetAllocationJournalHDListAsync(GLM00400DTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400List<GLM00400DTO> loResult = new GLM00400List<GLM00400DTO>();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400List<GLM00400DTO>, GLM00400DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00400.GetAllocationJournalHDList),
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

        public GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GLM00400InitialDTO> GetInitialVarAsync(GLM00400InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400InitialDTO loResult = new GLM00400InitialDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400InitialDTO, GLM00400InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00400.GetInitialVar),
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

        public GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GLM00400GLSystemParamDTO> GetSystemParamAsync(GLM00400GLSystemParamDTO poParam)
        {
            var loEx = new R_Exception();
            GLM00400GLSystemParamDTO loResult = new GLM00400GLSystemParamDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400GLSystemParamDTO, GLM00400GLSystemParamDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGLM00400.GetSystemParam),
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
