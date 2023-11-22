using System.Collections.Generic;

namespace GLR00200COMMON
{
    public interface IGLR00200
    {
        GLR00200InitialDTO GetInitialVar();
        GLR00200GLSystemParamDTO GetSystemParam();
        IAsyncEnumerable<GLR00200UniversalDTO> GetPrintMethodList();
        IAsyncEnumerable<GLR00200PeriodDTO> GetPriodList();
        GLR00200Result<GLR00200GetMinMaxAccount> GetMinMaxAccount();

    }

}
