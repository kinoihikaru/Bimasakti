using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class UploadTenantErrorDTO : UploadTenantExcelDTO
    {
        public string ErrorMessage { get; set; } = "";
    }
}
