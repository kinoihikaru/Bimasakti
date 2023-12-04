namespace GLR00200COMMON
{
    public class GLR00200PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CREPORT_TYPE { get; set; }
        public string CCURRENCY_TYPE { get; set; } = "L";
        public string CPRINT_METHOD { get; set; } = "00";
        public string CUSER_ID { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; } = "";
        public string CTO_CENTER_CODE { get; set; } = "";
        public string CPERIOD_MODE { get; set; } = "P";
        public string CYEAR { get; set; }
        public string CFROM_PERIOD_NO { get; set; } = "01";
        public string CTO_PERIOD_NO { get; set; } = "01";
        public string CFROM_DATE { get; set; }
        public string CTO_DATE { get; set; }
        public bool LINCLUDE_AUDIT_JRN { get; set; }
        public string CLANGUAGE_ID { get; set; }
    }
}
