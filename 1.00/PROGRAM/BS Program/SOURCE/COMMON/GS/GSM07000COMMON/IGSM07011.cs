using GSM07000COMMON.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON
{
    public interface IGSM07011 : R_IServiceCRUDBase<GSM07011ParameterDTO>
    {
        IAsyncEnumerable<GSM07011DTO> GetMultipleUserAssignmentList();
        SaveMultipleUserAssignmentDTO SaveMultipleUserAssignment(SaveMultipleUserAssignmentParameterDTO poParameter);
    }
}
