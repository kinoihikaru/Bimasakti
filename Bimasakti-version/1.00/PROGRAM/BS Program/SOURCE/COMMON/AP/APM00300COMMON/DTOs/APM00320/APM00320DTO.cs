using System;
using System.Globalization;

namespace APM00300COMMON
{
    public class APM00320DTO
    {
        private DateTime _cTAX_REG_DATE_DISPLAY;

        // param
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CLOB_CODE { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREC_ID { get; set; } = "";
        public string CPARENT_ID { get; set; }
        public string CSUPPLIER_REC_ID { get; set; } = "";
        public string CACTION { get; set; }

        // List result
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";

        // + Detail Result
        public string CADDRESS { get; set; } = "";
        public string CCITY_CODE { get; set; } = "";
        public string CCITY_NAME { get; set; } = "";
        public string CSTATE_CODE { get; set; } = "";
        public string CSTATE_NAME { get; set; } = "";
        public string CCOUNTRY_CODE { get; set; } = "";
        public string CCOUNTRY_NAME { get; set; } = "";
        public string CPOSTAL_CODE { get; set; } = "";
        public string CPHONE1 { get; set; } = "";
        public string CPHONE2 { get; set; } = "";
        public string CFAX1 { get; set; } = "";
        public string CFAX2 { get; set; } = "";
        public string CEMAIL1 { get; set; } = "";
        public string CEMAIL2 { get; set; } = "";
        public string CCONTACT_NAME1 { get; set; } = "";
        public string CCONTACT_PHONE1 { get; set; } = "";
        public string CCONTACT_EMAIL1 { get; set; } = "";
        public string CCONTACT_POSITION1 { get; set; } = "";
        public string CCONTACT_NAME2 { get; set; } = "";
        public string CCONTACT_PHONE2 { get; set; } = "";
        public string CCONTACT_EMAIL2 { get; set; } = "";
        public string CCONTACT_POSITION2 { get; set; } = "";
        public string CTAX_TYPE { get; set; } = "";
        public string CTAX_TYPE_NAME { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public string CTAX_REG_ID { get; set; } = "";
        public string CTAX_REG_DATE { get; set; } = "";
        public decimal NPO_BALANCE { get; set; }
        public decimal NBPO_BALANCE { get; set; }
        public decimal NDP_BALANCE { get; set; }
        public decimal NBDP_BALANCE { get; set; }
        public decimal NBALANCE { get; set; }
        public decimal NBBALANCE { get; set; }
        public decimal NLAST_PURCHASE { get; set; }
        public decimal NBLAST_PURCHASE { get; set; }
        public decimal NLAST_PAYMENT { get; set; }
        public decimal NBLAST_PAYMENT { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
