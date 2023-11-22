using R_APICommonDTO;
using System.Collections.Generic;

namespace GLB09900COMMON
{
    public class GLB09900List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
