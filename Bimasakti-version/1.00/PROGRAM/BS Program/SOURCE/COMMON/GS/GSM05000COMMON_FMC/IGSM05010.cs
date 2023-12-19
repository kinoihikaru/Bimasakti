using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM05000COMMON_FMC
{
    public interface IGSM05010 : R_IServiceCRUDBase<GSM05010DTO>
    {
        GSM05000SingleResult<GSM05011DTO> GetHeaderTransCodeNumber(GSM05011DTO poEntity);
        IAsyncEnumerable<GSM05010DTO> GetHeaderTransCodeNumberListStream();
    }
}