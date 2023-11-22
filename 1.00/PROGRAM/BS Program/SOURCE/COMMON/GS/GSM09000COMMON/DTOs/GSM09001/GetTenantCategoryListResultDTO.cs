using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09001
{
    public class GetTenantCategoryListResultDTO : R_APIResultBaseDTO
    {
        public List<GetTenantCategoryDTO> Data { get; set; }
    }
}
