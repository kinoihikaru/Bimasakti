using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03503
{
    public class GetLMM03503ListResultDTO : R_APIResultBaseDTO
    {
        public List<GetLMM03503ListDTO> Data { get; set; }
    }
}
