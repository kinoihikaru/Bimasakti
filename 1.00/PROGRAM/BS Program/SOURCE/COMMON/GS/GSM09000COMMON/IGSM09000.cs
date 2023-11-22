using GSM09000COMMON.DTOs.GSM09000;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON
{
    public interface IGSM09000 : R_IServiceCRUDBase<GSM09000DetailDTO>
    {
        IAsyncEnumerable<GSM09000DTO> GetTenantCategoryList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        GetParentPositionIfEmptyResultDTO GetParentPositionIfEmpty(GetParentPositionIfEmptyParameterDTO poParam);
        InsertNewRootIfListEmptyResultDTO InsertNewRootIfListEmpty(InsertNewRootIfListEmptyParameterDTO poParam);
    }
}
