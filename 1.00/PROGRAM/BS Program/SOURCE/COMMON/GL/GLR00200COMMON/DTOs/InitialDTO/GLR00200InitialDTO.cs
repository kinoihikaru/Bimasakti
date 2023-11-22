using R_APICommonDTO;

namespace GLR00200COMMON
{
    public class GLR00200InitialDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public int IMIN_YEAR { get; set; }
        public int IMAX_YEAR { get; set; }
    }


}
