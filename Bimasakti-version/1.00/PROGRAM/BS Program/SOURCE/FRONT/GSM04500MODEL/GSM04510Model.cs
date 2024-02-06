using GSM04500COMMON;
using R_APIClient;
using R_BlazorFrontEnd;
using R_BlazorFrontEnd.Exceptions;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GSM04500MODEL
{
    public class GSM04510Model : R_BusinessObjectServiceClientBase<GSM04510DTO>, IGSM04510
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04510";
        private const string DEFAULT_MODULE = "GS";

        public GSM04510Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM04510DTO>> GetJournalGroupGOAListAsync(GSM04510ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM04510DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, poEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_CODE, poEntity.CJRNGRP_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04510DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04510.GetJournalGroupGOAList),
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
        public IAsyncEnumerable<GSM04510DTO> GetJournalGroupGOAList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
