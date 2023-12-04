using LMM03500COMMON.DTOs.LMM03501;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03501 
    {
        IAsyncEnumerable<LMM03501DTO> GetTenantList();
        TemplateTenantDTO DownloadTemplateTenant();
    }
}
