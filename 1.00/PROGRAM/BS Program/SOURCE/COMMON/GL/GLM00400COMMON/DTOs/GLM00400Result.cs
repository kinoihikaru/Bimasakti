using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00400COMMON
{
    public class GLM00400Result<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    
}
