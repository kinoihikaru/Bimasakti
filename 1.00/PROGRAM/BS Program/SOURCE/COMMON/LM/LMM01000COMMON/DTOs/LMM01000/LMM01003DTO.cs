using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01003DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CUSER_ID { get; set; }

        // result

        public string CCURRENT_CHARGES_ID { get; set; }
        public string CCURRENT_CHARGES_NAME { get; set; }
        public string CNEW_CHARGES_ID { get; set; }
        public string CNEW_CHARGES_NAME { get; set; }
    }
}
