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
    public class GSM04511Model : R_BusinessObjectServiceClientBase<GSM04511DTO>, IGSM04511
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrl";
        private const string DEFAULT_ENDPOINT = "api/GSM04511";
        private const string DEFAULT_MODULE = "GS";

        public GSM04511Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        public async Task<List<GSM04511DTO>> GetJournalGroupGOAByDeptListAsync(GSM04511ParameterDTO poEntity)
        {
            var loEx = new R_Exception();
            List<GSM04511DTO> loResult = null;

            try
            {
                R_FrontContext.R_SetStreamingContext(ContextConstant.CPROPERTY_ID, poEntity.CPROPERTY_ID);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_TYPE, poEntity.CJRNGRP_TYPE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CJRNGRP_CODE, poEntity.CJRNGRP_CODE);
                R_FrontContext.R_SetStreamingContext(ContextConstant.CGOA_CODE, string.IsNullOrWhiteSpace(poEntity.CGOA_CODE) ? "" : poEntity.CGOA_CODE);

                R_HTTPClientWrapper.httpClientName = _HttpClientName;
                loResult = await R_HTTPClientWrapper.R_APIRequestStreamingObject<GSM04511DTO>(
                    _RequestServiceEndPoint,
                    nameof(IGSM04511.GetJournalGroupGOAByDeptList),
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
        public IAsyncEnumerable<GSM04511DTO> GetJournalGroupGOAByDeptList()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
