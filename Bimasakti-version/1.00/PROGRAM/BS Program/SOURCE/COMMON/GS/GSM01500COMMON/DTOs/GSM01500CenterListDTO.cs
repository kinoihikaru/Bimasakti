using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class GSM01500CenterListDTO : R_APIResultBaseDTO
    {
        public List<GSM01500DTO> Data { get; set; } 
    }
}
