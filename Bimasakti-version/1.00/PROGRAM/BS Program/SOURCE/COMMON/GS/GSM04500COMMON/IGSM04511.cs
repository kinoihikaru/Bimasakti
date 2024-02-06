using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM04500COMMON
{
    public interface IGSM04511 : R_IServiceCRUDBase<GSM04511DTO>
    {
        IAsyncEnumerable<GSM04511DTO> GetJournalGroupGOAByDeptList();
    }
}
