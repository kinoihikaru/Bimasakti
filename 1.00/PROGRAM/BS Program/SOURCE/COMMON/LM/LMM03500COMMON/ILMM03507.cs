using LMM03500COMMON.DTOs.LMM03507;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON
{
    public interface ILMM03507 : R_IServiceCRUDBase<LMM03507ParameterDTO>
    {
        IAsyncEnumerable<LMM03507DTO> GetMembersList();
        IAsyncEnumerable<GetIdTypeDTO> GetIdTypeList();/*
        GetAgeResultDTO GetAge();*/
    }
}
