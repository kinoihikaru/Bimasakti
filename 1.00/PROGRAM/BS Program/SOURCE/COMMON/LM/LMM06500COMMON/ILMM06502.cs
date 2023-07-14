using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public interface ILMM06502
    {
        LMM06500List<LMM06502DetailDTO> GetStaffMoveList(LMM06502DetailDTO poEntity);

        LMM06502DTO SaveNewMoveStaff(LMM06502DTO poEntity);
    }

}
