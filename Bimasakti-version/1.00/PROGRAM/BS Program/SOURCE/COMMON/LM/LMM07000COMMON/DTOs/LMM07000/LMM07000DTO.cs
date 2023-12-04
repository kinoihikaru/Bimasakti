using R_APICommonDTO;
using System;

namespace LMM07000COMMON
{
    public class LMM07000DTO
    {
        // Parameter
        public string CUSER_ID { get; set; }
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CACTION { get; set; }


        // Result
        public string CDISCOUNT_CODE { get; set; }
        public string CDISCOUNT_NAME { get; set; }
        public string CDISCOUNT_TYPE { get; set; }
        public string CDESCRIPTION { get; set; } = "";
        public string CDISCOUNT_TYPE_NAME { get; set; }
        public decimal NDISCOUNT_VALUE { get; set; }
        public string CAPPLY_PERIOD_YEAR_FROM { get; set; }
        public string CAPPLY_PERIOD_NO_FROM { get; set; }
        public string CAPPLY_PERIOD_YEAR_TO { get; set; }
        public string CAPPLY_PERIOD_NO_TO { get; set; }
        public string CAPPLY_DATE_FROM { get; set; }
        public string CAPPLY_DATE_TO { get; set; }
        public bool LACTIVE { get; set; } = true;
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
