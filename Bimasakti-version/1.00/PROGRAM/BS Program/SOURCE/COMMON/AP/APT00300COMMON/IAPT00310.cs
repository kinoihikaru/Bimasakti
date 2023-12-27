using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00300COMMON
{
    public interface IAPT00310 : R_IServiceCRUDBase<APT00310DTO>
    {
        APT00300SingleResult<APT00310LastCurrencyDTO> GetLastCurrencyRate(APT00310LastCurrencyParameterDTO poParam);
        IAsyncEnumerable<APT00311DTO> GetDebitNoteDTListStream();
        APT00300SingleResult<APT00311DTO> GetDebitNoteDT(APT00311DTO poParam);
        APT00300SingleResult<APT00311DTO> GenerateWHTaxDeducationDT(APT00311DTO poParam);
        APT00300SingleResult<APT00310DTO> SaveSummaryDebitNote(APT00310DTO poParam);

    }
}
