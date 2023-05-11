using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GSM03000Common;
using GSM03000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;

namespace GSM03000MODEL
{
    public class GSM03000Model : R_BusinessObjectServiceClientBase<GSM03000DTO>, IGSM03000
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM03000";
        public GSM03000Model(
            string pcHttpClientName = DEFAULT_HTTP, 
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, plSendWithContext, plSendWithToken)
        {
        }

        #region GetOtherChargesList
        IAsyncEnumerable<GSM03000DTO> IGSM03000.GetOtherChargesList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM03000ListDTO> GetOtherChargesListAsync(string pcID, string PcType)
        {
            var loEx = new R_Exception();
            GSM03000ListDTO loResult = new GSM03000ListDTO();
            

            try
            {
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, pcID);
                R_BlazorFrontEnd.R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, PcType);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM03000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM03000.GetOtherChargesList),
                    _SendWithContext,
                    _SendWithToken);

                loResult.Data = loTempResult;
            }
            catch (Exception ex)
            {
                loEx.Add(ex);
            }

            loEx.ThrowExceptionIfErrors();

            return loResult;
        }
        #endregion

        #region GetPropertyList

        public GSM03000PropertyListDTO GetProperty()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM03000PropertyListDTO> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            GSM03000PropertyListDTO loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestObject<GSM03000PropertyListDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM03000.GetProperty),
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
