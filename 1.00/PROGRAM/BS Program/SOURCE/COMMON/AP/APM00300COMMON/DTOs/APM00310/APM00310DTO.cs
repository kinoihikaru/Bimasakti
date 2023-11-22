using System;
using System.Globalization;

namespace APM00300COMMON
{
    public class APM00310DTO
    {
        private DateTime _cTAX_REG_DATE_DISPLAY;
        private string _cLAST_PURCHASE_DATE_DISPLAY;
        private string _cLAST_PAYMENT_DATE_DISPLAY;

        // param
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CLOB_CODE { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREC_ID { get; set; } = "";
        public string CREC_ID_SUPPLIER { get; set; }
        public string CACTION { get; set; }

        // List result
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; } = "";
        public string CJRNGRP_TYPE { get; set; } = "";
        public string CJRNGRP_CODE { get; set; } = "";
        public string CJRNGRP_NAME { get; set; } = "";
        public string CCATEGORY_TYPE { get; set; } = "";
        public string CCATEGORY_ID { get; set; } = "";
        public string CCATEGORY_NAME { get; set; } = "";
        public string CPAY_TERM_CODE { get; set; } = "";
        public string CPAY_TERM_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CLOB_NAME { get; set; } = "";
        public bool LONETIME { get; set; }
        public string CSTATUS { get; set; } = "0";
        public string CSTATUS_NAME { get; set; }
        public string CDELIVERY_OPTION { get; set; } = "I";
        public string CDELIVERY_OPTION_NAME { get; set; }

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
        public DateTime CTAX_REG_DATE_DISPLAY
        {
            get => _cTAX_REG_DATE_DISPLAY;
            set => _cTAX_REG_DATE_DISPLAY = !string.IsNullOrWhiteSpace(CTAX_REG_DATE)
                                    ? DateTime.ParseExact(CTAX_REG_DATE, "yyyyMMdd", CultureInfo.InvariantCulture)
                                    : default;
        }
        public decimal NPO_BALANCE { get; set; }
        public decimal NBPO_BALANCE { get; set; }
        public decimal NDP_BALANCE { get; set; }
        public decimal NBDP_BALANCE { get; set; }
        public decimal NBALANCE { get; set; }
        public decimal NBBALANCE { get; set; }
        public decimal NLAST_PURCHASE { get; set; }
        public decimal NBLAST_PURCHASE { get; set; }
        public string CLAST_PURCHASE_DATE { get; set; } = "";
        public string CLAST_PURCHASE_DATE_DISPLAY
        {
            get => _cLAST_PURCHASE_DATE_DISPLAY;
            set => _cLAST_PURCHASE_DATE_DISPLAY = !string.IsNullOrWhiteSpace(CLAST_PURCHASE_DATE)
                                    ? DateTime.ParseExact(CLAST_PURCHASE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy")
                                    : "";
        }
        public decimal NLAST_PAYMENT { get; set; }
        public decimal NBLAST_PAYMENT { get; set; }
        public string CLAST_PAYMENT_DATE { get; set; } = "";
        public string CLAST_PAYMENT_DATE_DISPLAY
        {
            get => _cLAST_PAYMENT_DATE_DISPLAY;
            set => _cLAST_PAYMENT_DATE_DISPLAY = !string.IsNullOrWhiteSpace(CLAST_PAYMENT_DATE)
                                    ? DateTime.ParseExact(CLAST_PAYMENT_DATE, "yyyyMMdd", CultureInfo.InvariantCulture).ToString("dd MMMM, yyyy")
                                    : "";
        }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
