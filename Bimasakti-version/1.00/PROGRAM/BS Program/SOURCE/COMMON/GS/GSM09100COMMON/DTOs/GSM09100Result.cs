using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GSM09100COMMON
{
    public class GSM09100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GSM09100SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
