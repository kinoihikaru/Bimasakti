using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class UploadTenantErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadTenantErrorDTO> Data { get; set; }
    }
}
