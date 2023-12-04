using System.Collections.Generic;

namespace LMM06500COMMON
{
    public interface ILMM06500Universal
    {
        IAsyncEnumerable<LMM06500UniversalDTO> GetPositionList();
        IAsyncEnumerable<LMM06500UniversalDTO> GetGenderList();
        IAsyncEnumerable<LMM06500DTOInitial> GetProperty();
        LMM06500AllListUniversalDTO GetAllUniversalList();
    }

}
