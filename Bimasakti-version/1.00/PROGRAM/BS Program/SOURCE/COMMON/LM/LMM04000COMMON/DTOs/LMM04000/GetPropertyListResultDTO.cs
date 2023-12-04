using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04000
{
    public class GetPropertyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPropertyListDTO> Data { get; set; }
    }
}
