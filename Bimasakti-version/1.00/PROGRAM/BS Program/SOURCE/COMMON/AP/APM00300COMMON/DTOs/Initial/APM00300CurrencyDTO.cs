using System;

namespace APM00300COMMON
{
    public class APM00300CurrencyDTO
    {
        private string _cCURRENCY_CODE_CCURRENCY_NAME;

        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }

        public string CCURRENCY_CODE { get; set; }
        public string CCURRENCY_SYMBOL { get; set; }
        public string CCURRENCY_NAME { get; set; }
        public string CCURRENCY_CODE_CCURRENCY_NAME { get => _cCURRENCY_CODE_CCURRENCY_NAME; set => _cCURRENCY_CODE_CCURRENCY_NAME = string.Format("{0} - {1}", CCURRENCY_CODE, CCURRENCY_NAME); }
    }
}
