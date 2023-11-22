using System.Collections.Generic;

namespace LMM06500COMMON
{
    public interface ILMM06502
    {
        IAsyncEnumerable<LMM06502DetailDTO> GetStaffMoveList();

        LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity);
    }

}
