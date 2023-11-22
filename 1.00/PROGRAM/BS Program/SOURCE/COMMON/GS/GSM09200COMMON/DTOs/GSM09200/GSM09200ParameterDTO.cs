using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09200
{
    public class GSM09200ParameterDTO
    {
        public GSM09200DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CACTION { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
