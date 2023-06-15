using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public interface ILMM01520 : R_IServiceCRUDBase<LMM01520DTO>
    {
        LMM01500List<LMM01522DTO> GetAdditionalIdLookup();
    }

}
