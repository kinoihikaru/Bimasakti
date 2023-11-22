using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON
{
    public class GetPropertyListResultDTO : R_APIResultBaseDTO
    {
        public List<GetPropertyListDTO> Data { get; set; }
    }
}
