using R_APICommonDTO;
using System;

namespace LMM06500COMMON
{
    public class LMM06500DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // result
        public string CSTAFF_ID { get; set; }
        public string CSTAFF_NAME { get; set; }
        public bool LACTIVE { get; set; } = true;
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CPOSITION { get; set; } = "";
        public string CPOSITION_DESCR { get; set; }
        public string CJOIN_DATE { get; set; }
        public string CSUPERVISOR { get; set; } = "";
        public string CSUPERVISOR_NAME { get; set; }
        public string CEMAIL { get; set; }
        public string CMOBILE_PHONE1 { get; set; }
        public string CCITY_CODE { get; set; } = "";
        public string CPOSTAL_CODE { get; set; } = "";
        public string CSTATE_CODE { get; set; } = "";
        public string CCOUNTRY_CODE { get; set; } = "";
        public string CMOBILE_PHONE2 { get; set; } = "";
        public string CGENDER { get; set; } = "F";
        public string CGENDER_DESCR { get; set; }
        public string CADDRESS { get; set; } = "";
        public string CINACTIVE_NOTE { get; set; } = "";
        public string CACTIVE_BY { get; set; }
        public DateTime DACTIVE_DATE { get; set; }
        public string CINACTIVE_BY { get; set; }
        public DateTime DINACTIVE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }

    }


}
