﻿using System;

namespace GSM04500COMMON
{
    public class GSM04500DTO
    {
        public string CACTION { get; set; }

        public string CCOMPANY_ID { get; set; }
        public string CPROPERTY_ID { get; set; }
        public string CJRNGRP_TYPE { get; set; }
        public string CJRNGRP_CODE { get; set; }
        public string CJRNGRP_NAME { get; set; }
        public bool LACCRUAL { get; set; }

        public string CCREATE_BY { get; set; }
        public DateTime? DCREATE_DATE { get; set; }
        public string CUPDATE_BY { get; set; }
        public DateTime? DUPDATE_DATE { get; set; }
    }
}
