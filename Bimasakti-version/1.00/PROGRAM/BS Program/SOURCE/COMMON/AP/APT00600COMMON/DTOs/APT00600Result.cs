using R_APICommonDTO;
using System.Collections.Generic;

namespace APT00600COMMON
{
    public class APT00600SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }
    public class APT00600ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }
}
