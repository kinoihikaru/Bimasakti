using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01020DTO
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
        public int NPIPE_SIZE { get; set; }
        public int NBUY_STANDING_CHARGE { get; set; }
        public string CUSAGE_RATE_MODE { get; set; }
        public int NSTANDING_CHARGE { get; set; }
        public int NBUY_USAGE_CHARGE_RATE { get; set; }
        public int NUSAGE_CHARGE_RATE { get; set; }
        public bool LUSAGE_MIN_CHARGE { get; set; }
        public int NUSAGE_MIN_CHARGE_AMT { get; set; }
        public int NMAINTENANCE_FEE { get; set; }
        public string CADMIN_FEE { get; set; }
        public int NADMIN_FEE_PCT { get; set; }
        public int NADMIN_FEE_AMT { get; set; }
        public bool LADMIN_FEE_TAX { get; set; }
        public bool LADMIN_FEE_SC { get; set; }
        public bool LADMIN_FEE_USAGE { get; set; }
        public bool LADMIN_FEE_MAINTENANCE { get; set; }
        public List<LMM01021DTO> CRATE_WG_LIST { get; set; }
    }
}
