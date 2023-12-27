using System;

namespace APT00300COMMON
{
    public class APT00300CurrencyDTO
    {
        private string _DISPLAY;

        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_SYMBOL { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CDISPLAY { get => _DISPLAY; set => _DISPLAY = string.Format("{0} - {1}", CCURRENCY_CODE, CCURRENCY_NAME); }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }
}
