using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04030
{
    public class LMM04030ResultDTO : R_APIResultBaseDTO
    {
        public List<LMM04030DTO> Data { get; set; }
    }
}
