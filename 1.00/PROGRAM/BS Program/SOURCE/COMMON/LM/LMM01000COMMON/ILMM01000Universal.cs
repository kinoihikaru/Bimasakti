using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public interface ILMM01000Universal
    {
        LMM01000List<LMM01000UniversalDTO> GetChargesTypeList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetStatusList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetTaxExemptionCodeList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetWithholdingTaxTypeList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetUsageRateModeList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetRateTypeList(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01000UniversalDTO> GetAdminFeeTypeList(LMM01000UniversalDTO poParam);

    }

}
