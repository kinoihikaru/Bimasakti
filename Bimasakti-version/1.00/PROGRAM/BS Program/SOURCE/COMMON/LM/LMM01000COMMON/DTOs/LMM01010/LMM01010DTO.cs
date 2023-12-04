using System.Collections.Generic;

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
        public string CUSAGE_RATE_MODE_DESCR { get; set; }
        public string CRATE_TYPE { get; set; } = "SR";
        public decimal NBUY_STANDING_CHARGE { get; set; }
        public decimal NSTANDING_CHARGE { get; set; }
        public decimal NBUY_LWBP_CHARGE { get; set; }
        public decimal NLWBP_CHARGE { get; set; }
        public decimal NBUY_WBP_CHARGE { get; set; }
        public decimal NWBP_CHARGE { get; set; }
        public decimal NBUY_TRANSFORMATOR_FEE { get; set; }
        public decimal NTRANSFORMATOR_FEE { get; set; }
        public bool LUSAGE_MIN_CHARGE { get; set; }
        public decimal NUSAGE_MIN_CHARGE { get; set; }
        public decimal NKWH_USED_MAX { get; set; }
        public decimal NKWH_USED_RATE { get; set; }
        public decimal NBUY_KWH_USED_RATE { get; set; }
        public decimal NBUY_KWA_USED_RATE { get; set; }
        public decimal NKWA_USED_RATE { get; set; }
        public decimal NRETRIBUTION_PCT { get; set; }
        public bool LRETRIBUTION_EXCLUDED_VAT { get; set; }
        public string CADMIN_FEE { get; set; }
        public string CADMIN_FEE_DESCR { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public decimal NOTHER_DISINCENTIVE_FACTOR { get; set; }
        public decimal NBUY_KVA_RANGE { get; set; }
        public decimal NKVA_RANGE { get; set; }
        public List<LMM01011DTO> CRATE_EC_LIST { get; set; }

        public byte[] CLOGO { get; set; }
    }
}
