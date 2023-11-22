using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM09100COMMON
{
    public interface IGSM09110
    {
        IAsyncEnumerable<GSM09110DTO> GetProductList();
        GSM09100SingleResult<GSM09120DTO> MoveProduct(GSM09120DTO poParam);
    }

}
