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
    public class GSM05010Model : R_BusinessObjectServiceClientBase<GSM05010DTO>, IGSM05010
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05010";
        private const string DEFAULT_MODULE = "GS";

        public GSM05010Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }


        public async Task<List<GSM05010DTO>> GetHeaderTransCodeNumberListStreamAsync(GSM05010DTO poParam)
        {
            var loEx = new R_Exception();
            List<GSM05010DTO> loResult = null;

            try
            {
                //Set Context
                R_FrontContext.R_SetStreamingContext(ContextConstant.CTRANSACTION_CODE, poParam.CTRANSACTION_CODE);

                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05010DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05010.GetHeaderTransCodeNumberListStream),
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

        public async Task<GSM05011DTO> GetHeaderTransCodeNumberAsync(GSM05011DTO poEntity)
        {
            var loEx = new R_Exception();
            GSM05011DTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05011DTO>, GSM05011DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05010.GetHeaderTransCodeNumber),
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
        public GSM05000SingleResult<GSM05011DTO> GetHeaderTransCodeNumber(GSM05011DTO poEntity)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<GSM05010DTO> GetHeaderTransCodeNumberListStream()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}