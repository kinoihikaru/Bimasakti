using System.Collections.Generic;

namespace LMM01000COMMON
{
    public class LMM01050DTO
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
        public string CADMIN_FEE { get; set; }
        public string CADMIN_FEE_DESCR { get; set; }
        public decimal NADMIN_FEE_PCT { get; set; }
        public decimal NADMIN_FEE_AMT { get; set; }
        public decimal NUNIT_AREA_VALID_FROM { get; set; }
        public decimal NUNIT_AREA_VALID_TO { get; set; }
        public bool LCUT_OFF_WEEKDAY { get; set; }
        public bool LHOLIDAY { get; set; }
        public bool LSATURDAY { get; set; }
        public bool LSUNDAY { get; set; }
        public List<LMM01051DTO> CRATE_OT_LIST { get; set; }
        public byte[] CLOGO { get; set; }
    }
}
