using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00500COMMON
{
    public interface IAPT00510 : R_IServiceCRUDBase<APT00510DTO>
    {
        IAsyncEnumerable<APT00510DTO> GetPurhcaseAdjustmentAllocStream();
    }
}
