using LMM03500COMMON.DTOs.LMM03504;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03504 : R_IServiceCRUDBase<LMM03504ParameterDTO>
    {
        LMM03504ResultDTO GetBilling(GetBillingParameterDTO poParam);
    }
}
