using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class UploadCenterErrorResultDTO : R_APIResultBaseDTO
    {
        public List<UploadCenterErrorDTO> Data { get; set; }
    }
}
