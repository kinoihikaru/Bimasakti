using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04030
{
    public class GetBankCodeResultDTO : R_APIResultBaseDTO
    {
        public List<GetBankCodeDTO> Data { get; set; } 
    }
}
