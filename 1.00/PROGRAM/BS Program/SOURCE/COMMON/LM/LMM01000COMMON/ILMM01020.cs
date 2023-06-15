using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public interface ILMM01020 : R_IServiceCRUDBase<LMM01020DTO>
    {
        LMM01000List<LMM01021DTO> GetRateWGList(LMM01021DTO poParam);
    }

}
