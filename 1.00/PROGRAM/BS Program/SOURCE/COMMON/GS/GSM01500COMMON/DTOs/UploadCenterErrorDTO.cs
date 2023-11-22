using System;
using System.Collections.Generic;
using System.Text;

namespace GSM01500COMMON.DTOs
{
    public class UploadCenterErrorDTO
    {
        public int SeqNo { get; set; }
        public string CCENTER_CODE { get; set; }
        public string CCENTER_NAME { get; set; }
        public bool ACTIVE { get; set; }
        public string NONACTIVEDATE { get; set; }
        public string ErrorMessage { get; set; }
    }
}
