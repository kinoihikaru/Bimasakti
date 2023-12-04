using System.Collections.Generic;

namespace GLB00600COMMON
{
    public interface IGLB00600
    {
        GLB00600GLSystemParamDTO GetSystemParam();
        GLB00600InitialDTO GetInitialVar(GLB00600InitialDTO poParam);
        GLB00600SuspenseAmountDTO GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam);
        GLB00600GSMTransactionCodeDTO GetInitialGSMTransactionCode(GLB00600GSMTransactionCodeDTO poParam);
        IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult(GLB00600DTO poParam);
        GLB00600DTO GetResultClosingEntries(GLB00600DTO poParam);
    }
}
