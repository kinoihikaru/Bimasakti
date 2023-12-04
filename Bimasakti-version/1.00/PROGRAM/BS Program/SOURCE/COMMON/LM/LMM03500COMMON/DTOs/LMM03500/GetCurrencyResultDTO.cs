using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03500
{
    public class GetCurrencyResultDTO : R_APIResultBaseDTO
    {
        public List<GetCurrencyDTO> Data { get; set; }
    }
}
