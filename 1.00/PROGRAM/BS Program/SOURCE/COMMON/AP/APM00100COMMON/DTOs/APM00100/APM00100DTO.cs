using System;
using System.Collections.Generic;
using System.Text;

namespace APM00100COMMON.DTOs.APM00100
{
    public class APM00100DTO
    {
        public string CDEPT_CODE { get; set; }
        public string CDEPT_NAME { get; set; }
        public string CCUR_RATETYPE_CODE { get; set; }
        public string CCUR_RATETYPE_DESCRIPTION { get; set; }
        public string CTAX_RATETYPE_CODE { get; set; }
        public string CTAX_RATETYPE_DESCRIPTION { get; set; }
        public bool LALLOW_EDIT_GLLINK { get; set; }
        public bool LGLLINK { get; set; }
        public bool LBACKDATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
