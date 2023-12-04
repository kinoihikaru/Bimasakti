using LMM04000COMMON.DTOs.LMM04010;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON
{
    public interface ILMM04010 : R_IServiceCRUDBase<ProfileTaxParameterDTO>
    {
        LMM04010ResultDTO GetContractorProfile(LMM04010ParameterDTO poParam);
        IAsyncEnumerable<GetContractorGroupDTO> GetContractorGroupList();
        IAsyncEnumerable<GetContractorCategoryDTO> GetContractorCategoryList();
        IAsyncEnumerable<GetJournalGroupDTO> GetJournalGroupList();
        IAsyncEnumerable<GetPaymentTermDTO> GetPaymentTermList();
        IAsyncEnumerable<GetLineOfBusinessDTO> GetLineOfBusinessList();
        IAsyncEnumerable<GetCurrencyDTO> GetCurrencyList();
    }
}
