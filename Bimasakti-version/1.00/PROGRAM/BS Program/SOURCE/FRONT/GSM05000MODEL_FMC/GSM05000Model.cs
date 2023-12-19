using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GSM05000COMMON_FMC;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using R_CommonFrontBackAPI;

namespace GSM05000MODEL_FMC
{
    public class GSM05000Model : R_BusinessObjectServiceClientBase<GSM05000DTO>, IGSM05000
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000";
        private const string DEFAULT_MODULE = "GS";

        public GSM05000Model(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM05000DTO>> GetTransactionCodeListStreamAsync()
        {
            var loEx = new R_Exception();
            List<GSM05000DTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM05000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.GetTransactionCodeListStream),
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

        public async Task<GSM05000ValidateDTO> ValidateTransactionCodeAsync(GSM05000ValidateDTO poEntity)
        {
            var loEx = new R_Exception();
            GSM05000ValidateDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000SingleResult<GSM05000ValidateDTO>, GSM05000ValidateDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000.ValidateTransactionCode),
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
        public IAsyncEnumerable<GSM05000DTO> GetTransactionCodeListStream()
        {
            throw new NotImplementedException();
        }

        public GSM05000SingleResult<GSM05000ValidateDTO> ValidateTransactionCode(GSM05000ValidateDTO poEntity)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}