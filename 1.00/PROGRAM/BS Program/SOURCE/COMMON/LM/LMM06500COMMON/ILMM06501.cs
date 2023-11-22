using System.Collections.Generic;

namespace LMM06500COMMON
{
    public interface ILMM06501
    {
        LMM06500UploadFileDTO DownloadTemplateFile();

        IAsyncEnumerable<LMM06501ErrorValidateDTO> GetErrorProcess();
    }

}
