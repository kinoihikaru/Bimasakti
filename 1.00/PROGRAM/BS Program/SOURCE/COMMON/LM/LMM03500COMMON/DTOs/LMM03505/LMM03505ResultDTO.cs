using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03505
{
    public class LMM03505ResultDTO : R_APIResultBaseDTO
    {
        public List<LMM03505DTO> Data { get; set; }
    }
}
