using System;
using System.Globalization;

namespace APT00300COMMON
{
    public class APT00310LastCurrencyDTO
    {
        public string CCURRENCY_CODE { get; set; }
        public string CRATETYPE_CODE { get; set; }
        public string CRATE_DATE { get; set; }
        public decimal NLBASE_RATE_AMOUNT { get; set; }
        public decimal NLCURRENCY_RATE_AMOUNT { get; set; }
        public decimal NBBASE_RATE_AMOUNT { get; set; }
        public decimal NBCURRENCY_RATE_AMOUNT { get; set; }
    }

    public class APT00310LastCurrencyParameterDTO
    {
        public string CCURRENCY_CODE { get; set; } = "";
        public string CRATETYPE_CODE { get; set; } = "";
        public string CRATE_DATE { get; set; } = "";
    }
}
