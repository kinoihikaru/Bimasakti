using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public interface ILMM01510 : R_IServiceCRUDBase<LMM01511DTO>
    {
        LMM01500List<LMM01510DTO> LMM01510TemplateAndBankAccountList();
    }

}
