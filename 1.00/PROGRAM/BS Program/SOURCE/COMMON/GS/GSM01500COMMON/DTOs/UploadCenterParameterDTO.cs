using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class UploadCenterParameterDTO
    {
        public List<UploadCenterDTO> Data { get; set; } = new List<UploadCenterDTO>();
    }
}
