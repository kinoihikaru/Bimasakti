using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01020 : R_IServiceCRUDBase<LMM01020DTO>
    {
        IAsyncEnumerable<LMM01021DTO> GetRateWGList();
    }

}
