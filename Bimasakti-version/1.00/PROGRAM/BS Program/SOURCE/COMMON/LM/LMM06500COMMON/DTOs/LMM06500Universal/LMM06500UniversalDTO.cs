using R_APICommonDTO;

namespace LMM06500COMMON
{
    public class LMM06500UniversalDTO : R_APIResultBaseDTO
    {
        private string cCODE = "F";

        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get => cCODE; set => cCODE = value.Trim(); }
        public string CDESCRIPTION { get; set; }
    }


}
