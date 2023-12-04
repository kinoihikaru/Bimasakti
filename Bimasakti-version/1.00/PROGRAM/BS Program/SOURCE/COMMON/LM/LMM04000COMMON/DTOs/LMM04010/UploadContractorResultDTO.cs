using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class UploadContractorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadContractorDTO> Data { get; set; }
    }
}
