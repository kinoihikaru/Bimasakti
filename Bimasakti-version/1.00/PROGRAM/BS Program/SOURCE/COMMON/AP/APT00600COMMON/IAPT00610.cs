using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00600COMMON
{
    public interface IAPT00610 : R_IServiceCRUDBase<APT00610DTO>
    {
        IAsyncEnumerable<APT00610DTO> GetPurhcaseAdjustmentAllocStream();
    }
}
