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
        private const string DEFAULT_MODULE = "GS";

        public GSM03000Model(
            string pcHttpClientName = DEFAULT_HTTP, 
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT, 
            bool plSendWithContext = true, 
            bool plSendWithToken = true) : 
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region GetOtherChargesList
        IAsyncEnumerable<GSM03000DTO> IGSM03000.GetOtherChargesList()
        {
            throw new NotImplementedException();
        }
        public async Task<GSM03000ListDTO> GetOtherChargesListAsync()
        {
            var loEx = new R_Exception();
            GSM03000ListDTO loResult = new GSM03000ListDTO();
            

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                var loTempResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM03000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM03000.GetOtherChargesList),
                    DEFAULT_MODULE,
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

        #region OtherChargesChangeStatus
        public GSM03000ActiveDTO GSM03000OtherChargesChangeStatus(GSM03000ActiveParameterDTO poParam)
        {
            throw new NotImplementedException();
        }

        public async Task GSM03000OtherChargesChangeStatusAsync(GSM03000ActiveParameterDTO poParam)
        {
            var loEx = new R_Exception();
            GSM03000ActiveDTO loRtn = new GSM03000ActiveDTO();

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;

                loRtn = await R_HTTPClientWrapper.R_APIRequestObject<GSM03000ActiveDTO, GSM03000ActiveParameterDTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM03000.GSM03000OtherChargesChangeStatus),
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
    }
}
