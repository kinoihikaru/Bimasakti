using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public interface ILMM01050 : R_IServiceCRUDBase<LMM01050DTO>
    {
        LMM01000List<LMM01051DTO> GetRateOTList(LMM01051DTO poParam);
    }

}
