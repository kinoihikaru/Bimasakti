using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class UploadContractorErrorDTO : UploadContractorExcelDTO
    {
        public string ErrorMessage { get; set; } = "";
    }
}
