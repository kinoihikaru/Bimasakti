namespace LMM01500COMMON
{
    public class LMM01511DTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CINVGRP_CODE { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }
        public string CDEPT_CODE { get; set; }

        // + result
        public string CDEPT_NAME { get; set; }
        public string CINVOICE_TEMPLATE { get; set; } = "";
        public string CBANK_CODE { get; set; }
        public string CBANK_NAME { get; set; }
        public string CBANK_ACCOUNT { get; set; }
    }
}
