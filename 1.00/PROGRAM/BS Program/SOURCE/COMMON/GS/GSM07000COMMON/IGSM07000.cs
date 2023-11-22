using GSM07000COMMON.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM07000COMMON
{
    public interface IGSM07000 : R_IServiceCRUDBase<GSM07000DTO>
    {
        IAsyncEnumerable<GSM07000DTO> GetActivityApprovalList();
        IAsyncEnumerable<ApprovalDTO> GetApprovalList();
    }
}
