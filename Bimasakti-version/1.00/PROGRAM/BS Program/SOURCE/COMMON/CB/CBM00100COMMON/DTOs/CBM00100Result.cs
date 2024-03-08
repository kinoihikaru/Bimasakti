using R_APICommonDTO;
using System.Collections.Generic;

namespace CBM00100COMMON
{
    public class CBM00100SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class CBM00100ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
