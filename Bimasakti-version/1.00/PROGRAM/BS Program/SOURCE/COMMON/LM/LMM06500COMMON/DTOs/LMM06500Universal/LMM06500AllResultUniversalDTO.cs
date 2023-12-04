using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM06500COMMON
{
    public class LMM06500AllListUniversalDTO : R_APIResultBaseDTO
    {
        public List<LMM06500DTOInitial> PropertyList { get; set; }
        public List<LMM06500UniversalDTO> PositionList { get; set; }
        public List<LMM06500UniversalDTO> GenderList { get; set; }
    }


}
