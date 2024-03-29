﻿using System;

namespace GSM02200COMMON
{
    public class GSM02200DTO
    {
        // Param
        public string CCOMPANY_ID { get; set; }
        public string CUSER_ID { get; set; }
        public string CACTION { get; set; }

        // Result
        public string CPARENT_CODE { get; set; }
        public string CPARENT_NAME { get; set; }
        public string CCODE { get; set; }
        public string CNAME { get; set; }
        public string CCODE_CNAME_DISPLAY { get; set; }
        public bool LHAS_CHILD { get; set; }
        public bool LACTIVE { get; set; }
        public string CREF_CODE { get; set; }
        public string CCREATOR_NAME { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public string CUPDATER_NAME { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
