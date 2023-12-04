using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class LMM03501ResultDTO : R_APIResultBaseDTO
    {
        public List<LMM03501DTO> Data { get; set; }
    }
}
