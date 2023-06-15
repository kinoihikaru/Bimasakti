using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01010DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // result
        public string CCHARGES_NAME { get; set; }
        public string CCURENCY_CODE { get; set; }
        public string CUSAGE_RATE_MODE { get; set; } = "HM";
        public string CRATE_TYPE { get; set; } = "SR";
        public int NBUY_STANDING_CHARGE { get; set; }
        public int NSTANDING_CHARGE { get; set; }
        public int NBUY_LWBP_CHARGE { get; set; }
        public int NLWBP_CHARGE { get; set; }
        public int NBUY_WBP_CHARGE { get; set; }
        public int NWBP_CHARGE { get; set; }
        public int NBUY_TRANSFORMATOR_FEE { get; set; }
        public int NTRANSFORMATOR_FEE { get; set; }
        public bool LUSAGE_MIN_CHARGE { get; set; }
        public int NUSAGE_MIN_CHARGE { get; set; }
        public int NKWH_USED_MAX { get; set; }
        public int NKWH_USED_RATE { get; set; }
        public int NBUY_KWH_USED_RATE { get; set; }
        public int NBUY_KWA_USED_RATE { get; set; }
        public int NKWA_USED_RATE { get; set; }
        public int NRETRIBUTION_PCT { get; set; }
        public bool LRETRIBUTION_EXCLUDED_VAT { get; set; }
        public string CADMIN_FEE { get; set; }
        public int NADMIN_FEE_PCT { get; set; }
        public int NADMIN_FEE_AMT { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public int NOTHER_DISINCENTIVE_FACTOR { get; set; }
        public int NBUY_KVA_RANGE { get; set; }
        public int NKVA_RANGE { get; set; }
        public List<LMM01011DTO> CRATE_EC_LIST { get; set; }
    }
}
