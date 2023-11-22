using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04010
{
    public class LMM04010DTO
    {
        public string CTENANT_ID { get; set; } = "";
        public string CTENANT_NAME { get; set; } = "";
        public string CADDRESS { get; set; } = "";
        public string CCITY_CODE { get; set; } = "";
        public string CSTATE_CODE { get; set; } = "";
        public string CCOUNTRY_CODE { get; set; } = "";
        public string CPOSTAL_CODE { get; set; } = "";
        public string CCITY_NAME { get; set; } = "";
        public string CSTATE_NAME { get; set; } = "";
        public string CCOUNTRY_NAME { get; set; } = "";
        public string CEMAIL { get; set; } = "";
        public string CPHONE1 { get; set; } = "";
        public string CPHONE2 { get; set; } = "";
        public string CTENANT_GROUP_ID { get; set; } = "";
        public string CTENANT_CATEGORY_ID { get; set; } = "";
        public string CTENANT_GROUP_NAME { get; set; } = "";
        public string CTENANT_CATEGORY_NAME { get; set; } = "";
        public string CATTENTION1_NAME { get; set; } = "";
        public string CATTENTION1_POSITION { get; set; } = "";
        public string CATTENTION1_EMAIL { get; set; } = "";
        public string CATTENTION1_MOBILE_PHONE1 { get; set; } = "";
        public string CATTENTION1_MOBILE_PHONE2 { get; set; } = "";
        public string CATTENTION2_NAME { get; set; } = "";
        public string CATTENTION2_POSITION { get; set; } = "";
        public string CATTENTION2_EMAIL { get; set; } = "";
        public string CATTENTION2_MOBILE_PHONE1 { get; set; } = "";
        public string CATTENTION2_MOBILE_PHONE2 { get; set; } = "";
        public string CJRNGRP_CODE { get; set; } = "";
        public string CJRNGRP_NAME { get; set; } = "";
        public string CPAYMENT_TERM_CODE { get; set; } = "";
        public string CPAYMENT_TERM_NAME { get; set; } = "";
        public string CCURRENCY_CODE { get; set; } = "";
        public string CCURRENCY_NAME { get; set; } = "";
        public string CLOB_CODE { get; set; } = "";
        public string CLOB_DESCRIPTION { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public void Clear()
        {
            CTENANT_ID = "";
            CTENANT_NAME = "";
            CADDRESS = "";
            CEMAIL = "";
            CPHONE1 = "";
            CPHONE2 = "";
            CTENANT_GROUP_ID = "";
            CTENANT_CATEGORY_ID = "";
            CATTENTION1_NAME = "";
            CATTENTION1_POSITION = "";
            CATTENTION1_EMAIL = "";
            CATTENTION1_MOBILE_PHONE1 = "";
            CATTENTION1_MOBILE_PHONE2 = "";
            CATTENTION2_NAME = "";
            CATTENTION2_POSITION = "";
            CATTENTION2_EMAIL = "";
            CATTENTION2_MOBILE_PHONE1 = "";
            CATTENTION2_MOBILE_PHONE2 = "";
            CJRNGRP_CODE = "";
            CJRNGRP_NAME = "";
            CPAYMENT_TERM_CODE = "";
            CPAYMENT_TERM_NAME = "";
            CCURRENCY_CODE = "";
            CCURRENCY_NAME = "";
            CLOB_CODE = "";
            CLOB_DESCRIPTION = "";
            CUPDATE_BY = "";
            DUPDATE_DATE = default(DateTime);
            CCREATE_BY = "";
            DCREATE_DATE = default(DateTime);
        }
    }
}
