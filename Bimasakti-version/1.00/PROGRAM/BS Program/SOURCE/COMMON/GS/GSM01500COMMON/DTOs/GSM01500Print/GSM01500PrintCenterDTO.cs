using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs.GSM01500Print
{
    public class GSM01500PrintCenterDTO
    {
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
    }
}
