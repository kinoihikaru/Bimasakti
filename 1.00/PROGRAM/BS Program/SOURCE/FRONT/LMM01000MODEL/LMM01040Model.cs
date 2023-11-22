using LMM01000COMMON;
using R_BusinessObjectFront;

namespace LMM01000MODEL
{
    public class LMM01040Model : R_BusinessObjectServiceClientBase<LMM01040DTO>, ILMM01040
    {
        private const string DEFAULT_HTTP = "R_DefaultServiceUrlLM";
        private const string DEFAULT_ENDPOINT = "api/LMM01040";
        private const string DEFAULT_MODULE = "LM";

        public LMM01040Model(
            string pcHttpClientName = DEFAULT_HTTP,
            string pcRequestServiceEndPoint = DEFAULT_ENDPOINT,
            bool plSendWithContext = true,
            bool plSendWithToken = true) :
            base(pcHttpClientName, pcRequestServiceEndPoint, DEFAULT_MODULE, plSendWithContext, plSendWithToken)
        {
        }
    }
}
