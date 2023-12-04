using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class GetContractorCategoryResultDTO : R_APIResultBaseDTO
    {
        public List<GetContractorCategoryDTO> Data { get; set; }
    }
}
