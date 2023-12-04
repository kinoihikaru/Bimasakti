using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01000AllResultInit : R_APIResultBaseDTO
    {
        public List<LMM01000DTOPropety> PropertyList { get; set; }
        public List<LMM01000UniversalDTO> TaxExemptionCodeList { get; set; }
        public List<LMM01000UniversalDTO> WithholdingTaxTypeList { get; set; }
        public List<LMM01000UniversalDTO> AccrualMethodTypeList { get; set; }
    }

}
