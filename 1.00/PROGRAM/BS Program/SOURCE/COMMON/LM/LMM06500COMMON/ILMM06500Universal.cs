using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public interface ILMM06500Universal
    {
        LMM06500List<LMM06500UniversalDTO> GetPositionList(LMM06500UniversalDTO poParam);
        LMM06500List<LMM06500UniversalDTO> GetGenderList(LMM06500UniversalDTO poParam);
    }

}
