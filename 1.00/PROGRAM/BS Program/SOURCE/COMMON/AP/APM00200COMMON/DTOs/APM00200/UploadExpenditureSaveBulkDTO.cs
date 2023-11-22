using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class UploadExpenditureSaveBulkDTO
    {
        public int INO { get; set; }
        public string CEXPENDITURE_ID { get; set; } = "";
        public string CEXPENDITURE_NAME { get; set; } = "";
        public string CEXPENDITURE_DESC { get; set; } = "";
        public string CJRNGRP_CODE { get; set; } = "";
        public string CCATEGORY_ID { get; set; } = "";
        public string CUNIT { get; set; } = "";
        public string CTAXABLE { get; set; } = "";
        public string CTAX_ID { get; set; } = "";
    }
}
