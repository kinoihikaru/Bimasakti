using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class GSM01500ResultDTO : R_APIResultBaseDTO
    {
        public GSM01500DTO Data { get; set; }
    }
}
