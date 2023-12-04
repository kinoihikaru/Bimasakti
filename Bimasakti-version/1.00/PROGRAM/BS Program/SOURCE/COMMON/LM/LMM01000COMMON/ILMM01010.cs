using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01010 : R_IServiceCRUDBase<LMM01010DTO>
    {
        IAsyncEnumerable<LMM01011DTO> GetRateECList();

    }

}
