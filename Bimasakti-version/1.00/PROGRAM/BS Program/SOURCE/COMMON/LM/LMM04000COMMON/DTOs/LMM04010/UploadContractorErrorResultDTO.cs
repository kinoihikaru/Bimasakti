using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class UploadContractorErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadContractorErrorDTO> Data { get; set; }
    }
}
