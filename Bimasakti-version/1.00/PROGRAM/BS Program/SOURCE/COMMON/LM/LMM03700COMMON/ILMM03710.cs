using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace LMM03700COMMON
{
    public interface ILMM03710 : R_IServiceCRUDBase<LMM03710DTO>
    {
        IAsyncEnumerable<LMM03710DTO> GetTenantClassList();
        IAsyncEnumerable<LMM03711DTO> GetTenantCLassTenantList();
        IAsyncEnumerable<LMM03711DTO> GetAssignTenantCLassList();
        LMM03700Record<LMM03711AssignTenantDTO> AssignTenantClass(LMM03711AssignTenantDTO poEntity);
        LMM03700Record<LMM03711MoveTenantDTO> MoveTenantClass(LMM03711MoveTenantDTO poEntity);
    }
}
