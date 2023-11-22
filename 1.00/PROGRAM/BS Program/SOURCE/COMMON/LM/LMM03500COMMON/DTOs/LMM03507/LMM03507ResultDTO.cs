using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03507
{
    public class LMM03507ResultDTO : R_APIResultBaseDTO
    {
        public List<LMM03507DTO> Data { get; set; }
    }
}
