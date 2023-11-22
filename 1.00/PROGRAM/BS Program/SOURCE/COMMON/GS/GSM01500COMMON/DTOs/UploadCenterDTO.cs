using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class UploadCenterDTO
    {
        public int INO { get; set; } = 0;
        public string CCOMPANY_ID { get; set; } = "";
        public string CCENTER_CODE { get; set; } = "";
        public string CCENTER_NAME { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
        public string NONACTIVE_DATE { get; set; } = "";
        public string CVALID { get; set; } = "Y";
        public string CNOTES { get; set; } = "";
    }
}
