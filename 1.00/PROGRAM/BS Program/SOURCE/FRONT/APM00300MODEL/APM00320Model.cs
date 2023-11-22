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
    public class APM00320Model : R_BusinessObjectServiceClientBase<APM00320DTO>, IAPM00320
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlAP";
        private const string DEFAULT_ENDPOINT = "api/APM00320";
        private const string DEFAULT_MODULE = "AP";

        public APM00320Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region Supplier Info
        public GLM00400SingleResult<APM00320DTO> GetSupplierInfo(APM00320DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<APM00320DTO> GetSupplierInfoAsync(APM00320DTO poParam)
        {
            var loEx = new R_Exception();
            APM00320DTO loResult = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400SingleResult<APM00320DTO>, APM00320DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00320.GetSupplierInfo),
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
        #endregion

        #region Supplier Sequence List
        public IAsyncEnumerable<APM00321DTO> GetSupplierSeqList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<APM00321DTO>> GetSupplierSeqListAsync(string poRecSupplierID)
        {
            var loEx = new R_Exception();
            List<APM00321DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CSUPPLIER_REC_ID, poRecSupplierID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<APM00321DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00320.GetSupplierSeqList),
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

        #region Save Supplier Info
        public GLM00400SingleResult<APM00320DTO> SaveSupplierInfo(APM00320DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<APM00320DTO> SaveSupplierInfoAsync(APM00320DTO poParam)
        {
            var loEx = new R_Exception();
            APM00320DTO loResult = null;

            try
            {
                //Set Context
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GLM00400SingleResult<APM00320DTO>, APM00320DTO>(
                    _RequestServiceEndPoint,
                    nameof(IAPM00320.SaveSupplierInfo),
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
        #endregion
    }
}
