using System;

namespace GSM05000COMMON_FMC
{
    public class GSM05000UniversalDTO
    {
        private string cCODE;

        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }

        // result
        public string CCODE { get => cCODE; set => cCODE = value.Trim(); }
        public string CDESCRIPTION { get; set; }
    }
}