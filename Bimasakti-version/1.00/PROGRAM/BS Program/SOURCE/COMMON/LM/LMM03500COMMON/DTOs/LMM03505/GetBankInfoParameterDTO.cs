using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03505
{
    public class GetBankInfoParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CSELECTED_TENANT_ID { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
    }
}
