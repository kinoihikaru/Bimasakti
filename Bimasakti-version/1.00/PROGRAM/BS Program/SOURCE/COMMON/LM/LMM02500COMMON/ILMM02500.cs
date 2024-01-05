using System.Collections.Generic;

namespace LMM02500COMMON
{
    public interface ILMM02500
    {
        IAsyncEnumerable<LMM02500DTO> GetTenantGrpist();
        IAsyncEnumerable<LMM02500TenantDTO> GetTenantList();
    }

}
