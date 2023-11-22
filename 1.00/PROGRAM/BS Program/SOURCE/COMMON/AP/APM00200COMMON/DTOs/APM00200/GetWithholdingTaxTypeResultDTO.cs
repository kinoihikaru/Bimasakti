using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class GetWithholdingTaxTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetWithholdingTaxTypeDTO> Data { get; set; }
    }
}
