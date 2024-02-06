using R_APICommonDTO;
using System.Collections.Generic;

namespace GSM04500COMMON
{
    public class GSM04500List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class GSM04500Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
