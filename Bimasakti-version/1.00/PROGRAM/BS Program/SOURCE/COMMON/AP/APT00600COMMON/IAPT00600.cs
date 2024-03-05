using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00600COMMON
{
    public interface IAPT00600 : R_IServiceCRUDBase<APT00600DTO>
    {
        IAsyncEnumerable<APT00600DTO> GetPurhcaseAdjustmentStream();
        APT00600SingleResult<APT00600SubmitRedraftDTO> SubmitPurchaseAdj(APT00600SubmitRedraftDTO poEntity);
        APT00600SingleResult<APT00600SubmitRedraftDTO> RedraftPurchaseAdj(APT00600SubmitRedraftDTO poEntity);
    }
}
