using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace GSM02200COMMON
{
    public interface IGSM02200 : R_IServiceCRUDBase<GSM02200DTO>
    {
        IAsyncEnumerable<GSM02200DTO> GetGeographyList();
        GSM02200UploadFileDTO DownloadTemplateFile();
        GSM02200SingleResult<GSM02200DTO> GSM02200GeographyChangeStatus(GSM02200DTO poParam);
    }
}
