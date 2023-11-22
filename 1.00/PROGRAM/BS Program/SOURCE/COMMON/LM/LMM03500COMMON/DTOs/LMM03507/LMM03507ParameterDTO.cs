using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03507
{
    public class LMM03507ParameterDTO
    {
        public LMM03507DetailDTO Data { get; set; }
        public string CLOGIN_COMPANY_ID { get; set; }
        public string CSELECTED_PROPERTY_ID { get; set; }
        public string CACTION { get; set; }
        public string CLOGIN_USER_ID { get; set; }
    }
}
