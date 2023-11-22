using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class UploadCenterResultDTO : R_APIResultBaseDTO
    {
        public List<UploadCenterDTO> Data { get; set; }
    }
}
