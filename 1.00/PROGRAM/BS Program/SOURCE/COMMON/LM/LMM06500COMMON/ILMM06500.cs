using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM06500COMMON
{
    public interface ILMM06500 : R_IServiceCRUDBase<LMM06500DTO>
    {
        IAsyncEnumerable<LMM06500DTOInitial> GetProperty();
        IAsyncEnumerable<LMM06500DTO> GetStaffList();
        LMM06500DTO LMM06500ActiveInactive(LMM06500DTO poParam);
    }

}
