using APM00100COMMON.DTOs.APM00100;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00100COMMON
{
    public interface IAPM00100 : R_IServiceCRUDBase<APM00100ParameterDTO>
    {
        GetAPMSystemParamResultDTO GetAPMSystemParam();
    }
}
