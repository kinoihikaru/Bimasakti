using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public interface ILMM06500 : R_IServiceCRUDBase<LMM06500DTO>
    {
        LMM06500List<LMM06500DTOInitial> GetProperty();
        LMM06500List<LMM06500DTO> GetStaffList();
        LMM06500DTO LMM06500ActiveInactive(LMM06500DTO poParam);
    }

}
