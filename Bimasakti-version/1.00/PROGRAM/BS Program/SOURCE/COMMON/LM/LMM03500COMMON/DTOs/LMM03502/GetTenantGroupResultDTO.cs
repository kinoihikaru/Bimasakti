using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03502
{
    public class GetTenantGroupResultDTO : R_APIResultBaseDTO
    {
        public List<GetTenantGroupDTO> Data { get; set; }
    }
}
