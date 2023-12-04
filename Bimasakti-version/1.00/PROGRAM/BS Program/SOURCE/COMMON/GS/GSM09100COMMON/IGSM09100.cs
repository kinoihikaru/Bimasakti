using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM09100COMMON
{
    public interface IGSM09100 : R_IServiceCRUDBase<GSM09100DTO>
    {
        IAsyncEnumerable<GSM09100PropertyDTO> GetProperty();
        GSM09100SingleResult<GSM09100InitialDTO> GetInitialProductCategory(GSM09100InitialDTO poParam);
        IAsyncEnumerable<GSM09100DTO> GetProductCategoryList();
    }

}
