using LMM02500COMMON;
using R_APIClient;
using R_BlazorFrontEnd.Exceptions;
using R_BlazorFrontEnd;
using R_BusinessObjectFront;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LMM02500MODEL
{
    public class LMM02510Model : R_BusinessObjectServiceClientBase<LMM02510DTO>, ILMM02510
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM02510";
        private const string DEFAULT_MODULE = "LM";

        public LMM02510Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }

        #region Not Implement
        
        #endregion
    }
}
