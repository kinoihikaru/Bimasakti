using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class GetCurrencyResultDTO : R_APIResultBaseDTO
    {
        public List<GetCurrencyDTO> Data { get; set; }
    }
}
