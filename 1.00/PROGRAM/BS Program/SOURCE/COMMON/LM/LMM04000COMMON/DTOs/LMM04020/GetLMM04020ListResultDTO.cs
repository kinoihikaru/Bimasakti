using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04020
{
    public class GetLMM04020ListResultDTO : R_APIResultBaseDTO
    {
        public List<GetLMM04020ListDTO> Data { get; set; }
    }
}
