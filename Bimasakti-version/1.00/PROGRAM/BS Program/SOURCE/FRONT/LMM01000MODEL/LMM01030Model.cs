using LMM01000COMMON;
using R_BusinessObjectFront;

namespace LMM01000MODEL
{
    public class LMM01030Model : R_BusinessObjectServiceClientBase<LMM01030DTO>, ILMM01030
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01030";
        private const string DEFAULT_MODULE = "LM";

        public LMM01030Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
    }
}
