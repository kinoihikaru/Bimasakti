using System;
using System.Collections.Generic;
using System.Text;

namespace LMM04000COMMON.DTOs.LMM04000
{
    public class LMM04000DTO
    {
        public string CTENANT_ID { get; set; }
        public string CTENANT_NAME { get; set; }
        public string CTENANT_GROUP_NAME { get; set; }
        public string CCONTRACTOR_CATEGORY_NAME { get; set; }
        public string CCONTRACTOR_TYPE_NAME { get; set; }
        public string CUNIT_NAME { get; set; }
        public string CPHONE1 { get; set; }
        public string CEMAIL { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; }
        public DateTime DCREATE_DATE { get; set; }
    }
}
