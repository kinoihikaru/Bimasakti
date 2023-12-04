using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class GetSelectedExpenditureResultDTO : R_APIResultBaseDTO
    {
        public APM00200DetailDTO Data { get; set; }
    }
}
