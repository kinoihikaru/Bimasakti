using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM05000COMMON_FMC
{
    public interface IGSM05000 : R_IServiceCRUDBase<GSM05000DTO>
    {
        IAsyncEnumerable<GSM05000DTO> GetTransactionCodeListStream();
        GSM05000SingleResult<GSM05000ValidateDTO> ValidateTransactionCode(GSM05000ValidateDTO poEntity);
    }
}