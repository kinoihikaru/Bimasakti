using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM05000COMMON_FMC
{
    public interface IGSM05021 : R_IServiceCRUDBase<GSM05021DTO>
    {
        IAsyncEnumerable<GSM05021DTO> GetApprovalReplacementListStream();
    }
}