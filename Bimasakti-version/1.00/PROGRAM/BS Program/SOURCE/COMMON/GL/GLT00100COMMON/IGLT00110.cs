using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GLT00100COMMON
{
    public interface IGLT00110 : R_IServiceCRUDBase<GLT00110DTO>
    {
        GLT00100RecordResult<GLT00100UpdateStatusDTO> UpdateJournalStatus(GLT00100UpdateStatusDTO poEntity);
    }
}
