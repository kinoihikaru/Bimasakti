using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00400COMMON
{
    public interface IGLM00410 : R_IServiceCRUDBase<GLM00410DTO>
    {
        GLM00400List<GLM00411DTO> GetAllocationAccountList(GLM00411DTO poParam);
        GLM00400List<GLM00412DTO> GetAllocationTargetCenterList(GLM00412DTO poParam);
        GLM00400List<GLM00413DTO> GetAllocationTargetCenterByPeriodList(GLM00413DTO poParam);
        GLM00400List<GLM00414DTO> GetAllocationPeriodList(GLM00414DTO poParam);
        GLM00400List<GLM00415DTO> GetAllocationPeriodByTargetCenterList(GLM00415DTO poParam);
    }

}
