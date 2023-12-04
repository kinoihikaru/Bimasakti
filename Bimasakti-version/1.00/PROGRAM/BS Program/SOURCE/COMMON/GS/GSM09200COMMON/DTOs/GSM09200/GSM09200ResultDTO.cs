using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09200
{
    public class GSM09200ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09200DTO> Data { get; set; }
    }
}
