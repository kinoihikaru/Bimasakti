namespace GSM03000Common.DTOs
{
    public class GSM03000ResultDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public bool LACTIVE { get; set; }
        public string CSTATUS_NAME { get; set; }
        public string CGLACCOUNT_NO { get; set; }
        public string CGL_ACCOUNT_NAME { get; set; }
        public string CDESCRIPTION { get; set; }

        public byte[] CLOGO { get; set; }
    }
}
