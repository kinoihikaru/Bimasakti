using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01000Universal
    {
        IAsyncEnumerable<LMM01000UniversalDTO> GetChargesTypeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetStatusList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetTaxExemptionCodeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetWithholdingTaxTypeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetUsageRateModeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetRateTypeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetAdminFeeTypeList();
        IAsyncEnumerable<LMM01000UniversalDTO> GetAccrualMethodList();

    }

}
