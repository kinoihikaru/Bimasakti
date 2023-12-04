using System;
using System.Collections.Generic;
using System.Text;

namespace GSM09200COMMON.DTOs.GSM09200
{
    public class GSM09200DetailDTO
    {
        public string CPARENT { get; set; } = "";
        public int ILEVEL { get; set; } = 0;
        public string CCATEGORY_ID { get; set; } = "";
        public string CCATEGORY_NAME { get; set; } = "";
        public string CNOTE { get; set; } = "";
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}
