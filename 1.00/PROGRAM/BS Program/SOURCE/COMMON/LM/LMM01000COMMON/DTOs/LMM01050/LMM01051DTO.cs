using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01051DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE_ID { get; set; }
        public string CCHARGES_ID { get; set; }
        public string CDAY_TYPE { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public int ILEVEL { get; set; }
        public string CLEVEL_DESC { get; set; }
        public int IHOURS_FROM { get; set; }
        public int IHOURS_TO { get; set; }
        public int NRATE_HOUR { get; set; }
    }
}
