using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class GetCenterDeptListParameter
    {
        public string CCOMPANY_ID { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CUSER_LOGIN_ID { get; set; } = "";
    }
}
