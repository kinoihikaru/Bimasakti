using R_APICommonDTO;
using System.Collections.Generic;

namespace APT00300COMMON
{
    public class APT00300SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class APT00300ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
