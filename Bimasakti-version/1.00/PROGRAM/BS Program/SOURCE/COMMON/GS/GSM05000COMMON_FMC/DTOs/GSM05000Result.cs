using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM05000COMMON_FMC
{
    public class GSM05000ListResult<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
    }

    public class GSM05000SingleResult<T> : R_APIResultBaseDTO
    {
        public T Data { get; set; }
    }

    public class GSM05000ParameterWithCRUDMode<T> : R_APIResultBaseDTO
    {
        public List<T> Data { get; set; }
        public eCRUDMode poCRUDMode { get; set; }  
    }
}
