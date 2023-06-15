using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01500COMMON
{
    public class LMM01531DTO : R_APIResultBaseDTO
    {
        // parameter
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CUSER_LANGUANGE { get; set; }

        // + result
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; } = "";
        public string UNIT_UTILITY_CHARGE { get; set; } = "";
        public string CCHARGES_TYPE { get; set; } = "";
        public string CCHARGES_TYPE_DESCR { get; set; } = "";
    }
}
