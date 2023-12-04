using R_APICommonDTO;
using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00200List<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

}
