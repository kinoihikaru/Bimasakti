using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09000
{
    public class GSM09000ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09000DTO> Data { get; set; }
    }
}
