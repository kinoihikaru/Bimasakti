using System;
using Lookup_LMCOMMON.DTOs;

namespace Lookup_LMCOMMON
{
    public interface IPublicLookupLM
    {
        LMLGenericList<LML00100DTO> LML00100GetSalesTaxList(LML00100ParameterDTO poParameter);
        LMLGenericList<LML00200DTO> LML00200UnitChargesList(LML00200ParameterDTO poParameter);
        LMLGenericList<LML00300DTO> LML00300SupervisorList(LML00300ParameterDTO poParameter);
        LMLGenericList<LML00400DTO> LML00400UtilityChargesList(LML00400ParameterDTO poParameter);
    }
}
