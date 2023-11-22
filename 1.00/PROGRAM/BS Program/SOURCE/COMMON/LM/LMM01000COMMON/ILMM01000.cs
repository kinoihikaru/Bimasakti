using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01000 : R_IServiceCRUDBase<LMM01000DTO>
    {
        IAsyncEnumerable<LMM01000DTOPropety> GetProperty();
        LMM01000UniversalDTO GetChargesType();
        IAsyncEnumerable<LMM01002DTO> GetChargesUtilityList();
        LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam);
        LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam);

    }

}
