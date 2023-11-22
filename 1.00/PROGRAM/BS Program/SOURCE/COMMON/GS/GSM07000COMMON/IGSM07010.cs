using GSM07000COMMON.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON
{
    public interface IGSM07010 : R_IServiceCRUDBase<GSM07010ParameterDTO>
    {
        IAsyncEnumerable<GSM07010DTO> GetActivityApprovalUserList();
        IAsyncEnumerable<GSM07010UserDTO> GetLookUpUserList();
    }
}
