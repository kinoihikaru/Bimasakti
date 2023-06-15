using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01002DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CUSER_ID { get; set; }

        // result
        public string CCHARGES_ID { get; set; }
        public string CCHARGES_NAME { get; set; }
        public string CSTATUS_DESCRIPTION { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
    }


}
