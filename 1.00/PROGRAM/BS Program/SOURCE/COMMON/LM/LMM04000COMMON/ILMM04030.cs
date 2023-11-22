using LMM04000COMMON.DTOs.LMM04030;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON
{
    public interface ILMM04030 : R_IServiceCRUDBase<LMM04030ParameterDTO>
    {
        IAsyncEnumerable<LMM04030DTO> GetBankInfoList();
        IAsyncEnumerable<GetBankCodeDTO> GetBankCodeList();
        TenantResultDTO GetTenant(TenantParameterDTO poParam);
    }
}
