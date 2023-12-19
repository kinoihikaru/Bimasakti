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
    public class GSM05000UniversalModel : R_BusinessObjectServiceClientBase<GSM05000UniversalDTO>, IGSM05000Universal
    {
        private const string DEFAULT_HTTP_NAME = "R_DefaultServiceUrl";
        private const string DEFAULT_SERVICEPOINT_NAME = "api/GSM05000Universal";
        private const string DEFAULT_MODULE = "GS";

        public GSM05000UniversalModel(
            string pcHttpClientName = DEFAULT_HTTP_NAME,
            string pcRequestServiceEndPoint = DEFAULT_SERVICEPOINT_NAME,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM05000UniversalDTO>> GetRefNoDelimiterListAsync()
        {
            var loEx = new R_Exception();
            List<GSM05000UniversalDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = DEFAULT_HTTP_NAME;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM05000ListResult<GSM05000UniversalDTO>>(
                    _RequestServiceEndPoint,
                    nameof(IGSM05000Universal.GetRefNoDelimiterList),
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
        public GSM05000ListResult<GSM05000UniversalDTO> GetRefNoDelimiterList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}