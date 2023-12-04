using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01000List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
