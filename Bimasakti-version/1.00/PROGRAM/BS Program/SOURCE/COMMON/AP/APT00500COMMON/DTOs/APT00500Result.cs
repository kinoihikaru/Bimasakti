using R_APICommonDTO;
using System.Collections.Generic;

namespace APT00500COMMON
{
    public class APT00500SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class APT00500ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
