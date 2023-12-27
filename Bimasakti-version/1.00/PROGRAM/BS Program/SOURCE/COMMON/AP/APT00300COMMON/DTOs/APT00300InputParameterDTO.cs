using System;
using System.Globalization;

namespace APT00300COMMON
{
    public class APT00300InputParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CDEPT_CODE { get; set; } = "";
        public string CTRANS_CODE { get; set; } = "";
        public string CREF_NO { get; set; } = "";
        public bool LPOPUP_MODE { get; set; } = false;
    }
}
