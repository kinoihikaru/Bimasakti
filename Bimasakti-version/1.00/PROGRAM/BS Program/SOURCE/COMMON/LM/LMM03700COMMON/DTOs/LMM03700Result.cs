using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM03700COMMON
{
    public class LMM03700List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class LMM03700Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
