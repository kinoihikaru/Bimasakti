using R_CommonFrontBackAPI.Log;

namespace GSM03000Common.DTOs
{
    public class GSM03000PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID_FROM { get; set; }
        public string CCHARGES_ID_TO { get; set; }
        public string CSHORT_BY { get; set; } = "01";
        public bool LPRINT_INACTIVE { get; set; }
        public string CUSER_LOGIN_ID { get; set; }
    }
}
