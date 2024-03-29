﻿using System;

namespace APT00300COMMON
{
    public class APT00300APSystemParamDTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCUR_RATETYPE_CODE { get; set; }
        public string CCUR_RATETYPE_DESCRIPTION { get; set; }
        public string CTAX_RATETYPE_CODE { get; set; }
        public string CTAX_RATETYPE_DESCRIPTION { get; set; }
        public bool LBACKDATE { get; set; }
        public bool LGLLINK { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
        public bool LALLOW_EDIT_GLLINK { get; set; }
    }
}
