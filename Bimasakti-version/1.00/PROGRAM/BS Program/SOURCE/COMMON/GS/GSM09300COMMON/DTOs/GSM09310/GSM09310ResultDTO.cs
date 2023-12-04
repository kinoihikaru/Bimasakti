using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09310
{
    public class GSM09310ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09310DTO> Data { get; set; }
    }
}