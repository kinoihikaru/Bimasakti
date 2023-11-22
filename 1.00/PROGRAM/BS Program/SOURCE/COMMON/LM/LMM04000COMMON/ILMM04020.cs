using LMM04000COMMON.DTOs.LMM04020;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON
{
    public interface ILMM04020
    {
        LMM04020ResultDTO GetTaxInfo(LMM04020ParameterDTO poParam);
        IAsyncEnumerable<GetLMM04020ListDTO> GetTaxTypeList();
        IAsyncEnumerable<GetLMM04020ListDTO> GetIdTypeList();
        IAsyncEnumerable<GetLMM04020ListDTO> GetTaxCodeList();
    }
}
