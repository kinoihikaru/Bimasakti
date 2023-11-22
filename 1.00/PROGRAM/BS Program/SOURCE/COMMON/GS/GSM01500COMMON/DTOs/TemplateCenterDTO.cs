using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class TemplateCenterDTO : R_APIResultBaseDTO
    {
        public byte[] FileBytes { get; set; }
    }
}
