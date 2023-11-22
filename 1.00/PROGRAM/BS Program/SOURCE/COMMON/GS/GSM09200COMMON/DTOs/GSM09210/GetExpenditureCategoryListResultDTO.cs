using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09210
{
    public class GetExpenditureCategoryListResultDTO : R_APIResultBaseDTO
    {
        public List<GetExpenditureCategoryDTO> Data { get; set; }
    }
}
