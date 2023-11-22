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
    public class GSM09110Model : R_BusinessObjectServiceClientBase<GSM09110DTO>, IGSM09110
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM09110";
        private const string DEFAULT_MODULE = "GS";

        public GSM09110Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
        #region Move Product
        public GSM09100SingleResult<GSM09120DTO> MoveProduct(GSM09120DTO poParam)
        {
            throw new NotImplementedException();
        }
        public async Task MoveProductAsync(GSM09120DTO poParam)
        {
            var loEx = new R_Exception();
            GSM09100SingleResult<GSM09120DTO> loRtn = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM09100SingleResult<GSM09120DTO>, GSM09120DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09110.MoveProduct),
                    poParam,
                    DEFAULT_MODULE,
                    _SendWithContext,
                    _SendWithToken);
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

        EndBlock:
            loEx.ThrowExceptionIfErrors();
        }
        #endregion

        #region Product List
        public IAsyncEnumerable<GSM09110DTO> GetProductList()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSM09110DTO>> GetProductListAsync(GSM09110DTO poParam)
        {
            var loEx = new R_Exception();
            List<GSM09110DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poParam.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCATEGORY_ID, poParam.CCATEGORY_ID);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM09110DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM09110.GetProductList),
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
