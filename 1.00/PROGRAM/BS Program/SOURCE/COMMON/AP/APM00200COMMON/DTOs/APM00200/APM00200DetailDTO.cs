using System;
using System.Collections.Generic;
using System.Text;

namespace APM00200COMMON.DTOs.APM00200
{
    public class APM00200DetailDTO
    {
        public string CREC_ID { get; set; } = "";
        public string CEXPENDITURE_ID { get; set; } = "";
        public string CEXPENDITURE_NAME { get; set; } = "";
        public string CEXPENDITURE_DESC { get; set; } = "";
        public string CJRNGRP_CODE { get; set; } = "";
        public string CJRNGRP_NAME { get; set; } = "";
        public string CCATEGORY_ID { get; set; } = "";
        public string CCATEGORY_NAME { get; set; } = "";
        public string COTHER_TAX_TYPE { get; set; } = "";
        public string COTHER_TAX_ID { get; set; } = "";
        public string COTHER_TAX_NAME { get; set; } = "";
        public string CUNIT { get; set; } = "";
        public bool LTAXABLE { get; set; } = false;
        public bool LWITHHOLDING_TAX { get; set; } = false;
        public string CWITHHOLDING_TAX_ID { get; set; } = "";
        public string CWITHHOLDING_TAX_TYPE { get; set; } = "";
        public string CWITHHOLDING_TAX_NAME { get; set; } = "";
        public bool LACTIVE { get; set; } = false;
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
    }
}
