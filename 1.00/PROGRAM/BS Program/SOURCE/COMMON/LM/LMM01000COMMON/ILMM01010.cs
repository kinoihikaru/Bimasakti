using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public interface ILMM01010 : R_IServiceCRUDBase<LMM01010DTO>
    {
        LMM01000List<LMM01011DTO> GetRateECList(LMM01011DTO poParam);
    }

}
