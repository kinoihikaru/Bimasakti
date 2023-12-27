using System;
using System.Globalization;

namespace APT00300COMMON
{
    public class APT00300DTO
    {
        private DateTime _REF_DATE;
        private DateTime _DUE_DATE;

        public string CREC_ID { get; set; }
        public string CREF_NO { get; set; }
        public string CREF_DATE { get; set; }
        public DateTime DREF_DATE { get => _REF_DATE; set => _REF_DATE = DateTime.ParseExact(CREF_DATE, "yyyyMMdd", CultureInfo.InvariantCulture); }
        public string CDUE_DATE { get; set; }
        public DateTime DDUE_DATE { get => _DUE_DATE; set => _DUE_DATE = DateTime.ParseExact(CDUE_DATE, "yyyyMMdd", CultureInfo.InvariantCulture); }
        public string CSUPPLIER_ID { get; set; }
        public string CSUPPLIER_NAME { get; set; }
        public string CDOC_NO { get; set; }
        public string CDOC_DATE { get; set; }
        public string CCURRENCY_CODE { get; set; }
        public string CTRANS_DESC { get; set; }
        public string CTRANS_STATUS { get; set; }
        public decimal NTRANS_AMOUNT { get; set; }
        public decimal NTOTAL_AMOUNT { get; set; }
        public string CTRANS_STATUS_NAME { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }

    public class APT00300ParameterDTO
    {
        public string CPROPERTY_ID { get; set; }
        public string CTRANS_CODE { get; set; }
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CSUPPLIER_ID { get; set; } = "";
        public string CSUPPLIER_NAME { get; set; }
        public string CFROM_PERIOD { get; set; } = DateTime.Now.ToString("MM");
        public string CTO_PERIOD { get; set; } = DateTime.Now.ToString("MM");
    }
}
