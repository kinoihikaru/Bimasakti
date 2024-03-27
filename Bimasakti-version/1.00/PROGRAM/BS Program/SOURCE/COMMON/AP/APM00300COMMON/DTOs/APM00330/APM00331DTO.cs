using System;
using System.Globalization;

namespace APM00300COMMON
{
    public class APM00331DTO
    {

        // param
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREC_ID { get; set; } = "";
        public string CSUPPLIER_REC_ID { get; set; } = "";
        public string CACTION { get; set; }

        // List result
        public string CSUPPLIER_ID { get; set; } = "";
        public string CBANK_CODE { get; set; } = "";
        public string CBANK_NAME { get; set; } = "";
        public string CBANK_ACCOUNT_NO { get; set; } = "";
        public string CBANK_ACCOUNT_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";

        // + Detail Result
        public string CBRANCH_NAME { get; set; } = "";
        public string CADDRESS { get; set; } = "";
        public string CCITY_CODE { get; set; } = "";
        public string CCITY_NAME { get; set; } = "";
        public string CSTATE_CODE { get; set; } = "";
        public string CSTATE_NAME { get; set; } = "";
        public string CCOUNTRY_CODE { get; set; } = "";
        public string CCOUNTRY_NAME { get; set; } = "";
        public string CPOSTAL_CODE { get; set; } = "";
        public string CALIAS_NAME { get; set; } = "";
        public string CPARENT_ID { get; set; } = "";

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
