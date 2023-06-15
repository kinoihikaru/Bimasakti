using GSM03000Common.DTOs;
using R_CommonFrontBackAPI;
using System;
using System.Collections.Generic;
using System.Text;

namespace GSM03000Common
{
    public interface IGSM03000 : R_IServiceCRUDBase<GSM03000DTO>
    {
        GSM03000PropertyListDTO GetProperty();
        IAsyncEnumerable<GSM03000DTO> GetOtherChargesList();
        GSM03000ActiveDTO GSM03000OtherChargesChangeStatus(GSM03000ActiveParameterDTO poParam);
    }
}
