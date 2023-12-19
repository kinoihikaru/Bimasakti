using System.Collections.Generic;
using R_CommonFrontBackAPI;

namespace GSM05000COMMON_FMC
{
    public interface IGSM05000Universal
    {
        GSM05000ListResult<GSM05000UniversalDTO> GetRefNoDelimiterList();
    }
}