using R_APICommonDTO;

namespace LMM06500COMMON
{
    public class LMM06502DetailDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string COLD_SUPERVISOR_ID { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public bool LSELECTED { get; set; }
        public string CSTAFF_ID { get; set; }
        public string CSTAFF_NAME { get; set; }
    }

    public class LMM06502DetailMoveDTO
    {
        public string CSTAFF_ID { get; set; }
    }
}
