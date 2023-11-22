using GSM09000COMMON.DTOs.GSM09001;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON
{
    public interface IGSM09001
    {
        IAsyncEnumerable<GSM09001DTO> GetTenantList();
        IAsyncEnumerable<GetTenantCategoryDTO> GetTenantCategoryList();
        GetTenantCategoryResultDTO GetTenantCategory();
        SaveMoveTenantCategoryResultDTO SaveMoveTenantCategory(SaveMoveTenantCategoryParameterDTO poParam);
    }
}
