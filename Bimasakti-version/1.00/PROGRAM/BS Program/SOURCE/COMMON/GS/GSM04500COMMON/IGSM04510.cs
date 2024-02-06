using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM04500COMMON
{
    public interface IGSM04510 : R_IServiceCRUDBase<GSM04510DTO>
    {
        IAsyncEnumerable<GSM04510DTO> GetJournalGroupGOAList();
    }
}
