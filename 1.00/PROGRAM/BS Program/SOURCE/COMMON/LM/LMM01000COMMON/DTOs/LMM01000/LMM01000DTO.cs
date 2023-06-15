using R_APICommonDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMM01000COMMON
{
    public class LMM01000DTO : R_APIResultBaseDTO
    {
        // param
        public string CCOMPANY_ID { get; set; } = "";
        public string CPROPERTY_ID { get; set; } = "";
        public string CUSER_ID { get; set; } = "";
        public string CACTION { get; set; } = "";
        public string CCHARGES_TYPE { get; set; }

        // result

        public string CCHARGES_ID { get; set; } = "";
        public string CCHARGES_NAME { get; set; } = "";
        public string CDESCRIPTION { get; set; } = "";
        public string CSTATUS { get; set; } = "00";
        public bool LACCRUAL { get; set; }
        public bool LTAXABLE { get; set; }
        public bool LTAX_EXEMPTION { get; set; }
        public string CTAX_EXEMPTION_CODE { get; set; } = "";
        public int ITAX_EXEMPTION_PCT { get; set; }
        public bool LOTHER_TAX { get; set; }
        public string COTHER_TAX_ID { get; set; } = "";
        public string CTAX_OTHER_NAME { get; set; } = "";
        public bool LWITHHOLDING_TAX { get; set; }
        public string CWITHHOLDING_TAX_NAME { get; set; } = "";
        public string CWITHHOLDING_TAX_TYPE { get; set; } = "";
        public string CWITHHOLDING_TAX_ID { get; set; } = "";
        public string CUTILITY_JRNGRP_CODE { get; set; } = "";
        public string CUTILITY_JRNGRP_NAME { get; set; } = "";
        public string CCREATE_BY { get; set; } = "";
        public DateTime DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; } = "";
        public DateTime DUPDATE_DATE { get; set; }
    }
}
