using System;

namespace APM00300COMMON
{
    public class APM00300DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CLOB_CODE { get; set; }
        public string CSEARCH_TEXT { get; set; }
        public string CLANGUAGE_ID { get; set; }
        public string CREC_ID { get; set; }

        // List result
        public string CSUPPLIER_ID   { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CJRNGRP_TYPE { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public string CCATEGORY_TYPE { get; set; }
        public string CCATEGORY_ID { get; set; }
        public string CCATEGORY_NAME { get; set; }
        public string CPAY_TERM_CODE { get; set; }
        public string CPAY_TERM_NAME { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CLOB_NAME { get; set; }
        public bool LONETIME { get; set; }
        public string CSTATUS { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CDELIVERY_OPTION { get; set; }
        public string CDELIVERY_OPTION_NAME { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
