using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00400COMMON
{
    public interface IGLM00400
    {
        GLM00400InitialDTO GetInitialVar(GLM00400InitialDTO poParam);
        GLM00400GLSystemParamDTO GetSystemParam(GLM00400GLSystemParamDTO poParam);
        GLM00400List<GLM00400DTO> GetAllocationJournalHDList(GLM00400DTO poParam);
    }

}
