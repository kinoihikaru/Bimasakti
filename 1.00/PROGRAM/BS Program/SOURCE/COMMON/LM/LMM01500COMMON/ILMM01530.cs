using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public interface ILMM01530 : R_IServiceCRUDBase<LMM01530DTO>
    {
        LMM01500List<LMM01530DTO> GetAllOtherChargerList();
        LMM01500List<LMM01531DTO> GetOtherChargesLookup(LMM01531DTO poParam);
    }

}
