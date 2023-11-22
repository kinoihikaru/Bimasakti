using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03500
{
    public class ProfileTaxResultDTO : R_APIResultBaseDTO
    {
        public LMM03502DTO Profile { get; set; }
        public LMM03503DTO TaxInfo { get; set; }
    }
}
