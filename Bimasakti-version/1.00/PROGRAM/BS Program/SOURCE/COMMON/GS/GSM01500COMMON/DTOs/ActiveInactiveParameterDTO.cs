using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class ActiveInactiveParameterDTO
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
        public string CUSER_ID { get; set; } = "";
    }
}
