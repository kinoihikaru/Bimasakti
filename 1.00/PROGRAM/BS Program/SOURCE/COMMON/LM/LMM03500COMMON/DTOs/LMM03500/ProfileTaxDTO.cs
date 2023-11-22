using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03500
{
    public class ProfileTaxDTO
    {
        public LMM03502DTO Profile { get; set; } = new LMM03502DTO();
        public LMM03503DTO TaxInfo { get; set; } = new LMM03503DTO();
    }
}
