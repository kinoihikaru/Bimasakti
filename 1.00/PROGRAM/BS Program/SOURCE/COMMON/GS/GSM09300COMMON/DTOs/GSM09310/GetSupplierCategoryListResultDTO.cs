using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09310
{
    public class GetSupplierCategoryListResultDTO : R_APIResultBaseDTO
    {
        public List<GetSupplierCategoryDTO> Data { get; set; }
    }
}
