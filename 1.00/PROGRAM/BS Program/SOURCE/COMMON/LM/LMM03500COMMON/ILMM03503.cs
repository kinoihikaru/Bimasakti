using LMM03500COMMON.DTOs.LMM03500;
using LMM03500COMMON.DTOs.LMM03502;
using LMM03500COMMON.DTOs.LMM03503;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03503 
    {
        LMM03503ResultDTO GetTaxInfo(LMM03503ParameterDTO poParam);
        IAsyncEnumerable<GetLMM03503ListDTO> GetTaxTypeList();
        IAsyncEnumerable<GetLMM03503ListDTO> GetIdTypeList();
        IAsyncEnumerable<GetLMM03503ListDTO> GetTaxCodeList();
    }
}
