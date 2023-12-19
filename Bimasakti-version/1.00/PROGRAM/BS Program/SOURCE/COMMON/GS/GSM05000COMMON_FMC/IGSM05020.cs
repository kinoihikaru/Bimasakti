using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM05000COMMON_FMC
{
    public interface IGSM05020 : R_IServiceCRUDBase<GSM05020DTO>
    {
        IAsyncEnumerable<GSM05020DTO> GetApprovalListStream();
        IAsyncEnumerable<GSM05020DeptDTO> GetDeptTransListStream();
        GSM05000SingleResult<GSM05020ValidateDTO> ValidateApprovalReplacement(GSM05020ValidateDTO poEntity);
        GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyToApproval(GSM05020ParamFromToDeptDTO poEntity);
        GSM05000SingleResult<GSM05020ParamFromToDeptDTO> CopyFromApproval(GSM05020ParamFromToDeptDTO poEntity);
        GSM05000SingleResult<GSM05020DTO> BatchSeqApproval(GSM05000ParameterWithCRUDMode<GSM05020DTO> poParameter);
    }
}