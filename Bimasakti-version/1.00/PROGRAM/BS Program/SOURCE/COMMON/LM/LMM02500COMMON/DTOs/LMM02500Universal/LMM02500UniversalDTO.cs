using System;

namespace LMM02500COMMON
{
    public class LMM02500UniversalDTO
    {
        private string cCODE = "";

        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get => cCODE; set => cCODE = value.Trim(); }
        public string CDESCRIPTION { get; set; }

    }


}
