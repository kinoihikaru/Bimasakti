using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03502 : R_IServiceCRUDBase<ProfileTaxParameterDTO>
    {
        LMM03502ResultDTO GetTenantProfile(LMM03502ParameterDTO poParam);
        IAsyncEnumerable<GetTenantGroupDTO> GetTenantGroupList();
        IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList();
        IAsyncEnumerable<GetTenantTypeDTO> GetTenantTypeList();
        IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList();
    }
}
