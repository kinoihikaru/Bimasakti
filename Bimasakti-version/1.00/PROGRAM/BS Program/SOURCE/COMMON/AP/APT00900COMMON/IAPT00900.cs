using System;
using System.Collections.Generic;

namespace APT00900COMMON
{
    public interface IAPT00900
    {
        IAsyncEnumerable<APT00900PropertyDTO> GetGSPropertyList();
        APT00900UploadFileDTO DownloadTemplateFile();
    }
}
