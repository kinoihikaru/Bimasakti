using LMM04000COMMON.DTOs.LMM04020;
using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class ProfileTaxResultDTO : R_APIResultBaseDTO
    {
        public LMM04010DTO Profile { get; set; }
        public LMM04020DTO TaxInfo { get; set; }
    }
}
