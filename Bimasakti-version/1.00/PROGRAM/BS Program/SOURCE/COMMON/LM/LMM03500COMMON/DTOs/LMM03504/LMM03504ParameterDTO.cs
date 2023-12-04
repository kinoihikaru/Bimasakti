using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03504
{
    public class LMM03504ParameterDTO
    {
        public LMM03504DTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; } = "";
        public string CSELECTED_PROPERTY_ID { get; set; } = "";
        public string CCUSTOMER_TYPE { get; set; } = "";
        public string CLOGIN_USER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
    }
}
