using GSM03000Common;
using GSM03000Common.DTOs;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<List<GSM03000DTO>> GetOtherChargesListAsync(string poPropertyId, string poChargesType)
        {
            var loEx = new R_Exception();
            List<GSM03000DTO> loResult = null;


            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poPropertyId);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CCHARGES_TYPE, poChargesType);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM03000DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM03000.GetOtherChargesList),
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

        #region GetPropertyList

        public IAsyncEnumerable<GSM03000PropertyDTO> GetProperty()
        {
            throw new NotImplementedException();
        }
        public async Task<List<GSM03000PropertyDTO>> GetPropertyAsync()
        {
            var loEx = new R_Exception();
            List<GSM03000PropertyDTO> loResult = null;

            try
            {
                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM03000PropertyDTO>(
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
