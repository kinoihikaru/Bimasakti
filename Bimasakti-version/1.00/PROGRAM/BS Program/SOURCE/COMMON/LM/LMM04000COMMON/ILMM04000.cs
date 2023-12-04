using LMM04000COMMON.DTOs.LMM04000;
using LMM04000COMMON.DTOs.LMM04010;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON
{
    public interface ILMM04000
    {
        IAsyncEnumerable<LMM04000DTO> GetContractorList();
        IAsyncEnumerable<GetPropertyListDTO> GetPropertyList();
        TemplateContractorDTO DownloadTemplateContractor();
    }
}
