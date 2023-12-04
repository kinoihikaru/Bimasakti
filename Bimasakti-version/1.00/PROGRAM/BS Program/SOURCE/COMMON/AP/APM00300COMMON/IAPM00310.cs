using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APM00300COMMON
{
    public interface IAPM00310 : R_IServiceCRUDBase<APM00310DTO>
    {
        IAsyncEnumerable<APM00300PayTermDTO> GetPayTermList();
        IAsyncEnumerable<APM00300CurrencyDTO> GetCurrencyList();
        IAsyncEnumerable<APM00300TaxTypeDTO> GetTaxTypeList();
    }
}
