using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APM00300COMMON
{
    public interface IAPM00330 : R_IServiceCRUDBase<APM00330DTO>
    {
        IAsyncEnumerable<APM00330DTO> GetSupplierBankList();

    }
}
