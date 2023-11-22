using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class UpdateExpenditureActiveFlagParameterDTO
    {
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_REC_ID { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
    }
}
