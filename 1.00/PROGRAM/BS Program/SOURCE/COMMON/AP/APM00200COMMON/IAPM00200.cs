using APM00200COMMON.DTOs.APM00200;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON
{
    public interface IAPM00200 : R_IServiceCRUDBase<APM00200ParameterDTO>
    {
        public UpdateExpenditureActiveFlagResultDTO UpdateExpenditureActiveFlag(UpdateExpenditureActiveFlagParameterDTO poParameter);
        public GetSelectedExpenditureResultDTO GetSelectedExpenditure(GetSelectedExpenditureParameterDTO poParameter);
        public IAsyncEnumerable<APM00200DTO> GetExpenditureList();
        public IAsyncEnumerable<GetWithholdingTaxTypeDTO> GetWithholdingTaxTypeList();
        public IAsyncEnumerable<GetPropertyDTO> GetPropertyList();
        TemplateExpenditureDTO DownloadTemplateExpenditure();
    }
}
