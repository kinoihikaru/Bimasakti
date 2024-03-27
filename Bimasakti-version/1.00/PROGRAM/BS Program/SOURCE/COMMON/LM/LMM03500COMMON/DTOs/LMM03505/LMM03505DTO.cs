using System;
using System.Collections.Generic;
using System.Text;

namespace LMM03500COMMON.DTOs.LMM03505
{
    public class LMM03505DTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CBANK_CODE { get; set; } = "";
        public string CBANK_NAME { get; set; } = "";
        public string CBANK_ACCOUNT_NO { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime? DUPDATE_DATE { get; set; }  
        public string CCREATE_BY { get; set; } = "";
        public DateTime? DCREATE_DATE { get; set; }
    }
}
