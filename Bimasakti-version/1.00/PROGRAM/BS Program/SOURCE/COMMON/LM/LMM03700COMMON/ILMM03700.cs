using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace LMM03700COMMON
{
    public interface ILMM03700 : R_IServiceCRUDBase<LMM03700DTO>
    {
        IAsyncEnumerable<LMM03700PropetyDTO> GetPropertyList();
        IAsyncEnumerable<LMM03700DTO> GetTenantClassGrpList();
    }
}
