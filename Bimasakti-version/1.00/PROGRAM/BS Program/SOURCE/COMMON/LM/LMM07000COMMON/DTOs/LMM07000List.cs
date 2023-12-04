using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM07000COMMON
{
    public class LMM07000List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class LMM07000Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

}
