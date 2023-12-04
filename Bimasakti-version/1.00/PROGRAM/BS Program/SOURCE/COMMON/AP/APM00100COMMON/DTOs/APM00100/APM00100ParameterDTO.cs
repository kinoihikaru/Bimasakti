using System;
using System.Collections.Generic;
using System.Text;

namespace APM00100COMMON.DTOs.APM00100
{
    public class APM00100ParameterDTO
    {
        public APM00100DTO Data { get; set; }
        public string CLOGIN_USER_ID { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CLOGIN_LANGUAGE_ID { get; set; }
        public string CACTION { get; set; }
    }
}
