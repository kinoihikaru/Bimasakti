using R_APICommonDTO;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public interface ILMM01500 : R_IServiceCRUDBase<LMM01500DTO>
    {
        LMM01500List<LMM01500DTOPropety> GetProperty();

        LMM01500List<LMM01501DTO> GetInvoiceGrpList();

        LMM01500DTO LMM01500ActiveInactive(LMM01500DTO poParam);

        LMM01500List<LMM01502DTO> LMM01500LookupBank();
    }

}
