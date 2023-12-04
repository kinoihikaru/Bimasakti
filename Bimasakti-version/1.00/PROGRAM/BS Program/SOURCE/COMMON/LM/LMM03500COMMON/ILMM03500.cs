using LMM03500COMMON.DTOs.LMM03500;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03500
    {
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        IAsyncEnumerable<GetCurrencyDTO> GetCurrencyList();
        TenantResultDTO GetTenant(TenantParameterDTO poParam);
    }
}
