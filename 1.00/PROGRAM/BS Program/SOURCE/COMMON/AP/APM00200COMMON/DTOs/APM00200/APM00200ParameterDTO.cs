using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class APM00200ParameterDTO
    {
        public APM00200DetailDTO Data { get; set; }
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CLOGIN_LANGUAGE_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
