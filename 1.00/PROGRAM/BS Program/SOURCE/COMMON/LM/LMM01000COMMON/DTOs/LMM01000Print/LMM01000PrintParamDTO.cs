namespace LMM01000COMMON
{
    public class LMM01000PrintParamDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CPROPERTY_NAME { get; set; }
        public string CCHARGE_TYPE_FROM { get; set; }
        public string CCHARGE_TYPE_TO { get; set; }
        public string CSHORT_BY { get; set; } = "01";
        public bool LPRINT_INACTIVE { get; set; }
        public bool LPRINT_DETAIL_ACC { get; set; }
        public string CUSER_ID { get; set; }
    }
}
