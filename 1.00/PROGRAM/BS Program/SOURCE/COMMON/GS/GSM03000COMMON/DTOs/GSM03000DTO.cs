using R_APICommonDTO;
using System;
using System.Collections.Generic;

namespace GSM03000Common.DTOs
{
    public class GSM03000DTO
    {
        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CCHARGES_TYPE { get; set; }
        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public bool LACTIVE { get; set; } = true;
        public string CSTATUS_DESCRIPTION { get; set; } = "";
        public string CGLACCOUNT_NO { get; set; } = "";
        public string CGLACCOUNT_NAME { get; set; } = "";
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CACTION { get; set; }
        public string CUSER_ID { get; set; }
    }

    public class GSM03000ListDTO : R_APIResultBaseDTO
    {
        public List<GSM03000DTO> Data { get; set; }
    }
}
