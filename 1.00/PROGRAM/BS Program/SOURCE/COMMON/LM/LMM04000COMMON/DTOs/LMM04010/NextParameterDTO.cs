using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class NextParameterDTO
    {
        public LMM04010DTO ProfileData { get; set; }
        public TabParameterDTO TabParameter { get; set; }
        public string ConductorMode { get; set; }
    }
}
