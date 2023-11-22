using System.Collections.Generic;

namespace LMM07000COMMON
{
    public interface ILMM07000Universal
    {
        IAsyncEnumerable<LMM07000DTOUniversal> GetChargesTypeList();
        IAsyncEnumerable<LMM07000DTOUniversal> GetDiscountTypeList();
        IAsyncEnumerable<LMM07000PeriodDTO> GetInvPeriodList();
    }

}
