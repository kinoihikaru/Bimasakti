using GSM09300COMMON.DTOs.GSM09300;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON
{
    public interface IGSM09300 : R_IServiceCRUDBase<GSM09300DetailDTO>
    {
        IAsyncEnumerable<GSM09300DTO> GetSupplierCategoryList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        ValidateIsCategoryExistResultDTO ValidateIsCategoryExist(ValidateIsCategoryExistParameterDTO poParam);
    }
}
