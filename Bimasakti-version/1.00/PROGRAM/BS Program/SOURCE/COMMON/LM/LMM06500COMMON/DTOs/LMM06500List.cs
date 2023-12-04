using R_APICommonDTO;
using System.Collections.Generic;

namespace LMM06500COMMON
{
    public class LMM06500List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
