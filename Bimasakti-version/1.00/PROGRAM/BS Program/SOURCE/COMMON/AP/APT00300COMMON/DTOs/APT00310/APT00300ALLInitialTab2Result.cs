using R_APICommonDTO;
using System.Collections.Generic;

namespace APT00300COMMON
{
    public class APT00300ALLInitialTab2Result
    {
        public List<APT00300PropertyDTO> VAR_PROPERTY_LIST { get; set; }
        public List<APT00300CurrencyDTO> VAR_CURRENCY_LIST { get; set; }
        public APT00300APSystemParamDTO VAR_AP_SYSTEM_PARAM { get; set; }
        public APT00300GSCompanyInfoDTO VAR_GSM_COMPANY { get; set; }
        public APT00300GSTransCodeInfoDTO VAR_GSM_TRANSACTION_CODE { get; set; }
    }
}
