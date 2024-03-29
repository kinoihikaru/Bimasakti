using System;

namespace GSM05000COMMON_FMC
{
    public class GSM05020DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }

        public string CSEQUENCE { get; set; }
        public int ISEQUENCE { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_NAME { get; set; }
        public string CPOSITION { get; set; }
        public bool LREPLACEMENT { get; set; }
        public decimal NLIMIT_AMOUNT { get; set; }
        public int LOWEST { get; set; }
        public int HIGHEST { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
    }
}