using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01050 : R_IServiceCRUDBase<LMM01050DTO>
    {
        IAsyncEnumerable<LMM01051DTO> GetRateOTList();
    }

}
