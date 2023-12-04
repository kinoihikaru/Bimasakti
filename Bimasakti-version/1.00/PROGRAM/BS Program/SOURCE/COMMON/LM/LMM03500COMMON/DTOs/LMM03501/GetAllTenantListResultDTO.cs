using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class GetAllTenantListResultDTO : R_APIResultBaseDTO
    {
        public List<GetAllTenantListDTO> Data { get; set; }
    }
}
