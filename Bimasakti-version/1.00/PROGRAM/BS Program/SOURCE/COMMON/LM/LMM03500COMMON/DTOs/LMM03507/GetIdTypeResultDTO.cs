using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03507
{
    public class GetIdTypeResultDTO : R_APIResultBaseDTO
    {
        public List<GetIdTypeDTO> Data { get; set; }
    }
}
