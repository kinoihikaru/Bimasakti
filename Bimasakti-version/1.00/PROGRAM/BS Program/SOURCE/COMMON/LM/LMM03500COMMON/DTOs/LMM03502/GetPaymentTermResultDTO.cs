using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03502
{
    public class GetPaymentTermResultDTO : R_APIResultBaseDTO
    {
        public List<GetPaymentTermDTO> Data { get; set; }
    }
}
