using R_CommonFrontBackAPI;
using System.Collections.Generic;

namespace LMM01000COMMON
{
    public interface ILMM01000 : R_IServiceCRUDBase<LMM01000DTO>
    {
        IAsyncEnumerable<LMM01002DTO> GetChargesUtilityList();
        LMM01000DTO LMM01000ActiveInactive(LMM01000DTO poParam);
        LMM01003DTO LMM01000CopyNewCharges(LMM01003DTO poParam);
        LMM01000Record<LMM01000BeforeDeleteDTO> ValidateBeforeDelete(LMM01000DTO poParam);

    }

}
