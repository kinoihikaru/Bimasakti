using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class CenterPrintParameterDTO
    {
        public string CCENTER_CODE_FROM { get; set; } = "";
        public string CCENTER_CODE_TO { get; set; } = "";
        public bool LPRINT_DEPT { get; set; } = false;
        public bool LPRINT_INACTIVE { get; set; } = false;
    }
}
