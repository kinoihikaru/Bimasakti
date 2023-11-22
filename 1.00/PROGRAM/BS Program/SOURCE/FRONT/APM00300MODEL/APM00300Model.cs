using APM00300COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APM00300MODEL
{
    public class APM00300Model : R_BusinessObjectServiceClientBase<APM00300DTO>, IAPM00300
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APM00300";
        private const string DEFAULT_MODULE = "AP";

        public APM00300Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        #region Get Supplier List
        public IAsyncEnumerable<APM00300DTO> GetAPSearchSupplierList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APM00300DTO>> GetAPSearchSupplierListAsync(APM00300DTO poParam)
        {
            var loEx = new R_Exception();
            List<APM00300DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CLOB_CODE, poParam.CLOB_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSEARCH_TEXT, poParam.CSEARCH_TEXT);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00300.GetAPSearchSupplierList),
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

        #region Initial
        public GLM00400SingleResult<APM00300InitialDTO> GetInitial()
        {
            throw new NotImplementedException();
        }

        public async Task<GLM00400SingleResult<APM00300InitialDTO>> GetInitialVarAsync()
        {
            var loEx = new R_Exception();
            GLM00400SingleResult<APM00300InitialDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400SingleResult<APM00300InitialDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00300.GetInitial),
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

        #region Property List
        public IAsyncEnumerable<APM00300PropertyDTO> GetGSPropertyList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<APM00300PropertyDTO>> GetGSPropertyListAsync()
        {
            var loEx = new R_Exception();
            List<APM00300PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00300.GetGSPropertyList),
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

        #region LOB
        public IAsyncEnumerable<APM00300LOBDTO> GetLOBList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<APM00300LOBDTO>> GetLOBListAsync()
        {
            var loEx = new R_Exception();
            List<APM00300LOBDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00300LOBDTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00300.GetLOBList),
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
