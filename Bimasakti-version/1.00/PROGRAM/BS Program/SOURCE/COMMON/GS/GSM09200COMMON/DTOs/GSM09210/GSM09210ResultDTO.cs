using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09210
{
    public class GSM09210ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09210DTO> Data { get; set; }
    }
}