using System.Collections.Generic;

namespace LMM02500COMMON
{
    public interface ILMM02500Universal
    {
        IAsyncEnumerable<LMM02500PropertyDTO> GetPropertyList();
        IAsyncEnumerable<LMM02500UniversalDTO> GetTaxTypeList();
        IAsyncEnumerable<LMM02500UniversalDTO> GetIDTypeList();
        IAsyncEnumerable<LMM02500UniversalDTO> GetTaxCodeList();
        LMM02500Record<LMM02510AllUniversalDataDTO> GetALlUniversalData();
    }

}
