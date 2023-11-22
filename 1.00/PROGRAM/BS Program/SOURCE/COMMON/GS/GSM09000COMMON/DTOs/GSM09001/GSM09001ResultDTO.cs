using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09001
{
    public class GSM09001ResultDTO : R_APIResultBaseDTO
    {
        public List<GSM09001DTO> Data { get; set; } 
    }
}
