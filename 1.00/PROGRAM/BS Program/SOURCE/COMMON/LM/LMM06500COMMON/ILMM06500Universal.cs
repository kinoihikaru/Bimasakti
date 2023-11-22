using System.Collections.Generic;

namespace LMM06500COMMON
{
    public interface ILMM06500Universal
    {
        IAsyncEnumerable<LMM06500UniversalDTO> GetPositionList(LMM06500UniversalDTO poParam);
        IAsyncEnumerable<LMM06500UniversalDTO> GetGenderList(LMM06500UniversalDTO poParam);
    }

}
