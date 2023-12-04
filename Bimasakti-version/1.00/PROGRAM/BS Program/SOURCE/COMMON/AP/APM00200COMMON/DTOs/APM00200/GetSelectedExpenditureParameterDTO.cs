using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class GetSelectedExpenditureParameterDTO
    {
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_LANGUAGE_ID { get; set; } = "";
        public string CREC_ID { get; set; } = "";
    }
}
