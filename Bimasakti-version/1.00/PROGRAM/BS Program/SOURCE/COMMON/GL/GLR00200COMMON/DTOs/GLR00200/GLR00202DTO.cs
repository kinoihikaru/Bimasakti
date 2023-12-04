using System.Collections.Generic;

namespace GLR00200COMMON
{
    public class GLR00202DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string CGLACCOUNT_TYPE { get; set; }
        public string CACCOUNT { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CDBCR { get; set; }
        public string CBSIS { get; set; }
        public decimal NBEGIN_BALANCE { get; set; }
        public decimal NEND_BALANCE { get; set; }
        public decimal NDEBIT_AMOUNT { get; set; }
        public decimal NDEBIT_AMOUNT_SUM { get; set; }
        public decimal NCREDIT_AMOUNT { get; set; }
        public decimal NCREDIT_AMOUNT_SUM { get; set; }
        public List<GLR00204DTO> PeriodDetailData { get; set; }

    }
}
