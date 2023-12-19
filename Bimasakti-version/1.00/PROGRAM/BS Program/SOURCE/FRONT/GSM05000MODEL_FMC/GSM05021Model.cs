using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;
using GSM05000COMMON_FMC;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05021Model : R_BusinessObjectServiceClientBase<GSM05021DTO>, IGSM05021
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05021";
        private const string DEFAULT_MODULE = "GS";

        public GSM05021Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM05021DTO>> GetApprovalReplacementListStreamAsync(GSM05021DTO poParam)
        {
            var loEx = new R_Exception();
            List<GSM05021DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANSACTION_CODE, poParam.CTRANSACTION_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CDEPT_CODE, poParam.CDEPT_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CUSER_ID, poParam.CUSER_ID);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05021DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05021.GetApprovalReplacementListStream),
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
        public IAsyncEnumerable<GSM05021DTO> GetApprovalReplacementListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}