using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON.DTOs
{
    public class GSM07000ResultDTO : R_APIResultBaseDTO
    {
        public GSM07000DTO Data { get; set; }
    }
}
