using GSM09200COMMON.DTOs.GSM09210;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON
{
    public interface IGSM09210
    {
        IAsyncEnumerable<GSM09210DTO> GetExpenditureList();
        GetExpenditureCategoryResultDTO GetExpenditureCategory();
        MoveExpenditureCategoryResultDTO MoveExpenditureCategory(MoveExpenditureCategoryParameterDTO poParam);
    }
}
