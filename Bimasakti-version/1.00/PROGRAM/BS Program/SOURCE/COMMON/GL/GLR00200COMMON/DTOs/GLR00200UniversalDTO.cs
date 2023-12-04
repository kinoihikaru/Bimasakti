using R_APICommonDTO;

namespace GLR00200COMMON
{
    public class GLR00200UniversalDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get; set; } = "";
        public string CNAME { get; set; }
    }


}
