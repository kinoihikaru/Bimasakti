using CBM00100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Threading.Tasks;

namespace CBM00100MODEL
{
    public class CBM00100Model : R_BusinessObjectServiceClientBase<CBM00100DTO>, ICBM00100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlCB";
        private const string DEFAULT_ENDPOINT = "api/CBM00100";
        private const string DEFAULT_MODULE = "CB";

        public CBM00100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<CBM00100ValidateInitDTO> GetInitValidateAsync()
        {
            var loEx = new R_Exception();
            CBM00100ValidateInitDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBM00100SingleResult<CBM00100ValidateInitDTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBM00100.GetInitValidate),
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
        public async Task<CBM00100DTO> GetSystemParamCBAsync()
        {
            var loEx = new R_Exception();
            CBM00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBM00100SingleResult<CBM00100DTO>>(
                    _RequestServiceEndPoint,
                    nameof(ICBM00100.GetSystemParamCB),
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
        public async Task<CBM00100DTO> SaveSystemParamCBAsync(CBM00100SaveParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            CBM00100DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<CBM00100SingleResult<CBM00100DTO>, CBM00100SaveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(ICBM00100.SaveSystemParamCB),
                    poEntity,
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
        public CBM00100SingleResult<CBM00100ValidateInitDTO> GetInitValidate()
        {
            throw new NotImplementedException();
        }
        public CBM00100SingleResult<CBM00100DTO> GetSystemParamCB()
        {
            throw new NotImplementedException();
        }
        public CBM00100SingleResult<CBM00100DTO> SaveSystemParamCB(CBM00100SaveParameterDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
