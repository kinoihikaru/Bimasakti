using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09300
{
    public class GSM09300ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09300DTO> Data { get; set; }
    }
}
