using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM06500COMMON
{
    public class LMM06502DTO : R_APIResultBaseDTO
    {
        public LMM06502HeaderDTO Header { get; set; }
        public List<LMM06502DetailDTO> Detail { get; set; }
    }


}
