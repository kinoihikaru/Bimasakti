using System;
using System.Collections.Generic;

namespace LMM02500COMMON
{
    public class LMM02500MoveTenantDTO
    {
        public LMM02500ParameterMoveTenantDTO Header { get; set; }
        public List<LMM02500TenantDTO> Detail { get; set; }
    }


}
