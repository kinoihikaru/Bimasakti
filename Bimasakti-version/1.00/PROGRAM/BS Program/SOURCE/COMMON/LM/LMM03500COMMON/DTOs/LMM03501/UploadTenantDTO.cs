using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03501
{
    public class UploadTenantDTO : UploadTenantExcelDTO
    {
        public int NO { get; set; } = 0;
        public string CompanyId { get; set; } = "";
        public string PropertyId { get; set; } = "";
        public string Valid { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
