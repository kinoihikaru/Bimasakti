namespace GLR00200COMMON
{
    public class GLR00200DTO
    {
        public string CGLACCOUNT_NO { get; set; }
        public string CGLACCOUNT_NAME { get; set; }
        public string NTOTAL_DEBIT { get; set; }
        public string NTOTAL_CREDIT { get; set; }
        public string CFROM_PERIOD { get; set; }
        public string CTO_PERIOD { get; set; }
        public string CCURRENCY { get; set; }
        public string CFROM_ACCOUNT_NO { get; set; }
        public string CTO_ACCOUNT_NO { get; set; }
        public string CFROM_CENTER_CODE { get; set; }
        public string CTO_CENTER_CODE { get; set; }
        public string CPRINT_METHOD_NAME { get; set; }
        public string CINCLUDE_AUDIT_JOURNAL { get; set; }

        public byte[] CLOGO { get; set; }
    }
}
