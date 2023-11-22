using LMM04000COMMON.DTOs.LMM04020;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class ProfileTaxDTO
    {
        public LMM04010DTO Profile { get; set; } = new LMM04010DTO();
        public LMM04020DTO TaxInfo { get; set; } = new LMM04020DTO();
    }
}
