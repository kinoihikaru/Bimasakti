using System.Collections.Generic;

namespace GLB00600COMMON
{
    public interface IGLB00600
    {
        GLB00600Record<GLB00600GLSystemParamDTO> GetSystemParam();
        GLB00600Record<GLB00600InitialDTO> GetInitialVar(GLB00600InitialDTO poParam);
        GLB00600Record<GLB00600SuspenseAmountDTO> GetInitialSupenseAmount(GLB00600SuspenseAmountDTO poParam);

        GLB00600Record<GLB00600GSMTransactionCodeDTO> GetInitialGSMTransactionCode();
        IAsyncEnumerable<GLB00600DTO> GetValidationClosingResult();
        GLB00600Record<GLB00600DTO> GetResultClosingEntries();

    }
}
