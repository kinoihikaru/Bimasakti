using GSM09100COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM09100MODEL
{
    public class GSM09100Model : R_BusinessObjectServiceClientBase<GSM09100DTO>, IGSM09100
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM09100";
        private const string DEFAULT_MODULE = "GS";

        public GSM09100Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region Initial
        public GSM09100SingleResult<GSM09100InitialDTO> GetInitialProductCategory(GSM09100InitialDTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task<GSM09100InitialDTO> GetInitialProductCategoryAsync(GSM09100InitialDTO poParam)
        {
            var loEx = new R_Exception();
            GSM09100InitialDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM09100SingleResult<GSM09100InitialDTO>, GSM09100InitialDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09100.GetInitialProductCategory),
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

        #region Product Category
        public IAsyncEnumerable<GSM09100DTO> GetProductCategoryList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSM09100DTO>> GetProductCategoryListAsync(string poPropertyId)
        {
            var loEx = new R_Exception();
            List<GSM09100DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09100DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09100.GetProductCategoryList),
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
        public IAsyncEnumerable<GSM09100PropertyDTO> GetProperty()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSM09100PropertyDTO>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<GSM09100PropertyDTO> loResult = null;


            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09100PropertyDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09100.GetProperty),
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
