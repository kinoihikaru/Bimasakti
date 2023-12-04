using GSM09200COMMON.DTOs.GSM09200;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON
{
    public interface IGSM09200 : R_IServiceCRUDBase<GSM09200DetailDTO>
    {
        IAsyncEnumerable<GSM09200DTO> GetExpenditureCategoryList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        ValidateIsCategoryExistResultDTO ValidateIsCategoryExist(ValidateIsCategoryExistParameterDTO poParam);
    }
}
