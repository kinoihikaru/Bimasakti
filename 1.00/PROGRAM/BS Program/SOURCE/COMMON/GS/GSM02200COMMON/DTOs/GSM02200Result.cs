using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GSM02200COMMON
{
    public class GSM02200ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GSM02200SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
