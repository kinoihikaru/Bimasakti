using GSM09300COMMON.DTOs.GSM09310;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON
{
    public interface IGSM09310
    {
        IAsyncEnumerable<GSM09310DTO> GetSupplierList();
        GetSupplierCategoryResultDTO GetSupplierCategory();
        MoveSupplierCategoryResultDTO MoveSupplierCategory(MoveSupplierCategoryParameterDTO poParam);
    }
}
