using LMM03500COMMON.DTOs.LMM03502;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs
{
    public class SaveProfileAndTaxDTO
    {
        public LMM03502DTO Data { get; set; }
        public string ConductorMode { get; set; }
    }
}
