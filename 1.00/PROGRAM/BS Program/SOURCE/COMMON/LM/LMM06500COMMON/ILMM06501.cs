using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public interface ILMM06501
    {
        LMM06500List<LMM06500DTO> GetStaffUploadList();

        LMM06500UploadFileDTO DownloadTemplateFile();
    }

}
