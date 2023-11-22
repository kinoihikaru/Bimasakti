using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class GetPaymentTermResultDTO : R_APIResultBaseDTO
    {
        public List<GetPaymentTermDTO> Data { get; set; }
    }
}
