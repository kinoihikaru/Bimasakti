using System;

namespace LMM02500COMMON
{
    public class LMM02510DTO
    {
        //Param
        public string CACTION { get; set; }

        //Profile
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CTENANT_GROUP_ID { get; set; }
        public string CTENANT_GROUP_NAME { get; set; }
        public string CADDRESS { get; set; }
        public string CEMAIL { get; set; }
        public string CPHONE1 { get; set; }
        public string CPHONE2 { get; set; } = "";
        public string CPIC_NAME { get; set; }
        public string CPIC_POSITION { get; set; }
        public string CPIC_EMAIL { get; set; }
        public string CPIC_MOBILE_PHONE1 { get; set; }
        public string CPIC_MOBILE_PHONE2 { get; set; } = "";
        public bool LUSE_GROUP_TAX { get; set; }

        //Tax Info
        public string CTAX_TYPE { get; set; } = "";
        public string CTAX_TYPE_DESCR { get; set; } = "";
        public bool LPPH { get; set; }
        public string CTAX_ID { get; set; } = "";
        public string CTAX_NAME { get; set; } = "";
        public string CID_TYPE { get; set; } = "";
        public string CID_TYPE_DESCR { get; set; } = "";
        public string CID_NO { get; set; } = "";
        public string CID_EXPIRED_DATE { get; set; } = "";
        public string CTAX_CODE { get; set; } = "";
        public string CTAX_CODE_DESCR { get; set; } = "";
        public decimal NTAX_CODE_LIMIT_AMOUNT { get; set; }
        public string CTAX_ADDRESS { get; set; } = "";
        public string CTAX_EMAIL { get; set; } = "";
        public string CTAX_PHONE1 { get; set; } = "";
        public string CTAX_PHONE2 { get; set; } = "";

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }


}
