using R_APICommonDTO;

namespace GSM03000Common.DTOs
{
    public class GSM03000ActiveDTO : R_APIResultBaseDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public bool LACTIVE { get; set; }
        public string CUSER_ID { get; set; }
    }

    public class GSM03000ActiveParameterDTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; }
        public bool LACTIVE { get; set; }
        public string CUSER_ID { get; set; }
    }
}
