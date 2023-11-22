using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03505
{
    public class GetBankCodeResultDTO : R_APIResultBaseDTO
    {
        public List<GetBankCodeDTO> Data { get; set; } 
    }
}
