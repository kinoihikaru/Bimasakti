using System;

namespace LMM07000COMMON
{
    public class LMM07000DTOUniversal
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get; set; } = "";
        public string CDESCRIPTION { get; set; }
    }
}
