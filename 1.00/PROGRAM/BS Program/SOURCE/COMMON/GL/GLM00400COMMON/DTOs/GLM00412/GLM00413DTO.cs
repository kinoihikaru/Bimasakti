using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace GLM00400COMMON
{
    public class GLM00413DTO
    {
        // param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_LANGUAGE { get; set; }
        public string CREC_ID_CENTER_ID { get; set; }

        // result
        public string CYEAR { get; set; }
        public string CPERIOD_NO { get; set; }
        public int NVALUE { get; set; }
    }


}
