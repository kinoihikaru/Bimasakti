using LMM03500COMMON.DTOs.LMM03505;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03505 : R_IServiceCRUDBase<LMM03505ParameterDTO>
    {
        IAsyncEnumerable<LMM03505DTO> GetBankInfoList();
    }
}
