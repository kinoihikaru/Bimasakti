using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;

namespace APT00300COMMON
{
    public interface IAPT00300Universal
    {
        APT00300SingleResult<APT00300ALLInitialTab1Result> GetAllInitialProcessTab1();
        APT00300SingleResult<APT00300ALLInitialTab2Result> GetAllInitialProcessTab2();
        APT00300SingleResult<APT00300PeriodYearRangeDTO> GetInitialGSMPeriodWithParameter(APT00300PeriodYearRangeDTO poEntity);
        APT00300SingleResult<APT00300APSystemParamDTO> GetInitialAPSystemParam();
        APT00300SingleResult<APT00300GSCompanyInfoDTO> GetInitialGSMCompanyInfo();
        APT00300SingleResult<APT00300GLSystemParamDTO> GetInitialGLSystemParam();
        APT00300SingleResult<APT00300GSTransCodeInfoDTO> GetInitialGSMTransCode();
        IAsyncEnumerable<APT00300PropertyDTO> GetInitialPropertyListStream();
        IAsyncEnumerable<APT00300GSBCodeDTO> GetInitialGSBCodeListStream();
        IAsyncEnumerable<APT00300CurrencyDTO> GetInitialCurrencyListStream();
    }
}
