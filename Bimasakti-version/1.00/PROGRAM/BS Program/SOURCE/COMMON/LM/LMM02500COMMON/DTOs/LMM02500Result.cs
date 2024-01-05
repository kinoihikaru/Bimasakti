using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM02500COMMON
{
    public class LMM02500List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class LMM02500Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
