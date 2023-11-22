using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09000COMMON.DTOs.GSM09000
{
    public class InsertNewRootIfListEmptyParameterDTO
    {
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CSELECTED_PROPERTY_NAME { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CACTION { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
