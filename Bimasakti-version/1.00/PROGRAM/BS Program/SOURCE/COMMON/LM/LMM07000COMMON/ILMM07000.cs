using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM07000COMMON
{
    public interface ILMM07000 : R_IServiceCRUDBase<LMM07000DTO>
    {
        IAsyncEnumerable<LMM07000DTOInitial> GetProperty();
        IAsyncEnumerable<LMM07000DTO> GetChargesDiscountList();
        LMM07000Record<LMM07000DTO> LMM07000ActiveInactive(LMM07000DTO poParam);
        LMM07000Record<LMM07000PeriodDTO> GetMaxInvoicePeriodValue(LMM07000PeriodDTO poParam);
    }

}
