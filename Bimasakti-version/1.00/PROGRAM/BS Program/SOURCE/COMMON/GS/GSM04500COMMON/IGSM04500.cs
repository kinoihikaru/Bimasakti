using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM04500COMMON
{
    public interface IGSM04500 : R_IServiceCRUDBase<GSM04500DTO>
    {
        IAsyncEnumerable<GSM04500DTO> GetJournalGroupList();
        GSM04500Record<GSM04500InitDTO> GetInitData();
        GSM04500FileByteDTO GetTemplateJournalGroupExcel();
    }
}
