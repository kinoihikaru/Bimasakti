using R_APICommonDTO;
using System.Collections.Generic;

namespace APM00300COMMON
{
    public class GLM00400SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class GLM00400ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
