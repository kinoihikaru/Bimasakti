using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09300COMMON.DTOs.GSM09300
{
    public class GSM09300ParameterDTO
    {
        public GSM09300DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CACTION { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
