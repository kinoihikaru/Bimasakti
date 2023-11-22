using R_APICommonDTO;

namespace LMM06500COMMON
{
    public class LMM06500UniversalDTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get; set; } = "";
        public string CDESCRIPTION { get; set; }
    }


}
