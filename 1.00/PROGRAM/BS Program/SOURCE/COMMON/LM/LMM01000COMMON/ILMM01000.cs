using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public interface ILMM01000 : R_IServiceCRUDBase<LMM01000DTO>
    {
        LMM01000List<LMM01000DTOPropety> GetProperty();
        LMM01000UniversalDTO GetChargesType(LMM01000UniversalDTO poParam);
        LMM01000List<LMM01002DTO> GetChargesUtilityList();
        LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam);
        LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam);

    }

}
