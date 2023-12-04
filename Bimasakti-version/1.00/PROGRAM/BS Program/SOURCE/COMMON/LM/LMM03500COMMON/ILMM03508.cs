using LMM03500COMMON.DTOs.LMM03508;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03508
    {
        IAsyncEnumerable<LMM03508DTO> GetFixVAList();
    }
}
