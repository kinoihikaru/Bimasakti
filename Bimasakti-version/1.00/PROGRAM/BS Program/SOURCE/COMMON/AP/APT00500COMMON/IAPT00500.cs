using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00500COMMON
{
    public interface IAPT00500 : R_IServiceCRUDBase<APT00500DTO>
    {
        IAsyncEnumerable<APT00500DTO> GetPurhcaseAdjustmentStream();
        APT00500SingleResult<APT00500SubmitRedraftDTO> SubmitPurchaseAdj(APT00500SubmitRedraftDTO poEntity);
        APT00500SingleResult<APT00500SubmitRedraftDTO> RedraftPurchaseAdj(APT00500SubmitRedraftDTO poEntity);
    }
}
