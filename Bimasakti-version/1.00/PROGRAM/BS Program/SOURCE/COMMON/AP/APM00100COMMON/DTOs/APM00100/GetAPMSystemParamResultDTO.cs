using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00100COMMON.DTOs.APM00100
{
    public class GetAPMSystemParamResultDTO : R_APIResultBaseDTO
    {
        public GetAPMSystemParamDTO Data { get; set; }
    }
}
