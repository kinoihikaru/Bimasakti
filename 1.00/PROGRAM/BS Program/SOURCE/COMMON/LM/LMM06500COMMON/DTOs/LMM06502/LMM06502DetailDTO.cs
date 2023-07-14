using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM06500COMMON
{
    public class LMM06502DetailDTO
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


}
