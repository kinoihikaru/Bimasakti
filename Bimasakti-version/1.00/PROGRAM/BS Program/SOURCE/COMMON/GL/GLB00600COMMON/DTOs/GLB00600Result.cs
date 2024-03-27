using R_APICommonDTO;
using System.Collections.Generic;

namespace GLB00600COMMON
{
    public class GLB00600List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
    public class GLB00600Record<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
}
